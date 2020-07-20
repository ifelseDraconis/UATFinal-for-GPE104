using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHandler : MonoBehaviour
{
    public static AudioHandler thisAudioBase;

    public GameManager instance;
    public AudioSource thisAudioOut;
    public AudioClip titleMusics;
    public AudioClip levelOneMusics;
    public AudioClip levelTwoMusics;
    public AudioClip coinPickup1;
    public AudioClip coinPickup2;
    public AudioClip playerDie;
    public AudioClip playerSplash;
    public AudioClip monsterDie;
    public AudioClip monsterSplash;
    public AudioClip keyPickup;
    public AudioClip doorOpen;

    private bool playerCanMove;

    // triggers before the rest of the game is begun
    void Awake()
    {
        if (thisAudioBase == null)
        {
            thisAudioBase = this;
        }
        else
        {
            Destroy(this);
        }
        thisAudioOut = GetComponent<AudioSource>();

        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        thisAudioOut.clip = titleMusics;
        thisAudioOut.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playMusic0()
    {
        thisAudioOut.clip = titleMusics;
        thisAudioOut.Play();
    }

    public void playMusic1()
    {
        thisAudioOut.clip = levelOneMusics;
        thisAudioOut.Play();
    }
}
