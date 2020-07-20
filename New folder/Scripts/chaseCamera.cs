using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chaseCamera : MonoBehaviour
{
    public GameObject playerSprite;
    public Transform playerLocation;
    public GameObject cameraObject;
    public Transform cameraLocation;
    public float maxX;
    public float maxY;
    public float minX;
    public float minY;
    private bool dontRun;

    // Start is called before the first frame update
    void Start()
    {
        if (playerSprite == null)
        {
            playerSprite = GameObject.FindWithTag("Player");
        }

        if (playerLocation == null)
        {
            playerLocation = playerSprite.GetComponent<Transform>();
        }

        if (cameraObject == null)
        {
            cameraObject = GameObject.FindWithTag("MainCamera");
        }

        if (cameraLocation == null)
        {
            cameraLocation = cameraObject.GetComponent<Transform>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
