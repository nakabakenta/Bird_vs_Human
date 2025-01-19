using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerSelect : MonoBehaviour
{
    //このオブジェクトのコンポーネント
    public GameObject[] meshPlayer = new GameObject[3];//メッシュプレイヤー
    public GameObject UIPlayerSelect;                  //UIプレイヤーセレクト

    // Start is called before the first frame update
    void Start()
    {
        GameManager.status = "Menu";

        meshPlayer[0].SetActive(false);
        meshPlayer[1].SetActive(false);
        meshPlayer[2].SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerSelectButton.buttonClick == false)
        {
            for (int i = 0; i < 3; i++)
            {
                if (i == GameManager.playerNumber)
                {
                    meshPlayer[i].SetActive(true);
                }
                else
                {
                    meshPlayer[i].SetActive(false);
                }
            }
        }
        else if (PlayerSelectButton.buttonClick == true)
        {
            UIPlayerSelect.SetActive(false);
        }
    }
}
