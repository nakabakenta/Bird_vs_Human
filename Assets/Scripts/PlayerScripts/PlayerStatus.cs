using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    //スズメ
    public static class Sparrow
    {
        public static string name = "Sparrow";//名前
        public static int hp = 6;             //体力
        public static int power = 7;          //攻撃力
        public static int speed = 8;          //移動速度
    }
    //カラス
    public static class Crow
    {
        public static string name = "Crow";//名前
        public static int hp = 8;          //体力
        public static int power = 10;      //攻撃力
        public static int speed = 6;       //移動速度
    }
    //コガラ
    public static class Chickadee
    {
        public static string name = "Chickadee";//名前
        public static int hp = 6;               //体力
        public static int power = 5;            //攻撃力
        public static int speed = 10;           //移動速度
    }
    //ペンギン
    public static class Penguin
    {
        public static string name = "Penguin";//名前
        public static int hp = 10;            //体力
        public static int power = 10;         //攻撃力
        public static int speed = 10;         //移動速度
    }
}
