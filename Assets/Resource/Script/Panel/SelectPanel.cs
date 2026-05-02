using UnityEngine;

public class SelectPanel : MonoBehaviour
{
    [SerializeField] private CardImageView RobotImage;
    [SerializeField] private CardImageView ArtifactImage;
    [SerializeField] private CardImageView PlanetImage;

    [SerializeField] private string noneImageText;
    private void Awake()
    {
        if (noneImageText != null)
        {
            RobotImage.SetNoneImageText = noneImageText;
            ArtifactImage.SetNoneImageText = noneImageText;
            PlanetImage.SetNoneImageText = noneImageText;
        }
    }
}
