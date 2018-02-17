using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartScript : MonoBehaviour {

    private Monitoringobject mobj;
    [HideInInspector]
    public MiniGame Minigame;

	// Use this for initialization
	void Start () {
        mobj = GameObject.FindGameObjectWithTag("Manager").GetComponent<Monitoringobject>();
	}
	
	// Update is called once per frame
	void Update () {
		if (GetComponent<Image>().color.a < 1)
        {
            gameObject.GetComponent<Image>().color += new Color(0, 0, 0, 0.1f);
        } 
	}

    public void tapButton()
    {
        iTween.MoveTo(gameObject, iTween.Hash(
            "position", new Vector3(0, -1, 0),
            "easeType", iTween.EaseType.easeInOutSine,
            "time", 1
            ));
        Debug.Log(MiniGame.Overvolume);
        MiniGame.Overvolume++;
        Minigame.Dokun();
        Destroy(gameObject, 1);
    }
}
