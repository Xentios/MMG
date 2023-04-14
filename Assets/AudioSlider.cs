using UnityEngine;
using UnityEngine.Audio;

public class AudioSlider : MonoBehaviour
{
    [SerializeField]
    private AudioMixer Mixer;
    [SerializeField]
    private AudioSource AudioSource;

    [SerializeField]
    private AudioMixMode MixMode;

    [SerializeField]
    private string ExposedParameterName;

    private void Start()
    {
        Mixer.SetFloat(ExposedParameterName, Mathf.Log10(PlayerPrefs.GetFloat(ExposedParameterName, 1) * 20));
    }

    public void OnChangeSlider(float Value)
    {
      
        switch (MixMode)
        {
            case AudioMixMode.LinearAudioSourceVolume:
            AudioSource.volume = Value;
            break;
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
    }


    public enum AudioMixMode
    {
        LinearAudioSourceVolume,
        LinearMixerVolume,
        LogrithmicMixerVolume
    }
}
