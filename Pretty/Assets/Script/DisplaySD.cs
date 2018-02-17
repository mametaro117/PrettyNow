using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplaySD : MonoBehaviour {

    [SerializeField]
    private GameObject SD_prefab;
    [SerializeField]
    private bool isFuwafuwa;
    [SerializeField]
    private Transform Parent;

    // どのキャラを選んだか
    private int charaNum;

    [SerializeField]
    private Sprite[] SDSprites = new Sprite[12];

	void Start () {
        // タグで取得
        charaNum = GameObject.FindGameObjectWithTag("CharaNum").GetComponent<CharaSelect>().CharactorSelectNum;
    }
	
    public void DisplaySprite(Collision2D col, int num)
    {
        // Positionを取得
        Vector3 vec3 = col.transform.position;
        // そのPositionにインスタンス生成
        GameObject obj = Instantiate(SD_prefab, vec3, Quaternion.identity, Parent) as GameObject;
        // SDの画像を表示する
        obj.GetComponent<Image>().sprite = SDSprites[(charaNum * 4) + num];

        // isFuwafuwaがTrueだったら
        if (isFuwafuwa)
        {
            // その場に表示するだけじゃなくふわふわさせる
            obj.AddComponent<FuwaFuwa2>();
            Destroy(obj, 5);
            return;
        }        
        // 表示したら削除する
        Destroy(obj, 1.0f);
    }
}
