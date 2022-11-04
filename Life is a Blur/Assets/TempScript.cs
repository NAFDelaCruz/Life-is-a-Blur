using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class TempScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI baseText;

    //Testing
    public Color baseColor;
    public Color targetColor;

    // Start is called before the first frame update
    void Start()
    {
        baseText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        baseColor = baseText.color;

        targetColor = Color.yellow;
    }

    //Do Things On Hover
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Hovering");
        baseText.color = targetColor;
    }

    //Do Things On Exit (Revert What On Enter Does)
    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Not Hovering");
        baseText.color = baseColor;
    }
}
