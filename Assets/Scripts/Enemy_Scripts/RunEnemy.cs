using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunEnemy : EnemyBase
{
    // Start is called before the first frame update
    void Start()
    {
        //�X�e�[�^�X��ݒ肷��
        enemyType = Enemy.EnemyType.Human.ToString();   //�G�̌^
        //�����̃A�j���[�V������ݒ肷��
        defaultAnimationNumber = (int)Enemy.HumanoidAnimation.Run;
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

        if (viewPortPosition.x < 1)
        {
            action = true;
        }

        if (viewPortPosition.x < 0 && hp <= 0)
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
                PlayerFind();//�֐�"PlayerFind"�����s����
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