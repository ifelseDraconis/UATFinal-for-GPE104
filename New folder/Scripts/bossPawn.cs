using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class bossPawn : MonoBehaviour
{
    public GameManager instance;
    public GameObject puppetTeer;
    public bossController thisCollar;
    public Transform thisMob;

    private pewPew thisShooting;
    private faceChange maskMerry;
    private Vector3 goHere;
    private float startingBossSpeed;
    private float bossSpeed;

    private float makeShadow;
    private float shadowCooldown;

    // Start is called before the first frame update
    void Start()
    {
        thisShooting = GetComponent<pewPew>();
        maskMerry = GetComponent<faceChange>();
        thisCollar = puppetTeer.GetComponent<bossController>();
        startingBossSpeed = GameManager.instance.startingBossSpeed;
        bossSpeed = startingBossSpeed;
        makeShadow = 0.8f;
        goHere = thisMob.position;
    }
       
    // Update is called once per frame
    void FixedUpdate()
    {
        moveAlong();
        shadowCooldown -= Time.deltaTime;
        if (shadowCooldown <= 0)
        {
            shadowCooldown = makeShadow;
            castAShadow();
        }
    }

    public void shootAFish()
    {
        thisShooting.shootFish();
    }

    public void shootASeeker()
    {
        thisShooting.shootSeeker();
    }

    public void castAShadow()
    {
        thisShooting.castShadow();
    }

    void moveAlong()
    {
        float xDiff = thisMob.position.x - goHere.x;

        if (xDiff < 2)
        {
            thisMob.localPosition = thisMob.localPosition + new Vector3(xDiff, 0, 0);
        }
        else
        {
            thisMob.localPosition = thisMob.localPosition + new Vector3(xDiff / bossSpeed, 0, 0);
        }
    }

    public void increaseBossSpeed()
    {
        bossSpeed--;
        if (bossSpeed <= 0)
        {
            bossSpeed = 3;
        }
    }
    
}
