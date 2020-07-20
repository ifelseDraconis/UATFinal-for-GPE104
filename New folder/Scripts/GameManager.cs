using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int thisCurrentScreen;

    public bool canMovePlayer;
    public bool hasKey;
    public float playerMovementSpeed;
    public int maxJumps;
    public float durationOfPlayerKill;
    public int maxLifeValue;
    public int currentLifeValue;
    public float forceOfJump;
    public int maxPlayerHealth;
    public float knockBackAmount;
    public float bossFadeRate;
    public float fishSpeed;
    public float seekerSpeed;
    public float startingBossSpeed;

    public Vector3 levelOneStartingLocation;
    public Vector3 levelTwoStartingLocation;

    public float distanceToGrounding;

    public enum GameState { titleScreen, levelOne, levelTwo, credits };
    public GameState currentScene;

    // triggers before the rest of the game is begun
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
        canMovePlayer = false;
        hasKey = false;
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        thisCurrentScreen = 0;
        currentScene = GameState.titleScreen;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setNewSpawnForPlayer()
    {
        GameObject thisController = GameObject.FindWithTag("PlayerController");
        pcController onController = thisController.GetComponent<pcController>();
        switch (currentScene)
        {
            case GameState.levelOne:
                onController.reSpawnLocation = levelOneStartingLocation;
                break;

            case GameState.levelTwo:
                onController.reSpawnLocation = levelTwoStartingLocation;
                break;
        }
        
    }

    public void forceThisSpawnForPlayer(Vector3 thisSpawnLocation)
    {
        GameObject thisController = GameObject.FindWithTag("PlayerController");
        pcController onController = thisController.GetComponent<pcController>();
        onController.reSpawnLocation = thisSpawnLocation;
    }

    public void returnToMain()
    {
        SceneManager.LoadScene(0);
        thisCurrentScreen = 0;
    }

    public void nextLevel()
    {
        thisCurrentScreen++;
        if (thisCurrentScreen == 1)
        {
            currentScene = GameState.levelOne;
        }
        if (thisCurrentScreen == 2)
        {
            currentScene = GameState.levelTwo;
        }
        setNewSpawnForPlayer();
        SceneManager.LoadScene(thisCurrentScreen);
    }

    public void resetLevel()
    {
        hasKey = false;
    }
}
