using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static string playerSelect;   //プレイヤー(自機)選択
    //ゲームプレイ中
    public static bool gameStart = false;//ゲームスタートフラグ

    public static int score;             //
    public static int remain;            //
    

    public static bool stage1;//
    public static bool stage2;//
    public static bool stage3;//
    public static bool stage4;//
    public static bool stage5;//

    public static bool secretCharacter;//
}
