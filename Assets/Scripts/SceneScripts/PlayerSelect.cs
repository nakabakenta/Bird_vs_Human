using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelect : MonoBehaviour
{
    //このオブジェクトのコンポーネント
    private SceneLoader sceneLoader;//"Script(SceneLoader)"

    // Start is called before the first frame update
    void Start()
    {
        sceneLoader = this.GetComponent<SceneLoader>();//この"Script(SceneLoader)"を取得する
        GameManager.gameStart = false;
    }
    //プレイヤーセレクト一覧
    //スズメ
    public void Sparrow()
    {
        GameManager.playerNumber = PlayerList.Player.number[0];//"GameManager"の"playerNumber"を"スズメ(0)"にする
        sceneLoader.StageSelect();                             //"Script(SceneLoader)"の"関数(StageSelect)"を実行する
    }
    //カラス
    public void Crow()
    {
        GameManager.playerNumber = PlayerList.Player.number[1];//"GameManager"の"playerNumber"を"カラス(1)"にする
        sceneLoader.StageSelect();                             //"Script(SceneLoader)"の"関数(StageSelect)"を実行する
    }
    //コガラ
    public void Chickadee()
    {
        GameManager.playerNumber = PlayerList.Player.number[2];//"GameManager"の"playerNumber"を"コガラ(2)"にする
        sceneLoader.StageSelect();                             //"Script(SceneLoader)"の"関数(StageSelect)"を実行する
    }
    //ペンギン
    public void Penguin()
    {
        GameManager.playerNumber = PlayerList.Player.number[3];//"GameManager"の"playerNumber"を"ペンギン(3)"にする
        sceneLoader.StageSelect();                             //"Script(SceneLoader)"の"関数(StageSelect)"を実行する
    }
}
