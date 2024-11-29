using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarEnemy : MonoBehaviour
{
    //
    public GameObject carRideEnemy;
    //�X�e�[�^�X
    private int hp = EnemyStatus.CarEnemy.hp;        //�̗�
    private float speed = EnemyStatus.CarEnemy.speed;//�ړ����x
    //����
    private float viewPointX;        //�r���[�|�C���g���W.X
    private bool isAnimation = false;//�A�j���[�V�����̉�
    private bool carExit = false;    //
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
        //�r���[�|�[�g���W���擾
        viewPointX = Camera.main.WorldToViewportPoint(this.transform.position).x;//��ʍ��W.X

        //�̗͂�0���� && �r���[�|�[�g���W.X��1����ł����
        if (hp > 0 && viewPointX < 1)
        {
            Behavior();//�s���֐�"Behavior"�����s����
        }
        //(�̗͂�"0�ȉ�" && �r���[�|�[�g���W.X��"0����"�ł����
        else if (hp <= 0 || viewPointX < 0)
        {
            Destroy();//�֐�"Destroy"�����s����
        }
    }

    //�֐�"Behavior"
    void Behavior()
    {
        if(this.transform.position.x + EnemyStatus.CarEnemy.rangeX > playerTransform.position.x &&
            this.transform.position.x - EnemyStatus.CarEnemy.rangeX < playerTransform.position.x &&
            isAnimation == false)
        {
            isAnimation = true;
        }
        else if (this.transform.position.z > playerTransform.position.z && isAnimation == true)
        {
            this.transform.position += speed * transform.forward * Time.deltaTime;//�O�����Ɉړ�����
        }
        else if(this.transform.position.z <= playerTransform.position.z && carExit == false)
        {
            Instantiate(carRideEnemy, this.transform.position, this.transform.rotation);
            carExit = true;
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
        GameManager.score += EnemyStatus.WalkEnemy.score;//
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
