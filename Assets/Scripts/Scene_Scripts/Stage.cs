using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : UIBase
{
    //処理
    public static int nowStage;                  //現在のステージ
    public static bool[] bossEnemy = new bool[5];//ボスの生存可否
    public static string status;                 //状態

    // Start is called before the first frame update
    void Start()
    {
        bossEnemy = new bool[5]                        //ボスの存在可否を"false(リセット)"する
        { 
            false, false, false, false, false 
        };
        bossEnemy[nowStage - 1] = true;                //現在のステージのボスを"true(生存)"にする
        status = "Play";                               //ゲームの状態を"Play"にする
    }

    // Update is called once per frame
    void Update()
    {
        if(status == "Play")
        {
            Time.timeScale = 1;
        }
        else if(status == "Pause")
        {
            Time.timeScale = 0;
        }

        if (PlayerController.remain <= 0)
        {
            GameManager.nowScene = "GameOver";
            LoadScene();
        }

        if(bossEnemy[nowStage - 1] == false)
        {
            status = "Clear";
        }
    }
}
