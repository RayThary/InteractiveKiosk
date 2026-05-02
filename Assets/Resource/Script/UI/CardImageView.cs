using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardImageView : MonoBehaviour
{
    [SerializeField]
    private Image image;
    [SerializeField]
    private TextMeshProUGUI imageText;
    private string noneImageText = "전시 이미지 준비 중";
    public string SetNoneImageText { set { noneImageText = value; } }

    void Start()
    {
        image = GetComponent<Image>();
        imageText = GetComponentInChildren<TextMeshProUGUI>();

        if (image.sprite == null)
        {
            image.enabled = false;
            imageText.text = noneImageText;
            imageText.gameObject.SetActive(true);
        }
        else
        {
            imageText.gameObject.SetActive(false);
        }



    }


}
