using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Flashing : MonoBehaviour {

    private Image img;
    private CanvasGroup cang;
    private bool isFadeflag = true;


	// Use this for initialization
	void Start () {
        cang = GetComponent<CanvasGroup>();
	}
	
	// Update is called once per frame
	void Update () {
		if (cang.alpha < 0.3f)
		{
			isFadeflag = !isFadeflag;
		}
		else if(cang.alpha == 1)
		{
			isFadeflag = !isFadeflag;
		}

		if (isFadeflag)
		{
			cang.alpha -= 0.5f * Time.deltaTime;

		}
		else
		{
			cang.alpha += 0.51f * Time.deltaTime;
		}
    }

    private IEnumerator Fadeing()
    {
        while (true)
        {
            yield return null;
        }
    }
}
