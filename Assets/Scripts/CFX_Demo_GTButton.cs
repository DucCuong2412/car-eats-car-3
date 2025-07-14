using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CFX_Demo_GTButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Color NormalColor = new Color32(128, 128, 128, 128);
    public Color HoverColor = new Color32(200, 200, 200, 200);

    public string Callback;
    public GameObject Receiver;

    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
        if (image != null)
            image.color = NormalColor;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (image != null)
            image.color = HoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (image != null)
            image.color = NormalColor;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (Receiver != null && !string.IsNullOrEmpty(Callback))
        {
            Receiver.SendMessage(Callback);
        }
    }
}
