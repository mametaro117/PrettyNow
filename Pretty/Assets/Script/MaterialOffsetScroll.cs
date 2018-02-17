using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Offsetでテクスチャをループさせるスクロールする
/// </summary>
public class MaterialOffsetScroll : MonoBehaviour {

    #region 変数

    /// <summary>
    /// マテリアルレンダラー
    /// </summary>
    private Renderer render;

    /// <summary>
    /// 反転
    /// </summary>
    public bool inverted = false;

    /// <summary>
    /// スピード
    /// </summary>
    public float speed = 0;

    #endregion

    /// <summary>
    /// Start() https://docs.unity3d.com/jp/540/ScriptReference/MonoBehaviour.Start.html
    /// </summary>
    void Start () {
        render = this.GetComponent<Renderer>();
	}

    /// <summary>
    /// Updata ()
    /// https://docs.unity3d.com/ja/540/Manual/ExecutionOrder.html
    /// </summary>
    void Update () {
        //反転するために一旦ローカル変数に保存
        float time = Time.time * speed;
        if (inverted) time *= -1;

        //Mathf https://docs.unity3d.com/jp/540/ScriptReference/Mathf.Repeat.html
        float scroll = Mathf.Repeat(time, 1);
        Vector2 offset = new Vector2(scroll, 0);

        //SetTextureOffset　でoffsetに代入
        render.sharedMaterial.SetTextureOffset("_MainTex", offset);


// 私のスピードに
//    -= ∧＿∧ 
// -= と(´･ω･`)ｼｭﾀｯ
//   -=  / と_ノ
// -=  _/／⌒ｿ

//   ついてこれるかな？
//  ∧＿∧ = -
// (´･ω･`)`つ = -ｻﾞｻﾞｯ
// 　`つ  \ = -
//　 \,⌒＼\,,,_ = -

//  .　　ﾟ｡
//　ｺﾎﾟ ﾟ
//      ﾟ｡ﾟ
//　　　　｡ﾟ
//        ﾟ｡ﾟｺﾎﾟ
//　　　　｡ﾟ
//_(┐「ε:)_ ここどこ？
//
//　 ＿人人人人人＿
//　 ＞   海底  ＜
//　 ￣Y^Y^Y^Y^Y￣


    }
}
