using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    //�G(����)
    public static float rotationY = 90.0f;//Y����]�Œ�

    //�G(����)
    public static class WalkEnemy
    {
        //�X�e�[�^�X
        public static int hp = 1;        //�̗�
        public static int power = 1;     //�U����
        public static float speed = 2.0f;//�ړ����x
        public static float jump = 3.0f; //�W�����v��
        //���̑��ݒ�
        public static int score = 100;//�|�������̃X�R�A
    }
    //�G(����)
    public static class RunEnemy
    {
        //�X�e�[�^�X
        public static int hp = 2;          //�̗�
        public static int power = 1;       //�U����
        public static float speed = 4.0f;  //�ړ����x
        public static float jump = 4.0f;   //�W�����v��
        public static float rangeX = 0.25f;//����.X
        public static float rangeY = 2.0f; //����.Y

        //���̑��ݒ�
        public static int score = 100;//�|�������̃X�R�A
    }
    //�{�X(�X�e�[�W1�`5)
    public static class BossEnemy
    {
        //�X�e�[�^�X
        public static int[] hp = new int[5] { 10, 10, 10, 10, 10 };                 //�̗�
        public static int[] power = new int[5] { 1, 1, 1, 1, 1 };                   //�U����
        public static float[] speed = new float[5] { 2.0f, 1.0f, 1.0f, 1.0f, 1.0f };//�ړ����x
        public static float[] jump = new float[5] { 4.0f, 1.0f, 1.0f, 1.0f, 1.0f }; //�W�����v��
        //���̑��ݒ�
        public static int[] score = new int[5] { 1000, 1000, 1000, 1000, 1000 };//�|�������̃X�R�A
    }
}
