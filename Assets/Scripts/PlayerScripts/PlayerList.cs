using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerList : MonoBehaviour
{
    //�v���C���[���X�g
    public static class Player
    {
        //�X�Y��(0),�J���X(1),�R�K��(2),�y���M��(3)
        public static int[] number = new int[] { 0, 1, 2, 3 };     //�ԍ�
        public static int[] hp = new int[] { 6, 8, 5, 10 };        //�̗�
        public static int[] power = new int[] { 7, 10, 5, 10 };    //�U����
        public static float[] speed = new float[] { 8, 6, 10, 10 };//�ړ����x
    }
}
