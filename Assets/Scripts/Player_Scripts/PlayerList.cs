using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerList : MonoBehaviour
{
    //�v���C���[���X�g
    public static class Player
    {
        //�X�Y��(0),�J���X(1),�R�K��(2),�y���M��(3)
        //�ԍ�
        public static int[] number = new int[] 
        { 0, 1, 2, 3 };
        //�̗�
        public static int[] hp = new int[] 
        { 5, 5, 5, 5 };
        //�U����
        public static int[] power = new int[]
        { 3, 5, 1, 5 };
        //�U���Ԋu
        public static float[,] attackInterval = new float[2, 4]
        { 
            { 1.0f, 2.0f, 0.5f, 0.5f },//�O����
            { 2.5f, 3.5f, 2.0f, 0.5f } //������
        };
        //�ő�o���l
        public static int[] maxExp = new int[]
        { 100, 100, 100, 100 };
    }

    public static class Invincible
    {
        public static int power = 6;
        public static float[] attackInterval = new float[2] { 0.25f, 0.25f };
    }
}
