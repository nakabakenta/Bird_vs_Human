using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunEnemy : MonoBehaviour
{
    //�X�e�[�^�X
    private int hp = EnemyList.RunEnemy.hp;        //�̗�
    private float speed = EnemyList.RunEnemy.speed;//�ړ����x
    private float jump = EnemyList.RunEnemy.jump;  //�W�����v��
    //����
    private float viewPointX;        //�r���[�|�C���g���W.X
    private float interval = 0.0f;   //�Ԋu
    private int nowAnimation;        //���݂̃A�j���[�V����
    private bool isAnimation = false;//�A�j���[�V�����̉�
    //���̃I�u�W�F�N�g�̃R���|�[�l���g(public)
    public AudioClip damage;
    public AudioClip scream;
    //���̃I�u�W�F�N�g�̃R���|�[�l���g(private)
    private Animator animator = null;//"Animator"
    private AudioSource audioSource; //"AudioSource"
    //���̃I�u�W�F�N�g�̃R���|�[�l���g(private)
    private Transform playerTransform;//"Transform"(�v���C���[)

    // Start is called before the first frame update
    void Start()
    {
        //���̃I�u�W�F�N�g�̃R���|�[�l���g���擾
        animator = this.GetComponent<Animator>();      //"Animator"
        audioSource = this.GetComponent<AudioSource>();//"AudioSource"
        //���̃I�u�W�F�N�g�̃R���|�[�l���g���擾
        playerTransform = GameObject.Find("Player").transform;//"Transform"(�v���C���[)
        //
        nowAnimation = EnemyList.HumanoidAnimation.run;//"nowAnimation"��"walk(����)"�ɂ���
        Animation();                                   //�֐�"Animation"�����s
    }

    // Update is called once per frame
    void Update()
    {
        //���̃I�u�W�F�N�g�̃r���[�|�[�g���W���擾
        viewPointX = Camera.main.WorldToViewportPoint(this.transform.position).x;//�r���[�|�[�g���W.X

        //"hp"��"0"���� && "viewPointX"��"1"����ł����
        if (hp > 0 && viewPointX < 1)
        {
            Behavior();//�s���֐����Ăяo��
        }
        //�̗͂�0�ȉ� && �r���[�|�[�g���W.X��0�����ł����
        else if (hp <= 0 && viewPointX < 0)
        {
            Destroy(this.gameObject);//���̃I�u�W�F�N�g������
        }
    }

    //�֐�"Behavior"
    void Behavior()
    {
        if (this.transform.position.x + EnemyList.RunEnemy.rangeX > playerTransform.position.x &&
            this.transform.position.x - EnemyList.RunEnemy.rangeX < playerTransform.position.x &&
            this.transform.position.y + EnemyList.RunEnemy.rangeY < playerTransform.position.y &&
            this.transform.position.y == 0.0f && nowAnimation == EnemyList.HumanoidAnimation.run && 
            isAnimation == false)
        {
            isAnimation = true;
            nowAnimation = EnemyList.HumanoidAnimation.jump;
            Animation();
        }

        if(nowAnimation == EnemyList.HumanoidAnimation.run)
        {
            this.transform.position = new Vector3(this.transform.position.x, 0.0f, 1.0f);
        }
        //
        if (this.transform.position.x > playerTransform.position.x)
        {
            this.transform.eulerAngles = new Vector3(this.transform.rotation.x, -EnemyList.rotation, this.transform.rotation.z);
        }
        //
        else if (this.transform.position.x < playerTransform.position.x)
        {
            this.transform.eulerAngles = new Vector3(this.transform.rotation.x, EnemyList.rotation, this.transform.rotation.z);
        }

        //
        if (nowAnimation == EnemyList.HumanoidAnimation.run)
        {
            this.transform.position += speed * transform.forward * Time.deltaTime;//�O�����Ɉړ�����
        }
        //
        else if (nowAnimation != EnemyList.HumanoidAnimation.run && isAnimation == true)
        {
            Wait();
        }

        //
        if (PlayerController.hp <= 0 && isAnimation == false)
        {
            nowAnimation = EnemyList.HumanoidAnimation.dance;
            Animation();//�A�j���[�V�����֐������s
        }
    }

    //�֐�"Animation"
    void Animation()
    {
        animator.SetInteger("Motion", nowAnimation);//"animator(Motion)"��"nowAnimation"��ݒ肷��
    }

    //�֐�"Wait"
    void Wait()
    {
        interval += Time.deltaTime;//�N�[���^�C����Time.deltaTime�𑫂�

        if (nowAnimation == EnemyList.HumanoidAnimation.punch ||
            nowAnimation == EnemyList.HumanoidAnimation.kick)
        {
            //
            if (nowAnimation == EnemyList.HumanoidAnimation.punch)
            {
                //
                if (interval >= 2.0f)
                {
                    interval = 0.0f;
                    isAnimation = false;
                    nowAnimation = EnemyList.HumanoidAnimation.run;
                    Animation();
                }
            }
            //
            else if (nowAnimation == EnemyList.HumanoidAnimation.kick)
            {
                //
                if (interval >= 1.5f)
                {
                    interval = 0.0f;
                    isAnimation = false;
                    nowAnimation = EnemyList.HumanoidAnimation.run;
                    Animation();
                }
            }
        }
        //
        else if(nowAnimation == EnemyList.HumanoidAnimation.jump)
        {
            if (interval >= 0.75f)
            {
                this.transform.position += jump * transform.up * Time.deltaTime;

                if (interval >= 2.0f)
                {
                    animator.SetFloat("MoveSpeed", 1.0f);            //"animator(MoveSpeed)"��"1.0f(�Đ�)"�ɂ���
                    interval = 0.0f;
                    isAnimation = false;
                    nowAnimation = EnemyList.HumanoidAnimation.run;
                    Animation();
                }
                else if (interval >= 1.0f)
                {
                    animator.SetFloat("MoveSpeed", 0.0f);            //"animator(MoveSpeed)"��"0.0f(��~)"�ɂ���
                }
            }
        }
    }

    //�֐�"Damage"
    void Damage()
    {
        hp -= PlayerList.Player.power[GameManager.playerNumber];//

        //"hp"��"0"���ゾ������
        if (hp > 0)
        {
            audioSource.PlayOneShot(damage);
        }
        //"hp"��"0"�ȉ���������
        else if (hp <= 0)
        {
            Invoke("Death", 0.01f);//�֐�"Death"��"0.01f"��Ɏ��s
        }
    }

    //�֐�"Death"
    void Death()
    {
        this.transform.position = new Vector3(this.transform.position.x, 0.0f, this.transform.position.z);//
        this.tag = "Untagged";                           //���̃^�O��"Untagged"�ɕύX����
        hp = 0;                                          //"hp"��"0"�ɂ���
        GameManager.score += EnemyList.WalkEnemy.score;  //"score"�𑫂�
        nowAnimation = EnemyList.HumanoidAnimation.death;//"nowAnimation"��"death(���S)"�ɂ���
        audioSource.PlayOneShot(scream);                 //"scream"��炷
        Animation();                                     //�֐�"Animation"�����s
    }

    //�����蔻��(OnTriggerEnter)
    void OnTriggerEnter(Collider collision)
    {
        //�Փ˂����I�u�W�F�N�g�̃^�O��"Player" && "action"��"false"��������
        if (collision.gameObject.tag == "Player" && isAnimation == false)
        {
            isAnimation = true;
            nowAnimation = (int)Random.Range(EnemyList.HumanoidAnimation.punch, EnemyList.HumanoidAnimation.kick + 1);//�����_��"10(�p���`)"�`"12(�L�b�N)"
            Animation();
        }
        //�^�OBullet�̕t�����I�u�W�F�N�g�ɏՓ˂�����
        if (collision.gameObject.tag == "Bullet" && hp > 0)
        {
            Damage();//�֐�Damage���Ăяo��
        }
    }
}