using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaveGunEnemy : EnemyBase
{
    // Start is called before the first frame update
    void Start()
    {
        //�X�e�[�^�X��ݒ�
        enemyType = Enemy.EnemyType.Human.ToString();//�G�̌^
        //�����̃A�j���[�V�����ԍ���ݒ肷��
        defaultAnimationNumber = (int)Enemy.HumanoidAnimation.HaveGunIdle;
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

        if (viewPortPosition.x < moveRange[0].range[0].x)
        {
            Destroy();//�֐�"Destroy"�����s����
        }
    }

    public override void Action()
    {
        if (PlayerBase.status != "Death")
        {
            SmoothPlayerDirection();//�֐�"SmoothPlayerDirection"�����s����
            ActionChange();
            AnimationFind(); //�֐�"AnimationFind"�����s����
        }
        else if (PlayerBase.status == "Death")
        {
            nowAnimationNumber = (int)Enemy.HumanoidAnimation.Dance;
            AnimationPlay();                                        //�֐�"AnimationPlay"�����s����
        }

        this.transform.position = new Vector3(this.transform.position.x, 0.0f, playerTransform.position.z);
    }

    public override void ActionChange()
    {
        if (nowAnimationNumber == (int)Enemy.HumanoidAnimation.HaveGunIdle)
        {
            nowAnimationNumber = (int)Enemy.HumanoidAnimation.GunPlay;
            AnimationPlay();                                          //�֐�"AnimationPlay"�����s����
        }
    }

    public override void ActionWait()
    {
        if(animationTimer >= nowAnimationLength)
        {
            if (nowBullet <= 0)
            {
                nowAnimationNumber = (int)Enemy.HumanoidAnimation.Reload;
                nowBullet = maxBullet;
            }
            else if (nowBullet > 0)
            {
                nowAnimationNumber = (int)Enemy.HumanoidAnimation.GunPlay;
                Instantiate(shotBullet, shotPosition.transform.position, Quaternion.identity);
                nowBullet -= 1;

                audioSource.PlayOneShot(shot);
            }
        }

        base.ActionWait();
    }
}