using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkEnemy : MonoBehaviour
{
    //�X�e�[�^�X�ϐ�
    public int hp;     //�̗�
    public float speed;//�ړ����x

    private int random = 0;       //
    private float coolTime = 0.0f;//�N�[���^�C��
    private bool isAnimation = false;

    //�R���|�[�l���g�擾�ϐ�
    private Transform setTransform;  //Transform�ϐ�
    private Animator animator = null;//Animator�ϐ�
    

    // Start is called before the first frame update
    void Start()
    {
        setTransform = this.gameObject.GetComponent<Transform>();//���̃I�u�W�F�N�g��Transform���擾
        animator = this.GetComponent<Animator>();                //���̃I�u�W�F�N�g��Animator���擾
        animator.SetBool("Walk", true);                          //Animator��Walk(���s���[�V����)��L��������
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
        if (hp > 0 && isAnimation == false)
        {
            this.transform.position += speed * transform.forward * Time.deltaTime;//�������Ɉړ�����
        }
        //
        else if (GameManager.playerHp <= 0)
        {
            animator.SetBool("Dance", true);//Animator��Dance(�_���X���[�V����)��L��������
        }
        //
        if (GameManager.playerHp > 0 && isAnimation == true)
        {
            coolTime += Time.deltaTime;//�N�[���^�C����Time.deltaTime�𑫂�

            //
            if(random == 1)
            {
                //
                if (coolTime >= 2.0f)
                {
                    coolTime = 0.0f;                       //
                    animator.SetInteger("AttackMotion", 0);//
                    animator.SetBool("Walk", true);        //Animator��Walk(���s���[�V����)��L��������
                    isAnimation = false;
                }
            }
            //
            else if(random == 2)
            {
                //
                if (coolTime >= 1.5f)
                {
                    coolTime = 0.0f;
                    animator.SetInteger("AttackMotion", 0);//
                    animator.SetBool("Walk", true);        //Animator��Walk(���s���[�V����)��L��������
                    isAnimation = false;
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
        if (collision.gameObject.tag == "Bullet")
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
        if (isAnimation)
        {
            return;
        }

        isAnimation = true;
        random = (int)Random.Range(1, 3);           //�����_������(1�`2)
        animator.SetBool("Walk", false);            //Animator��Walk(���s���[�V����)�𖳌�������
        animator.SetInteger("AttackMotion", random);//Animator��AttackMotion(1�`2)��L��������
        Debug.Log(random);                          //Debug.Log(random)
    }

    //�_���[�W����֐�
    void Damage()
    {
        hp -= 1;//�̗͂�-1����

        //�̗͂�0�ȉ���������
        if (hp <= 0)
        {
            this.tag = "Death";             //�^�O��Death�ɕύX����
            animator.SetBool("Walk", false);//Animator��Walk(���s���[�V����)�𖳌�������
            animator.SetBool("Death", true);//Animator��Death(���S���̃��[�V����)��L��������
        }
    }
}
