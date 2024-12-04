using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkEnemy : MonoBehaviour
{
    //�X�e�[�^�X
    private int hp = EnemyList.WalkEnemy.hp;        //�G�̗̑�
    private float speed = EnemyList.WalkEnemy.speed;//�G�̈ړ����x
    //����
    private float viewPointX;        //�r���[�|�C���g���W.X
    private float interval = 0.0f;   //�Ԋu
    private int nowAnimation;        //���݂̃A�j���[�V����
    private bool isAnimation = false;//�A�j���[�V�����̉�
    //���̃I�u�W�F�N�g�̃R���|�[�l���g(public)
    public AudioClip damage;
    public AudioClip scream;
    //���̃I�u�W�F�N�g�̃R���|�[�l���g(private)
    private Transform thisTransform; //"Transform"
    private Animator animator = null;//"Animator"
    private AudioSource audioSource; //"AudioSource"

    // Start is called before the first frame update
    void Start()
    {
        //���̃I�u�W�F�N�g�̃R���|�[�l���g���擾
        thisTransform = this.gameObject.GetComponent<Transform>();//"Transform"
        animator = this.GetComponent<Animator>();                 //"Animator"
        audioSource = this.GetComponent<AudioSource>();           //"AudioSource"
        
        nowAnimation = EnemyList.HumanoidAnimation.walk;//"nowAnimation"��"walk(����)"�ɂ���        
        Animation();                                    //�֐�"Animation"�����s
    }

    // Update is called once per frame
    void Update()
    {
        //�r���[�|�[�g���W���擾
        viewPointX = Camera.main.WorldToViewportPoint(this.transform.position).x;//��ʍ��W.X

        //�̗͂�0���� && �r���[�|�[�g���W.X��1����ł����
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

    void Behavior()
    {
        Vector3 localPosition = thisTransform.localPosition;//�I�u�W�F�N�g��
        Vector3 localAngle = thisTransform.localEulerAngles;//
        localPosition.y = 0.0f;  //
        localPosition.z = 1.0f;  //
        localAngle.y = -EnemyList.rotation;//
        thisTransform.localPosition = localPosition;       //���[�J�����W�ł̍��W��ݒ�
        thisTransform.localEulerAngles = localAngle;       //

        //
        if (nowAnimation == EnemyList.HumanoidAnimation.walk)
        {
            this.transform.position += speed * transform.forward * Time.deltaTime;//�O�����Ɉړ�����
        }
        //
        else if (nowAnimation != EnemyList.HumanoidAnimation.walk && isAnimation == true)
        {
            Wait();
        }

        //
        if (PlayerController.hp <= 0 && isAnimation == false)
        {
            nowAnimation = EnemyList.HumanoidAnimation.dance;
            Animation();//�֐�"Animation"�����s
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
                    nowAnimation = EnemyList.HumanoidAnimation.walk;
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
                    nowAnimation = EnemyList.HumanoidAnimation.walk;
                    Animation();
                }
            }
        }
    }

    //�֐�"Damage"
    void Damage()
    {
        hp -= PlayerList.Player.power[GameManager.playerNumber];//

        //"hp"��"0"���ゾ������
        if(hp > 0)
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
        this.tag = "Untagged";                           //���̃^�O��"Untagged"�ɕύX����
        hp = 0;                                          //�G�̗̑͂�"0"�ɂ���
        GameManager.score += EnemyList.WalkEnemy.score;  //
        nowAnimation = EnemyList.HumanoidAnimation.death;
        audioSource.PlayOneShot(scream);
        Animation();                                     //�֐�"Animation"�����s
    }

    //�����蔻��(OnTriggerEnter)
    void OnTriggerEnter(Collider collision)
    {
        //�Փ˂����I�u�W�F�N�g�̃^�O��"Player" && "isAnimation"��"false"�̏ꍇ
        if (collision.gameObject.tag == "Player" && isAnimation == false)
        {
            isAnimation = true;
            nowAnimation = (int)Random.Range(EnemyList.HumanoidAnimation.punch, EnemyList.HumanoidAnimation.kick + 1);//�����_��"10(�p���`)"�`"12(�L�b�N)"
            Animation();
        }
        //�Փ˂����I�u�W�F�N�g�̃^�O��"Bullet"�̏ꍇ
        if (collision.gameObject.tag == "Bullet" && this.tag != "Untagged")
        {
            Damage();//�֐�Damage���Ăяo��
        }
    }
}
