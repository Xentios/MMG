using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{

    public GameObject[] panels;
    private int index = 0;
    public void NextPanel()
    {
        panels[index].SetActive(false);
        index++;
        index %= panels.Length;
        panels[index].SetActive(true);
    }
}
