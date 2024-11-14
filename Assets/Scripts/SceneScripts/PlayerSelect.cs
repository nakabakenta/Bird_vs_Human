using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelect : MonoBehaviour
{
    private SceneLoader sceneLoader;//SceneLoader

    // Start is called before the first frame update
    void Start()
    {
        sceneLoader = this.GetComponent<SceneLoader>();//このオブジェクトのScript"SceneLoader"を取得する
    }

    //スズメ
    public void Sparrow()
    {
        GameManager.playerNumber = PlayerStatus.PlayerStatusList.number[0];//プレイヤー番号を"スズメ"の番号にする
        sceneLoader.StageSelect();                                         //"sceneLoader"の関数"StageSelect"を呼び出す
    }
    //カラス
    public void Crow()
    {
        GameManager.playerNumber = PlayerStatus.PlayerStatusList.number[1];
        sceneLoader.StageSelect();
    }
    //コガラ
    public void Chickadee()
    {
        GameManager.playerNumber = PlayerStatus.PlayerStatusList.number[2];
        sceneLoader.StageSelect();
    }
    //ペンギン
    public void Penguin()
    {
        GameManager.playerNumber = PlayerStatus.PlayerStatusList.number[3];
        sceneLoader.StageSelect();
    }
}
