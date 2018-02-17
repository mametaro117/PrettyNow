using System.Collections;
using UnityEngine;

public class TalkButton : MonoBehaviour {

    [SerializeField]
    private GameObject MenuGroup;
    private Talk talkScript;
    private int textNum;

    //オートモードがONかOFFか
    private bool _autoActive = false;
    //Menuバーが押されたかどうか
    private bool _menuActive = false;
    
    // Use this for initialization
    void Start () {
        talkScript = GetComponent<Talk>();
	}
	
	// Update is called once per frame
	void Update () {
		if(textNum != talkScript.textCount)
        {
            textNum = talkScript.textCount;
        }
	}

    /// <summary>
    /// オート機能
    /// </summary>
    public void Auto()
    {
        _autoActive = !_autoActive;

        if (_autoActive)
            StartCoroutine(AutoOn());
        else
            StopCoroutine(AutoOn());
    }

    /// <summary>
    /// オートＯＮ中のコルーチン処理
    /// </summary>
    /// <returns></returns>
    private IEnumerator AutoOn()
    {
        while (true)
        {
            if (!_autoActive)
                break;
            CriAtomSource.Status status = SoundManager.Instance.Sound_Voice.status;
            if (textNum <= talkScript.talkText.Length - 1)
            {
                if (status == CriAtomSource.Status.PlayEnd || status == CriAtomSource.Status.Stop)
                {
                    yield return new WaitForSeconds(1.0f);
                    talkScript.NextWord();
                }
                yield return new WaitForSeconds(0.1f);
                Debug.Log("音待ち");
            }
            yield return new WaitForSeconds(0.1f);
            Debug.Log("テキスト待ち");
        }
    }

    /// <summary>
    /// MenuバーON
    /// </summary>
    public void ONMenu()
    {
        if (!_menuActive)
        {
            MenuGroup.SetActive(true);
            _menuActive = true;
        }
        else
        {
            MenuGroup.SetActive(false);
            _menuActive = false;
        }
    }
}
