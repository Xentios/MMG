using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI text;
    // Start is called before the first frame update
    private GameManager gamemanager;
    void Start()
    {
        text = GetComponent<TMPro.TextMeshProUGUI>();
        gamemanager = GameObject.Find("GameManager").GetComponent<GameManager>();

    }

    // Update is called once per frame
    void Update()
    {
        text.text = ""+(int) gamemanager.gameTimerInSeconds;
    }
}
