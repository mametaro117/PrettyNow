using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class guragura : MonoBehaviour {

	// Use this for initialization
	void Start () {
        iTween.ShakePosition(gameObject, iTween.Hash("x", 0.3f, "y", 0.3f, "time", 0.5f));
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
