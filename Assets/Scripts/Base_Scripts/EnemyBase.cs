using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : CharacteBase
{
    //���̃I�u�W�F�N�g�̃R���|�[�l���g
    public AudioClip scream;//"AudioClip(���ѐ�)"
    //�X�e�[�^�X
    public string enemyType;//�G�̌^
    public float jump;      //�W�����v��
    //����
    public float rotation = 90.0f;
    public int defaultAnimationNumber, nowAnimationNumber;//�W���̃A�j���[�V�����ԍ�, ���݂̃A�j���[�V�����ԍ�
    public string nowAnimationName;                       //���݂̃A�j���[�V�����̖��O
    public float nowAnimationLength;                      //���݂̃A�j���[�V�����̒���
    public bool isAction = false;                         //�s���̉�
    public float animationTimer = 0.0f;                   //�A�j���[�V�����^�C�}�[
    public float changeAnimationTimer = 0.0f;
    public float jumpTimer = 0.0f;
    public bool isAnimation = false;                      //�A�j���[�V�����̉�
    public HumanoidAnimation humanoidAnimation;

    //�֐�"StartEnmey"
    public void StartEnemy()
    {
        //���̃I�u�W�F�N�g�̃R���|�[�l���g���擾
        animator = this.GetComponent<Animator>();
        runtimeAnimatorController = animator.runtimeAnimatorController;
        //
        nowAnimationNumber = defaultAnimationNumber;
        AnimationPlay();                            //�֐�"AnimationPlay"�����s����
    }

    //�֐�"UpdateEnmey"
    public virtual void UpdateEnemy()
    {
        //���̃I�u�W�F�N�g�̃r���[�|�[�g���W���擾
        viewPortPosition.x = Camera.main.WorldToViewportPoint(this.transform.position).x;

        //�r���[�|�[�g���W��"0����"�̏ꍇ
        if (viewPortPosition.x < 0)
        {
            Destroy();//�֐�"Destroy"�����s
        }

        if (isAction == false)
        {
            if (viewPortPosition.x < 1)
            {
                isAction = true;
            }
        }

        //"hp > 0 && isAction == true"
        if (hp > 0 && isAction == true)
        {
            Action();
        }
    }

    //�֐�"Action"
    public void Action()
    {
        //
        if (this.transform.position.z > playerTransform.position.z + 0.5f)
        {
            rotation = 180.0f;
        }
        //
        if (this.transform.position.z >= playerTransform.position.z - 0.5f &&
            this.transform.position.z <= playerTransform.position.z + 0.5f)
        {
            rotation = 90.0f;//
        }

        if (isAnimation == false)
        {
            //
            if (PlayerController.status != "Death")
            {
                changeAnimationTimer += Time.deltaTime;

                if (enemyType == EnemyType.WalkEnemy.ToString())
                {
                    this.transform.eulerAngles = new Vector3(this.transform.rotation.x, -rotation, this.transform.rotation.z);
                }
                else if (enemyType != EnemyType.WalkEnemy.ToString())
                {
                    if (enemyType == EnemyType.RunEnemy.ToString() || enemyType == EnemyType.BossEnemy.ToString())
                    {
                        if (this.transform.position.x + EnemyList.RunEnemy.range.x > playerTransform.position.x &&
                        this.transform.position.x - EnemyList.RunEnemy.range.x < playerTransform.position.x &&
                        this.transform.position.y + EnemyList.RunEnemy.range.y < playerTransform.position.y &&
                        this.transform.position.y == 0.0f && nowAnimationNumber == defaultAnimationNumber)
                        {
                            isAnimation = true;
                            nowAnimationNumber = (int)HumanoidAnimation.Jump;
                            AnimationPlay();
                        }
                    }

                    //
                    if (this.transform.position.x > playerTransform.position.x)
                    {
                        this.transform.eulerAngles = new Vector3(this.transform.rotation.x, -rotation, this.transform.rotation.z);
                    }
                    //
                    else if (this.transform.position.x < playerTransform.position.x)
                    {
                        this.transform.eulerAngles = new Vector3(this.transform.rotation.x, rotation, this.transform.rotation.z);
                    }
                }

                if (enemyType != EnemyType.HaveGunEnemy.ToString())
                {
                    this.transform.position += speed * transform.forward * Time.deltaTime;//�O�����Ɉړ�����
                }

                //�A�j���[�V�����؂�ւ��^�C�}�[��"5.0f�ȏ�"�̏ꍇ
                if (changeAnimationTimer >= 5.0f && enemyType == EnemyType.BossEnemy.ToString())
                {
                    isAnimation = true;
                    changeAnimationTimer = 0.0f;//�A�j���[�V�����؂�ւ��^�C�}�[������������

                    //
                    if (nowAnimationNumber == defaultAnimationNumber)
                    {
                        nowAnimationNumber = (int)HumanoidAnimation.Battlecry;//���݂̃A�j���[�V������"�Y����"�ɂ���
                    }
                    //
                    if (nowAnimationNumber == (int)HumanoidAnimation.CrazyRun)
                    {
                        nowAnimationNumber = (int)HumanoidAnimation.JumpAttack;//���݂̃A�j���[�V������"�W�����v�U��"�ɂ���
                    }

                    AnimationPlay();//�֐�"AnimationPlay"�����s����
                }
            }
            else if (PlayerController.status == "Death")
            {
                nowAnimationNumber = (int)HumanoidAnimation.Dance;
                AnimationPlay();                                  //�֐�"AnimationPlay"�����s����
            }
        }
        //
        else if (isAnimation == true)
        {
            AnimationChange();//�֐�"AnimationChange"�����s
        }

        if (nowAnimationNumber != (int)HumanoidAnimation.Jump && 
            nowAnimationNumber != (int)HumanoidAnimation.JumpAttack)
        {
            this.transform.position = new Vector3(this.transform.position.x, 0.0f, playerTransform.position.z);
        }
    }

    //�֐�"Animation"
    public void AnimationPlay()
    {
        humanoidAnimation = (HumanoidAnimation)nowAnimationNumber;
        nowAnimationName = humanoidAnimation.ToString();
        animator.SetInteger("Animation", nowAnimationNumber);        //"animator(Motion)"��"nowAnimation"��ݒ肵�čĐ�
    }

    //�֐�"Wait"
    public virtual void AnimationChange()
    {
        animationTimer += Time.deltaTime;//"animationTimer"��"Time.deltaTime(�o�ߎ���)"�𑫂�

        foreach (AnimationClip clip in runtimeAnimatorController.animationClips)
        {
            if (clip.name == nowAnimationName)
            {
                nowAnimationLength = clip.length;
            }
        }

        //
        if (nowAnimationNumber == (int)HumanoidAnimation.Jump)
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
                    nowAnimationNumber = defaultAnimationNumber;//
                    AnimationPlay();                            //�֐�"Animation"�����s
                }
                else if (animationTimer >= 2.3f)
                {
                    animator.SetFloat("MoveSpeed", 1.0f);//"animator(MoveSpeed)"��"1.0f(�Đ�)"�ɂ���
                }
                else if (animationTimer >= 1.2f)
                {
                    animator.SetFloat("MoveSpeed", 0.0f);//"animator(MoveSpeed)"��"0.0f(��~)"�ɂ���
                }
            }
        }
        //���݂̃A�j���[�V������"�W�����v�U��"�̏ꍇ
        else if (nowAnimationNumber == (int)HumanoidAnimation.JumpAttack)
        {
            //
            if (animationTimer >= nowAnimationLength / 2)
            {
                animationTimer = 0.0f;                                //�A�j���[�V�����^�C�}�[������������
                nowAnimationNumber = defaultAnimationNumber;          //���݂̃A�j���[�V������"����"�ɂ���
                speed = EnemyList.BossEnemy.speed[Stage.nowStage - 1];
                isAnimation = false;
                AnimationPlay();
            }
        }
        //���݂̃A�j���[�V������"�Y����"�̏ꍇ
        else if (nowAnimationNumber == (int)HumanoidAnimation.Battlecry)
        {
            //
            if (animationTimer >= nowAnimationLength / 2)
            {
                animationTimer = 0.0f;                               //�A�j���[�V�����^�C�}�[������������
                nowAnimationNumber = (int)HumanoidAnimation.CrazyRun;//���݂̃A�j���[�V������"����"�ɂ���
                speed *= 3.0f;                                       //�ړ����x��"*3"����
                isAnimation = false;
                AnimationPlay();
            }
        }
        else if (enemyType != EnemyType.HaveGunEnemy.ToString() && animationTimer >= nowAnimationLength)
        {
            animationTimer = 0.0f;
            isAnimation = false;
            nowAnimationNumber = defaultAnimationNumber;
            AnimationPlay();                            //�֐�"AnimationPlay"�����s����
        }
    }

    //�֐�"Enmey"
    public virtual void DamageEnemy()
    {
        hp -= PlayerBase.attack;

        //�̗͂�"0����" && ���݂̃A�j���[�V�����ԍ��������̃A�j���[�V�����ԍ��Ɠ������ꍇ
        if (hp > 0 && nowAnimationNumber == defaultAnimationNumber)
        {
            isAnimation = true;                                //"isAnimation = true"�ɂ���
            nowAnimationNumber = (int)HumanoidAnimation.Damage;
            audioSource.PlayOneShot(damage);                   //"damage"��炷
            AnimationPlay();                                   //�֐�"Animation"�����s
        }
        //"hp <= 0"�̏ꍇ
        else if (hp <= 0)
        {
            Invoke("DeathEnemy", 0.01f);//�֐�"DeathEnemy"��"0.01f"��Ɏ��s����
        }
    }

    //�֐�"Enmey"
    public virtual void DeathEnemy()
    {
        this.tag = "Untagged";                            //���̃^�O��"Untagged"�ɂ���
        hp = 0;                                           //�̗͂�"0"�ɂ���
        GameManager.score += EnemyList.WalkEnemy.score;   //�X�R�A�𑫂�
        PlayerController.exp += 10;                       //�o���l�𑫂�

        //�ʒu(.Y)��"0.0f"�ɂ���
        this.transform.position
            = new Vector3(this.transform.position.x, 0.0f, this.transform.position.z);
        nowAnimationNumber = (int)HumanoidAnimation.Death;
        audioSource.PlayOneShot(scream);                  //"scream"��炷
        AnimationPlay();                                  //�֐�"Animation"�����s
    }

    //�����蔻��(OnTriggerEnter)
    public virtual void OnTriggerEnter(Collider collision)
    {
        //�Փ˂����I�u�W�F�N�g��"tag == Player" && "isAnimation == false"�̏ꍇ
        if (collision.gameObject.tag == "Player" && isAnimation == false)
        {
            isAnimation = true;//"isAnimation = true"�ɂ���
            //�����_��"10(�p���`)"�`"12(�L�b�N)"
            nowAnimationNumber = (int)Random.Range((int)HumanoidAnimation.Punch,
                                                   (int)HumanoidAnimation.Kick + 1);
            AnimationPlay();//�֐�"AnimationPlay"�����s����
        }
        //�Փ˂����I�u�W�F�N�g�̃^�O��"Bullet" && �̗͂�"0����"�̏ꍇ
        if (collision.gameObject.tag == "Bullet" && hp > 0)
        {
            DamageEnemy();//�֐�"DamageEnemy"�����s����
        }
    }

    public enum EnemyType
    {
        WalkEnemy,
        RunEnemy,
        HaveGunEnemy,
        CarEnemy,
        BossEnemy,
    }

    public enum HumanoidAnimation
    {
        Walk        = 0,
        Run         = 1,
        CrazyRun    = 2,
        HaveGunIdle = 3,
        Punch       = 10,
        Kick        = 11,
        JumpAttack  = 12,
        GunPlay     = 13,
        Jump        = 20,
        Crouch      = 21,
        ExitCar     = 22,
        Battlecry   = 23,
        Reload      = 24,
        Dance       = 30,
        Damage      = 31,
        Death       = 32
    }
}
