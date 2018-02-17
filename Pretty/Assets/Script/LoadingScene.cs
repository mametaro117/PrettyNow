using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour {

    private int charaSeleNum;

    [SerializeField]
    private Text loadingText;
    [SerializeField]
    private Image loadingBar;

    private bool isReady = false;

	[SerializeField]
	private GameObject ReadyText;
    private GameObject parent;

    [SerializeField]
    private Image TipsImage;
    [SerializeField]
    private Sprite[] Tips = new Sprite[3];
    private Sprite DefaultTips;

    // Use this for initialization
    void Start () {
        isReady = false;

        // キャラの選択番号を取得
        try
        {
            charaSeleNum = GameObject.FindGameObjectWithTag("CharaNum").GetComponent<CharaSelect>().CharactorSelectNum;
        }
        catch (Exception e)
        {
            charaSeleNum = 0;
            Debug.Log("<color=red>" + e.Message + "</color>");
        }

        DefaultTips = TipsImage.GetComponent<Image>().sprite;
        StartCoroutine("LoadScene");
        parent = loadingText.transform.parent.gameObject;
    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Enter押した");
            StopCoroutine("TipsChenge");
            isReady = true;
        }
	}

    public IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(1);
        // 非同期でロードする
        AsyncOperation async = SceneManager.LoadSceneAsync("Main");
        // シーン遷移をさせない
        async.allowSceneActivation = false;
        while (async.progress < 0.9f)
        {
            loadingBar.fillAmount = async.progress;
            Debug.Log(async.progress);
            loadingText.text = "...." + loadingBar.fillAmount.ToString("F0") + "%";
            yield return null;
        }
        Debug.Log("Scene Loaded");
        StartCoroutine("TipsChenge");
        yield return null;
        iTween.ValueTo(loadingBar.gameObject, iTween.Hash("from", 0, "to", 1, "time", 2.0f, "easetype", iTween.EaseType.easeOutCubic, "onupdate", "OnUpdateBer", "onupdatetarget", gameObject));
        iTween.ValueTo(loadingText.gameObject, iTween.Hash("from", 0, "to", 100, "time", 2.0f, "easetype", iTween.EaseType.easeOutCubic, "onupdate", "OnUpdateText", "onupdatetarget", gameObject));
        yield return null;
        yield return new WaitForSeconds(2.3f);
        parent.SetActive(false);
		yield return new WaitForSeconds(0.5f);
		ReadyText.SetActive(true);

		while (isReady == false) yield return null;
        // シーン遷移許可
        async.allowSceneActivation = true;
        yield break;
    }

    IEnumerator TipsChenge()
    {
        while (true)
        {
            yield return new WaitForSeconds(2);
            TipsImage.sprite = Tips[charaSeleNum];
            Debug.Log("Tipchenge1");
            yield return new WaitForSeconds(2);
            TipsImage.sprite = DefaultTips;
            Debug.Log("Tipchenge2");
        }
    }

    void OnUpdateBer(float value)
    {
        loadingBar.fillAmount = value;
    }

    void OnUpdateText(int value)
    {
        loadingText.GetComponent<Text>().text = value.ToString() + "%";
    }
}
