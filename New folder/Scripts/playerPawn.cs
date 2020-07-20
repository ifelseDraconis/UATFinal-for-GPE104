using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerPawn : MonoBehaviour
{
    public AudioSource thisAudio;
    public Rigidbody2D thisRigid;
    public Transform thisGuy;

    public GameObject thisBoss;
    private pcController thisFeature;

    public bool collidingWithLadder;

    private int playerHealth;
    private int maxPlayerHealth;
    private bool canTakeDamage;

    private bool isWalking;
    private bool isJumping;
    private bool isAttacking;
    public bool isGrounded;
    public bool isDying;

    private float raycastDistanceForGrounding;



    // Start is called before the first frame update
    void Start()
    {
        if (thisRigid == null)
        {
            thisRigid = GetComponent<Rigidbody2D>();
        }

        if (thisBoss == null)
        {
            thisBoss = GameObject.FindWithTag("PlayerController");
        }

        if (thisAudio ==  null)
        {
            if (GameObject.FindWithTag("AudioBasis") != null)
            {
                GameObject thisAudioHolder = GameObject.FindWithTag("AudioBasis");
                thisAudio = thisAudioHolder.GetComponent<AudioSource>();
            }
        }
        maxPlayerHealth = GameManager.instance.maxPlayerHealth;
        thisFeature = thisBoss.GetComponent<pcController>();
        raycastDistanceForGrounding = GameManager.instance.distanceToGrounding;

    }

    // Update is called once per frame
    void Update()
    {
       if (collidingWithLadder)
        {
            thisRigid.gravityScale = 0;
        }
       else
        {
            thisRigid.gravityScale = 1;
        }

        
        
        if (isWalking)
        {
            GetComponent<Animator>().Play("Player_Walk");
            GetComponent<SpriteRenderer>().flipX = thisFeature.amIaSouthPaw();
        }

        if (isJumping)
        {
            GetComponent<Animator>().Play("Player_Jump");
            GetComponent<SpriteRenderer>().flipX = thisFeature.amIaSouthPaw();
        }

        if (isAttacking)
        {
            GetComponent<Animator>().Play("Player_Attack");
            GetComponent<SpriteRenderer>().flipX = thisFeature.amIaSouthPaw();
            // code to turn on the attack collider on the correct side of the player
            if (thisFeature.amIaSouthPaw())
            {
                // code for when the player is facing left

            }
            else
            {
                // code for when the player is facing right

            }
        }
        else
        {
            if (isJumping)
            {
                GetComponent<Animator>().Play("Player_Jump");
                GetComponent<SpriteRenderer>().flipX = thisFeature.amIaSouthPaw();
            }
        }
        
       

       
    }

    void FixedUpdate()
    {
        isPlayerGrounded();
        /* this updates the the damage and deals damage if the player is in a location to take a hit
        this won't be utulized in the basic platformer game as there are no enemies */
        if (canTakeDamage)
        {
            thisFeature.swatMe();
        }

        if (playerHealth <= 0)
        {
            thisFeature.startKillingPlayer();
        }

        isGrounded = isPlayerGrounded();
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        Debug.Log("GameObject2 collided with " + collision.gameObject.tag);
        // this checks to see if the collision is with a ladder to allow the player to climb it
        if (collision.gameObject.tag == "Ladder")
        {
            collidingWithLadder = true;
        }
        else
        {
            collidingWithLadder = false;
        }
        // this checks to see if the collision is with a hostile
        if (collision.gameObject.tag == "Enemy")
        {
            if (canTakeDamage) { 
                playerHealth--;
                canTakeDamage = false;
            }
            
           
        }

        if (collision.gameObject.tag == "Water" | collision.gameObject.tag == "Bullet")
        {
            playerHealth--;
            thisFeature.backToStart();
        }

        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
            thisFeature.updateGrounding(isGrounded);
        }
        else
        {
            isGrounded = false;
            thisFeature.updateGrounding(isGrounded);
        }


        resolveActions();
    }

    void OnTriggerExit2D(Collider2D other)
    {
        isGrounded = false;
        thisFeature.updateGrounding(isGrounded);
    }

    public void resetHealth()
    {
        // this returns the player's health to the max that the player can have
        playerHealth = maxPlayerHealth;
    }

    public void nowWalk()
    {
        isWalking = true;
        GetComponent<Animator>().Play("Player_Walk");
        GetComponent<SpriteRenderer>().flipX = thisFeature.amIaSouthPaw();
    }

    public void nowJump()
    {
        isJumping = true;
        GetComponent<Animator>().Play("Player_Jump");
        GetComponent<SpriteRenderer>().flipX = thisFeature.amIaSouthPaw();
    }

    public void nowAttack(bool isFacingLeft)
    {

        isAttacking = true;
        GetComponent<Animator>().Play("Player_Attack");
        GetComponent<SpriteRenderer>().flipX = thisFeature.amIaSouthPaw();
    }

    public void nowDie()
    {
        isDying = true;
    }

    void resolveActions()
    {
        if (isAttacking | isWalking | isJumping) 
        { 

        }
        else
        {
            GetComponent<Animator>().Play("Player_Idle");
            GetComponent<SpriteRenderer>().flipX = thisFeature.amIaSouthPaw();
        }
        isAttacking = false;
        isWalking = false;
        isJumping = false;
        
    }

    public bool isPlayerGrounded()
    {
        /* code to check if the player is in contact on the bottom with ground tiles
         this does two things of preforming a raycast during the call to check if the player is grounded
        and pushes that update to the public variable for player sprite so that it can be seen outside
        of the current object */

        //LayerMask mask = LayerMask.GetMask("Ground");
        //isGrounded = Physics.Raycast(thisGuy.localPosition, -thisGuy.up, raycastDistanceForGrounding, mask);
        //if (isGrounded)
        //{
        //    UnityEngine.Debug.Log("The player is on the ground.");
        //}
        //thisFeature.updateGrounding(isGrounded);
        return isGrounded;
    }
}
