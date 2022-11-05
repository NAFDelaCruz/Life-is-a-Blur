using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    TextMeshProUGUI baseText;

    //Color Values
    public Color baseColor;
    public Color targetColor;

    // Start is called before the first frame update
    void Start()
    {
        baseText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    //Do Things On Hover
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (gameObject.GetComponent<Button>().IsInteractable()) baseText.color = targetColor;
    }

    //Do Things On Exit (Revert What On Enter Does)
    public void OnPointerExit(PointerEventData eventData)
    {
        if (gameObject.GetComponent<Button>().IsInteractable()) baseText.color = baseColor;
    }
}
