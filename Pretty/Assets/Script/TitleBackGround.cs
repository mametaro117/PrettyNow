using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleBackGround : MonoBehaviour {

    public float ____Speed, ScreenSize;

    // Use this for initialization
    void Start()
    {
        ScreenSize = ScreenSize / 100;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        transform.Translate(0.1f * ____Speed, 0, 0);
        if (transform.position.x >= ScreenSize)
        {
            transform.position = new Vector3(-ScreenSize, transform.position.y, 0);
        }
    }
}
