using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyList : MonoBehaviour
{
    //����
    public static float rotation = 90.0f;//�i�ޕ���

    //�G(����)
    public static class WalkEnemy
    {
        //�X�e�[�^�X
        public static int hp = 2;        //�̗�
        public static int power = 1;     //�U����
        public static float speed = 2.0f;//�ړ����x
        public static int score = 100;   //�|�������̃X�R�A
        public static int exp = 10;      //�|�������̌o���l
    }
    //�G(����)
    public static class RunEnemy
    {
        //�X�e�[�^�X
        public static int hp = 5;        //�̗�
        public static int power = 1;     //�U����
        public static float speed = 4.0f;//�ړ����x
        public static float jump = 10.0f;//�W�����v��
        public static int score = 150;   //�|�������̃X�R�A
        public static int exp = 10;      //�|�������̌o���l

        //�v���C���[�̔F���͈�
        public static Vector3 range =
            new Vector3(0.25f, 2.0f, 0.0f);
    }
    //�G(���Ⴊ��)
    public static class CrouchEnemy
    {
        //�X�e�[�^�X
        public static int hp = 10;    //�̗�
        public static int power = 1;  //�U����
        public static int score = 300;//�|�������̃X�R�A
        public static int exp = 10;   //�|�������̌o���l
    }
    //�G(��)
    public static class CarEnemy
    {
        //�X�e�[�^�X
        public static int hp = 20;       //�̗�
        public static int power = 2;     //�U����
        public static float speed = 8.0f;//�ړ����x
        public static int score = 1000;  //�|�������̃X�R�A
        public static int exp = 10;      //�|�������̌o���l

        //�v���C���[�̔F���͈�
        public static Vector3 range =
            new Vector3(5.0f, 0.0f, 0.0f);
    }

    //�G(�ԏ��)
    public static class CarRideEnemy
    {
        //�X�e�[�^�X
        public static int hp = 5;        //�̗�
        public static int power = 1;     //�U����
        public static float speed = 4.0f;//�ړ����x
        public static float jump = 10.0f;//�W�����v��
        public static int score = 150;   //�|�������̃X�R�A
        public static int exp = 10;      //�|�������̌o���l

        //�v���C���[�̔F���͈�
        public static Vector3 range =
            new Vector3(0.25f, 2.0f, 0.0f);
    }

    //�G(�e����)
    public static class HaveGunEnemy
    {
        //�X�e�[�^�X
        public static int hp = 5;               //�̗�
        public static int power = 1;            //�U����
        public static float speed = 2.0f;       //�ړ����x
        public static float bulletSpeed = 15.0f;//�e�̑��x
        public static int score = 200;          //�|�������̃X�R�A
        public static int exp = 10;             //�|�������̌o���l
    }

    //�G(�퓬�@)
    public static class FighterJetEnemy
    {
        //�X�e�[�^�X
        public static int hp = 5;               //�̗�
        public static int power = 1;            //�U����
        public static float speed = 10.0f;      //�ړ����x
        public static float bulletSpeed = 20.0f;//�e�̑��x
        public static int score = 1000;         //�|�������̃X�R�A
        public static int exp = 10;             //�|�������̌o���l
    }

    //�{�X(�X�e�[�W1�`5)
    public static class BossEnemy
    {
        //�X�e�[�^�X
        public static int[] hp = new int[5] { 20, 30, 35, 40, 20 };                     //�̗�
        public static int[] power = new int[5] { 1, 1, 1, 1, 1 };                       //�U����
        public static float[] speed = new float[5] { 2.0f, 2.0f, 2.0f, 2.0f, 15.0f };   //�ړ����x
        public static float[] jump = new float[5] { 10.0f, 10.0f, 10.0f, 10.0f, 10.0f };//�W�����v��
        public static int[] score = new int[5] { 1000, 2000, 3000, 4000, 5000 };        //�|�������̃X�R�A

        //�v���C���[�̔F���͈�
        public static Vector3[] range = new Vector3[5]
        {
            new Vector3 (0.25f, 2.0f, 0.0f),
            new Vector3 (0.25f, 2.0f, 0.0f),
            new Vector3 (0.25f, 2.0f, 0.0f),
            new Vector3 (0.25f, 2.0f, 0.0f),
            new Vector3 (0.25f, 2.0f, 0.0f)
        };
    }

    //�q���[�}�m�C�h�A�j���[�V����
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
