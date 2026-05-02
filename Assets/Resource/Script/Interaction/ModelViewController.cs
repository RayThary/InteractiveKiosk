using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ModelViewController : MonoBehaviour, IDragHandler
{
    public void OnDrag(PointerEventData eventData)
    {
        if (model == null) return;

        float rotateX = eventData.delta.y * rotateSpeed;
        float rotateY = -eventData.delta.x * rotateSpeed;

        model.transform.Rotate(Vector3.up, rotateY, Space.World);
        model.transform.Rotate(Vector3.right, rotateX, Space.World);
    }
    private GameObject model;

    private Vector3 originPosition;
    private Quaternion originRotation;
    private Vector3 originScale;

    [SerializeField] private Button btnZoomIn;
    [SerializeField] private Button btnZoomOut;
    [SerializeField] private Button btnReset;

    [SerializeField] private float rotateSpeed;


    void Start()
    {
        btnZoomIn.onClick.AddListener(() => zoom(true));
        btnZoomOut.onClick.AddListener(() => zoom(false));
        btnReset.onClick.AddListener(resetModel);
    }

    private void zoom(bool _value)
    {
        if (model == null) return;

        float value = _value ? 0.1f : -0.1f;
        float nextScale = model.transform.localScale.x + value;

        if (nextScale < 0.5f || nextScale > 1.5f)
        {
            return;
        }

        model.transform.localScale += Vector3.one * value;
    }

    private void resetModel()
    {
        if (model == null) return;


        model.transform.localPosition = originPosition;
        model.transform.localRotation = originRotation;
        model.transform.localScale = originScale;
    }

    public void SetModel(GameObject _model)
    {
        model = _model;

        if (model == null)
        {
            return;
        }

        originPosition = model.transform.localPosition;
        originRotation = model.transform.localRotation;
        originScale = model.transform.localScale;
    }


}
