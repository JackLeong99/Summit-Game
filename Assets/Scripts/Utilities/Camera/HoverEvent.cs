using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverEvent : MonoBehaviour, IPointerEnterHandler, ISelectHandler
{
    [Header("Values")]
    public int panIndex;

    private PanHandler reference;

    public void Awake()
    {
        reference = GameObject.Find("PanHandler").GetComponent<PanHandler>(); 
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        EventSystem.current.SetSelectedGameObject(gameObject);
        reference.SwapFocus(panIndex);
    }

    public void OnSelect(BaseEventData eventData)
    {
        reference.SwapFocus(panIndex);
    }
}
