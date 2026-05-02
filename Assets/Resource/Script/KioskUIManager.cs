using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public enum ShowName 
{
    Robot,
    Artifact,
    Planet,
}

public class KioskUIManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> Panels = new List<GameObject>();
    [SerializeField] private GameObject initialPanel;

    [SerializeField] private GameObject detailPanel;
    [SerializeField] private Button robot;
    [SerializeField] private Button Artifact;
    [SerializeField] private Button Planet;

    [SerializeField] private float returnTime = 5;
    private float idleTimer = 0;

    #region
    //private Button startBtn;

    //private Button robotBtn;
    //private Button artifactBtn;
    //private Button planetBtn;
    //private Button backToStartBtn;

    //private Button quizStartBtn;
    //private Button backToSelectBtn;

    //private Button choiceBBtn;
    //private Button choiceCBtn;
    //private Button choiceABtn;
    //private Button homeBtn;
    #endregion
    void Start()
    {
        robot.onClick.AddListener(() => ShowDetail(ShowName.Robot));
        Artifact.onClick.AddListener(() => ShowDetail(ShowName.Artifact));
        Planet.onClick.AddListener(() => ShowDetail(ShowName.Planet));

        showPanel(initialPanel);
    }

    // Update is called once per frame
    void Update()
    {
        if (initialPanel != null && initialPanel.activeSelf)
        {
            idleTimer = 0f;
            return;
        }

        if (HasAnyInput())
        {
            idleTimer = 0f;
            return;
        }

        idleTimer += Time.unscaledDeltaTime;

        if (idleTimer >= returnTime)
        {
            idleTimer = 0;
            showPanel(initialPanel);
        }

    }

    //입력확인
    private bool HasAnyInput()
    {
        if (Keyboard.current != null && Keyboard.current.anyKey.wasPressedThisFrame)
            return true;

        if (Mouse.current != null)
        {
            if (Mouse.current.leftButton.wasPressedThisFrame)
                return true;

            if (Mouse.current.rightButton.wasPressedThisFrame)
                return true;

            if (Mouse.current.middleButton.wasPressedThisFrame)
                return true;

            if (Mouse.current.scroll.ReadValue().sqrMagnitude > 0.01f)
                return true;

            if (Mouse.current.delta.ReadValue().sqrMagnitude > 0.01f)
                return true;
        }

        if (Touchscreen.current != null)
        {
            if (Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
                return true;
        }

        return false;
    }


    private void showPanel(GameObject targetPanel)
    {
        for (int i = 0; i < Panels.Count; i++)
        {
            Panels[i].SetActive(false);
        }

        targetPanel.SetActive(true);
    }
    private void ShowDetail(ShowName _showName)
    {
        detailPanel.GetComponent<DetailPanel>().SetShowName = _showName;
        showPanel(detailPanel);
    }

    public void ShowPanelObject(GameObject _gameObject)
    {
        showPanel(_gameObject);
    }
  
}
