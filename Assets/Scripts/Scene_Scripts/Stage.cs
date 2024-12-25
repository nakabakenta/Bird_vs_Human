using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    //処理
    public static int nowStage;                  //現在のステージ
    public static bool[] bossEnemy = new bool[5];//ボスの生存可否
    public static string gameStatus;             //ゲームの状態

//このオブジェクトのコンポーネント
private SceneLoader sceneLoader;//"Script(SceneLoader)"

    // Start is called before the first frame update
    void Start()
    {
        sceneLoader = this.GetComponent<SceneLoader>();//この"Script(SceneLoader)"を取得する
        bossEnemy = new bool[5]                        //ボスの存在可否を"false(リセット)"する
        { 
            false, false, false, false, false 
        };
        bossEnemy[nowStage - 1] = true;                //現在のステージのボスを"true(生存)"にする
        gameStatus = "Play";                           //ゲームの状態を"Play"にする
    }

    // Update is called once per frame
    void Update()
    {
        if(gameStatus == "Play")
        {
            Time.timeScale = 1;
        }
        else if(gameStatus == "Pause")
        {
            Time.timeScale = 0;
        }

        if(PlayerController.remain <= 0 && PlayerController.hp <= 0)
        {
            sceneLoader.GameOver();
        }

        if(bossEnemy[nowStage - 1] == false)
        {
            gameStatus = "Clear";
        }
    }
}
