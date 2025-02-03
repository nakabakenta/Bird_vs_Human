using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyList : MonoBehaviour
{
    //敵(歩く)
    public static class WalkEnemy
    {
        //ステータス
        public static int hp = 2;        //体力
        public static float speed = 2.0f;//移動速度
    }
    //敵(走る)
    public static class RunEnemy
    {
        //ステータス
        public static int hp = 5;        //体力
        public static float speed = 4.0f;//移動速度
        public static float jump = 10.0f;//ジャンプ力

        //プレイヤーの認識範囲
        public static Vector3 range =
            new Vector3(0.25f, 2.0f, 0.0f);
    }
}