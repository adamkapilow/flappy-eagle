using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    // Detects which volume slider this is attatched to
    // and sets default volume accordingly.
    private void Awake()
    {
        Slider slider = gameObject.GetComponent<Slider>();
        string name = gameObject.name;
        
        if (name.StartsWith("Music", true, null))
        {
            slider.value = PlayerPrefs.GetFloat("music volume", 1);
        }
        else
        {
            slider.value = PlayerPrefs.GetFloat("effects volume", 0.7f);
        }
        
    }
    public void setMusicVolume(float volume)
    {
        GameManager.Instance.music.volume = volume;
        PlayerPrefs.SetFloat("music volume", volume);
    }

    public void setEffectsVolume(float volume)
    {
        Transform pipeCenter = GameManager.Instance.pipe.transform.Find("Pipe Center");
        PlayerPrefs.SetFloat("effects volume", volume);
        AudioSource effects = pipeCenter.GetComponent<AudioSource>();
        effects.volume = volume;
    }
}
