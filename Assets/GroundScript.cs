using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundScript : MonoBehaviour
{
    [SerializeField]
    public TerrainFeatures.TerrainType terrainType;

    private void Awake()
    {
        gameObject.layer = LayerMask.NameToLayer("Ground");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
