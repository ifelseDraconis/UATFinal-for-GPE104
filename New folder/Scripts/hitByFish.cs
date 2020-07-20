using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitByFish : MonoBehaviour
{
    public GameManager instance;
    public Transform thisFishLocation;
    public GameObject thisFish;
    public Rigidbody2D thisBullet;

    private float xDiff;
    private float yDiff;
    private float fishSpeed;


    // Start is called before the first frame update
    void Start()
    {
        if (thisFishLocation == null)
        {
            thisFishLocation = GetComponent<Transform>();
        }

        GameObject thePlayer = GameObject.FindWithTag("Player");
        Transform thePlayerLocation = thePlayer.GetComponent<Transform>();
        fishSpeed = GameManager.instance.fishSpeed;

        xDiff = thePlayerLocation.position.x - thisFishLocation.position.x;
        yDiff = thePlayerLocation.position.y - thisFishLocation.position.y;

        thisBullet.AddForce(new Vector3(xDiff * fishSpeed, yDiff * fishSpeed, 0), ForceMode2D.Impulse);

        Destroy(thisFish, 0.75f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        thisFishLocation.rotation = Quaternion.Euler(Vector3.forward * 2);
    }

}  
