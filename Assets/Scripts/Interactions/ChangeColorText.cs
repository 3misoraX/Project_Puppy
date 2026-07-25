using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChangeColorText : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI text;
    public Color baseColor; 
    public Color hoverColor;

    void OnEnable()
    {
        text.color = baseColor;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        text.color = hoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        text.color = baseColor;
    }
}