using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterJetEnemy : MonoBehaviour
{
    public GameObject enemyBullet;
    //�X�e�[�^�X
    private int hp = EnemyList.FighterJetEnemy.hp;        //�̗�
    private float speed = EnemyList.FighterJetEnemy.speed;//�ړ����x
    //����
    public float attackTimer = 0.25f;   //�U���Ԋu�^�C�}�[
    public float attackInterval = 0.25f;//�U���Ԋu
    private float viewPointX;           //�r���[�|�C���g���W.X
    //���̃I�u�W�F�N�g�̃R���|�[�l���g
    private Transform playerTransform;//"Transform(�v���C���[)"

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.Find("Player").transform;//�Q�[���I�u�W�F�N�g"Player"��T����"Transform"���擾
    }

    // Update is called once per frame
    void Update()
    {
        attackTimer += Time.deltaTime;

        //�r���[�|�[�g���W���擾
        viewPointX = Camera.main.WorldToViewportPoint(this.transform.position).x;//��ʍ��W.X

        //�̗͂�0���� && �r���[�|�[�g���W.X��1����ł����
        if (hp > 0)
        {
            Behavior();//�s���֐�"Behavior"�����s����
        }
        //(�̗͂�"0�ȉ�" && �r���[�|�[�g���W.X��"0����"�ł����
        else if (hp <= 0 && viewPointX < 0)
        {
            Destroy();//�֐�"Destroy"�����s����
        }
    }

    //�֐�"Behavior"
    void Behavior()
    {
        this.transform.position += speed * transform.forward * Time.deltaTime;//�O�����Ɉړ�����

        if (attackTimer > attackInterval)
        {
            Instantiate(enemyBullet, this.transform.position, Quaternion.Euler(this.transform.rotation.x, this.transform.rotation.y, 90));
            attackTimer = 0.0f;
        }
    }

    //�֐�"Damage"
    void Damage()
    {
        hp -= 1;//�̗͂�"-1"����

        //�̗͂�0�ȉ���������
        if (hp <= 0)
        {
            Death();//�֐�"Death"���S���Ăяo��
        }
    }

    //�֐�"Death"
    void Death()
    {
        hp = 0;                                          //�̗͂�"0"�ɂ���
        GameManager.score += EnemyList.WalkEnemy.score;//
        this.tag = "Death";                              //���̃^�O��"Death"�ɕύX����
    }

    //�֐�"Destroy"
    void Destroy()
    {
        Destroy(this.gameObject);//���̃I�u�W�F�N�g������
    }

    //�����蔻��(OnTriggerEnter)
    void OnTriggerEnter(Collider collision)
    {
        //�^�OBullet�̕t�����I�u�W�F�N�g�ɏՓ˂�����
        if (collision.gameObject.tag == "Bullet" && this.tag != "Death")
        {
            Damage();//�֐�Damage���Ăяo��
        }
    }
}
