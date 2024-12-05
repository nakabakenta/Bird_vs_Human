using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterJetEnemy : MonoBehaviour
{
    //�X�e�[�^�X
    private int hp = EnemyList.FighterJetEnemy.hp;        //�̗�
    private float speed = EnemyList.FighterJetEnemy.speed;//�ړ����x
    //����
    private float attackTimer = 0.5f;   //�U���Ԋu�^�C�}�[
    private float attackInterval = 0.5f;//�U���Ԋu
    private float viewPointX;           //�r���[�|�C���g���W.X
    private float bulletRotation;       //���˂���e�̕���
    //���̃I�u�W�F�N�g�̃R���|�[�l���g
    public GameObject bullet;       //"GameObject(�e)"
    public GameObject effect;       //"GameObject(�G�t�F�N�g)"
    public AudioClip damage;        //"AudioClip(�_���[�W)"
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
        attackTimer += Time.deltaTime;//�U���Ԋu��"Time.deltaTime(�o�ߎ���)"�𑫂�

        if (viewPointX < -0.5)
        {
            this.transform.position = new Vector3(this.transform.position.x, playerTransform.position.y, this.transform.position.z);
            this.transform.eulerAngles = new Vector3(this.transform.rotation.x, EnemyList.rotation, this.transform.rotation.z);
            bulletRotation = -EnemyList.rotation;
        }
        else if (viewPointX > 1.5)
        {
            this.transform.position = new Vector3(this.transform.position.x, playerTransform.position.y, this.transform.position.z);
            this.transform.eulerAngles = new Vector3(this.transform.rotation.x, -EnemyList.rotation, this.transform.rotation.z);
            bulletRotation = EnemyList.rotation;
        }

        this.transform.position += speed * transform.forward * Time.deltaTime;//�O�����Ɉړ�����

        if (attackTimer > attackInterval)
        {
            Instantiate(bullet, this.transform.position, Quaternion.Euler(this.transform.rotation.x, this.transform.rotation.y, bulletRotation));
            attackTimer = 0.0f;
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
        audioSource.PlayOneShot(explosion);            //"explosion"��炷
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
