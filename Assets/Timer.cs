using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public TMPro.TextMeshPro text;
    // Start is called before the first frame update
    private GameManager gamemanager;
    void Start()
    {
        text = GetComponent<TMPro.TextMeshPro>();
        gamemanager = GameObject.Find("GameManager").GetComponent<GameManager>();

    }

    // Update is called once per frame
    void Update()
    {
        text.text = ""+(int) gamemanager.gameTimerInSeconds;
    }
}
