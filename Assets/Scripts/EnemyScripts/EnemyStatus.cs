using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    //敵(共通)
    public static float rotationY = 90.0f;//Y軸回転固定

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
    //ボス(ステージ1～5)
    public static class BossEnemy
    {
        //ステータス
        public static int[] hp = new int[5] { 10, 10, 10, 10, 10 };//体力
        public static int[] power = new int[5] { 1, 1, 1, 1, 1 };  //攻撃力
        public static int[] speed = new int[5] { 2, 1, 1, 1, 1 };  //移動速度
        //その他設定
        public static int[] score = new int[5] { 1000, 1000, 1000, 1000, 1000 };//倒した時のスコア
    }
}
