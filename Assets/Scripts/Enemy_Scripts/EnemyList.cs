using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyList : MonoBehaviour
{
    //共通
    public static float rotation = 90.0f;//進む方向

    //敵(歩く)
    public static class WalkEnemy
    {
        //ステータス
        public static int hp = 2;        //体力
        public static int power = 1;     //攻撃力
        public static float speed = 2.0f;//移動速度
        public static int score = 100;   //倒した時のスコア
        public static int exp = 10;      //倒した時の経験値
    }
    //敵(走る)
    public static class RunEnemy
    {
        //ステータス
        public static int hp = 5;        //体力
        public static int power = 1;     //攻撃力
        public static float speed = 4.0f;//移動速度
        public static float jump = 10.0f;//ジャンプ力
        public static int score = 150;   //倒した時のスコア
        public static int exp = 10;      //倒した時の経験値

        //プレイヤーの認識範囲
        public static Vector3 range =
            new Vector3(0.25f, 2.0f, 0.0f);
    }
    //敵(しゃがむ)
    public static class CrouchEnemy
    {
        //ステータス
        public static int hp = 10;    //体力
        public static int power = 1;  //攻撃力
        public static int score = 300;//倒した時のスコア
        public static int exp = 10;   //倒した時の経験値
    }
    //敵(車)
    public static class CarEnemy
    {
        //ステータス
        public static int hp = 20;       //体力
        public static int power = 2;     //攻撃力
        public static float speed = 8.0f;//移動速度
        public static int score = 1000;  //倒した時のスコア
        public static int exp = 10;      //倒した時の経験値

        //プレイヤーの認識範囲
        public static Vector3 range =
            new Vector3(5.0f, 0.0f, 0.0f);
    }

    //敵(車乗車)
    public static class CarRideEnemy
    {
        //ステータス
        public static int hp = 5;        //体力
        public static int power = 1;     //攻撃力
        public static float speed = 4.0f;//移動速度
        public static float jump = 10.0f;//ジャンプ力
        public static int score = 150;   //倒した時のスコア
        public static int exp = 10;      //倒した時の経験値

        //プレイヤーの認識範囲
        public static Vector3 range =
            new Vector3(0.25f, 2.0f, 0.0f);
    }

    //敵(銃持ち)
    public static class HaveGunEnemy
    {
        //ステータス
        public static int hp = 5;               //体力
        public static int power = 1;            //攻撃力
        public static float speed = 2.0f;       //移動速度
        public static float bulletSpeed = 15.0f;//弾の速度
        public static int score = 200;          //倒した時のスコア
        public static int exp = 10;             //倒した時の経験値
    }

    //敵(戦闘機)
    public static class FighterJetEnemy
    {
        //ステータス
        public static int hp = 5;               //体力
        public static int power = 1;            //攻撃力
        public static float speed = 10.0f;      //移動速度
        public static float bulletSpeed = 20.0f;//弾の速度
        public static int score = 1000;         //倒した時のスコア
        public static int exp = 10;             //倒した時の経験値
    }

    //ボス(ステージ1～5)
    public static class BossEnemy
    {
        //ステータス
        public static int[] hp = new int[5] { 20, 30, 35, 40, 20 };                     //体力
        public static int[] power = new int[5] { 1, 1, 1, 1, 1 };                       //攻撃力
        public static float[] speed = new float[5] { 2.0f, 2.0f, 2.0f, 2.0f, 15.0f };   //移動速度
        public static float[] jump = new float[5] { 10.0f, 10.0f, 10.0f, 10.0f, 10.0f };//ジャンプ力
        public static int[] score = new int[5] { 1000, 2000, 3000, 4000, 5000 };        //倒した時のスコア

        //プレイヤーの認識範囲
        public static Vector3[] range = new Vector3[5]
        {
            new Vector3 (0.25f, 2.0f, 0.0f),
            new Vector3 (0.25f, 2.0f, 0.0f),
            new Vector3 (0.25f, 2.0f, 0.0f),
            new Vector3 (0.25f, 2.0f, 0.0f),
            new Vector3 (0.25f, 2.0f, 0.0f)
        };
    }

    //ヒューマノイドアニメーション
    public static class HumanoidAnimation
    {
        public static int walk         = 0;
        public static int run          = 1;
        public static int mutantRun    = 2;
        public static int haveGunIdle  = 3;
        public static int haveGunWalk  = 4;
        public static int punch        = 10;
        public static int kick         = 11;
        public static int jumpAttack   = 12;
        public static int gunPlay      = 13;
        public static int jump         = 20; 
        public static int crouch       = 21;
        public static int exitCar      = 22;
        public static int battlecry    = 23;
        public static int reload       = 24;
        public static int dance        = 30;
        public static int damage       = 31;
        public static int death        = 32;
    }
}
