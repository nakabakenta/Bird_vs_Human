using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : MonoBehaviour
{
    //�X�e�[�^�X
    private int hp = EnemyStatus.BossEnemy.hp[0];        //�̗�
    private float speed = EnemyStatus.BossEnemy.speed[0];//�ړ����x
    //����
    private int random = 0;       //�����_��
    private float waitTime = 0.0f;//�ҋ@����
    private bool attack = false;  //�U���t���O
    private float viewPointX;     //�r���[�|�C���g���W.X
    //�R���|�[�l���g
    private Transform setTransform;   //Transform
    private Transform playerTransform;//Transform(�v���C���[)
    private Animator animator = null; //Animator

    // Start is called before the first frame update
    void Start()
    {
        setTransform = this.gameObject.GetComponent<Transform>();//���̃I�u�W�F�N�g��Transform���擾
        animator = this.GetComponent<Animator>();                //���̃I�u�W�F�N�g��Animator���擾
        animator.SetInteger("Motion", 0);                        //Animator��"Motion 0"(����)��L���ɂ���
        playerTransform = GameObject.Find("Chickadee_Player").transform;//
    }

    // Update is called once per frame
    void Update()
    {
        //�ړ���̃r���[�|�[�g���W���擾
        viewPointX = Camera.main.WorldToViewportPoint(this.transform.position).x;//��ʍ��W.X

        //
        if (viewPointX < 1)
        {
            Behavior();
        }
    }

    //�����蔻��(OnTriggerEnter)
    void OnTriggerEnter(Collider collision)
    {
        //�^�OPlayer�̕t�����I�u�W�F�N�g�ɏՓ˂�����
        if (collision.gameObject.tag == "Player" && attack == false)
        {
            Animation();
        }
        //�^�OBullet�̕t�����I�u�W�F�N�g�ɏՓ˂�����
        if (collision.gameObject.tag == "Bullet" && this.tag != "Death")
        {
            Damage();//�֐�Damage���Ăяo��
        }
    }

    //����֐�
    void Behavior()
    {
        Vector3 localPosition = setTransform.localPosition;//�I�u�W�F�N�g��
        Vector3 localAngle = setTransform.localEulerAngles;//

        localPosition.y = 0.0f;//
        localPosition.z = 0.0f;//

        //
        if (this.transform.position.x > playerTransform.position.x)
        {
            localAngle.y = -EnemyStatus.rotationY;//
        }
        //
        else if (this.transform.position.x < playerTransform.position.x)
        {
            localAngle.y = EnemyStatus.rotationY;//
        }

        setTransform.localPosition = localPosition;//���[�J�����W�ł̍��W��ݒ�
        setTransform.localEulerAngles = localAngle;//

        //
        if(PlayerController.hp > 0)
        {
            //
            if (attack == false)
            {
                this.transform.position += speed * transform.forward * Time.deltaTime;//�O�����Ɉړ�����
            }
        }
        //
        else if(PlayerController.hp <= 0)
        {
            Animation();//�A�j���[�V�����֐������s
        }
        //
        if (attack == true)
        {
            Wait();
        }
        //�̗͂�0�ȉ� && �r���[�|�[�g���W.X��0�����ł����
        if (hp <= 0 && viewPointX < 0)
        {
            Destroy(this.gameObject);//���̃I�u�W�F�N�g������
        }
    }

    //�A�j���[�V�����֐�
    void Animation()
    {
        if(PlayerController.hp > 0)
        {
            attack = true;
            random = (int)Random.Range(1, 3);     //�����_������(1�`2)
            animator.SetInteger("Motion", random);//Animator��AttackMotion(1�`2)��L���ɂ���
            Debug.Log(random);                    //Debug.Log(random)
        }
        else if(PlayerController.hp <= 0)
        {
            animator.SetInteger("Motion", 3);//Animator��Motion 3(�_���X���[�V����)��L���ɂ���
        }
    }

    //�ҋ@�֐�
    void Wait()
    {
        waitTime += Time.deltaTime;//�N�[���^�C����Time.deltaTime�𑫂�
        //
        if (random == 1)
        {
            //
            if (waitTime >= 2.0f)
            {
                waitTime = 0.0f;                 //
                animator.SetInteger("Motion", 0);//
                attack = false;
            }
        }
        //
        else if (random == 2)
        {
            //
            if (waitTime >= 1.5f)
            {
                waitTime = 0.0f;
                animator.SetInteger("Motion", 0);//
                attack = false;
            }
        }
    }

    //�_���[�W�֐�
    void Damage()
    {
        hp -= 1;//�̗͂�-1����

        //�̗͂�0�ȉ���������
        if (hp <= 0)
        {
            hp = 0;
            GameManager.score += EnemyStatus.BossEnemy.score[0];//
            this.tag = "Death";                                 //�^�O��Death�ɕύX����
            animator.SetInteger("Motion", 4);                   //
            Stage.BossEnemy[0] = false;
        }
    }
}