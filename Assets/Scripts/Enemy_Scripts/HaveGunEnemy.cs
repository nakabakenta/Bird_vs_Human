using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaveGunEnemy : EnemyBase
{
    public int maxMagazine;
    private int nowMagazine;

    // Start is called before the first frame update
    void Start()
    {
        //�X�e�[�^�X��ݒ�
        enemyType = Enemy.EnemyType.Human.ToString();//�G�̌^
        nowMagazine = maxMagazine;
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

        if (viewPortPosition.x < 1)
        {
            action = true;
        }

        if (viewPortPosition.x < 0)
        {
            Destroy();//�֐�"Destroy"�����s����
        }
    }

    public override void Action()
    {
        if (PlayerBase.status != "Death")
        {
            PlayerFind();   //�֐�"PlayerFind"�����s����
            ActionChange();
            AnimationFind();//�֐�"AnimationFind"�����s����
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
            if (nowMagazine <= 0)
            {
                nowAnimationNumber = (int)Enemy.HumanoidAnimation.Reload;
                nowMagazine = maxMagazine;
            }
            else if (nowMagazine <= maxMagazine)
            {
                nowAnimationNumber = (int)Enemy.HumanoidAnimation.GunPlay;
                Instantiate(bullet, shotPosition.transform.position, Quaternion.identity);
                nowMagazine -= 1;
            }
        }

        base.ActionWait();
    }
}