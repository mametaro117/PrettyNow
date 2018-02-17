using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour {

    private float DelayTime = 4;
    private float CountDownTime = 4;
    [SerializeField]
    private float GameTime = 60;
    [SerializeField]
    private float PopTime = 10;
    private float GameTime_Max;
    private float PopTimeStack = 0;
    [SerializeField]
    private Text GameTime_Text;

    [SerializeField]
    private GameObject CountDownObj;

    [SerializeField]
    private GameObject TargetGroup;
    [SerializeField]
    private Monitoringobject M_obj;

	[SerializeField]
	private GameObject AnimObj;
    [SerializeField]
    private GameObject Rainbow;


	[SerializeField]
	private Transform From, To;
    [SerializeField]
    private Vector3[] SD_MovePos_From = new Vector3[3], SD_MovePos_To = new Vector3[3];

    public GameObject AlartObj;

    [SerializeField]
    private RectTransform[] Alart_Pos = new RectTransform[3];

    [HideInInspector]
	public GameObject Bobj;

    private CharaSelect charaSele;

    [SerializeField]
    private GameObject[] BounusImages  = new GameObject[3];

    private bool isStart = false;
    private bool isFinishCountDown = false;
    private bool isTimeUp = false;
	private bool isAnim = false;
	private bool isEnable = false;

	void Start () {
        charaSele = GameObject.FindGameObjectWithTag("CharaNum").GetComponent<CharaSelect>();
        Bobj = Instantiate(BounusImages[charaSele.CharactorSelectNum]) as GameObject;
        GameTime_Max = GameTime;

    }

    void Update () {
		//	待機時間
        DelayTime -= Time.deltaTime;
        //  「こいつを狙え」のアニメーション再生
        if (isAnim == false)
		{
			isAnim = true;
			AnimObj.GetComponent<Animator>().SetTrigger("Play");
		}

        if (DelayTime <= 0)
		{
            //  一度だけ通したい
			if (isEnable == false)
			{
				isEnable = true;
				CountDownObj.GetComponent<Animator>().SetTrigger("Start");
			}
            CountDownTime -= Time.deltaTime;
            CountDownTime = Mathf.Max(CountDownTime, 0);
            //GetComponent<Text>().text = CountDownTime.ToString("F0");
        }

        //  虹をタイムに合わせて表示する
        Rainbow.GetComponent<Image>().fillAmount = GameTime / GameTime_Max;

		//  カウントダウンタイマーの処理
        if (CountDownTime <= 0 && isStart == false && isFinishCountDown == false)
        {
            //  BGMの再生
            SoundManager.Instance.PlayCriAtomBGM("頭の中はお花畑");
            Debug.Log("カウントダウン通過");
            isFinishCountDown = true;
            isStart = true;
            GetComponent<Text>().enabled = false;
            // MonitoringObjectクラスのスタート関数を再生
            M_obj.StartGame();
        }
        //  制限時間実装
        if (isStart)
        {
            PopTimeStack += Time.deltaTime;
            GameTime -= Time.deltaTime;
        }
        if (PopTimeStack >= PopTime)
        {
            Debug.Log("SD出現");
            BonusTargets();
            PopTimeStack = 0;
        }
        //	0以下にさせない
		GameTime = Mathf.Max(GameTime, 0);
        //	Textの表示
		GameTime_Text.text = GameTime.ToString("F0");
		//	タイムが0になった時の処理
        if (GameTime <= 0 && isTimeUp == false)
        {
            isTimeUp = true;
            TimeUP();
        }
    }

    //  時間切れになった時の処理場所  
    void TimeUP()
    {
        //  TimeUPアニメーションの再生
        CountDownObj.GetComponent<Animator>().SetTrigger("TimeUp");
        //  シーン移動かカットイン挿入など
        //  TimeUpアニメーション分、遅く関数を呼ぶコルーチン
        StartCoroutine("TimeUPCorutine");
        //  ターゲットの非表示
        TargetGroup.SetActive(false);        
    }

    public void TimeStop()
    {
        isStart = false;
        Debug.Log("Timer停止");
    }

    public void TimeResume()
    {
        isStart = true;
        Debug.Log("Timer稼働");
    }

    //	ボーナス用のシャボンをとばす
    public void BonusTargets()
    {
        ShufflePosition();
        Bobj.transform.position = From.position;
        Bobj.GetComponent<SpriteRenderer>().enabled = true;
        AlartObj.GetComponent<Animator>().SetTrigger("Play");
        iTween.MoveTo(Bobj, iTween.Hash("position", To.transform, "easeType", iTween.EaseType.easeInQuart, "Time", 5f, "Delay", 0.1f));
    }

    //  SDが移動するPositionのランダム化
    private void ShufflePosition()
    {
        int i = Random.Range(0, 3);
        switch (i)
        {
            case 0:
                Bobj.transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
            case 1:
                Bobj.transform.rotation = Quaternion.Euler(0, 180f, 0);
                break;
            case 2:
                Bobj.transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
        }
        From.position = SD_MovePos_From[i];
        To.position = SD_MovePos_To[i];
        AlartObj.transform.position = Alart_Pos[i].transform.position;
    }


    IEnumerator TimeUPCorutine()
    {
        yield return new WaitForSeconds(1.2f);
        M_obj.StopGame();
        yield break;
    }
}