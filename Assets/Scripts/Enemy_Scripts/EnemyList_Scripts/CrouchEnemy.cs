using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchEnemy : MonoBehaviour
{
    //�X�e�[�^�X
    private int hp = EnemyList.CrouchEnemy.hp;//�̗�
    //����
    private float viewPointX;           //�r���[�|�C���g���W.X
    private int nowAnimation;           //���݂̃A�j���[�V����
    private float animationTimer = 0.0f;//�A�j���[�V�����^�C�}�[
    private bool isAnimation = false;   //�A�j���[�V�����̉�
    //���̃I�u�W�F�N�g�̃R���|�[�l���g
    public AudioClip damage;         //"AudioClip(�_���[�W)"
    public AudioClip scream;         //"AudioClip(���ѐ�)"
    private Animator animator = null;//"Animator"
    private AudioSource audioSource; //"AudioSource"

    // Start is called before the first frame update
    void Start()
    {
        //���̃I�u�W�F�N�g�̃R���|�[�l���g���擾
        animator = this.GetComponent<Animator>();      //"Animator"
        audioSource = this.GetComponent<AudioSource>();//"AudioSource"
        //
        nowAnimation = EnemyList.HumanoidAnimation.crouch;//"nowAnimation = crouch(���Ⴊ��)"�ɂ���
        Animation();                                      //�֐�"Animation"�����s
    }

    // Update is called once per frame
    void Update()
    {
        //���̃I�u�W�F�N�g�̃r���[�|�[�g���W���擾
        viewPointX = Camera.main.WorldToViewportPoint(this.transform.position).x;//��ʍ��W.X

        //"viewPointX < 0"�̏ꍇ
        if (viewPointX < 0)
        {
            Destroy();//�֐�"Destroy"�����s
        }
        //"hp > 0 && viewPointX < 1"�̏ꍇ
        else if (hp > 0 && viewPointX < 1)
        {
            Behavior();//�֐�"Behavior"�����s
        }
    }

    //�֐�"Behavior"
    void Behavior()
    {
        this.transform.position = new Vector3(this.transform.position.x, 0.0f, 1.0f);
        this.transform.eulerAngles = new Vector3(this.transform.rotation.x, -EnemyList.rotation, this.transform.rotation.z);

        //
        if (isAnimation == true)
        {
            Wait();//�֐�"Wait"�����s
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
        animator.SetInteger("Motion", nowAnimation);//"animator(Motion)"��"nowAnimation"��ݒ肵�čĐ�
    }

    //�֐�"Wait"
    void Wait()
    {
        animationTimer += Time.deltaTime;//�Ԋu��"Time.deltaTime(�o�ߎ���)"�𑫂�

        if (nowAnimation == EnemyList.HumanoidAnimation.punch ||
            nowAnimation == EnemyList.HumanoidAnimation.kick)
        {
            //
            if (nowAnimation == EnemyList.HumanoidAnimation.punch)
            {
                //
                if (animationTimer >= 2.0f)
                {
                    animationTimer = 0.0f;
                    isAnimation = false;
                    nowAnimation = EnemyList.HumanoidAnimation.crouch;
                    Animation();//�֐�"Animation"�����s
                }
            }
            //
            else if (nowAnimation == EnemyList.HumanoidAnimation.kick)
            {
                //
                if (animationTimer >= 1.5f)
                {
                    animationTimer = 0.0f;
                    isAnimation = false;
                    nowAnimation = EnemyList.HumanoidAnimation.crouch;
                    Animation();//�֐�"Animation"�����s
                }
            }
            //
            else if (nowAnimation == EnemyList.HumanoidAnimation.damage)
            {
                //
                if (animationTimer >= 1.0f)
                {
                    animationTimer = 0.0f;
                    isAnimation = false;
                    nowAnimation = EnemyList.HumanoidAnimation.crouch;
                    Animation();//�֐�"Animation"�����s
                }
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
        hp = 0;                                          //"hp"��"0"�ɂ���
        GameManager.score += EnemyList.WalkEnemy.score;  //"score"�𑫂�
        nowAnimation = EnemyList.HumanoidAnimation.death;//"nowAnimation"��"death(���S)"�ɂ���
        audioSource.PlayOneShot(scream);                 //"scream"��炷
        Animation();
    }

    //�֐�"Destroy"
    void Destroy()
    {
        Destroy(this.gameObject);//���̃I�u�W�F�N�g������
    }

    //�����蔻��(OnTriggerEnter)
    void OnTriggerEnter(Collider collision)
    {
        //�Փ˂����I�u�W�F�N�g�̃^�O��"Player" && "isAnimation == false"�̏ꍇ
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
