using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource engineSound;
    public AudioSource screechSound;
    public AudioSource buttonSound; 
    public AudioSource musicSound;
    // Start is called before the first frame update
    void Start()
    {
        PlayMusic();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayMusic()
    {
        musicSound.Play();
    }

    public void PlayScreech()
    {
        screechSound.Play();
    }

    public void PlayEngine()
    {
        engineSound.Play();
    }

    public void IncreaseEnginePitch(float rpm)
    {
        engineSound.pitch += Time.deltaTime * rpm / 550;
        engineSound.pitch = Mathf.Clamp(engineSound.pitch, 1, 2);
    }

    public void DecreaseEnginePitch(float rpm)
    {
        engineSound.pitch -= Time.deltaTime * rpm / 550;
        engineSound.pitch = Mathf.Clamp(engineSound.pitch, 1, 2);
    }

    public void PlayButton()
    {
        buttonSound.Play();
    }

    public void StopMusic()
    {
        musicSound.Stop();
    }

    public void StopScreech()
    {
        screechSound.Stop();
    }

    public void StopEngine()
    {
        engineSound.Stop();
    }

    public void StopButton()
    {
        buttonSound.Stop();
    }

    
}
