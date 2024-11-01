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
    private float coolTime = 0.0f;//�N�[���^�C��
    private bool isAttack = false;//�U�������ǂ���
    private float viewPointX;     //�r���[�|�C���g���W.X
    //�R���|�[�l���g
    private Transform setTransform;  //Transform
    private Animator animator = null;//Animator

    // Start is called before the first frame update
    void Start()
    {
        setTransform = this.gameObject.GetComponent<Transform>();//���̃I�u�W�F�N�g��Transform���擾
        animator = this.GetComponent<Animator>();                //���̃I�u�W�F�N�g��Animator���擾
        animator.SetInteger("Motion", 0);                        //Animator��"Motion, 0"(����)��L���ɂ���
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 localPosition = setTransform.localPosition;//�I�u�W�F�N�g��
        Vector3 localAngle = setTransform.localEulerAngles;//
        localPosition.y = 0.0f;//
        localPosition.z = 0.0f;//
        localAngle.y = EnemyStatus.rotationY;//
        setTransform.localPosition = localPosition;       //���[�J�����W�ł̍��W��ݒ�
        setTransform.localEulerAngles = localAngle;       //

        //
        if(viewPointX < 1)
        {
            Move();
        }

        //�ړ���̃r���[�|�[�g���W���擾
        viewPointX = Camera.main.WorldToViewportPoint(this.transform.position).x;//��ʍ��W.X

        //�r���[�|�[�g���W.X��0���� || �̗͂�0�ȉ� && �r���[�|�[�g���W.X��0�����ł����
        if (viewPointX < 0 || hp <= 0 && viewPointX < 0)
        {
            Destroy(this.gameObject);//���̃I�u�W�F�N�g������
        }
    }

    //�����蔻��(OnTriggerEnter)
    void OnTriggerEnter(Collider collision)
    {
        //�^�OPlayer�̕t�����I�u�W�F�N�g�ɏՓ˂�����
        if (collision.gameObject.tag == "Player")
        {
            RandomAnimation();
        }
        //�^�OBullet�̕t�����I�u�W�F�N�g�ɏՓ˂�����
        if (collision.gameObject.tag == "Bullet" && this.tag != "Death")
        {
            Damage();//�֐�Damage���Ăяo��
        }
        //�^�ODelete�̕t�����I�u�W�F�N�g�ɏՓ˂�����
        if (collision.gameObject.tag == "Delete")
        {
            Destroy(this.gameObject);//���̃I�u�W�F�N�g������
        }
    }

    void Move()
    {
        //
        if (hp > 0 && isAttack == false)
        {
            this.transform.position += speed * transform.forward * Time.deltaTime;//�������Ɉړ�����
        }
        //
        else if (PlayerController.hp <= 0)
        {
            animator.SetInteger("Motion", 3);//Animator��"Motion, 3"(�_���X���[�V����)��L���ɂ���
        }
        //
        if (PlayerController.hp > 0 && isAttack == true)
        {
            coolTime += Time.deltaTime;//�N�[���^�C����Time.deltaTime�𑫂�

            //�����_����"1"��������
            if (random == 1)
            {
                //
                if (coolTime >= 2.0f)
                {
                    coolTime = 0.0f;                 //
                    animator.SetInteger("Motion", 0);//
                    isAttack = false;
                }
            }
            //�����_����"2"��������
            else if (random == 2)
            {
                //
                if (coolTime >= 1.5f)
                {
                    coolTime = 0.0f;
                    animator.SetInteger("Motion", 0);//
                    isAttack = false;
                }
            }
        }
    }

    //�����_���A�j���[�V�����֐�
    void RandomAnimation()
    {
        if (isAttack)
        {
            return;
        }

        isAttack = true;
        random = (int)Random.Range(1, 3);     //�����_��(1�`2)
        animator.SetInteger("Motion", random);//Animator��AttackMotion(1�`2)��L���ɂ���
        Debug.Log(random);                    //Debug.Log(random)
    }

    //�_���[�W����֐�
    void Damage()
    {
        hp -= 1;//�̗͂�-1����

        //�̗͂�0�ȉ���������
        if (hp <= 0)
        {
            GameManager.score += EnemyStatus.WalkEnemy.score;//
            this.tag = "Death";              //�^�O��Death�ɕύX����
            animator.SetInteger("Motion", 4);//
        }
    }
}
