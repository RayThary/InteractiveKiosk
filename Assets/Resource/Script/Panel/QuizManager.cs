using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{

    [System.Serializable]
    private class CorrectQuizData
    {
        public ShowName showName;

        [TextArea]
        public string question;

        public string answerText;
    }

    [System.Serializable]
    private class WrongQuizData
    {
        public ShowName showName;

        public string answerText;

        [TextArea]
        public string resultDescription;
    }
    [Header("Quiz Data")]
    [SerializeField] private List<CorrectQuizData> correctQuizDatas;
    [SerializeField] private List<WrongQuizData> wrongQuizDatas;

    [Header("Quiz UI")]
    [SerializeField] private TextMeshProUGUI questionText;
    [SerializeField] private List<Button> answerButtons;

    private List<RuntimeAnswerData> currentAnswers = new List<RuntimeAnswerData>();
    [SerializeField] private ResultPanel resultPanel;
    private ShowName showName;

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

    public void SetQuiz(ShowName _showName)
    {
        showName = _showName;
        List<CorrectQuizData> correctList = new List<CorrectQuizData>();
        List<WrongQuizData> wrongList = new List<WrongQuizData>();

        // 각각 현재 showName 에맞게 정답/오답 추가
        for (int i = 0; i < correctQuizDatas.Count; i++)
        {
            if (correctQuizDatas[i].showName == _showName)
            {
                correctList.Add(correctQuizDatas[i]);
            }
        }

        for (int i = 0; i < wrongQuizDatas.Count; i++)
        {
            if (wrongQuizDatas[i].showName == _showName)
            {
                wrongList.Add(wrongQuizDatas[i]);
            }
        }

        // 안전 체크
        if (correctList.Count <= 0)
        {
            Debug.LogWarning("해당 타입의 정답 데이터가 없습니다: " + _showName);
            return;
        }

        if (wrongList.Count < 2)
        {
            Debug.LogWarning("해당 타입의 오답 데이터가 2개 미만입니다: " + _showName);
            return;
        }

        // 정답 및 오답 선택
        int correctIndex = Random.Range(0, correctList.Count);
        CorrectQuizData selectedCorrect = correctList[correctIndex];

        questionText.text = selectedCorrect.question;

        currentAnswers.Clear();
        currentAnswers.Add(new RuntimeAnswerData(selectedCorrect.answerText, true, "정답입니다. 콘텐츠의 핵심 내용을 잘 이해했습니다."));

        for (int i = 0; i < 2; i++)
        {
            int wrongIndex = Random.Range(0, wrongList.Count);
            WrongQuizData selectedWrong = wrongList[wrongIndex];

            currentAnswers.Add(new RuntimeAnswerData(selectedWrong.answerText, false, selectedWrong.resultDescription));

            wrongList.RemoveAt(wrongIndex);
        }

        //문제섞기 및 정답 적용
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
