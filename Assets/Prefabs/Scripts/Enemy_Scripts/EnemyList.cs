using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyList : MonoBehaviour
{
    //�G(����)
    public static class WalkEnemy
    {
        //�X�e�[�^�X
        public static int hp = 2;        //�̗�
        public static float speed = 2.0f;//�ړ����x
    }
    //�G(����)
    public static class RunEnemy
    {
        //�X�e�[�^�X
        public static int hp = 5;        //�̗�
        public static float speed = 4.0f;//�ړ����x
        public static float jump = 10.0f;//�W�����v��

        //�v���C���[�̔F���͈�
        public static Vector3 range =
            new Vector3(0.25f, 2.0f, 0.0f);
    }
}