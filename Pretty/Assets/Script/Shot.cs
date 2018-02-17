using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shot : MonoBehaviour {


    [SerializeField]
    private GameObject Obj;
    [SerializeField]
    private GameObject From, To;
    [SerializeField]
    private GameObject Meter;

    [SerializeField]
    private Transform Parent;

    [SerializeField]
    private int MaxBullet = 3;
    private int shotCnt = 0;

    private bool ShotEnable = false;

    [SerializeField]
    private Sprite[] BulletSprites = new Sprite[3];

    private Image img;
    private float Distance;

    private CharaSelect charaSele;


    // Use this for initialization
    void Start()
    {
        ShotEnable = false;
        charaSele = GameObject.FindGameObjectWithTag("CharaNum").GetComponent<CharaSelect>();
        img = Meter.GetComponent<Image>();
        Obj.GetComponent<Image>().sprite = BulletSprites[charaSele.CharactorSelectNum];
    }

    // Update is called once per frame
    void Update()
    {
        //  カーソルの方向に向ける
        var pos = Camera.main.WorldToScreenPoint(transform.parent.position);
        var rotation = Quaternion.LookRotation(Vector3.forward, Input.mousePosition - pos);
        transform.parent.rotation = rotation;
        //  マウスクリックしたPosを取得
        if (Input.GetMouseButtonDown(0) && ShotEnable)
        {
            if (shotCnt >= MaxBullet)
            {
                return;
            }
            BulletShot();
        }
    }

    void BulletShot()
    {
        shotCnt++;

        GameObject obj = Instantiate(Obj, this.transform.position, Quaternion.identity, Parent);
        // 速度の設定
        Vector3 force;
        Distance = img.fillAmount * 7.5f;
        Distance = Mathf.Max(Distance, 1.5f);
        force = gameObject.transform.up * Distance  * 200;
        // Rigidbodyに力を加えて発射
        obj.GetComponent<Rigidbody2D>().AddForce(force);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("aaa");
        Destroy(collision.gameObject);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        Debug.Log("bbb");
    }

    public void BulletDelete()
    {
        Debug.Log("shotCnt : " + shotCnt);
        shotCnt--;
    }

    public void ShotSetActive(bool b)
    {
        ShotEnable = b;
    }
}
