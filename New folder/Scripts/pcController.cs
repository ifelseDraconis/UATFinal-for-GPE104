using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class pcController : MonoBehaviour
{
    public GameManager instance;

    public GameObject thisPlayer;
    public AudioSource thisAS;

    public AudioClip jumpSound;
    public AudioClip attackSound;
    public AudioClip dieSound;
    public AudioClip walkSound;
    public AudioClip climbSound;

    private int jumpCount;
    private bool canJump;
    private bool canMovePlayer;
    private bool playerIsGrounded;
    private float playerMovementSpeed;
    private float playerDyingLoop;
    private bool playerIsDying;
    private bool playerFacingLeft;

    private float timePlay;
    private float noiseManage = 0.45f;

    public Vector3 reSpawnLocation;

    private Rigidbody2D rbPlayer;
    private playerPawn thisPawnship;
    public Transform thisPlayerSpot;

    // Start is called before the first frame update
    void Start()
    {
        GameObject thisGameManager = GameObject.FindWithTag("GameManager");
        instance = thisGameManager.GetComponent<GameManager>();
        playerMovementSpeed = GameManager.instance.playerMovementSpeed;
        thisPlayerSpot = thisPlayer.GetComponent<Transform>();
        playerMovementSpeed = GameManager.instance.playerMovementSpeed;
        rbPlayer = thisPlayer.GetComponent<Rigidbody2D>();
        thisPawnship = thisPlayer.GetComponent<playerPawn>();
        playerIsDying = false;
        playerFacingLeft = false;
    }

    // Update is called once per frame
    void Update()
    {
        // can the player move during this update
        canMovePlayer = GameManager.instance.canMovePlayer;

        if (thisPlayer == null)
        {
            GameObject newPlayer = GameObject.FindWithTag("Player");
            if (newPlayer != null)
            {
                thisPlayer = newPlayer;
                thisPlayerSpot = thisPlayer.GetComponent<Transform>();
                playerMovementSpeed = GameManager.instance.playerMovementSpeed;
                rbPlayer = thisPlayer.GetComponent<Rigidbody2D>();
                thisPawnship = thisPlayer.GetComponent<playerPawn>();
            }
        }
        else
        {
            if (GameManager.instance.canMovePlayer)
            {
                if (thisPawnship.isPlayerGrounded())
                {
                    jumpCount = 0;
                    canJump = true;
                }
                else
                {
                    // play the animation of the player jumping
                    thisPawnship.nowJump();
                    // play the audio for the jump
                    if (timePlay == 0)
                    {
                        AudioSource.PlayClipAtPoint(jumpSound, thisPlayerSpot.position, 0.5f);
                        timePlay += noiseManage;
                    }
                }
                if (jumpCount >= GameManager.instance.maxJumps)
                {
                    canJump = false;
                }
                else
                {
                    canJump = true;
                }
                if (Input.GetKey("w") | Input.GetKey("up"))
                {
                    if (thisPawnship.collidingWithLadder)
                    {
                        // code to climb up the ladder if the player is on it
                        thisPawnship.nowJump();
                        // code to make sounds for climbing down the ladder
                        if (timePlay == 0) 
                        {
                            AudioSource.PlayClipAtPoint(climbSound, thisPlayerSpot.position, 0.5f);
                            timePlay += noiseManage;
                        }
                        
                    }
                }

                if (Input.GetKey("a") | Input.GetKey("left"))
                {
                    playerFacingLeft = true;
                    thisPlayerSpot.localPosition = thisPlayerSpot.localPosition - new Vector3(playerMovementSpeed, 0, 0);
                    // code call the player sprite and have it move left
                    thisPawnship.nowWalk();
                    // code to make sounds for walking
                    if (timePlay == 0)
                    {
                        AudioSource.PlayClipAtPoint(walkSound, thisPlayerSpot.position, 0.5f);
                        timePlay += noiseManage;
                    }
                }

                if (Input.GetKey("d") | Input.GetKey("right"))
                {
                    playerFacingLeft = false;
                    thisPlayerSpot.localPosition = thisPlayerSpot.localPosition + new Vector3(playerMovementSpeed, 0, 0);
                    // code to have the player sprite move right
                    thisPawnship.nowWalk();
                    // code to make sound for walking
                    if (timePlay == 0)
                    {
                        AudioSource.PlayClipAtPoint(walkSound, thisPlayerSpot.position, 0.5f);
                        timePlay += noiseManage;
                    }
                }

                if (Input.GetKey("s") | Input.GetKey("down"))
                {
                    if (thisPawnship.collidingWithLadder)
                    {
                        // code to climb down the ladder if the player is on it
                        thisPawnship.nowJump();
                        // code to make sounds for climbing down the ladder
                        if (timePlay == 0)
                        {
                            AudioSource.PlayClipAtPoint(climbSound, thisPlayerSpot.position, 0.5f);
                            timePlay += noiseManage;
                        }
                    }
                }

                if (Input.GetKey("f"))
                {
                    // code to attack
                    // code to activate the attack correct attack box
                    // code to show the attacking animation
                    thisPawnship.nowAttack(playerFacingLeft);
                    if (timePlay == 0)
                    {
                        AudioSource.PlayClipAtPoint(attackSound, thisPlayerSpot.position, 0.5f);
                        timePlay += noiseManage;
                    }
                }

                if (Input.GetKeyDown("space"))
                {
                    // this checks to see if the player still has any jumps remaining before adding a vertical force
                    // to the rigidbody
                    if (canJump)
                    {
                        rbPlayer.AddForce(thisPlayerSpot.up * GameManager.instance.forceOfJump, ForceMode2D.Impulse);
                        // code to play the animation of the player jumping
                        jumpCount++;
                        AudioSource.PlayClipAtPoint(jumpSound, thisPlayerSpot.position, 0.7f);
                    }
                }
                
            }
        }

        if (playerIsDying)
        {
            GameManager.instance.canMovePlayer = false;
            if (playerDyingLoop <= GameManager.instance.durationOfPlayerKill)
            {
                // play the animation for the player dying
                thisPawnship.nowDie();
                playerDyingLoop += Time.deltaTime;
            }
            else
            {
                // code to kill the player and send them back to respawn
                stopKillingPlayer();
                backToStart();
                GameManager.instance.currentLifeValue--;
                if (GameManager.instance.currentLifeValue <= 0)
                {
                    GameManager.instance.returnToMain();
                }
            }
        }

        timePlay -= Time.deltaTime;
        if (timePlay <= 0)
        {
            timePlay = 0;
        }
    }

    public void startKillingPlayer()
    {
        // public method that allows to set the player dying bool to true
        playerIsDying = true;
    }

    public void stopKillingPlayer()
    {
        // this breaks the dying animation loop, and allows the player to move the character again.
        thisPawnship.resetHealth();
        playerIsDying = false;
        GameManager.instance.canMovePlayer = true;
    }

    public void backToStart()
    {
        // this teleports the player pawn back to the respawn location currently set
        thisPlayerSpot.localPosition = reSpawnLocation;
    }

    public void updateSpawn(Vector3 newSpawnLocation)
    {
        // this forces an update for the player spawn location
        GameManager.instance.forceThisSpawnForPlayer(newSpawnLocation);
    }

    public void swatMe()
    {
        // knockback trigger if the player is in contact with an enemy
        if (playerFacingLeft)
        {
            thisPlayerSpot.localPosition = thisPlayerSpot.localPosition + new Vector3(GameManager.instance.knockBackAmount, 0, 0);
        }
        else
        {
            thisPlayerSpot.localPosition = thisPlayerSpot.localPosition - new Vector3(GameManager.instance.knockBackAmount, 0, 0);
        }        
    }

    public bool amIaSouthPaw()
    {
        return playerFacingLeft;
    }

    public void updateGrounding(bool isGrounded)
    {
        playerIsGrounded = isGrounded;
    }
}
