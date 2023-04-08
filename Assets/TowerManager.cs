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
    }
    [SerializeField]
    GameObject[] towerPrefabs;

    [SerializeField]
    Sprite[] UItowers;

    [SerializeField]
    GameObject[] UITowers;
    // Update is called once per frame
   
    public static void CreateTower(GameObject towerSlot,GameObject towerPrefab)
    {
        var pos = new Vector3(towerSlot.transform.position.x, towerSlot.transform.position.y, 0);
        Instantiate(towerPrefab, pos, Quaternion.identity);       
    }
}
