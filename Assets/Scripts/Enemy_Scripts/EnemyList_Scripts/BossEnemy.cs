using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : MonoBehaviour
{
    //�X�e�[�^�X
    private int hp;     //�̗�
    private float speed;//�ړ����x
    private float jump; //�W�����v��
    //����
    private Vector3 viewPoint;                //�r���[�|�C���g���W
    private int nowAnimation;                 //���݂̃A�j���[�V����
    private float animationTimer = 0.0f;      //�A�j���[�V�����^�C�}�[
    private float changeAnimationTimer = 0.0f;//�A�j���[�V�����؂�ւ��^�C�}�[
    private float jumpTimer = 0.0f;           //�W�����v�^�C�}�[
    private bool isAnimation = false;         //�A�j���[�V�����̉�
    //���̃I�u�W�F�N�g�̃R���|�[�l���g                               
    public AudioClip damage;                                    //"AudioClip(�_���[�W)"
    public AudioClip scream;                                    //"AudioClip(���ѐ�)"
    private Animator animator = null;                           //"Animator"
    private RuntimeAnimatorController runtimeAnimatorController;//RuntimeAnimatorController
    private AudioSource audioSource;                            //"AudioSource"
    //���̃I�u�W�F�N�g�̃R���|�[�l���g
    private Transform playerTransform;//"Transform"(�v���C���[)

    // Start is called before the first frame update
    void Start()
    {
        //�X�e�[�^�X��ݒ肷��
        hp = EnemyList.BossEnemy.hp[Stage.nowStage - 1];      
        speed = EnemyList.BossEnemy.speed[Stage.nowStage - 1];
        jump = EnemyList.BossEnemy.jump[Stage.nowStage - 1];
        //���̃I�u�W�F�N�g�̃R���|�[�l���g���擾����
        animator = this.GetComponent<Animator>();      
        runtimeAnimatorController = animator.runtimeAnimatorController;
        audioSource = this.GetComponent<AudioSource>();
        //���̃I�u�W�F�N�g�̃R���|�[�l���g���擾����
        playerTransform = GameObject.Find("Player").transform;
        //�A�j���[�V������ݒ肷��
        nowAnimation = EnemyList.HumanoidAnimation.walk;//���݂̃A�j���[�V������"����"�ɂ���
        Animation();                                    //�֐�"Animation"�����s����
    }

    // Update is called once per frame
    void Update()
    {
        //���̃I�u�W�F�N�g�ʒu(���[���h���W)���r���[�|�C���g���W�ɕϊ�����
        viewPoint.x = Camera.main.WorldToViewportPoint(this.transform.position).x;//�r���[�|�[�g���W.X

        //�r���[�|�C���g���W.x��"0����"�̏ꍇ
        if (viewPoint.x < 0)
        {
            Destroy();//�֐�"Destroy"�����s����
        }
        //�̗͂�"0����" && �r���[�|�C���g���W.x��"1����"�̏ꍇ
        else if (hp > 0 && viewPoint.x < 1)
        {
            Action();//�֐�"Action"�����s����
        }
    }

    //�֐�"Action"
    void Action()
    {
        if (nowAnimation != EnemyList.HumanoidAnimation.jumpAttack && 
            nowAnimation != EnemyList.HumanoidAnimation.jump)
        {
            this.transform.position = new Vector3(this.transform.position.x, 0.0f, 1.0f);
        }

        //�A�j���[�V������"�Đ����Ă��Ȃ�"�ꍇ
        if (isAnimation == true)
        {
            Wait();//�֐�"Wait"�����s����
        }
        //�A�j���[�V������"�Đ����Ă���"�ꍇ
        else if (isAnimation == false)
        {
            //�v���C���[�̗̑͂�"0����"�̏ꍇ
            if (PlayerController.hp > 0)
            {
                if (this.transform.position.x + EnemyList.BossEnemy.range[Stage.nowStage - 1].x > playerTransform.position.x &&
                    this.transform.position.x - EnemyList.BossEnemy.range[Stage.nowStage - 1].x < playerTransform.position.x &&
                    this.transform.position.y + EnemyList.BossEnemy.range[Stage.nowStage - 1].y < playerTransform.position.y &&
                    this.transform.position.y == 0.0f && nowAnimation == EnemyList.HumanoidAnimation.walk)
                {
                    isAnimation = true;//�A�j���[�V������"�Đ����Ă���"�ɂ���
                    nowAnimation = EnemyList.HumanoidAnimation.jump;
                    Animation();//�֐�"Animation"�����s����
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

                changeAnimationTimer += Time.deltaTime;
                this.transform.position = new Vector3(this.transform.position.x, 0.0f, 1.0f);
                this.transform.position += speed * transform.forward * Time.deltaTime;//�O�����Ɉړ�����

                //�A�j���[�V�����؂�ւ��^�C�}�[��"5.0f�ȏ�"�̏ꍇ
                if (changeAnimationTimer >= 5.0f)
                {
                    changeAnimationTimer = 0.0f;//�A�j���[�V�����؂�ւ��^�C�}�[������������
                    ChangeAnimation();          //�֐�"ChangeAnimation"�����s����
                }
            }
            //�v���C���[�̗̑͂�"0�ȉ�"�̏ꍇ
            else if (PlayerController.hp <= 0)
            {
                nowAnimation = EnemyList.HumanoidAnimation.dance;//���݂̃A�j���[�V������"�_���X"�ɂ���
                Animation();//�֐�"Animation"�����s
            }
        }
    }

    //�֐�"ChangeAnimation"
    void ChangeAnimation()
    {
        //���݂̃A�j���[�V������"�����Ɠ�����"�ꍇ
        if (nowAnimation == EnemyList.HumanoidAnimation.walk)
        {
            isAnimation = true;                                  //�A�j���[�V������"�Đ����Ă���"�ɂ���
            nowAnimation = EnemyList.HumanoidAnimation.battlecry;//���݂̃A�j���[�V������"�Y����"�ɂ���
            Animation();                                         //�֐�"Animation"�����s����
        }
        //���݂̃A�j���[�V������"����Ɠ�����"�ꍇ
        else if (nowAnimation == EnemyList.HumanoidAnimation.crazyRun)
        {
            isAnimation = true;                                   //�A�j���[�V������"�Đ����Ă���"�ɂ���
            nowAnimation = EnemyList.HumanoidAnimation.jumpAttack;//���݂̃A�j���[�V������"�W�����v�U��"�ɂ���
            Animation();                                          //�֐�"Animation"�����s����
        }
    }

    //�֐�"Animation"
    void Animation()
    {
        animator.SetInteger("Motion", nowAnimation);//"�A�j���[�^�[��"���݂̃A�j���[�V����"��ݒ肵�čĐ�����
    }

    //�֐�"Wait"
    void Wait()
    {
        animationTimer += Time.deltaTime;//�A�j���[�V�����^�C�}�[�Ɍo�ߎ��Ԃ𑫂�

        //���݂̃A�j���[�V������"�p���`"�̏ꍇ
        if (nowAnimation == EnemyList.HumanoidAnimation.punch)
        {
            foreach (AnimationClip clip in runtimeAnimatorController.animationClips)
            {
                if (clip.name == "")//�w�肵�����O�̃A�j���[�V����������
                {
                    //Debug.Log($"�A�j���[�V������: {clip.name}, ����: {clip.length}�b");
                    //return;

                    //
                    if (animationTimer >= 2.12f)
                    {
                        animationTimer = 0.0f;                          //�A�j���[�V�����^�C�}�[������������
                        nowAnimation = EnemyList.HumanoidAnimation.walk;//���݂̃A�j���[�V������"����"�ɂ���
                    }
                }
            }
        }
        //���݂̃A�j���[�V������"�L�b�N"�̏ꍇ
        else if (nowAnimation == EnemyList.HumanoidAnimation.kick)
        {
            //
            if (animationTimer >= 1.15f)
            {
                animationTimer = 0.0f;                          //�A�j���[�V�����^�C�}�[������������
                nowAnimation = EnemyList.HumanoidAnimation.walk;//���݂̃A�j���[�V������"����"�ɂ���
            }
        }
        //���݂̃A�j���[�V������"�W�����v�U��"�̏ꍇ
        else if (nowAnimation == EnemyList.HumanoidAnimation.jumpAttack)
        {
            //
            if (animationTimer >= 1.605f)
            {
                animationTimer = 0.0f;                                //�A�j���[�V�����^�C�}�[������������
                nowAnimation = EnemyList.HumanoidAnimation.walk;      //���݂̃A�j���[�V������"����"�ɂ���
                speed = EnemyList.BossEnemy.speed[Stage.nowStage - 1];
            }
        }
        //���݂̃A�j���[�V������"�W�����v"�̏ꍇ
        else if (nowAnimation == EnemyList.HumanoidAnimation.jump)
        {
            jumpTimer += Time.deltaTime;//"jumpTimer"��"Time.deltaTime(�o�ߎ���)"�𑫂�
            //�A�j���[�V�����^�C�}�[��"0.8f�ȏ�" && �W�����v�^�C�}�[��"0.1f�ȏ�"�̏ꍇ
            if (animationTimer >= 0.8f && jumpTimer >= 0.1f)
            {
                jump -= 1.0f;    //�W�����v��"-1"����
                jumpTimer = 0.0f;//�W�����v�^�C�}�[������������
            }
            //�A�j���[�V�����^�C�}�[��"0.8f�ȏ�"�̏ꍇ
            if (animationTimer >= 0.8f)
            {
                this.transform.position += jump * transform.up * Time.deltaTime;//���̃I�u�W�F�N�g��"�W�����v"��������

                if (animationTimer >= 2.7f)
                {
                    jump = EnemyList.RunEnemy.jump;
                    animationTimer = 0.0f;
                    jumpTimer = 0.0f;
                    nowAnimation = EnemyList.HumanoidAnimation.walk;//���݂̃A�j���[�V������"����"�ɂ���
                }
                else if (animationTimer >= 2.3f)
                {
                    animator.SetFloat("MoveSpeed", 1.0f);//�A�j���[�^�[��"�������x"��"1.0f(����)"�ɂ���
                }
                else if (animationTimer >= 1.2f)
                {
                    animator.SetFloat("MoveSpeed", 0.0f);//�A�j���[�^�[��"�������x"��"0.0f(��~)"�ɂ���
                }
            }
        }
        //���݂̃A�j���[�V������"�Y����"�̏ꍇ
        else if (nowAnimation == EnemyList.HumanoidAnimation.battlecry)
        {
            //
            if (animationTimer >= 1.125f)
            {
                animationTimer = 0.0f;                               //�A�j���[�V�����^�C�}�[������������
                nowAnimation = EnemyList.HumanoidAnimation.crazyRun;//���݂̃A�j���[�V������"����"�ɂ���
                speed *= 3.0f;                                       //�ړ����x��"*3"����
            }
        }
        //���݂̃A�j���[�V������"�_���[�W"�̏ꍇ
        else if (nowAnimation == EnemyList.HumanoidAnimation.damage)
        {
            //
            if (animationTimer >= 1.13f)
            {
                animationTimer = 0.0f;                          //�A�j���[�V�����^�C�}�[������������
                nowAnimation = EnemyList.HumanoidAnimation.walk;//���݂̃A�j���[�V������"����"�ɂ���
            }
        }
        //�A�j���[�V�����^�C�}�[��"0.0f�Ɠ�����" && "�A�j���[�V������"�Đ����Ă���"�ꍇ
        if (animationTimer == 0.0f && isAnimation == true)
        {
            isAnimation = false;//�A�j���[�V������"�Đ����Ă��Ȃ�"�ɂ���
            Animation();        //�֐�"Animation"�����s
        }
    }

    //�֐�"Damage"
    void Damage()
    {
        hp -= PlayerList.Player.power[GameManager.playerNumber];//�̗͂�"-�v���C���[�̍U����"����

        //�̗͂�"0����" && ���݂̃A�j���[�V������"����"�̏ꍇ
        if (hp > 0 && nowAnimation == EnemyList.HumanoidAnimation.walk)
        {
            isAnimation = true;                               //�A�j���[�V������"�Đ����Ă���"�ɂ���
            nowAnimation = EnemyList.HumanoidAnimation.damage;//���݂̃A�j���[�V������"�_���[�W"�ɂ���
            audioSource.PlayOneShot(damage);                  //"�_���[�W"��炷
            Animation();                                      //�֐�"Animation"�����s����
        }
        //�̗͂�"0�ȉ�"�̏ꍇ
        else if (hp <= 0)
        {
            Invoke("Death", 0.01f);//�֐�"Death"��"0.01f"��Ɏ��s����
        }
    }

    //�֐�"Death"
    void Death()
    {
        hp = 0;                                     //�̗͂�"0"�ɂ���
        Stage.bossEnemy[Stage.nowStage - 1] = false;//�{�X��"�|����"�ɂ���
        this.tag = "Untagged";                      //���̃^�O��"Untagged"�ɂ���
        //�X�R�A�Ƀ{�X�̃X�R�A�𑫂�
        GameManager.score += EnemyList.BossEnemy.score[Stage.nowStage - 1];
        //���̃I�u�W�F�N�g�̈ʒu���Œ肷��
        this.transform.position = new Vector3(this.transform.position.x, 0.0f, this.transform.position.z);
        audioSource.PlayOneShot(scream);                 //"���ѐ�"��炷
        nowAnimation = EnemyList.HumanoidAnimation.death;//���݂̃A�j���[�V������"���S"�ɂ���
        Animation();                                     //�֐�"Animation"�����s����
    }

    //�֐�"Destroy"
    void Destroy()
    {
        Destroy(this.gameObject);//���̃I�u�W�F�N�g������
    }

    //�����蔻��(OnTriggerEnter)
    void OnTriggerEnter(Collider collision)
    {
        //�Փ˂����I�u�W�F�N�g�̃^�O��"Player" && �A�j���[�V������"�Đ�����Ă��Ȃ�"�ꍇ
        if (collision.gameObject.tag == "Player" && isAnimation == false)
        {
            isAnimation = true;//�A�j���[�V������"�Đ����Ă���"�ɂ���

            //�����_���Ȓl"10(�p���`)�`12(�L�b�N)"�����݂̃A�j���[�V�����ɓ����
            nowAnimation = (int)Random.Range(EnemyList.HumanoidAnimation.punch,
                                             EnemyList.HumanoidAnimation.kick + 1);
            Animation();//�֐�"Animation"�����s����
        }
        //�Փ˂����I�u�W�F�N�g�̃^�O��"Bullet" && �̗͂�"0����"�̏ꍇ
        if (collision.gameObject.tag == "Bullet" && hp > 0)
        {
            Damage();//�֐�"Damage"�����s����
        }
    }
}