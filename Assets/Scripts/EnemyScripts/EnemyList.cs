using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyList : MonoBehaviour
{
    //共通
    public static float direction = 90.0f;//進む方向

    //敵(歩く)
    public static class WalkEnemy
    {
        //ステータス
        public static int hp = 1;        //体力
        public static int power = 1;     //攻撃力
        public static float speed = 2.0f;//移動速度
        public static float jump = 3.0f; //ジャンプ力
        public static int score = 100;   //倒した時のスコア
    }
    //敵(走る)
    public static class RunEnemy
    {
        //ステータス
        public static int hp = 2;          //体力
        public static int power = 1;       //攻撃力
        public static float speed = 4.0f;  //移動速度
        public static float jump = 4.0f;   //ジャンプ力
        public static float rangeX = 0.25f;//プレイヤーの認識範囲.X
        public static float rangeY = 2.0f; //プレイヤーの認識範囲.Y
        public static int score = 100;     //倒した時のスコア
    }
    //敵(しゃがむ)
    public static class CrouchEnemy
    {
        //ステータス
        public static int hp = 5;     //体力
        public static int power = 1;  //攻撃力
        public static int score = 100;//倒した時のスコア
    }
    //敵(車)
    public static class CarEnemy
    {
        //ステータス
        public static int hp = 10;        //体力
        public static int power = 2;      //攻撃力
        public static float speed = 30.0f;//移動速度
        public static float rangeX = 3.0f;//プレイヤーの認識範囲.X
        public static int score = 500;    //倒した時のスコア
    }
    //敵(車乗車)
    public static class CarRideEnemy
    {
        //ステータス
        public static int hp = 2;          //体力
        public static int power = 1;       //攻撃力
        public static float speed = 4.0f;  //移動速度
        public static float jump = 4.0f;   //ジャンプ力
        public static float rangeX = 0.25f;//プレイヤーの認識範囲.X
        public static float rangeY = 2.0f; //プレイヤーの認識範囲.Y
        public static int score = 100;     //倒した時のスコア
    }

    public static class FighterJetEnemy
    {
        //ステータス
        public static int hp = 5;         //体力
        public static int power = 1;      //攻撃力
        public static float speed = 15.0f;//移動速度
        public static int score = 100;    //倒した時のスコア
    }

    //ボス(ステージ1～5)
    public static class BossEnemy
    {
        //ステータス
        public static int[] hp = new int[5] { 10, 10, 10, 10, 10 };                 //体力
        public static int[] power = new int[5] { 1, 1, 1, 1, 1 };                   //攻撃力
        public static float[] speed = new float[5] { 2.0f, 1.0f, 1.0f, 1.0f, 1.0f };//移動速度
        public static float[] jump = new float[5] { 4.0f, 1.0f, 1.0f, 1.0f, 1.0f }; //ジャンプ力
        public static int[] score = new int[5] { 1000, 1000, 1000, 1000, 1000 };    //倒した時のスコア
    }

    public static class HumanoidAnimation
    {
        public static int walk    = 0;
        public static int run     = 1;
        public static int punch   = 10;
        public static int kick    = 11;
        public static int jump    = 20; 
        public static int crouch  = 21;
        public static int carExit = 22;
        public static int dance   = 30;
        public static int death   = 31;
    }
}
