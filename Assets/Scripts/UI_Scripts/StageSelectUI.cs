using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelectUI : MonoBehaviour
{
    //このオブジェクトのコンポーネント
    public GameObject stageSelect;//ステージセレクト

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (StageSelectButton.buttonSelect == true) 
        {
            stageSelect.SetActive(false);
        }
    }
}
