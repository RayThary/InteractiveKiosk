using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DetailPanel : MonoBehaviour
{
    private ShowName showName;
    public ShowName SetShowName { set { showName = value; } }

    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI description;

    [System.Serializable]
    private class detail
    {
        public ShowName name;
        public string title;
        public string description;
        public GameObject model;
    }
    [SerializeField] private List<detail> objectSetting = new List<detail>();

    [SerializeField] private Button btnQuizPanel;
    [SerializeField] private GameObject QuizPanel;



    private void OnEnable()
    {
        onPanel(showName);
    }
    private void OnDisable()
    {
        offPanel();
    }


    private void onPanel(ShowName _showName)
    {
        for (int i = 0; i < objectSetting.Count; i++)
        {
            if (objectSetting[i].name == _showName)
            {

                title.text = objectSetting[i].title;
                description.text = objectSetting[i].description;

                objectSetting[i].model.SetActive(true);
                gameObject.GetComponentInChildren<ModelViewController>().SetModel(objectSetting[i].model);
                btnQuizPanel.onClick.RemoveAllListeners();
                btnQuizPanel.onClick.AddListener(() => ShowQuizPanel(_showName));
            }
        }
    }
    private void offPanel()
    {
        for (int i = 0; i < objectSetting.Count; i++)
        {
            if (objectSetting[i].model != null)
            {
                objectSetting[i].model.SetActive(false);
            }

        }
    }

    private void ShowQuizPanel(ShowName _showName)
    {
        QuizPanel.GetComponent<QuizManager>().SetQuiz(_showName);
        gameObject.SetActive(false);
        QuizPanel.SetActive(true);
    }
}
