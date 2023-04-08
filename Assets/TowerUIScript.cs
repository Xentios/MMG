using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TowerUIScript : MonoBehaviour, IPointerEnterHandler,IPointerDownHandler,IPointerExitHandler,IPointerMoveHandler,IPointerUpHandler
{

    [SerializeField]
    LayoutElement layoutElement;

    private bool isDragging;
    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = Vector3.one * 1.3f;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = Vector3.one;
       
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isDragging = true;
        layoutElement.ignoreLayout = true;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        isDragging = false;
        layoutElement.ignoreLayout = false;
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        
        if (isDragging)
        {
            layoutElement.ignoreLayout = true;
            transform.position = Input.mousePosition;          
        }
    }
  

    
}
