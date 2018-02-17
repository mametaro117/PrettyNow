using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Character : MonoBehaviour {

	[SerializeField]
	private GameObject Hero;
	[SerializeField]
	private GameObject nobu;
	[SerializeField]
	private GameObject van;
    [SerializeField]
	private GameObject YesNoButton;

    [SerializeField]
    private Text YesNoButtonText;


    [SerializeField]
    private CharaSelect charaSele;

	Animator anim;
	Vector3 MaxScale;

    //enum　数字を振ってくれる
    public enum CHARA_TYPE
    {
        HERO = 0,
        NOBU,
        VAN
    }

    private void Start()
    {
        YesNoButton.SetActive(false);
    }

    public void PushCharaserect(int name)
    {
        SoundManager.Instance.PlayCriAtomSE("button70");

        //選ばれたら移動＆大きくなる
        //ひとつのカードが選ばれたら他のカードは元の大きさに戻る
        switch (name)
        {
            case 0:
                iTween.ScaleTo(Hero, iTween.Hash("x", 1.1, "y", 1.1, "time", 0.1f));
                Hero.transform.SetSiblingIndex(4);
                charaSele.CharactorSelectNum = 0;
                NobuModoru();
                VanModoru();
                SoundManager.Instance.PlayCriAtomVoice("Soushi_7");
                YesNoButton.SetActive(true);
                break;
            case 1:
                iTween.ScaleTo(nobu, iTween.Hash("x", 1.1, "y", 1.1, "time", 0.1f));
                nobu.transform.SetSiblingIndex(5);
                charaSele.CharactorSelectNum = 1;
                HeroModoru();
                VanModoru();
                YesNoButton.SetActive(true);
                SoundManager.Instance.PlayCriAtomVoice("Nobu_10");
                break;
            case 2:
                iTween.ScaleTo(van, iTween.Hash("x", 1.1, "y", 1.1, "time", 0.1f));
                van.transform.SetSiblingIndex(4);
                charaSele.CharactorSelectNum = 2;
                HeroModoru();
				NobuModoru();
                YesNoButton.SetActive(true);
                SoundManager.Instance.PlayCriAtomVoice("Van_16");
                break;
            default:
                Debug.Log("予想外の値・エラー");
                break;
        }
	}


	//それぞれの戻る位置
	private void HeroModoru()
	{
        iTween.ScaleTo (Hero, iTween.Hash ("x", 1, "y", 1, "time", 0.1f));
        Hero.transform.SetSiblingIndex (0);
        Debug.Log("キャラナンバー" + charaSele.CharactorSelectNum);
    }
	private void NobuModoru()
	{
        iTween.ScaleTo (nobu, iTween.Hash ("x", 1, "y", 1, "time", 0.1f));
        nobu.transform.SetSiblingIndex(1);
        Debug.Log("キャラナンバー" + charaSele.CharactorSelectNum);
    }
    private void VanModoru()
	{
        iTween.ScaleTo (van, iTween.Hash("x", 1, "y", 1, "time", 0.1f));
        van.transform.SetSiblingIndex(1);
        Debug.Log("キャラナンバー" + charaSele.CharactorSelectNum);
    }

	public void Yes()
	{
        FadeManager.Instance.LoadScene(3, 1.0f);
	}

    public void No()
    {
        YesNoButton.SetActive(false);
        HeroModoru();
        NobuModoru();
        VanModoru();
    }
}
