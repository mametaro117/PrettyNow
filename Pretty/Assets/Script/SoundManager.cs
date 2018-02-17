using UnityEngine;
using System;
using System.Collections;


// 音管理クラス
public class SoundManager : MonoBehaviour
{

    protected static SoundManager instance;
    public CriAtomSource Sound_Voice;
    public CriAtomSource Sound_BGM;
    public CriAtomSource Sound_SE;
    public bool _end = false;

    CriAtomExPlayer player;
    //ボイスが終わったかどうか

    public static SoundManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (SoundManager)FindObjectOfType(typeof(SoundManager));

                if (instance == null)
                {
                    Debug.LogError("SoundManager Instance Error");
                }
            }
            return instance;
        }
    }

    void Awake()
    {
        
        GameObject[] obj = GameObject.FindGameObjectsWithTag("SoundManager");
        if (obj.Length > 1)
        {
            // 既に存在しているなら削除
            Destroy(gameObject);
        }
        else
        {
            // 音管理はシーン遷移では破棄させない
            DontDestroyOnLoad(gameObject);
        }
        Sound_Voice = gameObject.AddComponent<CriAtomSource>();
        Sound_BGM = gameObject.AddComponent<CriAtomSource>();
        Sound_SE = gameObject.AddComponent<CriAtomSource>();
    }

    void Start()
    {
        Sound_BGM.cueSheet = "BGM";
        Sound_BGM.cueName = "コミックパニック";
        Sound_SE.cueSheet = "SE";
        Sound_SE.cueName = "button70";
        _end = false;
    }

    void Update()
    {
        //検証用--------------------------------
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Sound_Voice.cueSheet = "Amelia";
            Sound_Voice.cueName = "Amelia_2";
            Sound_Voice.Play("Amelia_2");
            Sound_BGM.Play();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            Sound_Voice.cueSheet = "Soushi";
            Sound_Voice.cueName = "Soushi_5";
            Sound_Voice.Play();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            Sound_BGM.cueSheet = "BGM";
            Sound_BGM.cueName = "famipop4";
            Sound_BGM.Play();
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            Sound_SE.cueSheet = "SE";
            Sound_SE.cueName = "button70";
            Sound_SE.Play();
        }
        //---------------------------------------
    }

    /// <summary>
    /// ボイス再生
    /// 頭文字によって分ける
    /// </summary>
    /// <param name="name">鳴らしてほしい音</param>
    public void PlayCriAtomVoice(string name)
    {
        
        if (name.Substring(0, 1) == "S" || name.Substring(0, 1) == "m")
        {
            Sound_Voice.cueSheet = "Soushi";
        }
        else if (name.Substring(0, 1) == "E")
        {
            Sound_Voice.cueSheet = "Ester";
        }
        else if (name.Substring(0, 1) == "A")
        {
            Sound_Voice.cueSheet = "Amelia";
        }
        else if (name.Substring(0, 1) == "L")
        {
            Sound_Voice.cueSheet = "Libre";
        }
        else if (name.Substring(0, 1) == "N")
        {
            Sound_Voice.cueSheet = "Nobu";
        }
        else if (name.Substring(0, 1) == "V")
        {
            Sound_Voice.cueSheet = "Van";
        }
        
        Sound_Voice.cueName = name;
        Sound_Voice.Play(name);
        CriAtomSource.Status status = Sound_Voice.status;
        if (status == CriAtomSource.Status.PlayEnd)
        {
            _end = true;
            Debug.Log("ボイス止まった");
        }
        else
            _end = false;
    }

    /// <summary>
    /// ボイス停止
    /// </summary>
    /// <param name="name">止めるボイス</param>
    public void StopCriAtomVoice()
    {
        Sound_Voice.Stop();
    }

    /// <summary>
    /// BGM再生
    /// </summary>
    /// <param name="name"></param>
    public void PlayCriAtomBGM(string name)
    {
        Sound_BGM.cueSheet = "BGM";
        Sound_BGM.cueName = name;
        Sound_BGM.Play(name);
    }
    /// <summary>
    /// BGM停止
    /// </summary>
    /// <param name="name"></param>
    public void StopCriAtomBGM(string name)
    {
        Sound_BGM.Stop();
    }

    /// <summary>
    /// SE再生
    /// </summary>
    /// <param name="name"></param>
    public void PlayCriAtomSE(string name)
    {
        Sound_SE.cueSheet = "SE";
        Sound_SE.cueName = name;
        Sound_SE.Play(name);
    }
    /// <summary>
    /// SE停止
    /// </summary>
    /// <param name="name"></param>
    public void StopCriAtomSE(string name)
    {
        Sound_SE.Stop();
    }
}