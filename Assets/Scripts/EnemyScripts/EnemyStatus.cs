using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
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
}
