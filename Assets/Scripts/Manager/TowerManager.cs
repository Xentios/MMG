using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    public static TowerManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        towers = new List<TowerScript>();
        towerSlotPair = new Dictionary<TowerScript, TowerSlot>();
    }

    [SerializeField]
    GameObject towerUIHolder;

    [SerializeField]
    GameObject[] towerPrefabs;

    [SerializeField]
    GameObject[] UITowers;
    
    List<TowerScript> towers;

    private Dictionary<TowerScript,TowerSlot> towerSlotPair;

    public void OnDestroy()
    {
        foreach (var tower in towers)
        {
           tower.recyleEvent.RemoveListener(RecyleTower);
        }
    }

    public void CreateTower(GameObject towerSlot,GameObject towerPrefab)
    {
        
        var towerSlotInfo = towerSlot.GetComponent<TowerSlot>();
        towerSlotInfo.isOccupied = true;        
        var towerInfo = towerPrefab.GetComponent<TowerScript>();
        var offset_y=0f;
        var rotation=Quaternion.identity;
        var index_offset=towerSlotInfo.isTop?0:1;
        var index = 0;
        switch (towerInfo.selectedTowerTypes)
        {
            case TowerScript.TowerTypes.Wind:
            offset_y = 0.7f;
            offset_y= towerSlotInfo.isTop ? 0.7f : 0.4f;
            break;
            case TowerScript.TowerTypes.Stun:
            index = 2;
            //rotation = towerSlotInfo.isTop == false ? Quaternion.identity : Quaternion.Euler(0, 0, 180);
            break;
            default:
            break;
        }      
        var pos = new Vector3(towerSlot.transform.position.x, towerSlot.transform.position.y+ offset_y, 0);
        var newTower=Instantiate(towerPrefabs[index+index_offset], pos, rotation).GetComponent<TowerScript>();
        newTower.recyleEvent.AddListener(RecyleTower);
        towers.Add(newTower);
        towerSlotPair.Add(newTower, towerSlotInfo);

    }


    //("Remove Tower and Add back to UI");
    public  void RecyleTower(TowerScript t)
    {
        TowerScript towerToRemove=t;
        foreach (var tower in towers)
        {
            if (tower.towerType == t)
            {
                
                towerToRemove = tower;
                break;
               
            }
        }

        if (towerToRemove != null)
        {
            towerSlotPair.TryGetValue(towerToRemove, out TowerSlot towerSlotToFree);
            {
                towerSlotToFree.isOccupied = false;
            }
            towers.Remove(towerToRemove);
            int index = (int) towerToRemove.selectedTowerTypes;            
            GameObject.Destroy(towerToRemove.gameObject);
            Instantiate(UITowers[index], towerUIHolder.transform);
        }
       
    }

}
