using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EffectManager : MonoBehaviour {

    protected static EffectManager instance;
    [SerializeField]
    private ParticleSystem TapEffect;
    private Vector2 mousePos;
    public Camera camera;
    

    public static EffectManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = (EffectManager)FindObjectOfType(typeof(EffectManager));
                if(instance == null)
                {
                    Debug.Log("EffectManager Instance Error");
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        GameObject[] obj = GameObject.FindGameObjectsWithTag("EffectManager");
        if(obj.Length > 1)
        {
            //すでに存在しているのなら削除
            Destroy(gameObject);
        }
        else
        {
            //ない場合はDontDestroy
            DontDestroyOnLoad(gameObject);
        }
    }

    // Use this for initialization
    void Start () {
        
	}
	
    /// <summary>
    /// タップしたらキラキラエフェクト
    /// </summary>
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            mousePos = camera.ScreenToWorldPoint(Input.mousePosition + camera.transform.forward * 10);
            TapEffect.transform.position = mousePos;
            TapEffect.Emit(1);
        }
        if(camera == null)
        {
            camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        }
	}
}
