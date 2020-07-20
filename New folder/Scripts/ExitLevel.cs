using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitLevel : MonoBehaviour
{
    public GameManager instance;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {

            GameManager.instance.nextLevel();            
            
        }

    }
}
