using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerScript : MonoBehaviour
{

    [SerializeField]
    public Tower towerType;
    
    void Start()
    {
        towerType = new TowerWind(3f,true);
   
    }

    
    void Update()
    {
        towerType.Update(Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("ZomWick")){
            towerType.HandleZomWick(collision);
        }
    }
}
