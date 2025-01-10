using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerSelect : MonoBehaviour
{
    //このオブジェクトのコンポーネント
    public GameObject[] player = new GameObject[3];//"GameObject(プレイヤー)"

    // Start is called before the first frame update
    void Start()
    {
        GameManager.gameStart = false;

        player[0].SetActive(false);
        player[1].SetActive(false);
        player[2].SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerSelectButton.buttonSelect == false)
        {
            for (int i = 0; i < 3; i++)
            {
                if (i == GameManager.playerNumber)
                {
                    player[i].SetActive(true);
                }
                else
                {
                    player[i].SetActive(false);
                }
            }
        }
    }
}
