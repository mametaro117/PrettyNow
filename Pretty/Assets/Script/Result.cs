using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Result : MonoBehaviour
{
    [SerializeField]
    private Text text;
    [SerializeField]
    private Image chara;
    [SerializeField]
    private Image sdshabon;
    [SerializeField]
    private Text pretty_text;
    [SerializeField]
    private Text boy_text;
    [SerializeField]
    private Text sd_text;

    public Sprite[] Sprites = new Sprite[12];
    public Sprite[] SD = new Sprite[3];
    public Sprite[] Bshabon = new Sprite[3];
    private CharaSelect charaSele;
    private int imgInt = 0;
    private float waitTime = 0;
    private int maxCount;
    private int loopNum = 0;
    //各シャボン玉を何個当てたかの処理をまだ書いてない

    void Awake()
    {
        SoundManager.Instance.PlayCriAtomBGM("famipop4");
    }
    // Use this for initialization
    void Start()
    {
        charaSele = GameObject.FindGameObjectWithTag("CharaNum").GetComponent<CharaSelect>();
        UpText();
        ChangeSprite();
        pretty_text.text = "×" + charaSele.GirlBubblePoint.ToString();
        boy_text.text = "×" + charaSele.BoyBubblePoint.ToString();
        sd_text.text = "×" + charaSele.SDBubblePoint.ToString();

        maxCount = charaSele.CutinCount;
    }

    private void UpText()
    {
        switch (charaSele.CutinCount)
        {
            case 0:
                text.text = "男子の動きを追って\n魔法を当てよう！\n男子を可愛く出来てないよ！";
                break;
            case 1:
                text.text = "まだまだだね！\nいろんな魔法を駆使して\n魔法を男子に当てよう！";
                break;
            case 2:
                text.text = "いい感じに可愛くできたね！\nもっと魔法を当てると…";
                break;
            case 3:
                text.text = "完全に男子を可愛くできた！\nやったね！";
                break;
        }
        var txt = text.ToString().Replace("<br>", "\n");
    }

    private void ChangeSprite()
    {
        sdshabon.sprite = SD[charaSele.CharactorSelectNum];
    }


    public void SceneLoad()
    {
        SoundManager.Instance.StopCriAtomBGM("famipop4");
        FadeManager.Instance.LoadScene(7, 1.0f);
    }
}