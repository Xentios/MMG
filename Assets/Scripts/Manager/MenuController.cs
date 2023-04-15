using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
   
    public void Scenetransition()
    {
        SceneManager.LoadScene(1);
    }
    public void Scenetransition(float index)
    {
        SceneManager.LoadScene((int) index);
    }
    public void Scenetransition(string name)
    {
        SceneManager.LoadScene(name);
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
