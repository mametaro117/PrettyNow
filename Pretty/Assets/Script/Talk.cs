using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;               //uGUIの機能を使う時に必須
using UnityEngine.SceneManagement;
using DG.Tweening;

[System.Serializable]
public struct TalkDate
{
    public string name;
    public string voice;
    public string background;
    public string charaposition;
    public string frameFileName;
    public string characterFaceFile;
    public string charanimation;
    public string talkText;
}

//Conditional コンディショナル = アタッチのミス直し

public class Talk : MonoBehaviour
{
    [SerializeField]
    [Range(0.01f, 1.0f)]
    float intervalForCharacterDisplay = 1.0f;      //1文字の表示にかかる時間

    //どのシーンか判別
    public enum HANTEI {
        OP = 0,
        ED,
        INTRO
    }
    [SerializeField]
    private HANTEI hantei;

    private float timeUntilDisplay = 0;           // 表示にかかる時間
    private float timeElapsed = 1;                // 文字列の表示を開始した時間

    private List<TalkDate> _scenarioDataList = new List<TalkDate>();

    private Sprite m_frameSprite;               //会話中のフレーム
    private Sprite m_background;                //会話中の背景

    [SerializeField]
    private GameObject BackGround;
    [SerializeField]
    private GameObject Talkframe;
    [SerializeField]
    private GameObject Pos1;
    [SerializeField]
    private GameObject Pos2;
    [SerializeField]
    private GameObject Pos3;
    [SerializeField]
    private GameObject Pos4;
    [SerializeField]
    private GameObject flash;
    [SerializeField]
    private GameObject Menu;

    private GameObject nowPos;
    private Sequence sq;

    [HideInInspector]
    public StringBuilder talkText = new StringBuilder();

	private int m_textNumber;
    private int spriteNum = 0;

    int LineCount = 0;  //現在の行
    [HideInInspector]
    public int textCount = 0; // 表示中の文字数

    bool _isActive = true;
    
    [SerializeField]
    private Text _displeyText = null;
    [SerializeField]
    private Text _displaymini = null;

    private CharaSelect charasl;

    bool IsCompleteDisplayText
    {
        get { return Time.time > timeElapsed + timeUntilDisplay; }
    }

    void Awake()
    {
        SoundManager.Instance.PlayCriAtomBGM("Toy_clock");
        if (hantei != HANTEI.OP)
        {
            charasl = GameObject.FindGameObjectWithTag("CharaNum").GetComponent<CharaSelect>();
            if(hantei == HANTEI.ED)
            {
                spriteNum = charasl.CutinCount;
            }
        }
    }

    // Use this for initialization
    void Start()
    {
        readScenarios();

        SoundManager.Instance.PlayCriAtomVoice(_scenarioDataList[LineCount].voice);
        SetSFBA();
        _isActive = true;
    }
    
