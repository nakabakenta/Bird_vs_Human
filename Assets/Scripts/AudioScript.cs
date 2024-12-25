using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.nowScene == "PlayerSelect" || GameManager.nowScene == "StageSelect")
        {
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this.gameObject);//このオブジェクトを消す
        }
    }
}
