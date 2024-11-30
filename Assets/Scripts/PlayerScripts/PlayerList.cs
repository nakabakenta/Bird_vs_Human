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
        { 6, 8, 5, 10 };
        //攻撃力
        public static int[] power = new int[] 
        { 7, 10, 5, 10 };
        //移動速度
        public static float[] speed = new float[] 
        { 8.0f, 6.0f, 10.0f, 10.0f };
        //攻撃間隔
        public static float[,] attackInterval = new float[2, 4]
        { 
            { 0.5f, 0.5f, 0.5f, 0.5f },//前方向
            { 1.0f, 1.0f, 1.0f, 1.0f } //下方向
        };
    }

    public static class Invincible
    {
        public static float[] attackInterval = new float[2] { 0.25f, 0.25f };
    }
}
