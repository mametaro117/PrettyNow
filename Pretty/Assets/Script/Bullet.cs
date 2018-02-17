using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{
    private DisplaySD DispSD;
    private Monitoringobject M_obj;
    private Shot shot;
    [SerializeField]
    private int Life = 2;

    void Start()
    {
        DispSD = GameObject.FindGameObjectWithTag("Manager").GetComponent<DisplaySD>();
        M_obj = GameObject.FindGameObjectWithTag("Manager").GetComponent<Monitoringobject>();
        shot = GameObject.FindGameObjectWithTag("ShotManager").GetComponent<Shot>();
    }

    void Update()
    {
        if (gameObject.transform.position.x < -18.8 || gameObject.transform.position.x > 18.8 || gameObject.transform.position.y < -11 || gameObject.transform.position.y > 11)
        {
            shot.BulletDelete();
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Bear")
        {
            DispSD.DisplaySprite(collision,0);
            shot.BulletDelete();
            M_obj.HitBear();
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        if (collision.transform.tag == "Candy")
        {
            DispSD.DisplaySprite(collision, 1);
            shot.BulletDelete();
            M_obj.HitCandy();
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        if (collision.transform.tag == "Lip")
        {
            DispSD.DisplaySprite(collision, 2);
            shot.BulletDelete();
            M_obj.HitLip();
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
		if (collision.transform.tag == "Heart")
		{
			DispSD.DisplaySprite(collision, 3);
			shot.BulletDelete();
			M_obj.HitLip();
			Destroy(collision.gameObject);
			Destroy(gameObject);
		}
		if (collision.transform.tag == "Boy")
		{
			Debug.Log("Tag : " + collision.transform.tag);		
			shot.BulletDelete();
			M_obj.HitBoyObj();
			Destroy(collision.gameObject);
			Destroy(gameObject);
		}

        if (collision.transform.tag == "SD")
        {
            Debug.Log("Tag : " + collision.transform.tag);
            shot.BulletDelete();
            M_obj.HitSD();
            collision.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            Destroy(gameObject);
        }

        if (collision.transform.tag == "Wall")
        {
            Life--;
            if (Life <= 0)
            {
                shot.BulletDelete();
                Destroy(gameObject, 0.1f);
            }
        }
    }
}