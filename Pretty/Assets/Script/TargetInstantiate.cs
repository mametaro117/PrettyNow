using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TargetInstantiate : MonoBehaviour {


    [SerializeField]
    private float NextTime;
    [SerializeField]
    private float TargetSize = 1;
    [SerializeField]
    private GameObject[] TargetPrefabs;

    private CharaSelect charaSele;
    
	void Start () {
        charaSele = GameObject.FindGameObjectWithTag("CharaNum").GetComponent<CharaSelect>();
        PrefabChange();
    }
	
	// Update is called once per frame
	void Update () {

	}

    public IEnumerator Pop()
    {
        //  TargetGroupのtransformを取得
        var parent = this.transform;

        while (true)
        {
            //GameObject obj1 = Instantiate(TargetPrefabs[Random.Range(0,3)], HitCheck_R(), Quaternion.identity, parent) as GameObject;
            GameObject obj1 = Instantiate(TargetPrefabs[Random.Range(0, 7)], new Vector3(instancePosX(), -11, 0), Quaternion.identity, parent) as GameObject;
            iTween.ScaleTo(obj1, iTween.Hash("x", TargetSize, "y", TargetSize, "time", 3, "easeType", iTween.EaseType.easeOutBack));
            Destroy(obj1, 8);
            yield return new WaitForSeconds(NextTime);
        }
    }

    void PrefabChange()
    {
        switch (charaSele.CharactorSelectNum)
        {
            case 0:
                TargetPrefabs[5] = TargetPrefabs[4];
                TargetPrefabs[6] = TargetPrefabs[4];
                break;
            case 1:
                TargetPrefabs[4] = TargetPrefabs[5];
                TargetPrefabs[6] = TargetPrefabs[5];
                break;
            case 2:
                TargetPrefabs[4] = TargetPrefabs[6];
                TargetPrefabs[5] = TargetPrefabs[6];
                break;
        }
    }

    float instancePosX()
    {
        float x;
        do
        {
            x = Random.Range(-16.5f, 16.5f);
        }
        while (x >= -4.5 && x <= 4.5);      // xが-4.5以上4.5以下だったらやり直す

        return x;
    }
    
    private void Attention()
    {
        Debug.Log("<color=red>Error!!</color>");
        StopCoroutine("Pop");
    }

    public void FirstTrigger()
    {
        NextTime = 0.75f;
    }

    public void SecondTrigger()
    {
        NextTime = 1;
    }

    public void TargetDelete()
    {
        foreach (Transform transform in gameObject.transform)
        {
            // Transformからゲームオブジェクト取得・削除
            var go = transform.gameObject;
            Destroy(go);
        }
    }
}
