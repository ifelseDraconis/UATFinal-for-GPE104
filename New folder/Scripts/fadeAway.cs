using System;
using System.Collections;
using UnityEngine;

public class fadeAway : MonoBehaviour
{
    [SerializeField]
    public GameObject thisShadow;

    public GameManager instance;

    private float bossFadeRate;

    // Start is called before the first frame update
    void Start()
    {
        bossFadeRate = GameManager.instance.bossFadeRate;
        if (bossFadeRate <= 0.0f)
        {
            bossFadeRate = 2.0f;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Color currentColor = GetComponent<SpriteRenderer>().color;
        currentColor.a -= bossFadeRate;
        GetComponent<SpriteRenderer>().color = currentColor;

        if (currentColor.a <= 0.0f)
        {
            Destroy(thisShadow);
        }
    }
}
