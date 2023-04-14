using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject timeBar;

    [SerializeField]
    private float gameTimerInSeconds;

    [SerializeField]
    private GameObject zomWick;

    [SerializeField]
    private AudioClip winSound;
    [SerializeField]
    private AudioClip loseSound;

    [SerializeField]
    private AudioSource audioSource;

    public Volume Volume;

    private AudioSource music;

    private Volume winVolume;
    void Start()
    {
        var musicObject=GameObject.Find("Music");
        music = musicObject.GetComponent<AudioSource>();
        var winVolumeObject = GameObject.Find("GameWin Volume");
        winVolume = winVolumeObject.GetComponent<Volume>();
        timeBar.transform.DOScaleX(0, gameTimerInSeconds);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)){
            ChangeScene(0);
        }
       
        if (gameTimerInSeconds < 0) return;
        gameTimerInSeconds -= Time.deltaTime;
      
        if (gameTimerInSeconds < 0)
        {
            zomWick.GetComponent<EnemyMovement>().StopZomWick();
            music.volume = 0f;
            audioSource.clip = winSound;
            audioSource.Play();
            StartCoroutine(WinGameEffect());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {       
        audioSource.clip = loseSound;
        audioSource.Play();
        music.volume = 0f;
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
        yield return new WaitForSeconds(3f);        
        ChangeScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator WinGameEffect()
    {
       
        float duration = 2f;
        float elapsedTime = 0;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            winVolume.weight = elapsedTime/2;
            yield return null;
        }
        yield return new WaitForSeconds(3f);
        Scene scene = SceneManager.GetActiveScene();
        ChangeScene((scene.buildIndex + 1) % SceneManager.sceneCountInBuildSettings);
    }

    private void ChangeScene(int index)
    {
        DOTween.Clear();
        SceneManager.LoadScene(index);
    } 
}
