using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacteBase : MonoBehaviour
{
    //�X�e�[�^�X
    public float speed;//�ړ����x
    //����
    public bool isDamage;//�_���[�W�̉�
    //���W
    public Vector3 mousePosition, worldPosition, viewPortPosition;
    //���̃I�u�W�F�N�g�̃R���|�[�l���g
    public Transform thisTransform;                            //"Transform"
    public Rigidbody rigidBody;                                //"Rigidbody"
    public BoxCollider boxCollider;                            //"BoxCollider"
    public CapsuleCollider capsuleCollider;                    //"CapsuleCollider"
    public Renderer[] thisRenderer;                            //"Renderer"
    public AudioClip damage;                                   //"AudioClip(�_���[�W)"
    public Animator animator = null;                           //"Animator"
    public RuntimeAnimatorController runtimeAnimatorController;//"RuntimeAnimatorController"
    public AudioSource audioSource;                            //"AudioSource"
    //���̃I�u�W�F�N�g�̃R���|�[�l���g
    public Transform playerTransform;                         //"Transform(�v���C���[)"

    //�֐�"GetComponent"
    public void GetComponent()
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
        audioSource = this.GetComponent<AudioSource>();
        //���̃I�u�W�F�N�g�̃R���|�[�l���g���擾
        playerTransform = GameObject.Find("Player").transform;
    }

    //�֐�"Destroy"
    public void DestroyCharacte()
    {
        Destroy(this.gameObject);//���̃I�u�W�F�N�g������
    }
}

public class PlayerBase : CharacteBase
{
    //�X�e�[�^�X
    public static int hp;       //�̗�
    public int attack;          //�U����
    public static int remain;   //�c�@
    public static string status;//���
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
    //���̃I�u�W�F�N�g�̃R���|�[�l���g
    public GameObject nowPlayer;                       //"GameObject(���݂̃v���C���[)"
    public GameObject[] playerAlly = new GameObject[2];//"GameObject(�v���C���[�̖���)"

    //�֐�"PlayerGetComponent"
    public virtual void StartPlayer()
    {
        base.GetComponent();
        //����������������
        gageTimer = 0.0f;
        gageInterval = 20.0f;
        ally = 0;
        level = 1;
        exp = 0;
        //�I�������v���C���[�̃X�e�[�^�X��ݒ肷��
        hp = PlayerList.Player.hp[GameManager.playerNumber];                           //�̗�
        attackTimer[0] = PlayerList.Player.attackInterval[0, GameManager.playerNumber];//�U���^�C�}�[[�O��]
        attackTimer[1] = PlayerList.Player.attackInterval[1, GameManager.playerNumber];//�U���^�C�}�[[����]
        status = "Normal";                                                             //�v���C���[�̏�Ԃ�"Normal"�ɂ���
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
    }
}

public class EnemyBase : CharacteBase
{
    //�X�e�[�^�X
    public int hp;       //�̗�
    public float jump;   //�W�����v��
    public string status;//���
    //����
    public bool isAction = false;      //�s���̉�
    public float animationTimer = 0.0f;//�A�j���[�V�����^�C�}�[
    public bool isAnimation = false;   //�A�j���[�V�����̉�
    //���̃I�u�W�F�N�g�̃R���|�[�l���g
    public AudioClip scream;//"AudioClip(���ѐ�)"

    //�֐�"StartEnmey"
    public virtual void StartEnemy()
    {
        base.GetComponent();
        animator = this.GetComponent<Animator>();
        runtimeAnimatorController = animator.runtimeAnimatorController;
    }

    //�֐�"UpdateEnmey"
    public virtual void UpdateEnemy()
    {

    }

    //�֐�"Enmey"
    public virtual void DamageEnemy()
    {
        hp -= 1;//�̗͂�"-1"����
    }

    //�֐�"Enmey"
    public virtual void DeathEnemy()
    {
        hp = 0;//�̗͂�"0"�ɂ���
        audioSource.PlayOneShot(scream);//"scream"��炷
    }
}