using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class TittleManager : MonoBehaviour {

    /// <summary>
    /// ロゴが次に光るまでの待つ時間
    /// </summary>
    public float waitKira = 3.0f;

    private float time = 0.0f;
    [SerializeField]
    private Button Nextbutton;

    [SerializeField]
    private GameObject Kira;
    private Animation _anim;

	// Use this for initialization
	void Start () {
        SoundManager.Instance.PlayCriAtomBGM("コミックパニック");
        _anim = Kira.GetComponent<Animation>();
        //UniRx 一回だけクリック
        Nextbutton.OnClickAsObservable()
            .First()
            .Subscribe(_ => LoadScene());
    }
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        if(time >= waitKira)
        {
            _anim.Play();
            time = 0.0f;
        }
	}

    /// <summary>
    ///画面タップした時にランダムに音を出す処理
    /// </summary>
    private void randVoice()
    {
        string voiceName = string.Empty;
        int rand = Random.Range(0, 6);
        switch (rand)
        {
            case 0:
                voiceName = "Soushi_1";
                break;
            case 1:
                voiceName = "Van_15";
                break;
            case 2:
                voiceName = "Nobu_09";
                break;
            case 3:
                voiceName = "Amelia_prettynow";
                break;
            case 4:
                voiceName = "Ester_prettynow_E";
                break;
            case 5:
                voiceName = "Libre_prettynow";
                break;
        }
        Debug.Log("ランダムint: " + rand);
        SoundManager.Instance.PlayCriAtomVoice(voiceName);
    }

    /// <summary>
    /// シーン移動する処理
    /// </summary>
    private void LoadScene()
    {
        randVoice();
        SoundManager.Instance.StopCriAtomBGM("コミックパニック");
        FadeManager.Instance.LoadScene(1,1.0f);
    }
}
