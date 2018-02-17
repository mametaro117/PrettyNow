using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [HideInInspector]
    public List<GameObject> _buttons = new List<GameObject>();

    [SerializeField]
    private GameObject[] Bullets;
    [SerializeField]
    private Sprite[] SkillSprites;
    [SerializeField]
    private GameObject[] Maincharacters = new GameObject[3];
    [SerializeField]
    private GameObject CoolTimeObject;


    public static GameObject _bullets = null;

    public static bool isScrooling = false;
    public static bool isTimeup = false;

    public static float MaxHP = 100;
    public static float NowHP = 100;
    public static float MindPoint = MaxHP;
    public static float GameTime = 40;
    public static string sTag;
    public static string[] Tags = { "LeftSkill", "MidSkill", "RightSkill" };

    private bool isPlay = true;
    private float StartTime = 3;
    private int BulletNum = 0;
    public static int CutingCount = 0;


    public void Awake()
    {
		Setting ();
    }

    void Start()
    {
        _buttons[0] = GameObject.Find("SkillButton1");
        _buttons[1] = GameObject.Find("SkillButton2");
        _buttons[2] = GameObject.Find("SkillButton3");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            isPlay = !isPlay;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {

        }


        Time.timeScale = (isPlay == true) ? Time.timeScale = 1 : Time.timeScale = 0;

        if (!isScrooling && SceneManager.GetActiveScene().name == "Main" && !isTimeup)
        {
            StartTime -= Time.deltaTime;
            if (StartTime <= 0) {
                SoundManager.Instance.PlayCriAtomSE("button70");
                isScrooling = true;
                GameObject _Barrier = GameObject.Find("Barrier");
                _Barrier.GetComponent<Animator>().SetTrigger("Move");
            } 
        }
    }

    void Setting()
    {
        Maincharacters[DateManager.CharactorNum].SetActive(true);
        MindPoint = MaxHP;
		isScrooling = false;
		isTimeup = false;
    }


    public void Choice(int _no)
    {
        //  タップ音の再生
        SoundManager.Instance.PlayCriAtomSE("button70");

        if (_buttons.Count <= _no)
        {
            return;
        }
            

        // 選択しているボタンのtagを設定
        GameManager.sTag = _buttons[_no].tag;
        // 続けて押させないように、falseに設定
        _buttons[_no].GetComponent<Button>().interactable = false;
        // 選択しているボタン以外を押せるようにリセット
        for (int i = 0; i < GameManager.Tags.Length; i++)
        {
            if (GameManager.Tags[i] != GameManager.sTag)
            {
                GameObject FixButton = GameObject.FindGameObjectWithTag(GameManager.Tags[i]);
                FixButton.GetComponent<Button>().interactable = true;
            }
        }
        MakeInstantiateBullet();
    }

    private void MakeInstantiateBullet()
    {
        if (_bullets != null)
            Destroy(_bullets);
        // 弾丸の複製
        _bullets = GameObject.Instantiate(Bullets[BulletNum]) as GameObject;

        // 弾丸の位置を調整
        Vector3 pos = GameObject.FindGameObjectWithTag("Arrow").transform.position;
        pos.z = 10f;
        _bullets.transform.position = pos;
    }


    // 玉の発射時に呼び出す関数→ FripAndFire.cs
    public void Chengesprite()
    {
        int rand = Random.Range(0, 4);
        GameObject sButton = GameObject.FindGameObjectWithTag(GameManager.sTag);
        sButton.GetComponent<Image>().sprite = SkillSprites[rand];
        
        GameObject obj = GameObject.Instantiate(CoolTimeObject) as GameObject;
        obj.transform.SetParent(sButton.transform.root, false);
        obj.transform.position = sButton.transform.position;
        obj.transform.localScale = new Vector3(1, 1, 1);

        //  初期化
        for (int i = 0; i < GameManager.Tags.Length; i++)
        {
            GameObject FixButton = GameObject.FindGameObjectWithTag(GameManager.Tags[i]);
            FixButton.GetComponent<Button>().interactable = true;
        }
    }
}