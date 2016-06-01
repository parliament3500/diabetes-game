using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{
    public AudioClip enemyHit1, enemyHit2, insulinPickup, gameoverSound, landingSound, runningSound;
    public float sfxVolume;
    private AudioSource _audio, _running;
    private static SoundManager instance = null;

    void Awake()
    {
        _audio = GetComponent<AudioSource>();
        _running = GameObject.Find("Running sfx").GetComponent<AudioSource>();
    }

    void Start()
    {
        if (instance != null) Destroy(gameObject);
        else
        {
            instance = this;
            GameObject.DontDestroyOnLoad(gameObject);
        }
    }

    public void audioEnemyHit()
    {
        int rand = Random.Range(0,2);

        if(rand == 0) _audio.PlayOneShot(enemyHit1, sfxVolume);  
        else          _audio.PlayOneShot(enemyHit2, sfxVolume);  
           

    }

    public void audioInsulinHit()
    {
        _audio.PlayOneShot(insulinPickup, sfxVolume);
    }
    public void GameoverSound()
    {
        _audio.PlayOneShot(gameoverSound, sfxVolume);
    }
    public void LandingSound()
    {
        _audio.PlayOneShot(landingSound, sfxVolume);
    }

    /// <summary>
    /// true to play the sound, false if you don't
    /// </summary>
    /// <param name="isRunning">If set to <c>true</c> is running.</param>
    public void RunningSound(bool isRunning)
    {
        if(!_running.isPlaying && isRunning)
        {
            _running.loop = true;
            _running.clip = runningSound;
            _running.Play(); 
        }
        else if (isRunning == false)
        {
            _running.Stop();
        }
    }
}
