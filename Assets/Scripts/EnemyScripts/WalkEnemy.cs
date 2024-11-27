using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkEnemy : MonoBehaviour
{
    //�X�e�[�^�X
    private int hp = EnemyStatus.WalkEnemy.hp;        //�̗�
    private float speed = EnemyStatus.WalkEnemy.speed;//�ړ����x
    //����
    private int random = 0;       //�����_��
    private float interval = 0.0f;//�Ԋu
    private string nowAction;     //���݂̓���
    private bool action = false;  //����t���O
    private float viewPointX;     //�r���[�|�C���g���W.X
   //���̃I�u�W�F�N�g�̃R���|�[�l���g
    private Transform setTransform;  //"Transform"
    private Animator animator = null;//"Animator"

    // Start is called before the first frame update
    void Start()
    {
        setTransform = this.gameObject.GetComponent<Transform>();//���̃I�u�W�F�N�g��Transform���擾
        animator = this.GetComponent<Animator>();                //���̃I�u�W�F�N�g��Animator���擾
        animator.SetInteger("Motion", 0);                        //Animator��"Motion, 0"(����)��L���ɂ���
        nowAction = "Run";
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
        Vector3 localPosition = setTransform.localPosition;//�I�u�W�F�N�g��
        Vector3 localAngle = setTransform.localEulerAngles;//
        localPosition.y = 0.0f;//
        localPosition.z = 1.0f;//
        localAngle.y = -EnemyStatus.rotationY;//
        setTransform.localPosition = localPosition;       //���[�J�����W�ł̍��W��ݒ�
        setTransform.localEulerAngles = localAngle;       //

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
            random = (int)Random.Range(10, 12);     //�����_������(1�`2)
            animator.SetInteger("Motion", random);//Animator��AttackMotion(1�`2)��L���ɂ���
            Debug.Log(random);                    //Debug.Log(random)
        }
        else if (PlayerController.hp <= 0)
        {
            nowAction = "Dance";
            animator.SetInteger("Motion", 30);//"Animator"��"Motion, 3"(�_���X���[�V����)��L���ɂ���
        }
    }

    //�ҋ@�֐�
    void Wait()
    {
        interval += Time.deltaTime;//�N�[���^�C����Time.deltaTime�𑫂�

        if (nowAction == "Attack")
        {
            //
            if (random == 10)
            {
                //
                if (interval >= 2.0f)
                {
                    interval = 0.0f;                      //
                    animator.SetInteger("Motion", random);//
                    action = false;
                    nowAction = "Run";
                }
            }
            //
            else if (random == 11)
            {
                //
                if (interval >= 1.5f)
                {
                    interval = 0.0f;
                    animator.SetInteger("Motion", random);//
                    action = false;
                    nowAction = "Run";
                }
            }
        }
    }

    //�_���[�W����֐�
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
        hp = 0;                                          //�̗͂�"0"�ɂ���
        GameManager.score += EnemyStatus.WalkEnemy.score;//
        this.tag = "Death";                              //�^�O��"Death"�ɕύX����
        animator.SetInteger("Motion", 31);               //
    }

    //�����蔻��(OnTriggerEnter)
    void OnTriggerEnter(Collider collision)
    {
        //�^�OPlayer�̕t�����I�u�W�F�N�g�ɏՓ˂�����
        if (collision.gameObject.tag == "Player" && action == false)
        {
            action = true;
            nowAction = "Attack";
            Animation();
        }
        //�^�OBullet�̕t�����I�u�W�F�N�g�ɏՓ˂�����
        if (collision.gameObject.tag == "Bullet" && this.tag != "Death")
        {
            Damage();//�֐�Damage���Ăяo��
        }
    }
}
