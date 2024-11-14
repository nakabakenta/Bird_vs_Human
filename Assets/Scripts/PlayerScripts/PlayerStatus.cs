using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    //プレイヤーステータスリスト
    public static class PlayerStatusList
    {
        //スズメ,カラス,コガラ,ペンギン
        public static int[] number = new int[] { 0, 1, 2, 3 };     //番号
        public static int[] hp = new int[] { 6, 8, 5, 10 };        //体力
        public static int[] power = new int[] { 7, 10, 5, 10 };    //攻撃力
        public static float[] speed = new float[] { 8, 6, 10, 10 };//移動速度
    }
}
