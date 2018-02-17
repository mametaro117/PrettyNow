using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetScpript : MonoBehaviour {

    private GameObject Charasl;

	// Use this for initialization
	void Start () {
        Charasl = GameObject.FindGameObjectWithTag("CharaNum");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Reset()
    {
        Destroy(Charasl);
    }
}
