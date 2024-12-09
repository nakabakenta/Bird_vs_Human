using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunEnemy : MonoBehaviour
{
    //�X�e�[�^�X
    private int hp;     //�̗�
    private float speed;//�ړ����x
    private float jump; //�W�����v��
    //����
    private float viewPointX;           //�r���[�|�C���g���W.X
    private bool isAction = false;      //�s���̉�
    private int nowAnimation;           //���݂̃A�j���[�V����
    private float animationTimer = 0.0f;//�A�j���[�V�����^�C�}�[
    private float jumpTimer = 0.0f;     //�W�����v�^�C�}�[
    private bool isAnimation = false;   //�A�j���[�V�����̉�
    private delegate void ActionDelegate();
    private ActionDelegate nowAction;
    //���̃I�u�W�F�N�g�̃R���|�[�l���g
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
        hp = EnemyList.RunEnemy.hp;      //�̗�
        speed = EnemyList.RunEnemy.speed;//�ړ����x
        jump = EnemyList.RunEnemy.jump;  //�W�����v��
        //���̃I�u�W�F�N�g�̃R���|�[�l���g���擾
        animator = this.GetComponent<Animator>();      //"Animator"
        audioSource = this.GetComponent<AudioSource>();//"AudioSource"
        //���̃I�u�W�F�N�g�̃R���|�[�l���g���擾
        playerTransform = GameObject.Find("Player").transform;//"Transform(�v���C���[)"
        //
        Direction();
        //
        nowAnimation = EnemyList.HumanoidAnimation.run;//"nowAnimation = walk(����)"�ɂ���
        Animation();                                   //�֐�"Animation"�����s
    }

    // Update is called once per frame
    void Update()
    {
        //���̃I�u�W�F�N�g�̃r���[�|�[�g���W���擾
        viewPointX = Camera.main.WorldToViewportPoint(this.transform.position).x;//�r���[�|�[�g���W.X

        //"(hp <= 0 && viewPointX < 0) || (hp > 0 && Stage.bossEnemy[Stage.nowStage - 1] == false)"�̏ꍇ
        if ((hp <= 0 && viewPointX < 0) || (hp > 0 && Stage.bossEnemy[Stage.nowStage - 1] == false))
        {
            Destroy();//�֐�"Destroy"�����s
        }

        //
        if (this.transform.position.z > playerTransform.position.z + 0.1f)
        {
            if (isAction == true)
            {
                Direction();
            }
            else if (isAction == false)
            {
                //"viewPointX < 1"�̏ꍇ
                if (viewPointX < 1)
                {
                    isAction = true;
                }
            }
        }
        //
        else if (this.transform.position.z >= playerTransform.position.z - 0.1f &&
                 this.transform.position.z <= playerTransform.position.z + 0.1f)
        {
            if (isAction == true)
            {
                Direction();
            }
            else if (isAction == false)
            {
                //"viewPointX < 1"�̏ꍇ
                if (viewPointX < 1)
                {
                    isAction = true;
                }
            }
        }

        //"hp > 0 && isAction == true"
        if (hp > 0 && isAction == true)
        {
            nowAction();
        }
    }

    //�֐�"Direction"
    void Direction()
    {
        //
        if (this.transform.position.z > playerTransform.position.z + 0.1f)
        {
            nowAction = Vertical;//
        }
        //
        else if (this.transform.position.z >= playerTransform.position.z - 0.1f &&
                 this.transform.position.z <= playerTransform.position.z + 0.1f)
        {
            nowAction = Horizontal;//
        }
    }

    //�֐�"Vertical"
    void Vertical()
    {
        //
        if (isAnimation == true)
        {
            Wait();//�֐�"Wait"�����s
        }
        else if(isAnimation == false)
        {
            //
            if (PlayerController.hp > 0)
            {
                this.transform.position += speed * transform.forward * Time.deltaTime;//�O�����Ɉړ����� 
                this.transform.eulerAngles = new Vector3(this.transform.rotation.x, -EnemyList.rotation * 2, this.transform.rotation.z);
            }
            else if (PlayerController.hp <= 0)
            {
                nowAnimation = EnemyList.HumanoidAnimation.dance;
                Animation();//�֐�"Animation"�����s
            }
        }

        this.transform.position = new Vector3(this.transform.position.x, 0.0f, this.transform.position.z);
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
                this.transform.position += speed * transform.forward * Time.deltaTime;//�O�����Ɉړ����� 

                if (this.transform.position.x + EnemyList.RunEnemy.rangeX > playerTransform.position.x &&
                    this.transform.position.x - EnemyList.RunEnemy.rangeX < playerTransform.position.x &&
                    this.transform.position.y + EnemyList.RunEnemy.rangeY < playerTransform.position.y &&
                    this.transform.position.y == 0.0f && nowAnimation == EnemyList.HumanoidAnimation.run)
                {
                    isAnimation = true;
                    nowAnimation = EnemyList.HumanoidAnimation.jump;
                    Animation();
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
            }
            else if (PlayerController.hp <= 0)
            {
                nowAnimation = EnemyList.HumanoidAnimation.dance;
                Animation();//�֐�"Animation"�����s
            }
        }

        if (nowAnimation != EnemyList.HumanoidAnimation.jump)
        {
            this.transform.position = new Vector3(this.transform.position.x, 0.0f, playerTransform.position.z);
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

        //
        if (nowAnimation == EnemyList.HumanoidAnimation.punch)
        {
            //
            if (animationTimer >= 2.12f)
            {
                animationTimer = 0.0f;
                isAnimation = false;
                nowAnimation = EnemyList.HumanoidAnimation.run;
                Animation();//�֐�"Animation"�����s
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
                nowAnimation = EnemyList.HumanoidAnimation.run;
                Animation();//�֐�"Animation"�����s
            }
        }
        //
        else if(nowAnimation == EnemyList.HumanoidAnimation.jump)
        {
            jumpTimer += Time.deltaTime;//"jumpTimer"��"Time.deltaTime(�o�ߎ���)"�𑫂�

            if (animationTimer >= 0.8f && jumpTimer >= 0.1f)
            {
                jump -= 1.0f;
                jumpTimer = 0.0f;
            }

            if (animationTimer >= 0.8f)
            {
                this.transform.position += jump * transform.up * Time.deltaTime;

                if (animationTimer >= 2.7f)
                {
                    jump = EnemyList.RunEnemy.jump;
                    animationTimer = 0.0f;
                    jumpTimer = 0.0f;
                    isAnimation = false;
                    nowAnimation = EnemyList.HumanoidAnimation.run;//
                    Animation();                                   //�֐�"Animation"�����s
                }
                else if(animationTimer >= 2.3f)
                {
                    animator.SetFloat("MoveSpeed", 1.0f);//"animator(MoveSpeed)"��"1.0f(�Đ�)"�ɂ���
                }
                else if (animationTimer >= 1.2f)
                {
                    animator.SetFloat("MoveSpeed", 0.0f);//"animator(MoveSpeed)"��"0.0f(��~)"�ɂ���
                }
            }
        }
        //
        else if (nowAnimation == EnemyList.HumanoidAnimation.damage)
        {
            //
            if (animationTimer >= 1.13f)
            {
                animationTimer = 0.0f;
                isAnimation = false;
                nowAnimation = EnemyList.HumanoidAnimation.run;
                Animation();//�֐�"Animation"�����s
            }
        }
    }

    //�֐�"Damage"
    void Damage()
    {
        hp -= PlayerList.Player.power[GameManager.playerNumber];//

        //"hp > 0 && nowAnimation != jump"�̏ꍇ
        if (hp > 0 && nowAnimation != EnemyList.HumanoidAnimation.jump)
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
        this.transform.position = new Vector3(this.transform.position.x, 0.0f, this.transform.position.z);//
        this.tag = "Untagged";                           //"this.tag = Untagged"�ɂ���
        hp = 0;                                          //"hp"��"0"�ɂ���
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