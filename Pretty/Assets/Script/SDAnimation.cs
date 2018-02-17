using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SDAnimation : MonoBehaviour {

	[SerializeField]
	private Sprite[] AnimationImage;


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetSprite()
	{
		gameObject.GetComponent<SpriteRenderer>().sprite = AnimationImage[1];
	}
}
