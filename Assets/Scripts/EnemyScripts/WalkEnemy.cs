using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkEnemy : MonoBehaviour
{
    public AudioClip damage;
    public AudioClip scream;

    //�X�e�[�^�X
    private int hp = EnemyList.WalkEnemy.hp;        //�G�̗̑�
    private float speed = EnemyList.WalkEnemy.speed;//�G�̈ړ����x
    //����
    private float viewPointX;     //�r���[�|�C���g���W.X
    private float interval = 0.0f;//�Ԋu
    //�A�j���[�V����
    private int nowAnimation;        //���݂̃A�j���[�V����
    private bool isAnimation = false;//�A�j���[�V�����̉�
    //���̃I�u�W�F�N�g�̃R���|�[�l���g
    private Transform thisTransform; //"Transform"
    private Animator animator = null;//"Animator"
    private AudioSource audioSource; //"AudioSource"

    // Start is called before the first frame update
    void Start()
    {
        thisTransform = this.gameObject.GetComponent<Transform>();//���̃I�u�W�F�N�g��"Transform"���擾
        animator = this.GetComponent<Animator>();                 //���̃I�u�W�F�N�g��"Animator���擾
        audioSource = this.GetComponent<AudioSource>();
        nowAnimation = EnemyList.HumanoidAnimation.walk;
        Animation();
    }

    // Update is called once per frame
    void Update()
    {
        //�r���[�|�[�g���W���擾
        viewPointX = Camera.main.WorldToViewportPoint(this.transform.position).x;//��ʍ��W.X

        //�̗͂�0���� && �r���[�|�[�g���W.X��1����ł����
        if (hp > 0 && viewPointX < 1)
        {
            Behavior();//�s���֐����Ăяo��
        }
        //�̗͂�0�ȉ� && �r���[�|�[�g���W.X��0�����ł����
        else if (hp <= 0 && viewPointX < 0)
        {
            Destroy(this.gameObject);//���̃I�u�W�F�N�g������
        }
    }

    void Behavior()
    {
        Vector3 localPosition = thisTransform.localPosition;//�I�u�W�F�N�g��
        Vector3 localAngle = thisTransform.localEulerAngles;//
        localPosition.y = 0.0f;  //
        localPosition.z = 1.0f;  //
        localAngle.y = -EnemyList.direction;//
        thisTransform.localPosition = localPosition;       //���[�J�����W�ł̍��W��ݒ�
        thisTransform.localEulerAngles = localAngle;       //

        //
        if (nowAnimation == EnemyList.HumanoidAnimation.walk)
        {
            this.transform.position += speed * transform.forward * Time.deltaTime;//�O�����Ɉړ�����
        }
        //
        else if (nowAnimation != EnemyList.HumanoidAnimation.walk && isAnimation == true)
        {
            Wait();
        }

        //
        if (PlayerController.hp <= 0 && isAnimation == false)
        {
            nowAnimation = EnemyList.HumanoidAnimation.dance;
            Animation();//�A�j���[�V�����֐������s
        }
    }

    //�A�j���[�V�����֐�
    void Animation()
    {
        animator.SetInteger("Motion", nowAnimation);//"animator(Motion)"��"nowAnimation"��ݒ肷��
        Debug.Log(nowAnimation);//"Debug.Log(nowAnimation)"
    }

    //�ҋ@�֐�
    void Wait()
    {
        interval += Time.deltaTime;//�N�[���^�C����Time.deltaTime�𑫂�

        if (nowAnimation == EnemyList.HumanoidAnimation.punch ||
            nowAnimation == EnemyList.HumanoidAnimation.kick)
        {
            //
            if (nowAnimation == EnemyList.HumanoidAnimation.punch)
            {
                //
                if (interval >= 2.0f)
                {
                    interval = 0.0f;
                    isAnimation = false;
                    nowAnimation = EnemyList.HumanoidAnimation.walk;
                    Animation();
                }
            }
            //
            else if (nowAnimation == EnemyList.HumanoidAnimation.kick)
            {
                //
                if (interval >= 1.5f)
                {
                    interval = 0.0f;
                    isAnimation = false;
                    nowAnimation = EnemyList.HumanoidAnimation.walk;
                    Animation();
                }
            }
        }
    }

    //�_���[�W����֐�
    void Damage()
    {
        hp -= PlayerList.Player.power[GameManager.playerNumber];//

        //"hp"��"0"���ゾ������
        if(hp > 0)
        {
            audioSource.PlayOneShot(damage);
        }
        //"hp"��"0"�ȉ���������
        else if (hp <= 0)
        {
            Death();//"Death(�֐�)"���Ăяo��
        }
    }

    //���S�֐�
    void Death()
    {
        audioSource.PlayOneShot(scream);
        hp = 0;                                            //�̗͂�"0"�ɂ���
        GameManager.score += EnemyList.WalkEnemy.score;  //
        this.tag = "Death";                                //�^�O��"Death"�ɕύX����
        nowAnimation = EnemyList.HumanoidAnimation.death;
        Animation();
    }

    //�����蔻��(OnTriggerEnter)
    void OnTriggerEnter(Collider collision)
    {
        //�^�OPlayer�̕t�����I�u�W�F�N�g�ɏՓ˂�����
        if (collision.gameObject.tag == "Player" && isAnimation == false)
        {
            isAnimation = true;
            nowAnimation = (int)Random.Range(EnemyList.HumanoidAnimation.punch, EnemyList.HumanoidAnimation.kick + 1);//�����_��"10(�p���`)"�`"12(�L�b�N)"
            Animation();
        }
        //�^�OBullet�̕t�����I�u�W�F�N�g�ɏՓ˂�����
        if (collision.gameObject.tag == "Bullet" && this.tag != "Death")
        {
            Damage();//�֐�Damage���Ăяo��
        }
    }
}
