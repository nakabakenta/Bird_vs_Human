using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarEnemy : MonoBehaviour
{
    //�X�e�[�^�X
    private int hp = EnemyList.CarEnemy.hp;        //�̗�
    private float speed = EnemyList.CarEnemy.speed;//�ړ����x
    //����
    private float viewPointX;        //�r���[�|�C���g���W.X
    private bool isAnimation = false;//�A�j���[�V�����̉�
    private bool carExit = false;    //
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
        //���̃I�u�W�F�N�g�̃R���|�[�l���g���擾
        thisTransform = this.GetComponent<Transform>();//"Transform"
        audioSource = this.GetComponent<AudioSource>();//"AudioSource"
        //���̃I�u�W�F�N�g�̃R���|�[�l���g���擾
        playerTransform = GameObject.Find("Player").transform;//"Transform(�v���C���[)"
    }

    // Update is called once per frame
    void Update()
    {
        //���̃I�u�W�F�N�g�̃r���[�|�[�g���W���擾
        viewPointX = Camera.main.WorldToViewportPoint(this.transform.position).x;//��ʍ��W.X

        //"hp > 0" && "viewPointX < 1"�̏ꍇ
        if (hp > 0 && viewPointX < 1)
        {
            Behavior();//�֐�"Behavior"�����s
        }
        //"hp <= 0" && "viewPointX < 0"�̏ꍇ
        else if (hp <= 0 && viewPointX < 0)
        {
            Destroy();//�֐�"Destroy"�����s
        }
    }

    //�֐�"Behavior"
    void Behavior()
    {
        if(this.transform.position.x + EnemyList.CarEnemy.rangeX > playerTransform.position.x &&
            this.transform.position.x - EnemyList.CarEnemy.rangeX < playerTransform.position.x &&
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
            Instantiate(enemy, this.transform.position, this.transform.rotation);
            audioSource.PlayOneShot(horn);
            audioSource.PlayOneShot(brake);
            carExit = true;
        }
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
        //�Փ˂����I�u�W�F�N�g�̃^�O��"Bullet" && "hp > 0"�̏ꍇ
        if (collision.gameObject.tag == "Bullet" && hp > 0)
        {
            Damage();//�֐�"Damage"�����s
        }
    }
}
