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
    public int score;       //�X�R�A
    //����
    public float rotation;
    public bool isAction = false;                         //�s���̉�
    public bool playerFind;                               //�v���C���[�T���̉�
    public int defaultAnimationNumber, nowAnimationNumber;//�W���̃A�j���[�V�����ԍ�, ���݂̃A�j���[�V�����ԍ�
    public string nowAnimationName;                       //���݂̃A�j���[�V�����̖��O
    public float nowAnimationLength;                      //���݂̃A�j���[�V�����̒���
    public float animationTimer = 0.0f;                   //�A�j���[�V�����^�C�}�[
    public float animationChangeTimer = 0.0f;             //�A�j���[�V�����؂�ւ��^�C�}�[
    public float jumpTimer = 0.0f;                        //�W�����v�^�C�}�[
    public bool isAnimation = false;                      //�A�j���[�V�����̉�
    public HumanoidAnimation humanoidAnimation;           //"enum(HumanoidAnimation)"

    //�֐�"StartEnmey"
    public void StartEnemy()
    {
        //���̃I�u�W�F�N�g�̃R���|�[�l���g���擾
        animator = this.GetComponent<Animator>();
        runtimeAnimatorController = animator.runtimeAnimatorController;
        //����������������
        nowAnimationNumber = defaultAnimationNumber;//���݂̃A�j���[�V�����ԍ��ɕW���̃A�j���[�V�����ԍ���ݒ肷��
        AnimationPlay();                            //�֐�"AnimationPlay"�����s����
    }

    //�֐�"UpdateEnmey"
    public void UpdateEnemy()
    {
        //���̃I�u�W�F�N�g�̃��[���h���W���r���[�|�[�g���W�ɕϊ����Ď擾����
        viewPortPosition.x = Camera.main.WorldToViewportPoint(this.transform.position).x;

        //�r���[�|�[�g���W��"0����"�̏ꍇ
        if (viewPortPosition.x < 0)
        {
            if(enemyType == EnemyType.Normal.ToString() ||
               enemyType == EnemyType.Wait.ToString() ||
               enemyType == EnemyType.Vehicle.ToString())
            {
                Destroy();//�֐�"Destroy"�����s����
            }
            else if(enemyType == EnemyType.Find.ToString() ||
                    enemyType == EnemyType.Boss.ToString())
            {
                if(hp <= 0)
                {
                    Destroy();//�֐�"Destroy"�����s����
                }
            }
        }

        if (isAction == false)
        {
            if (viewPortPosition.x < 1)
            {
                isAction = true;
            }
        }

        //�̗͂�"0����" && �s��"����"�̏ꍇ
        if (hp > 0 && isAction == true)
        {
            Action();//�֐�"Action"�����s����
        }

        if(enemyType == EnemyType.Find.ToString() && Stage.bossEnemy[Stage.nowStage - 1] == false)
        {
            Destroy();//�֐�"Destroy"�����s����
        }
    }

    //�֐�"Action"
    public void Action()
    {
        //
        if (isAnimation == true)
        {
            AnimationFind();//�֐�"AnimationFind"�����s����
        }
        //
        else if (isAnimation == false)
        {
            //
            if (PlayerBase.status != "Death")
            {
                Direction();//�֐�"Direction"�����s����

                animationChangeTimer += Time.deltaTime;

                if (enemyType != EnemyType.Normal.ToString())
                {
                    if (enemyType == EnemyType.Find.ToString() || enemyType == EnemyType.Boss.ToString())
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
                }

                if (enemyType != EnemyType.Wait.ToString())
                {
                    this.transform.position += speed * transform.forward * Time.deltaTime;//�O�����Ɉړ�����
                }

                //�A�j���[�V�����؂�ւ��^�C�}�[��"5.0f�ȏ�"�̏ꍇ
                if (animationChangeTimer >= 5.0f && enemyType == EnemyType.Boss.ToString())
                {
                    isAnimation = true;
                    animationChangeTimer = 0.0f;//�A�j���[�V�����؂�ւ��^�C�}�[������������

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
            else if (PlayerBase.status == "Death")
            {
                nowAnimationNumber = (int)HumanoidAnimation.Dance;
                AnimationPlay();                                  //�֐�"AnimationPlay"�����s����
            }

            AddAction();//�֐�"AddAction"�����s����
        }

        if (nowAnimationNumber != (int)HumanoidAnimation.Jump && 
            nowAnimationNumber != (int)HumanoidAnimation.JumpAttack)
        {
            this.transform.position = new Vector3(this.transform.position.x, 0.0f, playerTransform.position.z);
        }

        this.transform.eulerAngles = new Vector3(this.transform.rotation.x, rotation, this.transform.rotation.z);
    }

    //�֐�"Direction"
    public void Direction()
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
            rotation = -90.0f;//
        }

        if (playerFind == true)
        {
            //
            if (this.transform.position.x > playerTransform.position.x)
            {
                rotation = -90.0f;//
            }
            //
            if (this.transform.position.x < playerTransform.position.x)
            {
                rotation = 90.0f;//
            }
        }
    }

    //�֐�"AddAction"
    public virtual void AddAction()
    {
        
    }

    //�֐�"Animation"
    public void AnimationPlay()
    {
        humanoidAnimation = (HumanoidAnimation)nowAnimationNumber;
        nowAnimationName = humanoidAnimation.ToString();
        animator.SetInteger("Animation", nowAnimationNumber);        //"animator(Motion)"��"nowAnimation"��ݒ肵�čĐ�
    }

    //�֐�"AnimationFind"
    public void AnimationFind()
    {
        animationTimer += Time.deltaTime;//"animationTimer"��"Time.deltaTime(�o�ߎ���)"�𑫂�

        foreach (AnimationClip clip in runtimeAnimatorController.animationClips)
        {
            if (clip.name == nowAnimationName)
            {
                nowAnimationLength = clip.length;
            }
        }

        AnimationWait();//�֐�"AnimationWait"�����s����
    }

    //�֐�"AnimationWait"
    public void AnimationWait()
    {
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
        else if (nowAnimationNumber == (int)HumanoidAnimation.ExitCar)
        {
            if (animationTimer >= nowAnimationLength / 2)
            {   //�A�j���[�V�����^�C�}�[������������
                defaultAnimationNumber = (int)HumanoidAnimation.Run;//���݂̃A�j���[�V������"����"�ɂ���
                AnimationChange();
            }
        }
        else if (animationTimer >= nowAnimationLength)
        {
            AddAnimationChange();
            AnimationChange();
        }
    }

    public virtual void AddAnimationChange()
    {

    }

    public void AnimationChange()
    {
        animationTimer = 0.0f;

        if (isAnimation == true)
        {
            isAnimation = false;
            nowAnimationNumber = defaultAnimationNumber;
        }

        AnimationPlay();//�֐�"AnimationPlay"�����s����
    }


    //�֐�"Enmey"
    public virtual void DamageEnemy()
    {
        hp -= PlayerBase.attackPower;

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
        if(enemyType == EnemyType.Boss.ToString())
        {
            Stage.bossEnemy[Stage.nowStage - 1] = false;
        }

        this.tag = "Untagged";    //���̃^�O��"Untagged"�ɂ���
        hp = 0;                   //�̗͂�"0"�ɂ���
        GameManager.score += 1;   //�X�R�A�𑫂�
        PlayerBase.exp += 1;//�o���l�𑫂�

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
        Normal,
        Find,
        Wait,
        Vehicle,
        Boss,
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
