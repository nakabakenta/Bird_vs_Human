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
    public void Destroy()
    {
        Destroy(this.gameObject);//���̃I�u�W�F�N�g������
    }
}

public class PlayerBase : CharacteBase
{
    //�X�e�[�^�X
    public static int hp;       //�̗�
    public static int attack;   //�U����
    public static int remain;   //�c�@
    public static string status;//���
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
    public bool isObjRenderer;                              //objRenderer�̉�
    public float levelAttackInterval = 0.0f;                //���x���A�b�v���̍U���Ԋu�Z�k
    public bool isAction = false;      //�s���̉�
    public bool isAnimation = false;   //�A�j���[�V�����̉�
    //���̃I�u�W�F�N�g�̃R���|�[�l���g
    public GameObject nowPlayer;                       //"GameObject(���݂̃v���C���[)"
    public GameObject[] playerAlly = new GameObject[2];//"GameObject(�v���C���[�̖���)"

    //�֐�"StartPlayer"
    public void StartPlayer()
    {
        //����������������
        gageTimer = 0.0f;
        gageTimeInterval = 20.0f;
        ally = 0;
        level = 1;
        exp = 0;
        //�I�������v���C���[�̃X�e�[�^�X��ݒ肷��
        hp = PlayerList.Player.hp[GameManager.playerNumber];                           //�̗�
        attack = PlayerList.Player.power[GameManager.playerNumber];                    //�U����
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
    //���̃I�u�W�F�N�g�̃R���|�[�l���g
    public AudioClip scream;                 //"AudioClip(���ѐ�)"
    //�X�e�[�^�X
    public int hp;       //�̗�
    public float jump;   //�W�����v��
    public string status;//���
    //����
    public int nowAnimationNumber;           //���݂̃A�j���[�V�����̔ԍ�
    public string nowAnimationName;          //���݂̃A�j���[�V�����̖��O
    public float nowAnimationLength;         //���݂̃A�j���[�V�����̒���
    public bool isAction = false;            //�s���̉�
    public float animationTimer = 0.0f;      //�A�j���[�V�����^�C�}�[
    public bool isAnimation = false;         //�A�j���[�V�����̉�
    public HumanoidAnimation humanoidAnimation;
    public delegate void DirectionDelegate();//""
    public DirectionDelegate nowDirection;

    //�֐�"StartEnmey"
    public virtual void StartEnemy()
    {
        animator = this.GetComponent<Animator>();
        runtimeAnimatorController = animator.runtimeAnimatorController;
    }

    //�֐�"UpdateEnmey"
    public virtual void UpdateEnemy()
    {
        //���̃I�u�W�F�N�g�̃r���[�|�[�g���W���擾
        viewPortPosition.x = Camera.main.WorldToViewportPoint(this.transform.position).x;

        //�r���[�|�[�g���W��"0����"�̏ꍇ
        if (viewPortPosition.x < 0)
        {
            Destroy();//�֐�"Destroy"�����s
        }
    }

    //�֐�"Animation"
    public void AnimationPlay()
    {
        animator.SetInteger("Motion", nowAnimationNumber);//"animator(Motion)"��"nowAnimation"��ݒ肵�čĐ�
    }

    //�֐�"Wait"
    public void Wait()
    {
        animationTimer += Time.deltaTime;//"animationTimer"��"Time.deltaTime(�o�ߎ���)"�𑫂�

        foreach (AnimationClip clip in runtimeAnimatorController.animationClips)
        {
            if (clip.name == nowAnimationName)
            {
                nowAnimationLength = clip.length;
            }
        }

        if (animationTimer >= nowAnimationLength)
        {
            animationTimer = 0.0f;
            isAnimation = false;
            nowAnimationNumber = (int)HumanoidAnimation.Walk;
            nowAnimationName = HumanoidAnimation.Walk.ToString();
            AnimationPlay();//�֐�"Animation"�����s
        }
    }

    //�֐�"Enmey"
    public virtual void DamageEnemy()
    {
        hp -= PlayerBase.attack;

        //"hp > 0"�̏ꍇ
        if (hp > 0)
        {
            isAnimation = true;                                    //"isAnimation = true"�ɂ���
            nowAnimationNumber = (int)HumanoidAnimation.Damage;
            nowAnimationName = HumanoidAnimation.Damage.ToString();
            audioSource.PlayOneShot(damage);                       //"damage"��炷
            AnimationPlay();                                       //�֐�"Animation"�����s
        }
        //"hp <= 0"�̏ꍇ
        else if (hp <= 0)
        {
            Invoke("DeathEnemy", 0.01f);//�֐�"DeathEnemy"��"0.01f"��Ɏ��s����
        }
    }

    //�֐�"Enmey"
    public virtual void DeathEnemy()
    {
        this.tag = "Untagged";                                //���̃^�O��"Untagged"�ɂ���
        hp = 0;                                               //�̗͂�"0"�ɂ���
        audioSource.PlayOneShot(scream);//"scream"��炷
        GameManager.score += EnemyList.WalkEnemy.score;       //�X�R�A�𑫂�
        PlayerController.exp += EnemyList.WalkEnemy.exp;      //�o���l�𑫂�
        nowAnimationNumber = (int)HumanoidAnimation.Death;
        nowAnimationName = HumanoidAnimation.Death.ToString();    
        AnimationPlay();                                      //�֐�"Animation"�����s
    }

    //�����蔻��(OnTriggerEnter)
    public virtual void OnTriggerEnter(Collider collision)
    {
        //�Փ˂����I�u�W�F�N�g��"tag == Player" && "isAnimation == false"�̏ꍇ
        if (collision.gameObject.tag == "Player" && isAnimation == false)
        {
            isAnimation = true;//"isAnimation = true"�ɂ���
            //�����_��"10(�p���`)"�`"12(�L�b�N)"
            nowAnimationNumber = (int)Random.Range((int)HumanoidAnimation.Punch,
                                                   (int)HumanoidAnimation.Kick + 1);
            humanoidAnimation = (HumanoidAnimation)nowAnimationNumber;
            nowAnimationName = humanoidAnimation.ToString();

            AnimationPlay();//�֐�"AnimationPlay"�����s����
        }
        //�Փ˂����I�u�W�F�N�g�̃^�O��"Bullet" && "hp > 0"�̏ꍇ
        if (collision.gameObject.tag == "Bullet" && hp > 0)
        {
            DamageEnemy();//�֐�"DamageEnemy"�����s����
        }
    }

    public enum HumanoidAnimation
    {
        Walk = 0,
        Run = 1,
        CrazyRun = 2,
        HaveGunIdle = 3,
        Punch = 10,
        Kick = 11,
        Dance  = 30,
        Damage = 31,
        Death  = 32
    }
}