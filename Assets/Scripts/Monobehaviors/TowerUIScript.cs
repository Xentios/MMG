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

    private Canvas canvasComponent;

    private void Awake()
    {
        canvasComponent = GetComponent<Canvas>();
    }

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
        layoutElement.ignoreLayout = true;
        canvasComponent.sortingOrder=2;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        var pos=Camera.main.ScreenToWorldPoint(eventData.position);
        RaycastHit2D hit= Physics2D.Raycast(pos, Vector2.down, 1, towerSpotLayerMask);
        
        if (hit.collider == null || hit.collider.GetComponent<TowerSlot>().isOccupied)
        {           
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
            layoutElement.ignoreLayout = false;
            canvasComponent.sortingOrder = 1;

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
