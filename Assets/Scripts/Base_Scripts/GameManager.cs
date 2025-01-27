using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //プレイヤー番号
    public static int selectPlayer;
    //ゲームシーン
    public static bool playBegin;//状態
    public static int score;             //スコア
    public static string nowScene; 
    public static string nextScene;

    //共通
    public static bool[] stageClear = new bool[5] { false, false, false, false, false };//ステージクリアフラグ
}

//バグ
//ボスの動き
