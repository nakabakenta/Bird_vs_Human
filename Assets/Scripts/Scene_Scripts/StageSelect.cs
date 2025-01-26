using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelect : MonoBehaviour
{
    //このオブジェクトのコンポーネント
    public GameObject stageSelectUI;//ステージセレクト

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (StageSelectButton.buttonClick == true) 
        {
            stageSelectUI.SetActive(false);
        }
    }
}
