using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyList : MonoBehaviour
{
    //����
    public static float direction = 90.0f;//�i�ޕ���

    //�G(����)
    public static class WalkEnemy
    {
        //�X�e�[�^�X
        public static int hp = 1;        //�̗�
        public static int power = 1;     //�U����
        public static float speed = 2.0f;//�ړ����x
        public static float jump = 3.0f; //�W�����v��
        public static int score = 100;   //�|�������̃X�R�A
    }
    //�G(����)
    public static class RunEnemy
    {
        //�X�e�[�^�X
        public static int hp = 2;          //�̗�
        public static int power = 1;       //�U����
        public static float speed = 4.0f;  //�ړ����x
        public static float jump = 4.0f;   //�W�����v��
        public static float rangeX = 0.25f;//�v���C���[�̔F���͈�.X
        public static float rangeY = 2.0f; //�v���C���[�̔F���͈�.Y
        public static int score = 100;     //�|�������̃X�R�A
    }
    //�G(���Ⴊ��)
    public static class CrouchEnemy
    {
        //�X�e�[�^�X
        public static int hp = 5;     //�̗�
        public static int power = 1;  //�U����
        public static int score = 100;//�|�������̃X�R�A
    }
    //�G(��)
    public static class CarEnemy
    {
        //�X�e�[�^�X
        public static int hp = 10;        //�̗�
        public static int power = 2;      //�U����
        public static float speed = 30.0f;//�ړ����x
        public static float rangeX = 3.0f;//�v���C���[�̔F���͈�.X
        public static int score = 500;    //�|�������̃X�R�A
    }
    //�G(�ԏ��)
    public static class CarRideEnemy
    {
        //�X�e�[�^�X
        public static int hp = 2;          //�̗�
        public static int power = 1;       //�U����
        public static float speed = 4.0f;  //�ړ����x
        public static float jump = 4.0f;   //�W�����v��
        public static float rangeX = 0.25f;//�v���C���[�̔F���͈�.X
        public static float rangeY = 2.0f; //�v���C���[�̔F���͈�.Y
        public static int score = 100;     //�|�������̃X�R�A
    }

    public static class FighterJetEnemy
    {
        //�X�e�[�^�X
        public static int hp = 5;         //�̗�
        public static int power = 1;      //�U����
        public static float speed = 15.0f;//�ړ����x
        public static int score = 100;    //�|�������̃X�R�A
    }

    //�{�X(�X�e�[�W1�`5)
    public static class BossEnemy
    {
        //�X�e�[�^�X
        public static int[] hp = new int[5] { 10, 10, 10, 10, 10 };                 //�̗�
        public static int[] power = new int[5] { 1, 1, 1, 1, 1 };                   //�U����
        public static float[] speed = new float[5] { 2.0f, 1.0f, 1.0f, 1.0f, 1.0f };//�ړ����x
        public static float[] jump = new float[5] { 4.0f, 1.0f, 1.0f, 1.0f, 1.0f }; //�W�����v��
        public static int[] score = new int[5] { 1000, 1000, 1000, 1000, 1000 };    //�|�������̃X�R�A
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
