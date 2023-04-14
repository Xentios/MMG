using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DificultyManager : MonoBehaviour
{

    public FloatReference EnemySpeed;
    public FloatReference ReloadTimer;

    //public Toggle Easy;
    //public Toggle Normal;
    //public Toggle Hard;
    public ToggleGroup difToggleGroup;
    // Start is called before the first frame update
    void Start()
    {
        //Easy.interactable = false;
        //Normal.onValueChanged.AddListener(SetEasy);
        
        var toggles = difToggleGroup.GetComponentsInChildren<Toggle>();        
        foreach (var toggle in toggles)
        {
           toggle.onValueChanged.AddListener(SetDifficulty);
        }
    }


    public void SetDifficulty(bool isOn)
    {
        if (isOn)
        {
            var toggle = difToggleGroup.GetFirstActiveToggle();
            var x=toggle.GetComponent<ToggleValue>();

            EnemySpeed = x.EnemySpeed;
            ReloadTimer = x.ReloadTimer;
        }       
    }
    

}
