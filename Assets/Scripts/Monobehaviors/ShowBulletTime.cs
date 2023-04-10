using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class ShowBulletTime : MonoBehaviour
{
    [SerializeField]
    private SimpleCharacterController simpleCharacterController;
    [SerializeField]
    private TMP_Text textField;

    private void Update()
    {
        textField.text = "" +(int) Mathf.Max(0f,simpleCharacterController.shootTimer);
    }

}
