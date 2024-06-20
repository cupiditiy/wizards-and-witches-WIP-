using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ButtonHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI buttonText; // Reference to the TMP text component
    public float normalFontSize = 14f; // Normal font size
    public float hoverFontSize = 10f;  // Font size on hover

    void Start()
    {
        // Set the text to normal font size at the start
        if (buttonText != null)
        {
            buttonText.fontSize = normalFontSize;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Set the text to hover font size when the pointer enters
        if (buttonText != null)
        {
            buttonText.fontSize = hoverFontSize;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Set the text back to normal font size when the pointer exits
        if (buttonText != null)
        {
            buttonText.fontSize = normalFontSize;
        }
    }
}
