using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : MonoBehaviour
{
    //�X�e�[�^�X
    private int hp = EnemyList.BossEnemy.hp[0];        //�̗�
    private float speed = EnemyList.BossEnemy.speed[0];//�ړ����x
    private float jump = EnemyList.BossEnemy.speed[0]; //�W�����v��
    //����
    private float viewPointX;     //�r���[�|�C���g���W.X
    private float interval = 0.0f;//�Ԋu
    //�A�j���[�V����
    private int nowAnimation;        //���݂̃A�j���[�V����
    private bool isAnimation = false;//�A�j���[�V�����̉�
    //���̃I�u�W�F�N�g�̃R���|�[�l���g
    private Transform thisTransform;  //"Transform"(���̃I�u�W�F�N�g)
    private Animator animator = null; //"Animator"(���̃I�u�W�F�N�g)
    //���̃I�u�W�F�N�g�̃R���|�[�l���g
    private Transform playerTransform;//"Transform"(�v���C���[)

    // Start is called before the first frame update
    void Start()
    {
        thisTransform = this.gameObject.GetComponent<Transform>();//���̃I�u�W�F�N�g��"Transform"���擾
        animator = this.GetComponent<Animator>();                 //���̃I�u�W�F�N�g��"Animator"���擾
        playerTransform = GameObject.Find("Player").transform;    //�Q�[���I�u�W�F�N�g"Player"��T����"Transform"���擾
        nowAnimation = EnemyList.HumanoidAnimation.walk;
        Animation();
    }

    // Update is called once per frame
    void Update()
    {
        //���̃I�u�W�F�N�g�̃r���[�|�[�g���W���擾
        viewPointX = Camera.main.WorldToViewportPoint(this.transform.position).x;//�r���[�|�[�g���W.X

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
        Vector3 localPosition = thisTransform.localPosition;//
        Vector3 localAngle = thisTransform.localEulerAngles;//

        if (this.transform.position.x + EnemyList.RunEnemy.rangeX > playerTransform.position.x &&
            this.transform.position.x - EnemyList.RunEnemy.rangeX < playerTransform.position.x &&
            this.transform.position.y + EnemyList.RunEnemy.rangeY < playerTransform.position.y &&
            this.transform.position.y == 0.0f && nowAnimation == EnemyList.HumanoidAnimation.walk &&
            isAnimation == false)
        {
            isAnimation = true;
            nowAnimation = EnemyList.HumanoidAnimation.jump;
            Animation();
        }

        if (nowAnimation == EnemyList.HumanoidAnimation.walk)
        {
            localPosition.y = 0.0f;//
        }

        localPosition.z = 1.0f;//

        //
        if (this.transform.position.x > playerTransform.position.x)
        {
            localAngle.y = -EnemyList.rotation;//
        }
        //
        else if (this.transform.position.x < playerTransform.position.x)
        {
            localAngle.y = EnemyList.rotation;//
        }

        thisTransform.localPosition = localPosition;//���[�J�����W�ł̍��W��ݒ�
        thisTransform.localEulerAngles = localAngle;//

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
        Debug.Log(nowAnimation);                    //"Debug.Log(nowAnimation)"
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
        //
        else if (nowAnimation == EnemyList.HumanoidAnimation.jump)
        {
            if (interval >= 0.75f)
            {
                this.transform.position += jump * transform.up * Time.deltaTime;

                if (interval >= 2.0f)
                {
                    animator.SetFloat("MoveSpeed", 1.0f);             //"animator(MoveSpeed)"��"1.0f(�Đ�)"�ɂ���
                    interval = 0.0f;
                    isAnimation = false;
                    nowAnimation = EnemyList.HumanoidAnimation.walk;
                    Animation();
                }
                else if (interval >= 1.0f)
                {
                    animator.SetFloat("MoveSpeed", 0.0f);             //"animator(MoveSpeed)"��"0.0f(��~)"�ɂ���
                }
            }
        }
    }

    //�_���[�W�֐�
    void Damage()
    {
        hp -= PlayerList.Player.power[GameManager.playerNumber];//

        //�̗͂�0�ȉ���������
        if (hp <= 0)
        {
            Death();//�֐�"Death"���S���Ăяo��
        }
    }

    //���S�֐�
    void Death()
    {
        hp = 0;                                            //�̗͂�"0"�ɂ���
        this.thisTransform.position = new Vector3(this.thisTransform.position.x, 0.0f, this.thisTransform.position.z);//
        GameManager.score += EnemyList.WalkEnemy.score;   //
        this.tag = "Untagged";                            //�^�O��"Untagged"�ɕύX����
        nowAnimation = EnemyList.HumanoidAnimation.death;
        Stage.bossEnemy[Stage.nowStage - 1] = false;
        Animation();
    }

    //�����蔻��(OnTriggerEnter)
    void OnTriggerEnter(Collider collision)
    {
        //�Փ˂����I�u�W�F�N�g�̃^�O��"Player" && "action"��"false"��������
        if (collision.gameObject.tag == "Player" && isAnimation == false)
        {
            isAnimation = true;
            nowAnimation = (int)Random.Range(EnemyList.HumanoidAnimation.punch, EnemyList.HumanoidAnimation.kick + 1);//�����_��"10(�p���`)"�`"12(�L�b�N)"
            Animation();
        }
        //�^�OBullet�̕t�����I�u�W�F�N�g�ɏՓ˂�����
        if (collision.gameObject.tag == "Bullet" && hp > 0)
        {
            Damage();//�֐�Damage���Ăяo��
        }
    }
}