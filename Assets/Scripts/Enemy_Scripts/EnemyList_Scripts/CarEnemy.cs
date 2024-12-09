using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarEnemy : MonoBehaviour
{
    //�X�e�[�^�X
    private int hp;     //�̗�
    private float speed;//�ړ����x
    //����
    private float viewPointX;              //�r���[�|�C���g���W.X
    private bool isAction = false;         //�s���̉�
    private bool carExit = false;          //
    private delegate void ActionDelegate();//
    private ActionDelegate nowAction;      //
    //���̃I�u�W�F�N�g�̃R���|�[�l���g
    public GameObject enemy;        //"GameObject(�G)"
    public GameObject effect;       //"GameObject(�G�t�F�N�g)"
    public AudioClip damage;        //"AudioClip(�_���[�W)"
    public AudioClip brake;         //"AudioClip(�u���[�L)"
    public AudioClip horn;          //"AudioClip(�N���N�V����)"
    public AudioClip explosion;     //"AudioClip(����)"
    private Transform thisTransform;//"Transform"
    private AudioSource audioSource;//"AudioSource"
    //���̃I�u�W�F�N�g�̃R���|�[�l���g
    private Transform playerTransform;//"Transform(�v���C���[)"

    // Start is called before the first frame update
    void Start()
    {
        carExit = false;

        //�X�e�[�^�X��ݒ�
        hp = EnemyList.CarEnemy.hp;      //�̗�                       
        speed = EnemyList.CarEnemy.speed;//�ړ����x
        //���̃I�u�W�F�N�g�̃R���|�[�l���g���擾
        thisTransform = this.GetComponent<Transform>();//"Transform"
        audioSource = this.GetComponent<AudioSource>();//"AudioSource"
        //���̃I�u�W�F�N�g�̃R���|�[�l���g���擾
        playerTransform = GameObject.Find("Player").transform;//"Transform(�v���C���[)"
        //
        Direction();
    }                                   

    // Update is called once per frame
    void Update()
    {
        //���̃I�u�W�F�N�g�̃r���[�|�[�g���W���擾
        viewPointX = Camera.main.WorldToViewportPoint(this.transform.position).x;//��ʍ��W.X

        if (isAction == false)
        {
            //"viewPointX < 1 && nowAction == Vertical"�̏ꍇ
            if (viewPointX < 0.6 && nowAction == Vertical)
            {
                isAction = true;
            }
            //"viewPointX < 1 && nowAction == Horizontal"�̏ꍇ
            else if (viewPointX < 1.2 && nowAction == Horizontal)
            {
                isAction = true;
            }
        }

        //"hp > 0 && isAction == true"
        if (hp > 0 && isAction == true)
        {
            nowAction();//�֐�"Action"�����s
        }

        //"viewPointX < 0"�̏ꍇ
        if (viewPointX < 0)
        {
            Destroy();//�֐�"Destroy"�����s
        }
    }

    void Direction()
    {
        //
        if (this.transform.position.z > playerTransform.position.z + 0.5f)
        {
            nowAction = Vertical;//
        }
        //
        else if (this.transform.position.z >= playerTransform.position.z - 0.5f &&
                 this.transform.position.z <= playerTransform.position.z + 0.5f)
        {
            nowAction = Horizontal;//
        }
    }

    //�֐�"Vertical"
    void Vertical()
    {
        if (this.transform.position.z > playerTransform.position.z)
        {
            this.transform.position += speed * transform.forward * Time.deltaTime;//�O�����Ɉړ�����
        }
        else if (this.transform.position.z <= playerTransform.position.z && carExit == false)
        {
            Instantiate(enemy, this.transform.position, this.transform.rotation);
            audioSource.PlayOneShot(horn);
            audioSource.PlayOneShot(brake);
            carExit = true;
        }
    }

    //�֐�"Horizontal"
    void Horizontal()
    {
        this.transform.position += speed * transform.forward * Time.deltaTime;//�O�����Ɉړ�����
    }

    //�֐�"Damage"
    void Damage()
    {
        hp -= PlayerList.Player.power[GameManager.playerNumber];//

        //"hp > 0"�̏ꍇ
        if (hp > 0)
        {
            audioSource.PlayOneShot(damage);
        }
        //"hp <= 0"�̏ꍇ
        else if (hp <= 0)
        {
            Invoke("Death", 0.01f);//�֐�"Death"��"0.01f"��Ɏ��s
        }
    }

    //�֐�"Death"
    void Death()
    {
        this.tag = "Untagged";                         //����"this.tag == Untagged"�ɂ���
        hp = 0;                                        //"hp"��"0"�ɂ���
        GameManager.score += EnemyList.WalkEnemy.score;//"score"�𑫂�

        //
        Instantiate(effect, this.transform.position, this.transform.rotation, thisTransform);
        audioSource.PlayOneShot(explosion);//"explosion"��炷
    }

    //�֐�"Destroy"
    void Destroy()
    {
        Destroy(this.gameObject);//���̃I�u�W�F�N�g������
    }

    //�����蔻��(OnTriggerEnter)
    void OnTriggerEnter(Collider collision)
    {
        //�Փ˂����I�u�W�F�N�g�̃^�O��"Bullet && hp > 0"�̏ꍇ
        if (collision.gameObject.tag == "Bullet" && hp > 0)
        {
            Damage();//�֐�"Damage"�����s
        }
    }
}
