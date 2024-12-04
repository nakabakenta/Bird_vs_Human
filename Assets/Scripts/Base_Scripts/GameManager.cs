using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //プレイヤーセレクトシーン
    public static int playerNumber;//プレイヤー番号
    //ゲームシーン
    public static bool gameStart = false;//ゲームスタートフラグ
    public static int score;             //スコア
    public static int remain;            //残機
    //共通
    public static bool[] stageClear = new bool[5] { false, false, false, false, false };//ステージクリアフラグ
    public static bool secretCharacter;//
}
