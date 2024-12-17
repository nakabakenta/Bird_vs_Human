using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaveGunEnemy : MonoBehaviour
{
    //�X�e�[�^�X
    private int hp;     //�̗�
    private float speed;//�ړ����x
    //����
    private float viewPointX;              //�r���[�|�C���g���W.X
    private bool isAction = false;         //�s���̉�
    private int nowAnimation;              //���݂̃A�j���[�V����
    private float animationTimer = 0.0f;   //�A�j���[�V�����^�C�}�[
    private bool isAnimation = false;      //�A�j���[�V�����̉�
    //���̃I�u�W�F�N�g�̃R���|�[�l���g
    public GameObject bullet;
    public AudioClip damage;         //"AudioClip(�_���[�W)"
    public AudioClip scream;         //"AudioClip(���ѐ�)"
    private Animator animator = null;//"Animator"
    private AudioSource audioSource; //"AudioSource"
    //���̃I�u�W�F�N�g�̃R���|�[�l���g
    private Transform playerTransform;//"Transform(�v���C���[)"

    // Start is called before the first frame update
    void Start()
    {
        //�X�e�[�^�X��ݒ�
        hp = EnemyList.HaveGunEnemy.hp;      //�̗�
        speed = EnemyList.HaveGunEnemy.speed;//�ړ����x
        isAnimation = true;
        //���̃I�u�W�F�N�g�̃R���|�[�l���g���擾
        animator = this.GetComponent<Animator>();      //"Animator"
        audioSource = this.GetComponent<AudioSource>();//"AudioSource"
        //���̃I�u�W�F�N�g�̃R���|�[�l���g���擾
        playerTransform = GameObject.Find("Player").transform;//"Transform(�v���C���[)"
        nowAnimation = EnemyList.HumanoidAnimation.gunPlay;   //        
        Animation();                                          //�֐�"Animation"�����s
    }

    // Update is called once per frame
    void Update()
    {
        //���̃I�u�W�F�N�g�̃r���[�|�[�g���W���擾
        viewPointX = Camera.main.WorldToViewportPoint(this.transform.position).x;//��ʍ��W.X

        if (isAction == true)
        {
            Horizontal();
        }
        else if (isAction == false)
        {
            if (viewPointX < 1)
            {
                isAction = true;
            }
        }

        //"viewPointX < 0"�̏ꍇ
        if (viewPointX < 0)
        {
            Destroy();//�֐�"Destroy"�����s
        }
    }

    //�֐�"Horizontal"
    void Horizontal()
    {
        //
        if (isAnimation == true)
        {
            Wait();//�֐�"Wait"�����s
        }
        else if (isAnimation == false)
        {
            //
            if (PlayerController.hp > 0)
            {
               
            }
            else if (PlayerController.hp <= 0)
            {
                nowAnimation = EnemyList.HumanoidAnimation.dance;
                Animation();//�֐�"Animation"�����s
            }
        }

        this.transform.position = new Vector3(this.transform.position.x, 0.0f, playerTransform.position.z);
    }

    //�֐�"Animation"
    void Animation()
    {
        animator.SetInteger("Motion", nowAnimation);//"animator(Motion)"��"nowAnimation"��ݒ肵�čĐ�
    }

    //�֐�"Wait"
    void Wait()
    {
        animationTimer += Time.deltaTime;//" animationTimer"��"Time.deltaTime(�o�ߎ���)"�𑫂�

        //
        if (nowAnimation == EnemyList.HumanoidAnimation.punch)
        {
            //
            if (animationTimer >= 2.12f)
            {
                animationTimer = 0.0f;
                isAnimation = false;
            }
        }
        //
        else if (nowAnimation == EnemyList.HumanoidAnimation.kick)
        {
            //
            if (animationTimer >= 1.15f)
            {
                animationTimer = 0.0f;
                isAnimation = false;
            }
        }
        //
        else if (nowAnimation == EnemyList.HumanoidAnimation.gunPlay)
        {
            if (animationTimer >= 0.34f)
            {
                Instantiate(bullet, this.transform.position, Quaternion.Euler(this.transform.rotation.x, this.transform.rotation.y, 90));
                animationTimer = 0.0f;
                Animation();//�֐�"Animation"�����s
            }
        }
        //
        else if(nowAnimation == EnemyList.HumanoidAnimation.damage)
        {
            //
            if (animationTimer >= 1.13f)
            {
                animationTimer = 0.0f;
                isAnimation = false;
            }
        }
    }

    //�֐�"Damage"
    void Damage()
    {
        hp -= PlayerList.Player.power[GameManager.playerNumber];//

        //"hp > 0"�̏ꍇ
        if (hp > 0)
        {
            isAnimation = true;                               //"isAnimation = true"�ɂ���
            nowAnimation = EnemyList.HumanoidAnimation.damage;//"nowAnimation = damage(�_���[�W)"�ɂ���
            audioSource.PlayOneShot(damage);                  //"damage"��炷
            Animation();                                      //�֐�"Animation"�����s
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
        this.tag = "Untagged";                           //"this.tag = Untagged"�ɂ���
        hp = 0;                                          //"hp = 0"�ɂ���
        GameManager.score += EnemyList.WalkEnemy.score;  //"score"�𑫂�
        nowAnimation = EnemyList.HumanoidAnimation.death;//"nowAnimation = death(���S)"�ɂ���
        audioSource.PlayOneShot(scream);                 //"scream"��炷
        Animation();                                     //�֐�"Animation"�����s
    }

    //�֐�"Destroy"
    void Destroy()
    {
        Destroy(this.gameObject);//���̃I�u�W�F�N�g������
    }

    //�����蔻��(OnTriggerEnter)
    void OnTriggerEnter(Collider collision)
    {
        //�Փ˂����I�u�W�F�N�g��"tag == Player" && "isAnimation == false"�̏ꍇ
        if (collision.gameObject.tag == "Player" && isAnimation == false)
        {
            isAnimation = true;//"isAnimation = true"�ɂ���

            //�����_��"10(�p���`)"�`"12(�L�b�N)"
            nowAnimation = (int)Random.Range(EnemyList.HumanoidAnimation.punch,
                                             EnemyList.HumanoidAnimation.kick + 1);
            Animation();//�֐�"Animation"�����s
        }
        //�Փ˂����I�u�W�F�N�g�̃^�O��"Bullet" && "hp > 0"�̏ꍇ
        if (collision.gameObject.tag == "Bullet" && hp > 0)
        {
            Damage();//�֐�"Damage"�����s
        }
    }
}