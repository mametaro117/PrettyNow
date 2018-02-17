using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MiniGame : MonoBehaviour {

    //  タップするハートのprefab
    [SerializeField]
    private GameObject HeartPrefab;
    //  配置するハートの数
    private int volume = 6;
    //  ハートをタップした回数
    public static int Overvolume = 0;

    //  メーターの増加をさせるために取得
    [SerializeField]
    private GameObject Berobj;
    //  メーター内部を光らせるアニメーションの開始用
    [SerializeField]
    private Animator MpberbgAnimator;

    [SerializeField]
    private Transform Parent;

    [SerializeField]
    private Transform[] pos = new Transform[6];

    private Monitoringobject Mobj;
    private CharaSelect charaSele;

	// Use this for initialization
	void Start () {
        Mobj = GameObject.FindGameObjectWithTag("Manager").GetComponent<Monitoringobject>();
        charaSele = GameObject.FindGameObjectWithTag("CharaNum").GetComponent<CharaSelect>();
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void CutIn_Minigame()
    {
        GetComponent<Animator>().SetTrigger("Play");
        StartCoroutine(MinigamePlay());
    }
    
    public void Dokun()
    {        
        MpberbgAnimator.SetTrigger("Resume");
        float fromValue = (Overvolume - 1) * 0.16666f;
        float toValue = Overvolume * 0.16666f;
        iTween.ValueTo(Berobj.gameObject, iTween.Hash("from", fromValue, "to", toValue, "time", 1.0f, "easetype", iTween.EaseType.easeOutCubic, "onupdate", "OnUpdateBer", "onupdatetarget", gameObject));
        Debug.Log("FillAmount増加");
        Mobj.ShakePositionHeartRight();
        Debug.Log("PunchScale");
    }

    void OnUpdateBer(float value)
    {
        Berobj.GetComponent<Image>().fillAmount = value;
    }

    IEnumerator MinigamePlay()
    {
        //  メーターの移動アニメーション終了を待つ
        yield return new WaitForSeconds(2f);
        //  魔法少女のカットインボイス再生
        switch (charaSele.CharactorSelectNum)
        {
            case 0:
                SoundManager.Instance.PlayCriAtomVoice("Ester_mottokawaiku");
                break;
            case 1:
                SoundManager.Instance.PlayCriAtomVoice("Amelia_sugoiyatu");
                break;
            case 2:
                SoundManager.Instance.PlayCriAtomVoice("Libre_miigakawaiisister");
                break;
        }
        //  メーターのキラキラアニメーション開始
        MpberbgAnimator.ResetTrigger("Resume");
        MpberbgAnimator.SetTrigger("Play");
        //  ハートを生成する
        for(int i = 0; i < volume; i++)
        {
            //var parent = transform.parent;
            GameObject obj = Instantiate(HeartPrefab, pos[i].position , Quaternion.identity, Parent) as GameObject;
            obj.GetComponent<Image>().color = new Vector4(1, 1, 1, 0);
            obj.GetComponent<HeartScript>().Minigame = gameObject.GetComponent<MiniGame>();
        }
        //
        Mobj.applayBer_V();
        //yield return new WaitForSeconds(2f);

        while (volume > Overvolume)
        {
            yield return null;
        }
        Overvolume = 0;
        yield return new WaitForSeconds(0.5f);
        Debug.Log("<color=red>MiniGame関数</color>");
        GetComponent<Animator>().SetTrigger("Resume");
        Mobj.CutIn();
        yield break;
    }
}
