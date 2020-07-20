using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class faceChange : MonoBehaviour
{
    public SpriteRenderer thisDraw;
    public Sprite startingFace;
    public Sprite secondFace;
    public Sprite finalFace;
    private Sprite currentFace;

    void Start()
    {
        currentFace = startingFace;
    }

    public void faceOne()
    {
        thisDraw.sprite = startingFace;
    }

    public void faceTwo()
    {
        thisDraw.sprite = secondFace;
    }

    public void lastFace()
    {
        thisDraw.sprite = finalFace;
    }

    public Sprite whatIsCurrent()
    {
        return currentFace;
    }
}
