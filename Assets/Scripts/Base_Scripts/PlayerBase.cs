using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : CharacteBase
{
    //�X�e�[�^�X
    public static int attack;   //�U����
    public static int remain;   //�c�@
    public static string status;//���
    //���W
    public Vector3 mousePosition;
    //����
    public static float[] attackTimer = new float[2];       //�U���^�C�}�[([�O��],[����])
    public static float[] attackTimeInterval = new float[2];//�U�����ԊԊu([�O��],[����])
    public static float gageTimer;                          //�Q�[�W�^�C�}�[
    public static float gageTimeInterval;                   //�Q�[�W���ԊԊu
    public static int level;                                //���x��
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
        hp = PlayerList.Player.hp[GameManager.playerNumber];                           //�̗�
        attack = PlayerList.Player.power[GameManager.playerNumber];                    //�U����
        attackTimer[0] = PlayerList.Player.attackInterval[0, GameManager.playerNumber];//�U���^�C�}�[[�O��]
        attackTimer[1] = PlayerList.Player.attackInterval[1, GameManager.playerNumber];//�U���^�C�}�[[����]
        status = "Normal";                                                             //�v���C���[�̏�Ԃ�"Normal"�ɂ���
        //����������������
        gageTimer = 0.0f;
        gageTimeInterval = 20.0f;
        ally = 0;
        level = 1;
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
}