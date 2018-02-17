using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaSelect : MonoBehaviour {

    [HideInInspector]
    public int CharactorSelectNum = -1;
    [HideInInspector]
    public int GirlBubblePoint = 0;
    [HideInInspector]
    public int BoyBubblePoint = 0;
    [HideInInspector]
    public int SDBubblePoint = 0;
    [HideInInspector]
    public int CutinCount = 0;

    // Use this for initialization
    void Start () {
        DontDestroyOnLoad(this);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
