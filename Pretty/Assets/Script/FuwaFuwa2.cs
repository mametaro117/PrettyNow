using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuwaFuwa2 : MonoBehaviour {

    private float activetime = 0;

    void FixedUpdate()
    {

    }

    void Start () {
        
    }

    void Update()
    {
        activetime += Time.deltaTime * 3;
        transform.position += new Vector3(Mathf.Sin(activetime) * Time.deltaTime, Random.Range(3, 5) * Time.deltaTime, 0);
    }
}
