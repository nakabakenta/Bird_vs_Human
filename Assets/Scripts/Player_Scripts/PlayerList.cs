using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerList : MonoBehaviour
{
    //プレイヤーリスト
    public static class Player
    {
        //スズメ(0),カラス(1),コガラ(2),ペンギン(3)
        //番号
        public static int[] number = new int[] 
        { 0, 1, 2, 3 };
        //体力
        public static int[] hp = new int[] 
        { 5, 5, 5, 5 };
        //攻撃力
        public static int[] power = new int[]
        { 3, 5, 1, 5 };
        //攻撃間隔
        public static float[,] attackInterval = new float[2, 4]
        { 
            { 1.0f, 2.0f, 0.5f, 0.5f },//前方向
            { 2.5f, 3.5f, 2.0f, 0.5f } //下方向
        };
        //最大経験値
        public static int[] maxExp = new int[]
        { 100, 100, 100, 100 };
    }

    public static class Invincible
    {
        public static int power = 6;
        public static float[] attackInterval = new float[2] { 0.25f, 0.25f };
    }
}
