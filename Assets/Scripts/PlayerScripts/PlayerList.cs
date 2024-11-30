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
        { 6, 8, 5, 10 };
        //�U����
        public static int[] power = new int[] 
        { 7, 10, 5, 10 };
        //�ړ����x
        public static float[] speed = new float[] 
        { 8.0f, 6.0f, 10.0f, 10.0f };
        //�U���Ԋu
        public static float[,] attackInterval = new float[2, 4]
        { 
            { 0.5f, 0.5f, 0.5f, 0.5f },//�O����
            { 1.0f, 1.0f, 1.0f, 1.0f } //������
        };
    }

    public static class Invincible
    {
        public static float[] attackInterval = new float[2] { 0.25f, 0.25f };
    }
}
