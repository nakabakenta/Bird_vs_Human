using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : EnemyBase
{
    // Start is called before the first frame update
    void Start()
    {
        //�X�e�[�^�X��ݒ肷��
        enemyType = Enemy.EnemyType.Human.ToString();   //�G�̌^
        bossEnemy = true;
        //�����̃A�j���[�V������ݒ肷��
        defaultAnimationNumber = (int)Enemy.HumanoidAnimation.Walk;
        //�֐������s����
        GetComponent();//�R���|�[�l���g����������
        BaseStart();   //�֐�"BaseStart"�����s����
    }

    // Update is called once per frame
    void Update()
    {
        BaseUpdate();
    }

    public override void BaseUpdate()
    {
        base.BaseUpdate();

        if (viewPortPosition.x < moveRange[0].range[1].x)
        {
            action = true;
        }
    }

    public override void Action()
    {
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
                PlayerDirection();//�֐�"PlayerDirection"�����s����
                Move();

                actionChangeTimer += Time.deltaTime;

                if (this.transform.position.x + actionRange.x > playerTransform.position.x &&
                    this.transform.position.x - actionRange.x < playerTransform.position.x &&
                    this.transform.position.y + actionRange.y < playerTransform.position.y &&
                    nowAnimationNumber == defaultAnimationNumber)
                {
                    isAnimation = true;
                    nowAnimationNumber = (int)Enemy.HumanoidAnimation.Jump;
                    AnimationPlay();
                }

                if (actionChangeTimer >= actionChangeInterval)
                {
                    isAnimation = true;
                    actionChangeTimer = 0.0f;
                    ActionChange();
                }
            }
            else if (PlayerBase.status == "Death")
            {
                nowAnimationNumber = (int)Enemy.HumanoidAnimation.Dance;
                AnimationPlay();                                        //�֐�"AnimationPlay"�����s����
            }
        }

        if(nowAnimationNumber == (int)Enemy.HumanoidAnimation.Throw)
        {
            PlayerDirection();
        }

        if (nowAnimationNumber != (int)Enemy.HumanoidAnimation.Jump &&
            nowAnimationNumber != (int)Enemy.HumanoidAnimation.JumpAttack)
        {
            this.transform.position = new Vector3(this.transform.position.x, 0.0f, playerTransform.position.z);
        }
    }

    public override void ActionChange()
    {
        //
        if (nowAnimationNumber == defaultAnimationNumber)
        {
            int[] randomAnimation = { 14, 23 };
            nowAnimationNumber = randomAnimation[Random.Range(0, randomAnimation.Length)];
        }
        if (nowAnimationNumber == (int)Enemy.HumanoidAnimation.CrazyRun)
        {
            nowAnimationNumber = (int)Enemy.HumanoidAnimation.JumpAttack;//���݂̃A�j���[�V������"�W�����v�U��"�ɂ���
        }

        AnimationPlay();//�֐�"AnimationPlay"�����s����
    }

    public override void ActionWait()
    {
        if (nowAnimationNumber == (int)Enemy.HumanoidAnimation.Throw)
        {
            if (animationTimer >= 2.25f)
            {
                if (nowBullet > 0)
                {
                    Instantiate(bullet, shotPosition.transform.position, Quaternion.identity);
                    nowBullet -= 1;
                }
            }

            if (animationTimer >= nowAnimationLength)
            {
                if(nowBullet <= 0)
                {
                    nowBullet = maxBullet;
                }
            }
        }

        base.ActionWait();
    }

    public override void DeathEnemy()
    {
        bossEnemy = false;
        base.DeathEnemy();
    }
}