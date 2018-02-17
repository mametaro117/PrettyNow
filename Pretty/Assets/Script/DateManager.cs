using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DateManager : MonoBehaviour {        

    private static DateManager instance;
    public static DateManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (DateManager)FindObjectOfType(typeof(DateManager));
                if (instance == null)
                {
                    Debug.LogError(typeof(DateManager) + "is nothing");
                }
            }
            return instance;
        }
    }

    public static int CharactorNum;     // どのキャラで進行するか保存するため

    private int num = 100;              //  初期値

    public void Awake()
    {
        Initialize();
        if (this != Instance)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Initialize()
    {
        CharactorNum = num;
    }
}
