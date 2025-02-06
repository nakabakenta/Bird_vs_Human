using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowEnemy : EnemyBase
{
    // Start is called before the first frame update
    void Start()
    {
        //�X�e�[�^�X��ݒ肷��
        enemyType = Enemy.EnemyType.Human.ToString();//�G�̌^
        //�����̃A�j���[�V������ݒ肷��
        defaultAnimationNumber = (int)Enemy.HumanoidAnimation.Run;
        //�֐������s����
        GetComponent();//�R���|�[�l���g������
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

        if (viewPortPosition.x < moveRange[0].range[0].x && hp <= 0)
        {
            Destroy();//�֐�"Destroy"�����s����
        }
    }

    public override void Action()
    {
        if (isAnimation == true)
        {
            AnimationFind();        //�֐�"AnimationFind"�����s����
        }
        //
        else if (isAnimation == false)
        {
            if (PlayerBase.status != "Death")
            {
                SmoothPlayerDirection();//�֐�"SmoothPlayerDirection"�����s����
                Move();

                actionChangeTimer += Time.deltaTime;

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

        this.transform.position = new Vector3(this.transform.position.x, 0.0f, playerTransform.position.z);
    }

    public override void ActionChange()
    {
        //
        if (nowAnimationNumber == defaultAnimationNumber)
        {
            nowAnimationNumber = (int)Enemy.HumanoidAnimation.Throw;
        }

        isAnimation = true;
        AnimationPlay();//�֐�"AnimationPlay"�����s����
    }

    public override void ActionWait()
    {
        if (nowAnimationNumber == (int)Enemy.HumanoidAnimation.Throw)
        {
            if (animationTimer >= 2.5f)
            {
                if (nowBullet > 0)
                {
                    Vector3 bulletDirection = (playerTransform.position - this.transform.position).normalized;
                    Instantiate(bulletShot, positionShot.transform.position, Quaternion.LookRotation(bulletDirection));
                    nowBullet -= 1;
                }
            }

            if (animationTimer >= nowAnimationLength)
            {
                if (nowBullet <= 0)
                {
                    nowBullet = maxBullet;
                }
            }
        }

        base.ActionWait();
    }
}
