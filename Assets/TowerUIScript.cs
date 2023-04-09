using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TowerUIScript : MonoBehaviour, IPointerEnterHandler,IPointerDownHandler,IPointerExitHandler,IPointerMoveHandler,IPointerUpHandler,IDragHandler
{

    [SerializeField]
    LayoutElement layoutElement;
    [SerializeField]
    private LayerMask towerSpotLayerMask;
    [SerializeField]
    GameObject myPrefab;

    private bool isDragging;
   

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.dragging) return;
        
        transform.localScale = Vector3.one * 1.3f;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData.dragging) return;
        transform.localScale = Vector3.one;
       
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isDragging = true;
        layoutElement.ignoreLayout = true;       
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        var pos=Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit= Physics2D.Raycast(pos, Vector2.down, 1, towerSpotLayerMask);
        ;
        if (hit.collider == null || hit.collider.GetComponent<TowerSlot>().isOccupied)
        {
            isDragging = false;
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
            layoutElement.ignoreLayout = false;
           
        }
        else
        {   
            TowerManager.instance.CreateTower(hit.collider.gameObject, myPrefab);           
            GameObject.Destroy(transform.gameObject);
        }
    }


    public void OnPointerMove(PointerEventData eventData)
    {
        //return;//TODO
        //if (isDragging)
        //{
        //    layoutElement.ignoreLayout = true;
        //    transform.position = eventData.position;
        //}
    }

    public void OnDrag(PointerEventData eventData)
    {
        layoutElement.ignoreLayout = true;
        transform.position = new Vector3(eventData.position.x, eventData.position.y, -3);
    }
}
