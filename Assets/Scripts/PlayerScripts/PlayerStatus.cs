using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    //�v���C���[�X�e�[�^�X���X�g
    public static class PlayerStatusList
    {
        //�X�Y��,�J���X,�R�K��,�y���M��
        public static int[] number = new int[] { 0, 1, 2, 3 };     //�ԍ�
        public static int[] hp = new int[] { 6, 8, 5, 10 };        //�̗�
        public static int[] power = new int[] { 7, 10, 5, 10 };    //�U����
        public static float[] speed = new float[] { 8, 6, 10, 10 };//�ړ����x
    }
}
