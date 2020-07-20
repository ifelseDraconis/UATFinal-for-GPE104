using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class seekingSphere : MonoBehaviour
{
    public GameManager instance;
    public Transform thisSeekerLocation;
    public GameObject thisSeeker;

    private float xDiff;
    private float yDiff;
    private float changeRate;

    // lower divide means a higher speed
    private float seekerSpeed;

    private GameObject thePlayer;
    private Transform thePlayerLocation;

    // Start is called before the first frame update
    void Start()
    {
        if (thisSeekerLocation == null)
        {
            thisSeekerLocation = GetComponent<Transform>();
        }

        thePlayer = GameObject.FindWithTag("Player");
        thePlayerLocation = thePlayer.GetComponent<Transform>();
        seekerSpeed = GameManager.instance.seekerSpeed;
        if (seekerSpeed <= 0)
        {
            seekerSpeed = 12;
        }

        Destroy(thisSeeker, 0.6f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
                
        xDiff = thePlayerLocation.position.x - thisSeekerLocation.position.x;
        yDiff = thePlayerLocation.position.y - thisSeekerLocation.position.y;
        changeRate = yDiff / xDiff;
        if (changeRate < 3)
        {
            thisSeekerLocation.localPosition = thisSeekerLocation.localPosition + new Vector3(xDiff / 2.0f, yDiff / 2.0f, 0.0f);
        }
        else
        {
            thisSeekerLocation.localPosition = thisSeekerLocation.localPosition + (new Vector3(xDiff, yDiff, 0.0f) / seekerSpeed);
        }
    }
}
