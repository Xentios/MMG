using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private float gameTimerInSeconds;

    [SerializeField]
    private TMP_Text gameTimerText;

    [SerializeField]
    private GameObject zomWick;

    [SerializeField]
    private AudioClip winSound;
    [SerializeField]
    private AudioClip loseSound;

    [SerializeField]
    private AudioSource audioSource;

    public Volume Volume;
    void Start()
    {

    }


    void Update()
    {
        if (gameTimerInSeconds < 0) return;
        gameTimerInSeconds -= Time.deltaTime;
        gameTimerText.text = "" + (int) gameTimerInSeconds;
        if (gameTimerInSeconds < 0)
        {
            audioSource.clip = winSound;
            audioSource.Play();
            StartCoroutine(WinGameEffect());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Game Over");
        audioSource.clip = loseSound;
        audioSource.Play();
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

        yield return new WaitForSeconds(7f);
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.buildIndex + 1);
    }

    IEnumerator WinGameEffect()
    {
        yield return new WaitForSeconds(7f);
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene((scene.buildIndex + 1)%4);
    }
}
