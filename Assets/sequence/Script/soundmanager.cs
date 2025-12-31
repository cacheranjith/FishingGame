using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class soundmanager : MonoBehaviour
{
    [SerializeField] AudioMixer AudioMixer;
    [SerializeField] Slider musicslider;
    [SerializeField] Slider sfxslider;
    // Start is called before the first frame update
    void Start()
    {
        changevolume();
        changesfx();
    }

    // Update is called once per frame
    public void changevolume()
    {
        float volume = musicslider.value;
        AudioMixer.SetFloat("music", Mathf.Log10(volume)*20);
    }

    public void changesfx()
    {
        float volume = sfxslider.value;
        AudioMixer.SetFloat("sfx", Mathf.Log10(volume) * 20);
    }
}