    /// <summary>
    /// タップした時の処理
    /// </summary>
    public void TapButton()
    {
        //読み込み準備ができていなければ何もしない
        if (!_isActive) return;
        
        if (Input.GetMouseButtonUp(0))
        {
            // テキストがすべて表示されてなければすべて表示
            if (IsUpdatingText())
            {
                talkText.Length = 0; // 文字をクリア
                talkText.Append(_scenarioDataList[LineCount].talkText);
                textCount = talkText.Length - 1;
                _displeyText.text = talkText.ToString();
                Debug.Log("きた１");
            }else
            {
                if (!NextWord())
                {
                    Debug.Log("きた２");
                    LoadScene();
                    return;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        timeUntilDisplay += Time.deltaTime;

        if (timeUntilDisplay >= intervalForCharacterDisplay)
        {
            timeUntilDisplay = 0;

            //次に表示する文字を追加
                talkText.Append(_scenarioDataList[LineCount].talkText[textCount]);
            if (IsUpdatingText())
            {
                textCount++;
                _displeyText.text = talkText.ToString();
            }
        }
    }

    bool IsUpdatingText()
    {
        return textCount < _scenarioDataList[LineCount].talkText.Length - 1;
    }

    /// <summary>
    /// 次の行にいく処理
    /// </summary>
    /// <returns></returns>
    public bool NextWord()
    {
        LineCount++;//行数
        
        if (LineCount >= _scenarioDataList.Count)
        {
            return false;
        }
        //次の行にいったら前に使っていた音を止める。
        //次に指定された音を出す。
        SoundManager.Instance.StopCriAtomVoice();
        ChangeVoice();
        SetSFBA();
        textCount = 0;
        talkText.Length = 0; // 表示するテキストを空にする
        DispNameLabel();
        return true;
    }

    /// <summary>
    /// テキスト読み込み初期化
    /// </summary>
    /// <param name="no">テキスト番号</param>
    /// <returns></returns>
    void readScenarios()
    {
        //シナリオをリソースから読み込む
        LoadScenarioFromResources(m_textNumber);

        LineCount = 0;          //行数        
        textCount = 0;          //表示中の文字数
        timeUntilDisplay = 0;
        DispNameLabel();
    }

    /// <summary>
    /// TalkFrameの名前いれるとこに名前入れる処理
    /// </summary>
    private void DispNameLabel()
    {
        _displaymini.text = _scenarioDataList[LineCount].name;
    }

    /// <summary>
    /// どのテキストを読むかの処理とコンマ区切りの処理
    /// </summary>
    /// <param name="no"></param>
    void LoadScenarioFromResources(int no)
    {
        _scenarioDataList.Clear();
        
        if (hantei == HANTEI.INTRO)
            IntroResult();
        else if(hantei == HANTEI.ED)
            EdResult();
        else
            m_textNumber = 10;
        //テキストを全部読む
        var text = Resources.Load<TextAsset>("scenarios" + m_textNumber).text;
        var blocks = text.Split('\n');
        
        for (int i = 0; i < blocks.Length; ++i)
        {
            if (blocks[i] == "/") break;
            var blockStrs = blocks[i].Split(',');
            var talkData = new TalkDate();
            talkData.name = blockStrs[0];
            talkData.voice = blockStrs[1];
            talkData.background = blockStrs[2];
            talkData.charaposition = blockStrs[3];
            talkData.frameFileName = blockStrs[4];
            talkData.characterFaceFile = blockStrs[5];
            talkData.charanimation = blockStrs[6];
            talkData.talkText = blockStrs[7];
            //<br>を\nに変えて改行させる
            var txt = talkData.talkText.Replace("<br>", "\n");
            talkData.talkText = txt;
            _scenarioDataList.Add(talkData);
        }
    }

    /// <summary>
    /// キャラごとの会話シーンを選ぶ処理
    /// </summary>
    void IntroResult()
    {
		int charaNum = charasl.CharactorSelectNum;
        Debug.Log("番号きてるよ: " + charaNum);
        
        if (charaNum == 0)
            m_textNumber = 7;
        else if (charaNum == 1)
            m_textNumber = 8;
        else
            m_textNumber = 9;
    }

    /// <summary>
    /// エンド分岐を選ぶ処理
    /// </summary>
    void EdResult()
    {
        int charaNum = charasl.CharactorSelectNum;
        
        if(charaNum == 0)
        {
            if (charasl.CutinCount <= 1)
                m_textNumber = 2;
            else
                m_textNumber = 1;

        }else if(charaNum == 1)
        {
            if (charasl.CutinCount <= 1)
                m_textNumber = 6;
            else
                m_textNumber = 5;

        }else if(charaNum == 2)
        {
            if (charasl.CutinCount <= 1)
                m_textNumber = 4;
            else
                m_textNumber = 3;
        }
        Debug.Log("キャラ番号 = " + m_textNumber);
    }

    ///<summary>
    ///フレームとイメージ画像を入れ替える
    ///</summary>
    protected void SetSFBA()
    {
        ChangeSprite();
        ChangeFrame();
        ChangeBackGround();
        ChangeAnimation();
    }

    /// <summary>
    /// フレーム画像入れ替え
    /// </summary>
    /// <param name="num">フレームリスト番号</param>
    /// <param name="name">読み込みファイル名</param>
    protected void ChangeFrame()
    {
        string path = "image/UI/" + _scenarioDataList[LineCount].frameFileName;
        m_frameSprite = null;
        //Resources.UnloadUnusedAssets();       //使ってないモノを開放する

        m_frameSprite = Resources.Load<Sprite>(path);

        Image sp = Talkframe.GetComponent<Image>();
        sp.sprite = m_frameSprite;
    }

    /// <summary>
    /// イメージ画像入れ替え
    /// </summary>
    /// <param name="num">イメージリスト番号</param>
    /// <param name="name">読み込みファイル名</param>
    public void ChangeSprite()
    {
        string name = _scenarioDataList[LineCount].characterFaceFile;
        string place = _scenarioDataList[LineCount].charaposition;
        string path;
        if (spriteNum == 0)
        {
            path = "image/Character/" + name;
        }
        else
        {
            if(name.Substring(0,1) == "S" || name.Substring(0,1) == "V"|| name.Substring(0,1) == "N" )
                path = "image/Character/" + spriteNum + name;
            else
                path = "image/Character/" + name;
        }

        nowPos = GameObject.Find("Canvas/Pos" + place);

        if (name.Substring(0, 2) == "3E")
        {
            Pos2.GetComponent<Image>().sprite = Resources.Load<Sprite>("image/Character/Enashi");
            Pos3.GetComponent<Image>().sprite = Resources.Load<Sprite>("image/Character/Enashi");
            Pos4.GetComponent<Image>().sprite = Resources.Load<Sprite>("image/Character/Enashi");
        }
        else
        {
            nowPos.GetComponent<Image>().sprite = Resources.Load<Sprite>(path);
            Debug.Log("Position:" + place);
        }
    }

    /// <summary>
    /// ボイスの変更
    /// </summary>
    /// 
    void ChangeVoice()
    {
        string name = _scenarioDataList[LineCount].voice;
        SoundManager.Instance.PlayCriAtomVoice(name);
    }

    /// <summary>
    /// 背景の変更
    /// </summary>
    void ChangeBackGround()
    {
        string name = _scenarioDataList[LineCount].background;
        m_background = null;
        //トーク中にピカって光るとこ　アニメーションでやる予定
        if (name == "Pika")
        {
            Menu.SetActive(false);
            flash.GetComponent<Image>().DOColor(new Color(1, 1, 1, 1), 0.25f).SetEase(Ease.InOutQuart);
            Debug.Log("きてるよ！");
        }else if (name == "Tozi")
        {
            flash.GetComponent<Image>().DOColor(new Color(1, 1, 1, 0), 0.25f).SetEase(Ease.InOutQuart);
            m_background = Resources.Load<Sprite>("image/UI/back");
            Image img = BackGround.GetComponent<Image>();
            img.sprite = m_background;
            Menu.SetActive(true);
        }
        else
        {
            string path = "image/UI/" + name;
            m_background = Resources.Load<Sprite>(path);
            Image img = BackGround.GetComponent<Image>();
            img.sprite = m_background;
        }
    }

    /// <summary>
    /// キャラがアニメーションする処理
    /// </summary>
    private void ChangeAnimation()
    {
        string name = _scenarioDataList[LineCount].charanimation;

        if (name != null)
        {
            switch (name.Substring(0, 2))
            {
                case "Bu":
                    nowPos.transform.DOPunchPosition(new Vector2(20f, 0), 0.5f, elasticity: 2f, vibrato: 20);
                    break;
                case "Py":
                    nowPos.transform.DOJump(new Vector2(nowPos.transform.position.x, nowPos.transform.position.y), 5, 1, 0.3f);
                    break;
                case "Ri":
                    nowPos.transform.DOLocalMoveX(800, 0.5f).From();
                    break;
                case "Le":
                    nowPos.transform.DOMoveX(-100f, 0.5f).From();
                    break;
                case "na":
                    Debug.Log("なしでっせ");
                    break;
                default:
                    Debug.Log("ないよ");
                    break;
            }
            Debug.Log("アニメーション:" + name.Substring(0, 4));
        }
        else
            return;
    }

    /// <summary>
    /// シーン移動の処理
    /// </summary>
    public void LoadScene()
    {
        SoundManager.Instance.StopCriAtomVoice();
        SoundManager.Instance.StopCriAtomBGM("Toy_clock");
        switch (hantei)
        {
            case HANTEI.OP:
                SceneManager.LoadScene("CharacterChoose");
                break;
            case HANTEI.INTRO:
                SceneManager.LoadScene("Loading");
                break;
            case HANTEI.ED:
                GetComponent<ResetScpript>().Reset();
                SceneManager.LoadScene("Title");
                break;
        }
        Resources.UnloadUnusedAssets();
    }
}