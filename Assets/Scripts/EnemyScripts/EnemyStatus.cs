using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    //敵(歩く)
    public static class WalkEnemy
    {
        //ステータス
        public static int hp = 1;   //体力
        public static int power = 1;//攻撃力
        public static int speed = 2;//移動速度
        //その他設定
        public static int score = 100;//倒した時のスコア
    }
    //敵(走る)
    public static class RunEnemy
    {
        //ステータス
        public static int hp = 2;   //体力
        public static int power = 1;//攻撃力
        public static int speed = 4;//移動速度
        //その他設定
        public static int score = 100;//倒した時のスコア
    }
}
