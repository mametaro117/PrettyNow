using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monitoringobject : MonoBehaviour {

    #region プロパティ

    [SerializeField]
    private GameObject boy, Girl_front, Girl_back, Card_start,Card_end;  // 立ち絵の参照
    [SerializeField]
    private GameObject meterObj;
    [SerializeField]
    private GameObject Arrow;

    private Shot sht;

    [SerializeField]
    private float MP_Point;
    private float MaxMP_Witch;

    [SerializeField]
    private GameObject CutInCountText, MPBer;
    [SerializeField]
    private GameObject HeartLeft, HeartRight;

    private int CutinCount = 0;
    private int HitCount_GirlBullet = 0;
    private int HitCount_BoyBullet = 0;
    private int HitCount_SD = 0;

    [SerializeField]
    private TargetInstantiate TargIns;
    private CharaSelect charaSele;
    private CountDown CotDn;

    [SerializeField]
    private GameObject TargetBubble;

    [Space(10), SerializeField]
    private Animator Cutin_MagicGirl_animator;
    [SerializeField]
    private Animator Cutin_Boy_animator;

    [SerializeField]
    private Sprite[] TargetBubbleImages = new Sprite[3];

    [SerializeField]
    private Sprite[] CharaCards = new Sprite[12];
    [SerializeField]
    private Sprite[] DefaultSprites = new Sprite[3];
    [SerializeField]
    private Sprite[] FirstChengeSprites = new Sprite[3];
    [SerializeField]
    private Sprite[] SecondChengeSprites = new Sprite[3];
    [SerializeField]
    private Sprite[] FinalChengeSprites = new Sprite[3];
    [SerializeField]
    private Sprite[] MagicGirlSprites = new Sprite[3];


    #endregion

    void Start () {
        Initialize();
    }	

	void Update () {
        // ショートカット用
        if (Input.GetKeyDown(KeyCode.Z)){
            charaSele.CutinCount = CutinCount;
            Debug.Log("ショートカット");
            FadeManager.Instance.LoadScene(6, 1.0f);
            SoundManager.Instance.StopCriAtomBGM("");
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            MP_Point = MaxMP_Witch;
            applayBer();
        }
    }
    #region シャボン玉関数
    //--------------------------------
    //  シャボン玉に弾を当てた時の処理

    public void HitBear()
    {
        //  Result用のスコア加算
        charaSele.GirlBubblePoint++;
        switch (charaSele.CharactorSelectNum)
        {
            case 0:
                SoundManager.Instance.PlayCriAtomVoice("Soushi_8");
                MP_Point += 2;
                ShakeRotationHeartRight();
                break;
            case 1:
                SoundManager.Instance.PlayCriAtomVoice("Nobu_11");
                MP_Point += 1;
                ShakeRotationHeartRight();
                break;
            case 2:
                SoundManager.Instance.PlayCriAtomVoice("Van_17");
                MP_Point += 1;
                ShakeRotationHeartRight();
                break;
            default:
                Debug.Log("予想外のエラー");
                break;
        }
        //  数値をゲージに反映させる
        applayBer();
    }
    public void HitCandy()
    {
        //  Result用のスコア加算
        charaSele.GirlBubblePoint++;
        switch (charaSele.CharactorSelectNum)
        {
            case 0:
                SoundManager.Instance.PlayCriAtomVoice("Soushi_5");
                MP_Point += 1;
                ShakeRotationHeartRight();
                break;
            case 1:
                SoundManager.Instance.PlayCriAtomVoice("Nobu_14");
                MP_Point += 2;
                ShakeRotationHeartRight();
                break;
            case 2:
                SoundManager.Instance.PlayCriAtomVoice("Van_20");
                MP_Point += 1;
                ShakeRotationHeartRight();
                break;
            default:
                Debug.Log("予想外のエラー");
                break;
        }
        //  数値をゲージに反映させる
        applayBer();
    }
    public void HitLip()
    {
        //  Result用のスコア加算
        charaSele.GirlBubblePoint++;
        switch (charaSele.CharactorSelectNum)
        {
            case 0:
                SoundManager.Instance.PlayCriAtomVoice("Soushi_9");
                MP_Point += 1;
                break;
            case 1:
                SoundManager.Instance.PlayCriAtomVoice("Nobu_13");
                MP_Point += 2;
                break;
            case 2:
                SoundManager.Instance.PlayCriAtomVoice("Van_19");
                MP_Point += 1;
                break;
            default:
                Debug.Log("予想外のエラー");
                break;
        }
        ShakeRotationHeartRight();
        applayBer();
    }
	public void HitHeart()
	{
        //  Result用のスコア加算
        charaSele.GirlBubblePoint++;
        switch (charaSele.CharactorSelectNum)
        {
            case 0:
                SoundManager.Instance.PlayCriAtomVoice("Soshi_6");
                break;
            case 1:
                SoundManager.Instance.PlayCriAtomVoice("Nobu_12");
                break;
            case 2:
                SoundManager.Instance.PlayCriAtomVoice("Van_18");
                break;
            default:
                Debug.Log("予想外のエラー");
                break;
        }
        MP_Point += 1;
		ShakeRotationHeartRight();
		applayBer();
	}
	public void HitBoyObj()
	{
        //  Result用のスコア加算
        charaSele.BoyBubblePoint++;
        MP_Point -= 2;
        if(MP_Point <= 0)
        {
            MP_Point = 0;
        }
		ShakeRotationHeartRight();
        applayBer();
    }
    public void HitSD()
    {
        //  Result用のスコア加算
        charaSele.SDBubblePoint++;
        MP_Point += 4;
        PanchScaleRight();
        applayBer();
    }
    //--------------------------------
    #endregion

    //  初期化
    void Initialize()
    {
        if (GameObject.FindGameObjectWithTag("CharaNum").GetComponent<CharaSelect>() != null)
        {
            charaSele = GameObject.FindGameObjectWithTag("CharaNum").GetComponent<CharaSelect>();
            TargetBubble.GetComponent<Image>().sprite = TargetBubbleImages[charaSele.CharactorSelectNum];
            boy.GetComponent<Image>().sprite = DefaultSprites[charaSele.CharactorSelectNum];
            Girl_front.GetComponent<Image>().sprite = MagicGirlSprites[charaSele.CharactorSelectNum];
            Girl_back.GetComponent<Image>().sprite = MagicGirlSprites[charaSele.CharactorSelectNum];
            boy.GetComponent<Image>().SetNativeSize();
        }
        if (GameObject.FindGameObjectWithTag("Time").GetComponent<CountDown>() != null)
        {
            CotDn = GameObject.FindGameObjectWithTag("Time").GetComponent<CountDown>();
            CotDn.TimeStop();
        }
        sht = Arrow.GetComponentInChildren<Shot>();
        MaxMP_Witch = MP_Point;
        MP_Point = 0;
        applayBer();
    }

    //  ゲームスタートの関数
    public void StartGame()
    {
        Debug.Log("StartGame関数");
        sht.ShotSetActive(true);
        meterObj.GetComponent<Animator>().SetTrigger("Play");
        Arrow.GetComponent<Image>().enabled = true;
        // シャボン玉の生成コルーチン
        TargIns.StartCoroutine("Pop");
    }

    //  アニメーションのストップ・一時停止
    public void DisableArrow()
    {
        Debug.Log("DisableArrow関数");
        sht.ShotSetActive(false);
        meterObj.GetComponent<Animator>().SetTrigger("Resume");
        Arrow.GetComponent<Image>().enabled = false;
    }

    //  ゲーム終了の関数
    public void StopGame()
    {
        TargIns.StopCoroutine("Pop");
        charaSele.CutinCount = CutinCount;
        Debug.Log("数値変化");
        // シーン遷移
        FadeManager.Instance.LoadScene(6, 1.0f);
        SoundManager.Instance.StopCriAtomBGM("");
    }

    #region ハートのアニメーション
    public void ShakePositionHeartRight()
    {
        iTween.ShakePosition(HeartRight, iTween.Hash("x", 0.3f, "y", 0.3f, "time", 0.5f));
    }

    void ShakeHeartRightVertex()
    {
        iTween.ShakePosition(HeartRight, iTween.Hash("y", 0.5f, "time", 0.5f));
    }

    public void ShakeRotationHeartRight()
    {
        iTween.ShakeRotation(HeartRight, iTween.Hash("z", 10, "time", 0.5f));
    }
    public void PanchScaleRight()
    {
        iTween.PunchScale(HeartRight, iTween.Hash("x", 1,"y", 1, "time", 0.5f));
    }

    #endregion

    //時間ないので緊急用
    public void applayBer_V()
    {
        Debug.Log("applayBer");
        CutInCountText.GetComponent<Text>().text = CutinCount.ToString("F0");
        MPBer.GetComponent<Image>().fillAmount = MP_Point / MaxMP_Witch;
    }

    // HPとMPのパラメータを割合表示
    void applayBer()
    {
        //  必殺ゲージがいっぱいになったとき
        if (MP_Point >= MaxMP_Witch)
        {
            callMinigame();
            return;
        }

        //  最大HPを越さないように
        //HP_MainPlayer = Mathf.Min(MaxHP_MainPlayer, HP_MainPlayer);
        //  各バーの割合表示
        CutInCountText.GetComponent<Text>().text = CutinCount.ToString("F0");
        MPBer.GetComponent<Image>().fillAmount = MP_Point / MaxMP_Witch;
    }

    void callMinigame()
    {
        try
        {
            CotDn.Bobj.GetComponent<iTween>();
        }
        catch(Exception e)
        {
            Debug.Log(e);
        }
        finally
        {
            //  SDの玉を横切らせない
            iTween.Stop(CotDn.Bobj);
            //  アラート画像の非表示
            CotDn.AlartObj.GetComponent<Image>().enabled = false;
        }

        //ミニゲームが入るのでTimeの停止
        CotDn.TimeStop();
        // 矢印やメーターの停止
        DisableArrow();
        // 魔法少女のカットイン
        Cutin_MagicGirl_animator.SetTrigger("Play");
        // ミニゲームの挿入
        HeartRight.GetComponentInChildren<MiniGame>().CutIn_Minigame();
        //  アラート画像の表示
        CotDn.AlartObj.GetComponent<Image>().enabled = true;
        Debug.Log("ミニゲームのポーズ");
        TargIns.TargetDelete();
        TargIns.StopCoroutine("Pop");
        //  ゲージの割合反映
        MPBer.GetComponent<Image>().fillAmount = MP_Point / MaxMP_Witch;
        //  アニメーションで変化させても面白いかも
        MP_Point = 0;
    }

    //  魔法少女の必殺技を発動するときの関数
    public void CutIn()
    {
        Debug.Log("CutInメソッド作動");
        Cutin_MagicGirl_animator.SetTrigger("Resume");
        //  タイマーの停止
        CotDn.TimeStop();
        switch (CutinCount)
        {
            case 0:
                StartCoroutine(CutinCoroutine(4.0f, false));
				TargIns.FirstTrigger();
                break;
            case 1:
                StartCoroutine(CutinCoroutine(4.0f, false));
				TargIns.SecondTrigger();
                break;
            case 2:
                StartCoroutine(CutinCoroutine(4.0f, true));
                break;
            default:
                break;
        }
    }

    //  カットイン時の停止処理用コルーチン
    IEnumerator CutinCoroutine(float num, bool isLoadScane)
    {
        yield return new WaitForSeconds(0.3f);
        switch (CutinCount)
        {
            case 0:
                Card_start.GetComponent<Image>().sprite = CharaCards[charaSele.CharactorSelectNum * 4 + 0];
                Card_end.GetComponent<Image>().sprite = CharaCards[charaSele.CharactorSelectNum * 4 + 1];
                Cutin_Boy_animator.SetTrigger("Play");
                switch (charaSele.CharactorSelectNum)
                {
                    case 0:
                        SoundManager.Instance.PlayCriAtomVoice("Soushi_14");
                        break;
                    case 1:
                        SoundManager.Instance.PlayCriAtomVoice("Nobu_17");
                        break;
                    case 2:
                        SoundManager.Instance.PlayCriAtomVoice("Van_21");
                        break;
                }
                boy.GetComponent<Image>().sprite = FirstChengeSprites[charaSele.CharactorSelectNum];
                break;
            case 1:
                Card_start.GetComponent<Image>().sprite = CharaCards[charaSele.CharactorSelectNum * 4 + 1];
                Card_end.GetComponent<Image>().sprite = CharaCards[charaSele.CharactorSelectNum * 4 + 2];
                Cutin_Boy_animator.SetTrigger("Play");
                switch (charaSele.CharactorSelectNum)
                {
                    case 0:
                        SoundManager.Instance.PlayCriAtomVoice("Soushi_15");
                        break;
                    case 1:
                        SoundManager.Instance.PlayCriAtomVoice("Nobu_18");
                        break;
                    case 2:
                        SoundManager.Instance.PlayCriAtomVoice("Van_25");
                        break;
                }
                boy.GetComponent<Image>().sprite = SecondChengeSprites[charaSele.CharactorSelectNum];
                break;
            case 2:
                Card_start.GetComponent<Image>().sprite = CharaCards[charaSele.CharactorSelectNum * 4 + 2];
                Card_end.GetComponent<Image>().sprite = CharaCards[charaSele.CharactorSelectNum * 4 + 3];
                Cutin_Boy_animator.SetTrigger("Play");
                switch (charaSele.CharactorSelectNum)
                {
                    case 0:
                        SoundManager.Instance.PlayCriAtomVoice("Soushi_13");
                        break;
                    case 1:
                        SoundManager.Instance.PlayCriAtomVoice("Nobu_19");
                        break;
                    case 2:
                        SoundManager.Instance.PlayCriAtomVoice("Van_26");
                        break;
                }
                boy.GetComponent<Image>().sprite = FinalChengeSprites[charaSele.CharactorSelectNum];
                break;
            default:
                break;
        }
        boy.GetComponent<Image>().SetNativeSize();
        applayBer_V();
        yield return new WaitForSeconds(num);
        CutinCount++;
        CutInCountText.GetComponent<Text>().text = CutinCount.ToString("F0");

        // TRUEだったらシーン遷移する
        if (isLoadScane)
        {
            Debug.Log("数値変化");
            charaSele.CutinCount = CutinCount;
            yield return new WaitForSeconds(1.0f);
            FadeManager.Instance.LoadScene(6, 1.0f);
            SoundManager.Instance.StopCriAtomBGM("");
            yield break;
        }
        StartGame();
        CotDn.TimeResume();
        yield break;
    }
}
