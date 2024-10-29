using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunEnemy : MonoBehaviour
{
    //�X�e�[�^�X
    private int hp = EnemyStatus.RunEnemy.hp;        //�̗�
    private float speed = EnemyStatus.RunEnemy.speed;//�ړ����x
    //
    private int random = 0;       //�����_��
    private float coolTime = 0.0f;//�N�[���^�C��
    private bool isAttack = false;//�U�������ǂ���

    //�R���|�[�l���g�擾�ϐ�
    private Transform setTransform;   //Transform
    private Transform playerTransform;//Transform(�v���C���[)
    private Animator animator = null; //Animator

    // Start is called before the first frame update
    void Start()
    {
        setTransform = this.gameObject.GetComponent<Transform>();//���̃I�u�W�F�N�g��Transform���擾
        animator = this.GetComponent<Animator>();                //���̃I�u�W�F�N�g��Animator���擾
        animator.SetInteger("Motion", 0);                        //Animator��"Motion 0"(����)��L���ɂ���
        playerTransform = GameObject.Find("Chickadee_Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 localPosition = setTransform.localPosition;//�I�u�W�F�N�g��
        Vector3 localAngle = setTransform.localEulerAngles;//
        localPosition.x = 0.0f;//
        localPosition.y = 0.0f;//
        localAngle.y = 180.0f; //
        setTransform.localPosition = localPosition;       //���[�J�����W�ł̍��W��ݒ�
        setTransform.localEulerAngles = localAngle;       //

        //
        if (hp > 0 && isAttack == false && this.transform.position.z > playerTransform.position.z)
        {
            this.transform.position += speed * transform.forward * Time.deltaTime;//�������Ɉړ�����
        }
        //
        else if (hp > 0 && isAttack == false && this.transform.position.z < playerTransform.position.z)
        {
            this.transform.position -= speed * transform.forward * Time.deltaTime;
        }
        //
        else if (hp > 0 && isAttack == false && this.transform.position.z == playerTransform.position.z)
        {
            animator.SetInteger("Motion", 10);//Animator��Motion 3(�_���X���[�V����)��L���ɂ���
        }
        //
        else if (PlayerController.hp <= 0)
        {
            animator.SetInteger("Motion", 3);//Animator��Motion 3(�_���X���[�V����)��L���ɂ���
        }
        //
        if (PlayerController.hp > 0 && isAttack == true)
        {
            coolTime += Time.deltaTime;//�N�[���^�C����Time.deltaTime�𑫂�

            //
            if(random == 1)
            {
                //
                if (coolTime >= 2.0f)
                {
                    coolTime = 0.0f;                       //
                    animator.SetInteger("Motion", 0);//
                    isAttack = false;
                }
            }
            //
            else if(random == 2)
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

    //�����蔻��(OnTriggerEnter)
    void OnTriggerEnter(Collider collision)
    {
        //�^�OPlayer�̕t�����I�u�W�F�N�g�ɏՓ˂�����
        if (collision.gameObject.tag == "Player")
        {
            Animation();
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

    //�A�j���[�V�����֐�
    void Animation()
    {
        if (isAttack)
        {
            return;
        }

        isAttack = true;
        random = (int)Random.Range(1, 3);     //�����_������(1�`2)
        animator.SetInteger("Motion", random);//Animator��AttackMotion(1�`2)��L��������
        Debug.Log(random);                    //Debug.Log(random)
    }

    //�_���[�W����֐�
    void Damage()
    {
        hp -= 1;//�̗͂�-1����

        //�̗͂�0�ȉ���������
        if (hp <= 0)
        {
            GameManager.score += EnemyStatus.RunEnemy.score;//
            this.tag = "Death";                             //�^�O��Death�ɕύX����
            animator.SetInteger("Motion", 4);               //
        }
    }
}
