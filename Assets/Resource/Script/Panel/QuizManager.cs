using System.Collections.Generic;
using System.IO;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    [System.Serializable]
    private class QuizJsonData
    {
        public List<CorrectQuizJsonData> correctQuizDatas = new List<CorrectQuizJsonData>();
        public List<WrongQuizJsonData> wrongQuizDatas = new List<WrongQuizJsonData>();
    }

    [System.Serializable]
    private class CorrectQuizJsonData
    {
        public string showName;

        [TextArea]
        public string question;

        public string answerText;
    }

    [System.Serializable]
    private class WrongQuizJsonData
    {
        public string showName;

        public string answerText;

        [TextArea]
        public string resultDescription;
    }

    private class RuntimeAnswerData
    {
        public string answerText;
        public bool isCorrect;
        public string resultDescription;

        public RuntimeAnswerData(string answerText, bool isCorrect, string resultDescription)
        {
            this.answerText = answerText;
            this.isCorrect = isCorrect;
            this.resultDescription = resultDescription;
        }
    }

    [Header("Json")]
    [SerializeField] private string quizDataFileName = "QuizData.json";

    [Header("Quiz UI")]
    [SerializeField] private TextMeshProUGUI questionText;
    [SerializeField] private List<Button> answerButtons;

    [SerializeField] private ResultPanel resultPanel;

    private QuizJsonData quizJsonData;
    private List<RuntimeAnswerData> currentAnswers = new List<RuntimeAnswerData>();

    private ShowName showName;

    private void Awake()
    {
        LoadQuizData();
    }

    private void LoadQuizData()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, quizDataFileName);        
        

        if (!File.Exists(filePath))
        {
            Debug.LogWarning("QuizData.json 파일을 찾을 수 없습니다: " + filePath);
            return;
        }

        //한글지정
        string json = File.ReadAllText(filePath, Encoding.UTF8);
        quizJsonData = JsonUtility.FromJson<QuizJsonData>(json);

        if (quizJsonData == null)
        {
            Debug.LogWarning("QuizData.json 파싱 실패");
            return;
        }

        if (quizJsonData.correctQuizDatas == null || quizJsonData.wrongQuizDatas == null)
        {
            Debug.LogWarning("QuizData.json 내부 리스트가 비어 있습니다.");
            return;
        }
    }

    public void SetQuiz(ShowName _showName)
    {
        showName = _showName;

        if (quizJsonData == null)
        {
            Debug.LogWarning("QuizData가 로드되지 않았습니다.");
            return;
        }

        List<CorrectQuizJsonData> correctList = new List<CorrectQuizJsonData>();
        List<WrongQuizJsonData> wrongList = new List<WrongQuizJsonData>();

        string targetShowName = _showName.ToString();

        for (int i = 0; i < quizJsonData.correctQuizDatas.Count; i++)
        {
            if (quizJsonData.correctQuizDatas[i].showName == targetShowName)
            {
                correctList.Add(quizJsonData.correctQuizDatas[i]);
            }
        }

        for (int i = 0; i < quizJsonData.wrongQuizDatas.Count; i++)
        {
            if (quizJsonData.wrongQuizDatas[i].showName == targetShowName)
            {
                wrongList.Add(quizJsonData.wrongQuizDatas[i]);
            }
        }

        if (correctList.Count <= 0)
        {
            Debug.LogWarning("해당 콘텐츠의 정답 데이터가 없습니다: " + targetShowName);
            return;
        }

        if (wrongList.Count < 2)
        {
            Debug.LogWarning("해당 콘텐츠의 오답 데이터가 2개 미만입니다: " + targetShowName);
            return;
        }

        int correctIndex = Random.Range(0, correctList.Count);
        CorrectQuizJsonData selectedCorrect = correctList[correctIndex];

        questionText.text = selectedCorrect.question;

        currentAnswers.Clear();

        currentAnswers.Add(new RuntimeAnswerData(
            selectedCorrect.answerText,
            true,
            "정답입니다. 콘텐츠의 핵심 내용을 잘 이해했습니다."
        ));

        for (int i = 0; i < 2; i++)
        {
            int wrongIndex = Random.Range(0, wrongList.Count);
            WrongQuizJsonData selectedWrong = wrongList[wrongIndex];

            currentAnswers.Add(new RuntimeAnswerData(
                selectedWrong.answerText,
                false,
                selectedWrong.resultDescription
            ));

            wrongList.RemoveAt(wrongIndex);
        }

        ShuffleAnswers();
        ApplyAnswersToButtons();
    }

    private void ShuffleAnswers()
    {
        for (int i = 0; i < currentAnswers.Count; i++)
        {
            int randomIndex = Random.Range(i, currentAnswers.Count);

            RuntimeAnswerData temp = currentAnswers[i];
            currentAnswers[i] = currentAnswers[randomIndex];
            currentAnswers[randomIndex] = temp;
        }
    }

    private void ApplyAnswersToButtons()
    {
        for (int i = 0; i < answerButtons.Count; i++)
        {
            if (i >= currentAnswers.Count)
            {
                answerButtons[i].gameObject.SetActive(false);
                continue;
            }

            answerButtons[i].gameObject.SetActive(true);

            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = currentAnswers[i].answerText;

            int index = i;

            answerButtons[i].onClick.RemoveAllListeners();
            answerButtons[i].onClick.AddListener(() => SelectAnswer(index));
        }
    }

    private void SelectAnswer(int _index)
    {
        RuntimeAnswerData selectedAnswer = currentAnswers[_index];

        gameObject.SetActive(false);

        resultPanel.SetResult(selectedAnswer.isCorrect, selectedAnswer.resultDescription, showName);
        resultPanel.gameObject.SetActive(true);
    }
}