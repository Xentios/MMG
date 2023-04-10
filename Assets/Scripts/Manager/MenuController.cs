using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Scenetransition()
    {
        SceneManager.LoadScene(1);
    }
    public void Scenetransition(float index)
    {
        SceneManager.LoadScene((int) index);
    }

    public void ExitGame()
    {



       

    #if UNITY_WEBGL
            if (Screen.fullScreen) Screen.fullScreen = false;
    #else
            Application.Quit();
    #endif



    }
}
