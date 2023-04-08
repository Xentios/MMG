using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private float gameTimerInSeconds;

    [SerializeField]
    private TMP_Text gameTimerText;
    void Start()
    {
        
    }

    
    void Update()
    {
        gameTimerInSeconds -= Time.deltaTime;
        gameTimerText.text =" : "+ (int) gameTimerInSeconds + "";
    }
}
