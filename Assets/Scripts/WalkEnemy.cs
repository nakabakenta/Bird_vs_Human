using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkEnemy : MonoBehaviour
{
    //�X�e�[�^�X�ϐ�
    public int hp;     //�̗�
    public float speed;//�ړ����x

    private float coolTime = 0.0f;//�N�[���^�C��
    private bool waik = true;    //���s�t���O

    //�R���|�[�l���g�擾�ϐ�
    private Animator animator = null;       //Animator�ϐ�

    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();//���̃I�u�W�F�N�g��Animator���擾
        animator.SetBool("Walk", true);//Animator��Walk(���s���[�V����)��L��������
    }

    // Update is called once per frame
    void Update()
    {
        //
        if (hp > 0 && waik == true)
        {
            this.transform.position += speed * transform.forward * Time.deltaTime;//�������Ɉړ�����
        }
        //
        else if(PlayerController.hp <= 0)
        {
            waik = false;
            animator.SetBool("Dance", true);//Animator��Dance(�_���X���[�V����)��L��������
        }
        //
       if(waik == false)
        {
            coolTime += Time.deltaTime;//�N�[���^�C����Time.deltaTime�𑫂�

            if(coolTime >= 3.5f)
            {
                coolTime = 0.0f;
                animator.SetBool("Attack", false);//Animator��Attack(�U�����[�V����)�𖳌�������
                animator.SetBool("Walk", true);   //Animator��Walk(���s���[�V����)�𖳌�������
                waik = true;
            }
        }
    }

    //�����蔻��(OnTriggerEnter)
    void OnTriggerEnter(Collider collision)
    {
        //�^�OPlayer�̕t�����I�u�W�F�N�g�ɏՓ˂�����
        if (collision.gameObject.tag == "Player")
        {
            waik = false;
            animator.SetBool("Walk", false); //Animator��Walk(���s���[�V����)�𖳌�������
            animator.SetBool("Attack", true);//Animator��Attack(�U�����[�V����)��L��������
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

    //�_���[�W����
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
