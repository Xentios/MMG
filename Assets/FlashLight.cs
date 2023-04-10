using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : MonoBehaviour
{

    public UnityEngine.Rendering.Universal.Light2D changecolor;
   

    private void Update()
    {
        changecolor.intensity = Mathf.PingPong(Time.time, 1)*150 + 100;
    }


}
