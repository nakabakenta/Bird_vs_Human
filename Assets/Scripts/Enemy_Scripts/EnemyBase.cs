using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : CharacteBase
{
    //���̃I�u�W�F�N�g�̃R���|�[�l���g
    public GameObject bulletShot, bulletPut;//"GameObject(�e)"
    public GameObject effectDeath;          //"GameObject(�G�t�F�N�g)"
    public GameObject shotPosition;         //"GameObject(���ˈʒu)"
    public GameObject sEMove;
    public AudioClip sEShot, sEDeath;
    //�X�e�[�^�X
    public float jump;         //�W�����v��
    public float actionChangeInterval, shotBulletInterval, putBulletInterval;//�s���ύX�Ԋu, �U���Ԋu
    public float rotationSpeed;//��]���x
    public int maxBullet;
    public Vector3 actionRange;
    public static bool bossEnemy;//
    //�G�̌^
    protected string enemyType;
    /*�y�����z*/
    //�W���̃A�j���[�V�����ԍ�, ���݂̃A�j���[�V�����ԍ�
    protected int defaultAnimationNumber, nowAnimationNumber;
    //�s���ύX�^�C�}�[, �U���^�C�}�[
    protected float actionChangeTimer = 0.0f, attackTimer = 0.0f, putBulletTimer = 0.0f;
    //�s���̉�, �A�j���[�V�����̉�
    protected int nowBullet;
    protected string nowAnimationName;       //���݂̃A�j���[�V�����̖��O
    protected float nowAnimationLength;      //���݂̃A�j���[�V�����̒���
    protected float animationTimer = 0.0f;   //�A�j���[�V�����^�C�}�[
    protected bool action = false, isAnimation = false;
    private float jumpTimer = 0.0f;          //�W�����v�^�C�}�[
    private float gravity = 0.0f;            //�d��          
    private Enemy.HumanoidAnimation humanoidAnimation;//"enum(HumanoidAnimation)"

    //�֐�"BaseStart"
    public void BaseStart()
    {
        nowBullet = maxBullet;

        if (enemyType == Enemy.EnemyType.Human.ToString())
        {
            //���̃I�u�W�F�N�g�̃R���|�[�l���g���擾
            animator = this.GetComponent<Animator>();
            runtimeAnimatorController = animator.runtimeAnimatorController;
            //����������������
            nowAnimationNumber = defaultAnimationNumber;//���݂̃A�j���[�V�����ԍ��ɕW���̃A�j���[�V�����ԍ���ݒ肷��
            AnimationPlay();                            //�֐�"AnimationPlay"�����s����
        }
    }

    //�֐�"BaseUpdate"
    public virtual void BaseUpdate()
    {
        //���̃I�u�W�F�N�g�̃��[���h���W���r���[�|�[�g���W�ɕϊ����Ď擾����
        viewPortPosition.x = Camera.main.WorldToViewportPoint(this.transform.position).x;

        //�̗͂�"0����" && �s��"����"�̏ꍇ
        if (hp > 0 && action == true)
        {
            Action();//�֐�"Action"�����s����
        }

        if (sEMove != null)
        {
            if(viewPortPosition.x > moveRange[0].range[0].x && viewPortPosition.x < moveRange[0].range[1].x)
            {
                sEMove.SetActive(true);
            }
            else if(viewPortPosition.x < moveRange[0].range[0].x || viewPortPosition.x > moveRange[0].range[1].x)
            {
                sEMove.SetActive(false);
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
            direction.y = (int)Characte.Direction.Vertical;
        }
        //
        if (this.transform.position.z >= playerTransform.position.z - 0.1f &&
            this.transform.position.z <= playerTransform.position.z + 0.1f)
        {
            direction.y = -(int)Characte.Direction.Horizontal;
        }
    }

    public void CoarsePlayerDirection()
    {
        direction = playerTransform.position - this.transform.position;
        direction.y = 0.0f;

        this.transform.rotation = Quaternion.LookRotation(direction);
    }

    public void SmoothPlayerDirection()
    {
        direction = playerTransform.position - this.transform.position;
        direction.y = 0.0f;

        if (direction != Vector3.zero)
        {
            Quaternion quaternion = Quaternion.LookRotation(direction);
            this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, quaternion, rotationSpeed * Time.deltaTime);
        }
    }

    //�֐�"Animation"
    public void AnimationPlay()
    {
        humanoidAnimation = (Enemy.HumanoidAnimation)nowAnimationNumber;
        nowAnimationName = humanoidAnimation.ToString();
        animator.SetInteger("Animation", nowAnimationNumber);           //"animator(Motion)"��"nowAnimation"��ݒ肵�čĐ�
    }

    public virtual void ActionChange()
    {

    }

    //�֐�"AnimationFind"
    public void AnimationFind()
    {
        foreach (AnimationClip clip in runtimeAnimatorController.animationClips)
        {
            if (clip.name == nowAnimationName)
            {
                nowAnimationLength = clip.length;
            }
        }

        animationTimer += Time.deltaTime;//"animationTimer"��"Time.deltaTime(�o�ߎ���)"�𑫂�
        ActionWait();                    //�֐�"ActionWait"�����s����
    }

    //�֐�"ActionWait"
    public virtual void ActionWait()
    {
        //
        if (nowAnimationNumber == (int)Enemy.HumanoidAnimation.Jump)
        {
            jumpTimer += Time.deltaTime;//"jumpTimer"��"Time.deltaTime(�o�ߎ���)"�𑫂�

            if (animationTimer >= 0.8f && jumpTimer >= 0.1f)
            {
                gravity += 1.0f;
                jumpTimer = 0.0f;
            }

            if (animationTimer >= 0.8f)
            {
                this.transform.position += (jump - gravity) * transform.up * Time.deltaTime;

                if (animationTimer >= 2.7f)
                {
                    gravity = 0.0f;
                    jumpTimer = 0.0f;
                    ActionReset();
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
                moveSpeed /= 3.0f;
                ActionReset();
            }
        }
        //���݂̃A�j���[�V������"�Y����"�̏ꍇ
        else if (nowAnimationNumber == (int)Enemy.HumanoidAnimation.Battlecry)
        {
            //
            if (animationTimer >= nowAnimationLength / 2)
            {
                animationTimer = 0.0f;                                     //�A�j���[�V�����^�C�}�[������������
                nowAnimationNumber = (int)Enemy.HumanoidAnimation.CrazyRun;//���݂̃A�j���[�V������"����"�ɂ���
                moveSpeed *= 3.0f;                                         //�ړ����x��"*3"����
                isAnimation = false;
                AnimationPlay();
            }
        }
        else if (nowAnimationNumber == (int)Enemy.HumanoidAnimation.ExitCar)
        {
            if (animationTimer >= nowAnimationLength / 2)
            {   //�A�j���[�V�����^�C�}�[������������
                defaultAnimationNumber = (int)Enemy.HumanoidAnimation.Run;//���݂̃A�j���[�V������"����"�ɂ���
                ActionReset();
            }
        }
        else if (animationTimer >= nowAnimationLength)
        {
            ActionReset();
        }
    }

    public void ActionReset()
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
        if (hp > 0)
        {
            audioSource.PlayOneShot(damage);//"damage"��炷

            if (enemyType == Enemy.EnemyType.Human.ToString() && nowAnimationNumber == defaultAnimationNumber)
            {
                isAnimation = true;                                      //"isAnimation = true"�ɂ���
                nowAnimationNumber = (int)Enemy.HumanoidAnimation.Damage;
                AnimationPlay();                                         //�֐�"Animation"�����s
            }
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

        if (enemyType == Enemy.EnemyType.Human.ToString())
        {
            audioSource.PlayOneShot(sEDeath);//"death"��炷

            //�ʒu(.Y)��"0.0f"�ɂ���
            this.transform.position
                = new Vector3(this.transform.position.x, 0.0f, this.transform.position.z);
            nowAnimationNumber = (int)Enemy.HumanoidAnimation.Death;
            AnimationPlay();                                        //�֐�"Animation"�����s
        }
        else if(enemyType == Enemy.EnemyType.Vehicle.ToString())
        {
            Instantiate(effectDeath, this.transform.position, this.transform.rotation);
            Invoke("Destroy", 1.0f);//�֐�"Destroy"��"5.0f"��Ɏ��s
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
        //�Փ˂����I�u�W�F�N�g�̃^�O��"PlayerBullet" && �̗͂�"0����"�̏ꍇ
        if (collision.gameObject.tag == "PlayerBullet" && hp > 0)
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
            Throw = 14,
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