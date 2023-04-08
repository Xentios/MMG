using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TowerUIScript : MonoBehaviour, IPointerEnterHandler,IPointerDownHandler,IPointerExitHandler,IPointerMoveHandler,IPointerUpHandler
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
        var pos=Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit= Physics2D.Raycast(pos, Vector2.down, 1, towerSpotLayerMask);       
        if (hit.collider == null)
        {
            isDragging = false;
            layoutElement.ignoreLayout = false;
        }
        else
        {
            TowerManager.CreateTower(hit.collider.gameObject, myPrefab);
            transform.gameObject.SetActive(false);//TODO may need reset of fields 

        }
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
