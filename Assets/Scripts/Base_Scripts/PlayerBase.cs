using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    //�X�e�[�^�X
    public static int hp;    //�̗�
    public int attack;       //�U����
    public float speed;      //�ړ����x
    public static int remain;//�c�@
    public string status;    //���
    //����
    public static float[] attackTimer = new float[2];   //�U���^�C�}�[([�O��],[����])
    public static float[] attackInterval = new float[2];//�U���Ԋu([�O��],[����])
    public static float gageTimer;                      //�Q�[�W�^�C�}�[
    public static float gageInterval;                   //�Q�[�W�~�ώ���
    public static int level;                            //���x��
    public static int exp;                              //�o���l
    public static int ally;                             //������
    public float invincibleTimer = 0.0f;                //���G�^�C�}�[
    public float invincibleInterval = 10.0f;            //���G��������
    public float blinkingTime = 1.0f;                   //�_�Ŏ�������
    public float rendererSwitch = 0.05f;                //Renderer�؂�ւ�����
    public float rendererTimer;                         //Renderer�؂�ւ��̌o�ߎ���
    public float rendererTotalTime;                     //Renderer�؂�ւ��̍��v�o�ߎ���
    public bool isObjRenderer;                          //objRenderer�̉�
    public float levelAttackInterval = 0.0f;            //���x���A�b�v���̍U���Ԋu�Z�k
    public bool isAction = false;      //�s���̉�
    public bool isAnimation = false;   //�A�j���[�V�����̉�
    public bool isDamage;              //�_���[�W�̉�
    //���W
    public Vector3 mousePosition, worldPosition, viewPortPosition;
    //���̃I�u�W�F�N�g�̃R���|�[�l���g
    public Transform thisTransform;                            //"Transform"
    public Rigidbody rigidBody;                                //"Rigidbody"
    public BoxCollider boxCollider;                            //"BoxCollider"
    public CapsuleCollider capsuleCollider;                    //"CapsuleCollider"
    public Renderer[] thisRenderer;                            //"Renderer"
    public AudioClip damage;                                   //"AudioClip(�_���[�W)"
    public AudioClip scream;                                   //"AudioClip(���ѐ�)"
    public Animator animator = null;                           //"Animator"
    public RuntimeAnimatorController runtimeAnimatorController;//"RuntimeAnimatorController"
    public AudioSource audioSource;                            //"AudioSource"
    //���̃I�u�W�F�N�g�̃R���|�[�l���g
    public Transform playerTransform;                         //"Transform(�v���C���[)"

    //�֐�"GetComponent"
    public virtual void GetComponent()
    {
        //���̃I�u�W�F�N�g�̃R���|�[�l���g���擾
        thisTransform = this.GetComponent<Transform>();
        rigidBody = this.gameObject.GetComponent<Rigidbody>();
        //"BoxCollider"�����݂��Ă���ꍇ
        if (TryGetComponent<BoxCollider>(out boxCollider))
        {
            boxCollider = this.gameObject.GetComponent<BoxCollider>();
        }
        //"CapsuleCollider"�����݂��Ă���ꍇ
        if (TryGetComponent<CapsuleCollider>(out capsuleCollider))
        {
            capsuleCollider = this.gameObject.GetComponent<CapsuleCollider>();
        }
        thisRenderer = this.gameObject.GetComponentsInChildren<Renderer>();
        animator = this.GetComponent<Animator>();
        runtimeAnimatorController = animator.runtimeAnimatorController;
        audioSource = this.GetComponent<AudioSource>();
        //���̃I�u�W�F�N�g�̃R���|�[�l���g���擾
        playerTransform = GameObject.Find("Player").transform;
    }

    //�֐�"Damage"
    public virtual void Damage()
    {
        hp -= 1;//�̗͂�"-1"����
    }

    //�֐�"Death"
    public virtual void Death()
    {
        hp = 0;//�̗͂�"0"�ɂ���
    }

    //�֐�"Destroy"
    public virtual void Destroy()
    {
        Destroy(this.gameObject);//���̃I�u�W�F�N�g������
    }
}
