using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneLoader : MonoBehaviour
{
    public GameManager instance;
    public GameObject thisAudio;
    private AudioHandler thisBeatBox;

    void Start()
    {
        GameManager.instance.canMovePlayer = true;
    }

    public void beginGameLevelOne()
    {
        SceneManager.LoadScene(2);
        thisBeatBox = thisAudio.GetComponent<AudioHandler>();
        //thisBeatBox.playMusic1();
    }

    public void beginCredits()
    {
        SceneManager.LoadScene(3);
    }
}
