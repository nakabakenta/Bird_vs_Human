using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarRideEnemy : EnemyBase
{
    // Start is called before the first frame update
    void Start()
    {
        //�X�e�[�^�X��ݒ肷��
        enemyType = Enemy.EnemyType.Human.ToString();   //�G�̌^
        //����������������
        isAnimation = true;
        //�����̃A�j���[�V������ݒ肷��
        defaultAnimationNumber = (int)Enemy.HumanoidAnimation.ExitCar;
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

        if (viewPortPosition.x < moveRange[0].range[0].x && hp <= 0)
        {
            Destroy();//�֐�"Destroy"�����s����
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

                if (this.transform.position.x + actionRange.x > playerTransform.position.x &&
                    this.transform.position.x - actionRange.x < playerTransform.position.x &&
                    this.transform.position.y + actionRange.y < playerTransform.position.y &&
                    nowAnimationNumber == defaultAnimationNumber)
                {
                    isAnimation = true;
                    nowAnimationNumber = (int)Enemy.HumanoidAnimation.Jump;
                    AnimationPlay();
                }
            }
            else if (PlayerBase.status == "Death")
            {
                nowAnimationNumber = (int)Enemy.HumanoidAnimation.Dance;
                AnimationPlay();                                        //�֐�"AnimationPlay"�����s����
            }
        }

        if (nowAnimationNumber != (int)Enemy.HumanoidAnimation.Jump &&
            nowAnimationNumber != (int)Enemy.HumanoidAnimation.JumpAttack)
        {
            this.transform.position = new Vector3(this.transform.position.x, 0.0f, playerTransform.position.z);
        }
    }
}