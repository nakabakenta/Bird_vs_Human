using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //プレイヤーセレクトシーン
    public static string playerSelect;   //プレイヤー(自機)選択
    //ゲームシーン
    public static bool gameStart = false;//ゲームスタートフラグ
    public static int score;             //スコア
    public static int remain;            //残機

    public static bool stage1;//
    public static bool stage2;//
    public static bool stage3;//
    public static bool stage4;//
    public static bool stage5;//

    public static bool secretCharacter;//
}
