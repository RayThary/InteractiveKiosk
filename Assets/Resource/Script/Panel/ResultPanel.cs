using TMPro;
using UnityEngine;

public class ResultPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI resultTitleText;
    [SerializeField] private TextMeshProUGUI resultDescriptionText;
    [SerializeField] private TextMeshProUGUI resultContentNameText;

    public void SetResult(bool _isCorrect, string _description,ShowName _showName)
    {
        resultTitleText.text = _isCorrect ? "정답입니다" : "오답입니다";
        resultContentNameText.text = "콘텐츠 : " + _showName.ToString();
        resultDescriptionText.text = _description;
    }
}
