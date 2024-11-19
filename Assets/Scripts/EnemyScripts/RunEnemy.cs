using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunEnemy : MonoBehaviour
{
    //�X�e�[�^�X
    private int hp = EnemyStatus.RunEnemy.hp;        //�̗�
    private float speed = EnemyStatus.RunEnemy.speed;//�ړ����x
    private float jump = EnemyStatus.RunEnemy.jump;  //�W�����v��
    //����
    private int random = 0;       //�����_��
    private float interval = 0.0f;//�Ԋu
    private string nowAction;     //���݂̃A�N�V����
    private bool action = false;  //�A�N�V�����t���O
    private float viewPointX;     //�r���[�|�C���g���W.X
    //���̃I�u�W�F�N�g�̃R���|�[�l���g
    private Transform thisTransform;  //"Transform"(���̃I�u�W�F�N�g)
    private Animator animator = null; //"Animator"(���̃I�u�W�F�N�g)
    //���̃I�u�W�F�N�g�̃R���|�[�l���g
    private Transform playerTransform;//"Transform"(�v���C���[)

    //private Rigidbody rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        thisTransform = this.gameObject.GetComponent<Transform>();//���̃I�u�W�F�N�g��"Transform"���擾
        animator = this.GetComponent<Animator>();                 //���̃I�u�W�F�N�g��"Animator"���擾
        animator.SetInteger("Motion", 0);                         //�A�j���[�V������"Motion, 0"(����)�ɂ���
        playerTransform = GameObject.Find("Player").transform;    //
        nowAction = "Run";
        //rigidBody = this.GetComponent<Rigidbody>();
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

    //�s���֐�
    void Behavior()
    {
        Vector3 localPosition = thisTransform.localPosition;//
        Vector3 localAngle = thisTransform.localEulerAngles;//

        if (this.transform.position.x + EnemyStatus.RunEnemy.rangeX > playerTransform.position.x &&
            this.transform.position.x - EnemyStatus.RunEnemy.rangeX < playerTransform.position.x &&
            this.transform.position.y + EnemyStatus.RunEnemy.rangeY < playerTransform.position.y &&
            nowAction == "Run" && action == false && this.transform.position.y == 0.0f)
        {
            action = true;
            nowAction = "jump";
            Animation();
        }

        if(nowAction == "Run")
        {
            localPosition.y = 0.0f;//
        }

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

        thisTransform.localPosition = localPosition;//���[�J�����W�ł̍��W��ݒ�
        thisTransform.localEulerAngles = localAngle;//

        //
        if (nowAction == "Run")
        {
            this.transform.position += speed * transform.forward * Time.deltaTime;//�O�����Ɉړ�����
        }
        //
        else if (nowAction != "Run" && action == true)
        {
            Wait();
        }

        //
        if (PlayerController.hp <= 0 && action == false)
        {
            Animation();//�A�j���[�V�����֐������s
        }
    }

    //�A�j���[�V�����֐�
    void Animation()
    {
        if (PlayerController.hp > 0 && nowAction == "Attack")
        {
            random = (int)Random.Range(1, 3);     //�����_������(1�`2)
            animator.SetInteger("Motion", random);//"Animator"��"Motion, 1�`2"(�U��)��L���ɂ���
            Debug.Log(random);                    //�f�o�b�N���O
        }
        else if(PlayerController.hp > 0 && nowAction == "jump")
        {
            animator.SetInteger("Motion", 10);
        }
        else if (PlayerController.hp <= 0)
        {
            nowAction = "Dance";
            animator.SetInteger("Motion", 3);//"Animator"��"Motion, 3"(�_���X)��L���ɂ���
        }
    }

    //�ҋ@�֐�
    void Wait()
    {
        interval += Time.deltaTime;//�N�[���^�C����Time.deltaTime�𑫂�

        if (nowAction == "Attack")
        {
            //
            if (random == 1)
            {
                //
                if (interval >= 2.0f)
                {
                    interval = 0.0f;                 //
                    animator.SetInteger("Motion", 0);//
                    action = false;
                    nowAction = "Run";
                }
            }
            //
            else if (random == 2)
            {
                //
                if (interval >= 1.5f)
                {
                    interval = 0.0f;
                    animator.SetInteger("Motion", 0);//
                    action = false;
                    nowAction = "Run";
                }
            }
        }
        //
        else if(nowAction == "jump")
        {
            if (interval >= 0.75f)
            {
                this.transform.position += jump * transform.up * Time.deltaTime;

                if(interval >= 1.0f)
                {
                    animator.SetFloat("MoveSpeed", 0.0f);//�ꎞ��~
                }

                //
                if (interval >= 2.0f)
                {
                    animator.SetFloat("MoveSpeed", 1.0f);//
                    interval = 0.0f;
                    animator.SetInteger("Motion", 0);//
                    action = false;
                    nowAction = "Run";
                }
            }
        }
    }

    //�_���[�W�֐�
    void Damage()
    {
        hp -= 1;//�̗͂�"-1"����

        //�̗͂�0�ȉ���������
        if (hp <= 0)
        {
            Death();//�֐�"Death"���S���Ăяo��
        }
    }

    //���S�֐�
    void Death()
    {
        this.transform.position = new Vector3(thisTransform.position.x, 0.0f, thisTransform.position.z);
        hp = 0;                                         //�̗͂�"0"�ɂ���
        GameManager.score += EnemyStatus.RunEnemy.score;//
        this.tag = "Death";                             //�^�O��"Death"�ɕύX����
        animator.SetInteger("Motion", 4);               //
    }

    //�����蔻��(OnTriggerEnter)
    void OnTriggerEnter(Collider collision)
    {
        //�Փ˂����I�u�W�F�N�g�̃^�O��"Player" && "action"��"false"��������
        if (collision.gameObject.tag == "Player" && action == false)
        {
            action = true;
            nowAction = "Attack";
            Animation();
        }
        //�^�OBullet�̕t�����I�u�W�F�N�g�ɏՓ˂�����
        if (collision.gameObject.tag == "Bullet" && hp > 0)
        {
            Damage();//�֐�Damage���Ăяo��
        }
    }
}
