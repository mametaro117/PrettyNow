using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultChara : MonoBehaviour {

    private CharaSelect charaSele;
    public Sprite[] CharaSprites;
    private int spriteNum = 0;

	// Use this for initialization
	void Start () {
        charaSele = GameObject.FindGameObjectWithTag("CharaNum").GetComponent<CharaSelect>();
        Debug.Log("CharaSele.CutinCount:" + charaSele.CutinCount);
        switch (charaSele.CharactorSelectNum)
        {
            case 0:
                gameObject.GetComponent<Image>().sprite = CharaSprites[0];
                break;
            case 1:
                gameObject.GetComponent<Image>().sprite = CharaSprites[4];
                break;
            case 2:
                gameObject.GetComponent<Image>().sprite = CharaSprites[8];
                break;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ChangeSprite()
    {
        //charaSele.CutinCount = ;
        switch(charaSele.CharactorSelectNum)
        {
            case 0:
                gameObject.GetComponent<Image>().sprite = CharaSprites[spriteNum];
                break;
            case 1:
                gameObject.GetComponent<Image>().sprite = CharaSprites[spriteNum + 4];
                break;
            case 2:
                gameObject.GetComponent<Image>().sprite = CharaSprites[spriteNum + 8];
                break;
        }
        spriteNum++;
        Debug.Log("きたよ:" + spriteNum);
    }

    public void End()
    {
        if(spriteNum > charaSele.CutinCount)
        {
            gameObject.GetComponent<Animator>().Stop();
            Debug.Log("終わり");
        }
    }
}
