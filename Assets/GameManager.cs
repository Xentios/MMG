using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering;


public class GameManager : MonoBehaviour
{
    [SerializeField]
    private float gameTimerInSeconds;

    [SerializeField]
    private TMP_Text gameTimerText;

    [SerializeField]
    private GameObject zomWick;

    public Volume Volume;
    void Start()
    {

    }


    void Update()
    {
        gameTimerInSeconds -= Time.deltaTime;
        gameTimerText.text = " : " + (int) gameTimerInSeconds + "";
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Game Over");
        StartCoroutine(EndGameEffect());
    }

    IEnumerator EndGameEffect()
    {
        float duration = 1f;
        float elapsedTime = 0;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            Volume.weight = elapsedTime;           
            yield return null;
        }
    }
}
