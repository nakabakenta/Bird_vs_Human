using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //プレイヤー番号
    public static int playerNumber;
    //ゲームシーン
    public static bool gameStart = false;//ゲームスタートフラグ
    public static int score;             //スコア
    public static string nowScene;

    //共通
    public static bool[] stageClear = new bool[5] { false, false, false, false, false };//ステージクリアフラグ
}
