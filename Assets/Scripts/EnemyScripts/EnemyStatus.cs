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
        public static int hp = 1;   //�̗�
        public static int power = 1;//�U����
        public static int speed = 2;//�ړ����x
        //���̑��ݒ�
        public static int score = 100;//�|�������̃X�R�A
    }
    //�G(����)
    public static class RunEnemy
    {
        //�X�e�[�^�X
        public static int hp = 2;   //�̗�
        public static int power = 1;//�U����
        public static int speed = 4;//�ړ����x
        //���̑��ݒ�
        public static int score = 100;//�|�������̃X�R�A
    }
    //�{�X(�X�e�[�W1�`5)
    public static class BossEnemy
    {
        //�X�e�[�^�X
        public static int[] hp = new int[5] { 10, 10, 10, 10, 10 };//�̗�
        public static int[] power = new int[5] { 1, 1, 1, 1, 1 };  //�U����
        public static int[] speed = new int[5] { 2, 1, 1, 1, 1 };  //�ړ����x
        //���̑��ݒ�
        public static int[] score = new int[5] { 1000, 1000, 1000, 1000, 1000 };//�|�������̃X�R�A
    }
}
