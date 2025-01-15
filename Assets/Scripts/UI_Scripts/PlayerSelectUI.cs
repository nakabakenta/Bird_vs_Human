using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelectUI : MonoBehaviour
{
    //このオブジェクトのコンポーネント
    public GameObject playerSelect;//プレイヤーセレクト

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerSelectButton.buttonSelect == true)
        {
            playerSelect.SetActive(false);
        }
    }
}
