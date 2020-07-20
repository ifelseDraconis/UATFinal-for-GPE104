using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pewPew : MonoBehaviour
{
    public GameManager instance;
    public GameObject bossController;
    public Transform bossLocation;
    public GameObject fishMissile;
    public GameObject seekerMissile;
    public GameObject bossShadow;

    private faceChange whatMask;
    private Sprite currentFace;

    // Start is called before the first frame update
    void Start()
    {
        whatMask = GetComponent<faceChange>();   
    }

    public void shootFish()
    {
        // lobs a fish at the player
        Instantiate(fishMissile, bossLocation.localPosition + new Vector3(0, 2, 0), Quaternion.identity);
    }

    public void shootSeeker()
    {
        // shoots a seeking missile at the player
        Instantiate(seekerMissile, bossLocation.localPosition + new Vector3(0, 3, 0), Quaternion.identity);
    }

    public void castShadow()
    {
        // calls to update on what is the currently displayed mask
        updateMask();
        Instantiate(bossShadow, bossLocation.localPosition + new Vector3(0, 0, 0), Quaternion.identity);
    }

    void updateMask()
    {
        // fetches the current mask from the faceChanges class attached to the boss
        currentFace = whatMask.whatIsCurrent();
    }
}
