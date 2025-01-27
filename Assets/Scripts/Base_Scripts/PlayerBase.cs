using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : CharacteBase
{
    //�X�e�[�^�X
    public static int attackPower;//�U����
    public static int remain;     //�c�@
    public static string status;  //���
    //���W
    public Vector3 mousePosition;
    //����
    public static float[] attackTimer = new float[2];       //�U���^�C�}�[([�O��],[����])
    public static float[] attackTimeInterval = new float[2];//�U�����ԊԊu([�O��],[����])
    public static float gageTimer;                          //�Q�[�W�^�C�}�[
    public static float gageTimeInterval;                   //�Q�[�W���ԊԊu
    public static int maxExp = 10;                          //�ő�o���l
    public static int exp;                                  //�o���l
    public static int ally;                                 //������
    public float invincibleTimer = 0.0f;                    //���G�^�C�}�[
    public float invincibleInterval = 10.0f;                //���G��������
    public float blinkingTime = 1.0f;                       //�_�Ŏ�������
    public float rendererSwitch = 0.05f;                    //Renderer�؂�ւ�����
    public float rendererTimer;                             //Renderer�؂�ւ��̌o�ߎ���
    public float rendererTotalTime;                         //Renderer�؂�ւ��̍��v�o�ߎ���
    public bool isRenderer;                               �@//Renderer�̉�
    public float levelAttackInterval = 0.0f;                //���x���A�b�v���̍U���Ԋu�Z�k
    public bool isAction = false;      //�s���̉�
    public bool isAnimation = false;   //�A�j���[�V�����̉�
    //���̃I�u�W�F�N�g�̃R���|�[�l���g
    public GameObject nowPlayer;                       //"GameObject(���݂̃v���C���[)"
    public GameObject[] playerAlly = new GameObject[2];//"GameObject(�v���C���[�̖���)"

    //�֐�"StartPlayer"
    public void StartPlayer()
    {
        //�I�������v���C���[�̃X�e�[�^�X��ݒ肷��
        hp = Player.hp[GameManager.selectPlayer];                     //�̗�
        attackPower = Player.attackPower[GameManager.selectPlayer];   //�U����

        if(GameManager.selectPlayer == 0)
        {
            attackTimer[0] = 2.0f;
            attackTimer[1] = 2.0f;
        }
        else if (GameManager.selectPlayer == 1)
        {
            attackTimer[0] = 3.0f;
            attackTimer[1] = 3.0f;
        }
        else if (GameManager.selectPlayer == 2)
        {
            attackTimer[0] = 1.0f;
            attackTimer[1] = 1.0f;
        }

        status = "Normal";                                            //�v���C���[�̏�Ԃ�"Normal"�ɂ���

        //����������������
        gageTimer = 0.0f;
        gageTimeInterval = 20.0f;
        ally = 0;
        exp = 0;

        //�Q�[���̏�Ԃ�"Menu"�̏ꍇ
        if (GameManager.status == "Menu")
        {
            remain = 3;                 //�c�@
            GameManager.status = "Play";//�Q�[���̏�Ԃ�"Play"�ɂ���
        }
    }

    public virtual void UpdatePlayer()
    {

    }

    //�֐�"DamagePlayer"
    public virtual void DamagePlayer()
    {
        //�_���[�W��"�󂯂Ă���"�ꍇ
        if (isDamage == true)
        {
            return;//�Ԃ�
        }

        hp -= 1;//�̗͂�"-1"����
    }

    //�֐�"DeathPlayer"
    public void DeathPlayer()
    {
        boxCollider.enabled = false;    //BoxCollider��"����"�ɂ���
        rigidBody.useGravity = true;    //RigidBody�̏d�͂�"�L��"�ɂ���
        animator.SetBool("Death", true);//Animator��"Death"�ɂ���
        hp = 0;                         //�̗͂�"0"�ɂ���
        remain -= 1;                    //�c�@��"-1"����
        status = "Death";
    }

    public static class Player
    {
        public enum PlayerName
        {
            Sparrow = 0,
            Crow = 1,
            Chickadee = 2,
            Penguin = 3,
        }

        //�̗�
        public static int[] hp = new int[] 
        { 4, 4, 4, 4 };
        //�U����
        public static int[] attackPower = new int[]
        { 3, 6, 1, 5 };
        //�U�����x
        public static float[] attackSpeed = new float[]
        { 4.0f, 2.0f, 6.0f, 6.0f };
    }

    public static class InvincibleStatus
    {
        public static float attackSpeed = 0.5f;
    }
}