using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : CharacteBase
{
    //���̃I�u�W�F�N�g�̃R���|�[�l���g
    public AudioClip death;//"AudioClip(���S)"
    //�X�e�[�^�X
    public string enemyType;     //�G�̌^
    public string enemyOption;   //�G�̐ݒ�
    public float jump;           //�W�����v��
    public static bool bossEnemy;//
    //����
    public bool action = false;                           //�s���̉�
    public bool playerFind;                               //�v���C���[�T���̉�
    public int defaultAnimationNumber, nowAnimationNumber;//�W���̃A�j���[�V�����ԍ�, ���݂̃A�j���[�V�����ԍ�
    public bool isAnimation = false;                      //�A�j���[�V�����̉�
    private string nowAnimationName;                      //���݂̃A�j���[�V�����̖��O
    private float nowAnimationLength;                     //���݂̃A�j���[�V�����̒���
    private float animationTimer = 0.0f;                  //�A�j���[�V�����^�C�}�[
    private float animationChangeTimer = 0.0f;            //�A�j���[�V�����؂�ւ��^�C�}�[
    private float jumpTimer = 0.0f;                       //�W�����v�^�C�}�[
    private Enemy.HumanoidAnimation humanoidAnimation;    //"enum(HumanoidAnimation)"

    //�֐�"StartAnimation"
    public void StartAnimation()
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

        if(enemyType == Enemy.EnemyType.Human.ToString())
        {
            if (action == false)
            {
                if (viewPortPosition.x < 1)
                {
                    action = true;
                }
            }
            //�̗͂�"0����" && �s��"����"�̏ꍇ
            if (hp > 0 && action == true)
            {
                Action();//�֐�"Action"�����s����
            }

            //�r���[�|�[�g���W��"0����"�̏ꍇ
            if (viewPortPosition.x < 0)
            {
                if (enemyOption == Enemy.EnemyOption.Normal.ToString() ||
                    enemyOption == Enemy.EnemyOption.Wait.ToString())
                {
                    Destroy();//�֐�"Destroy"�����s����
                }
                else if (enemyOption == Enemy.EnemyOption.Find.ToString() ||
                         enemyOption == Enemy.EnemyOption.Boss.ToString())
                {
                    if (hp <= 0)
                    {
                        Destroy();//�֐�"Destroy"�����s����
                    }
                }
            }
        }
        else if(enemyType == Enemy.EnemyType.Vehicle.ToString())
        {
            if (enemyOption == Enemy.EnemyOption.Normal.ToString())
            {
                if (action == false)
                {
                    if (rotation == (int)Characte.Direction.Vertical)
                    {
                        if (this.thisTransform.position.x < playerTransform.position.x + 5.0f)
                        {
                            action = true;
                        }
                    }
                    else if(rotation == -(int)Characte.Direction.Horizontal)
                    {
                        if (viewPortPosition.x < 1.25)
                        {
                            action = true;
                        }
                    }
                }

                //�̗͂�"0����" && �s��"����"�̏ꍇ
                if (hp > 0 && action == true)
                {
                    Action();//�֐�"Action"�����s����
                }
            }
            else if(enemyOption == Enemy.EnemyOption.Find.ToString() ||
                    enemyOption == Enemy.EnemyOption.Boss.ToString())
            {
                //�̗͂�"0����" && �s��"����"�̏ꍇ
                if (hp > 0)
                {
                    Action();//�֐�"Action"�����s����
                }
            }
        }

        if (bossEnemy == false)
        {
            Destroy();//�֐�"Destroy"�����s����
        }
    }

    //�֐�"Action"
    public virtual void Action()
    {
        if (enemyType == Enemy.EnemyType.Human.ToString())
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

                    if (enemyOption != Enemy.EnemyOption.Normal.ToString())
                    {
                        if (enemyOption == Enemy.EnemyOption.Find.ToString() || enemyOption == Enemy.EnemyOption.Boss.ToString())
                        {
                            if (this.transform.position.x + EnemyList.RunEnemy.range.x > playerTransform.position.x &&
                            this.transform.position.x - EnemyList.RunEnemy.range.x < playerTransform.position.x &&
                            this.transform.position.y + EnemyList.RunEnemy.range.y < playerTransform.position.y &&
                            this.transform.position.y == 0.0f && nowAnimationNumber == defaultAnimationNumber)
                            {
                                isAnimation = true;
                                nowAnimationNumber = (int)Enemy.HumanoidAnimation.Jump;
                                AnimationPlay();
                            }
                        }
                    }

                    if (enemyOption != Enemy.EnemyOption.Wait.ToString())
                    {
                        Move();
                    }

                    //�A�j���[�V�����؂�ւ��^�C�}�[��"5.0f�ȏ�"�̏ꍇ
                    if (animationChangeTimer >= 5.0f && enemyOption == Enemy.EnemyOption.Boss.ToString())
                    {
                        isAnimation = true;
                        animationChangeTimer = 0.0f;//�A�j���[�V�����؂�ւ��^�C�}�[������������

                        //
                        if (nowAnimationNumber == defaultAnimationNumber)
                        {
                            nowAnimationNumber = (int)Enemy.HumanoidAnimation.Battlecry;//���݂̃A�j���[�V������"�Y����"�ɂ���
                        }
                        //
                        if (nowAnimationNumber == (int)Enemy.HumanoidAnimation.CrazyRun)
                        {
                            nowAnimationNumber = (int)Enemy.HumanoidAnimation.JumpAttack;//���݂̃A�j���[�V������"�W�����v�U��"�ɂ���
                        }

                        AnimationPlay();//�֐�"AnimationPlay"�����s����
                    }
                }
                else if (PlayerBase.status == "Death")
                {
                    nowAnimationNumber = (int)Enemy.HumanoidAnimation.Dance;
                    AnimationPlay();                                  //�֐�"AnimationPlay"�����s����
                }

                AddAction();//�֐�"AddAction"�����s����
            }

            if (nowAnimationNumber != (int)Enemy.HumanoidAnimation.Jump &&
                nowAnimationNumber != (int)Enemy.HumanoidAnimation.JumpAttack)
            {
                this.transform.position = new Vector3(this.transform.position.x, 0.0f, playerTransform.position.z);
            }
        }
    }

    //�֐�"Move"
    public void Move()
    {
        this.transform.position += moveSpeed * transform.forward * Time.deltaTime;//�O�����Ɉړ�����
    }

    //�֐�"Direction"
    public void Direction()
    {
        //
        if (this.transform.position.z > playerTransform.position.z + 0.1f)
        {
            rotation = (int)Characte.Direction.Vertical;
        }
        //
        if (this.transform.position.z >= playerTransform.position.z - 0.1f &&
            this.transform.position.z <= playerTransform.position.z + 0.1f)
        {
            rotation = -(int)Characte.Direction.Horizontal;
        }

        if (playerFind == true)
        {
            //
            if (this.transform.position.x > playerTransform.position.x)
            {
                rotation = -(int)Characte.Direction.Horizontal;
            }
            //
            if (this.transform.position.x < playerTransform.position.x)
            {
                rotation = (int)Characte.Direction.Horizontal;
            }
        }

        this.transform.eulerAngles = new Vector3(this.transform.rotation.x, rotation, this.transform.rotation.z);
    }

    //�֐�"AddAction"
    public virtual void AddAction()
    {
        
    }

    //�֐�"Animation"
    public void AnimationPlay()
    {
        humanoidAnimation = (Enemy.HumanoidAnimation)nowAnimationNumber;
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
        if (nowAnimationNumber == (int)Enemy.HumanoidAnimation.Jump)
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
        else if (nowAnimationNumber == (int)Enemy.HumanoidAnimation.JumpAttack)
        {
            //
            if (animationTimer >= nowAnimationLength / 2)
            {
                animationTimer = 0.0f;                                //�A�j���[�V�����^�C�}�[������������
                nowAnimationNumber = defaultAnimationNumber;          //���݂̃A�j���[�V������"����"�ɂ���
                moveSpeed /= 3.0f;
                isAnimation = false;
                AnimationPlay();
            }
        }
        //���݂̃A�j���[�V������"�Y����"�̏ꍇ
        else if (nowAnimationNumber == (int)Enemy.HumanoidAnimation.Battlecry)
        {
            //
            if (animationTimer >= nowAnimationLength / 2)
            {
                animationTimer = 0.0f;                               //�A�j���[�V�����^�C�}�[������������
                nowAnimationNumber = (int)Enemy.HumanoidAnimation.CrazyRun;//���݂̃A�j���[�V������"����"�ɂ���
                moveSpeed *= 3.0f;                                       //�ړ����x��"*3"����
                isAnimation = false;
                AnimationPlay();
            }
        }
        else if (nowAnimationNumber == (int)Enemy.HumanoidAnimation.ExitCar)
        {
            if (animationTimer >= nowAnimationLength / 2)
            {   //�A�j���[�V�����^�C�}�[������������
                defaultAnimationNumber = (int)Enemy.HumanoidAnimation.Run;//���݂̃A�j���[�V������"����"�ɂ���
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
    public void DamageEnemy()
    {
        hp -= PlayerBase.attackPower;

        //�̗͂�"0����" && ���݂̃A�j���[�V�����ԍ��������̃A�j���[�V�����ԍ��Ɠ������ꍇ
        if (hp > 0 && enemyType == Enemy.EnemyType.Human.ToString() && nowAnimationNumber == defaultAnimationNumber)
        {
            isAnimation = true;                                      //"isAnimation = true"�ɂ���
            nowAnimationNumber = (int)Enemy.HumanoidAnimation.Damage;
            audioSource.PlayOneShot(damage);                         //"damage"��炷
            AnimationPlay();                                         //�֐�"Animation"�����s
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
        this.tag = "Untagged";         //���̃^�O��"Untagged"�ɂ���
        hp = 0;                        //�̗͂�"0"�ɂ���
        GameManager.score += 1;        //�X�R�A�𑫂�
        PlayerBase.exp += 1;           //�o���l�𑫂�
        audioSource.PlayOneShot(death);//"death"��炷

        if (enemyType == Enemy.EnemyType.Human.ToString())
        {
            //�ʒu(.Y)��"0.0f"�ɂ���
            this.transform.position
                = new Vector3(this.transform.position.x, 0.0f, this.transform.position.z);
            nowAnimationNumber = (int)Enemy.HumanoidAnimation.Death;
            AnimationPlay();                                 //�֐�"Animation"�����s
        }
    }

    //�����蔻��(OnTriggerEnter)
    public virtual void OnTriggerEnter(Collider collision)
    {
        if(enemyType == Enemy.EnemyType.Human.ToString())
        {
            //�Փ˂����I�u�W�F�N�g��"tag == Player" && "isAnimation == false"�̏ꍇ
            if (collision.gameObject.tag == "Player" && isAnimation == false)
            {
                isAnimation = true;//"isAnimation = true"�ɂ���
                                   //�����_��"10(�p���`)"�`"12(�L�b�N)"
                nowAnimationNumber = (int)Random.Range((int)Enemy.HumanoidAnimation.Punch,
                                                       (int)Enemy.HumanoidAnimation.Kick + 1);
                AnimationPlay();//�֐�"AnimationPlay"�����s����
            }
        }
        //�Փ˂����I�u�W�F�N�g�̃^�O��"Bullet" && �̗͂�"0����"�̏ꍇ
        if (collision.gameObject.tag == "Bullet" && hp > 0)
        {
            DamageEnemy();//�֐�"DamageEnemy"�����s����
        }
    }

    public static class Enemy
    {
        public enum EnemyType
        {
            Human,
            Vehicle,
        }

        public enum EnemyOption
        {
            Normal,
            Find,
            Wait,
            Boss,
        }

        public enum HumanoidAnimation
        {
            Walk = 0,
            Run = 1,
            CrazyRun = 2,
            HaveGunIdle = 3,
            Punch = 10,
            Kick = 11,
            JumpAttack = 12,
            GunPlay = 13,
            Jump = 20,
            Crouch = 21,
            ExitCar = 22,
            Battlecry = 23,
            Reload = 24,
            Dance = 30,
            Damage = 31,
            Death = 32
        }
    }
}
