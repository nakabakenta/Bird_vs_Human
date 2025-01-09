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
        
        
    }

    // カーソルがボタンに重なったときに実行されるイベント
    public void OnPointerEnter(PointerEventData eventData)
    {
        
        Debug.Log("ボタンにカーソルが重なりました");
    }

    //プレイヤーセレクト一覧
    //スズメ
    public void Sparrow()
    {
        GameManager.playerNumber = PlayerList.Player.number[0];//"GameManager"の"playerNumber"を"スズメ(0)"にする
        
    }
    //カラス
    public void Crow()
    {
        GameManager.playerNumber = PlayerList.Player.number[1];//"GameManager"の"playerNumber"を"カラス(1)"にする
        
    }
    //コガラ
    public void Chickadee()
    {
        GameManager.playerNumber = PlayerList.Player.number[2];//"GameManager"の"playerNumber"を"コガラ(2)"にする
        
    }
    //ペンギン
    public void Penguin()
    {
        GameManager.playerNumber = PlayerList.Player.number[3];//"GameManager"の"playerNumber"を"ペンギン(3)"にする
        
    }
}
