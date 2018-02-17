using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour {

	bool One;

	void Awake(){
		Debug.Log ("音出てる？");
	}
	void Start()
	{
		One = true;
	}

    private AsyncOperation async;

    IEnumerator Fade()
    {
        // 非同期でロード開始
        async = SceneManager.LoadSceneAsync("CharacterChoose");
        // デフォルトはtrue。ロード完了したら勝手にシーンきりかえ発生しないよう設定。
        async.allowSceneActivation = false;

        // 非同期読み込み中の処理
        while (!async.isDone)
        {
            //Debug.Log(async.progress * 100 + "%");

            yield return new WaitForSeconds(0);
            
        }
        yield return async;
    }



    // Update is called once per frame
    void Update()
    {
		

    }

    public void TapStartButton()
    {
		if(One){
			//SoundManager.Instance.PlayVoice(0);
			// タッチしたら遷移する（検証用）
			FadeManager.Instance.LoadScene(1, 0.3f);
			//async.allowSceneActivation = true;
			//SoundManager.Instance.StopBGM();
			One = false;
			}
    }
}