using UnityEngine;
using UnityEngine.Audio;

public class AudioSlider : MonoBehaviour
{
    

    [SerializeField]
    private AudioMixer Mixer;

    [SerializeField]
    private AudioMixMode MixMode;

    [SerializeField]
    private string ExposedParameterName;

    [SerializeField]
    private AudioSource Example_effect;
    private void Start()
    {
      
        Mixer.SetFloat(ExposedParameterName, Mathf.Log10(PlayerPrefs.GetFloat(ExposedParameterName, 0) * 20));
    }

    public void OnChangeSlider(float Value)
    {
      
        switch (MixMode)
        {
           
            case AudioMixMode.LinearMixerVolume:
            Mixer.SetFloat(ExposedParameterName, (-80 + Value * 80));
            break;
            case AudioMixMode.LogrithmicMixerVolume:
            Mixer.SetFloat(ExposedParameterName, Mathf.Log10(Value) * 20);
            break;
        }

        float a = Mathf.Log10(Value) * 20;

        PlayerPrefs.SetFloat(ExposedParameterName, Value);
        PlayerPrefs.Save();

        if (Example_effect != null && Example_effect.isPlaying==false)
        {
            Example_effect.Play();
        }
    }


    public enum AudioMixMode
    {       
        LinearMixerVolume,
        LogrithmicMixerVolume
    }
}
