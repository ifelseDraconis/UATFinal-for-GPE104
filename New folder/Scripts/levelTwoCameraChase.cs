using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelTwoCameraChase : MonoBehaviour
{
    public float xMin;
    public float xMax;
    public float cameraDrag;
    public float screenSizeX;

    public Transform playerLocation;
    public Transform cameraLocation;
        
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerLocation.position.x > xMin & playerLocation.position.x < xMax)
        {
            if (playerLocation.position.x < 0 & cameraLocation.position.x < playerLocation.position.x)
            {
                cameraLocation.localPosition = cameraLocation.localPosition + new Vector3(cameraDrag, 0, 0);
            }
            if (playerLocation.position.x < 0 & cameraLocation.position.x > playerLocation.position.x)
            {
                cameraLocation.localPosition = cameraLocation.localPosition - new Vector3(cameraDrag, 0, 0);
            }
            if (playerLocation.position.x > 0 & cameraLocation.position.x < playerLocation.position.x)
            {
                cameraLocation.localPosition = cameraLocation.localPosition + new Vector3(cameraDrag, 0, 0);
            }
            if (playerLocation.position.x > 0 & cameraLocation.position.x > playerLocation.position.x)
            {
                cameraLocation.localPosition = cameraLocation.localPosition - new Vector3(cameraDrag, 0, 0);
            }
        }
    }
}
