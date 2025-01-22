using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaveGunEnemy : EnemyBase
{
    public bool isReload = false;
    //���̃I�u�W�F�N�g�̃R���|�[�l���g
    public GameObject gun;           //"GameObject(�e)"
    public GameObject bullet;        //"GameObject(�e)"

    // Start is called before the first frame update
    void Start()
    {
        //�X�e�[�^�X��ݒ�
        enemyName = EnemyName.HaveGunEnemy.ToString();//���O
        hp = EnemyList.HaveGunEnemy.hp;            //�̗�
        //�����̃A�j���[�V�����ԍ���ݒ肷��
        defaultAnimationNumber = (int)HumanoidAnimation.HaveGunIdle;
        //�֐������s����
        GetComponent();//�R���|�[�l���g����������
        StartEnemy();  //�G�̐ݒ������
    }

    // Update is called once per frame
    void Update()
    {
        base.UpdateEnemy();

        if (isAction == true)
        {
            AnimationChange();//�֐�"Wait"�����s
        }
    }

    public override void AnimationChange()
    {
        base.AnimationChange();

        if (nowAnimationNumber == (int)HumanoidAnimation.HaveGunIdle)
        {
            nowAnimationNumber = (int)HumanoidAnimation.GunPlay;
            AnimationPlay();                                          //�֐�"AnimationPlay"�����s����
        }
        else
        {
            if(isReload == true)
            {
                Instantiate(bullet, gun.transform.position, Quaternion.identity);
                isReload = true;
            }
            else if(isReload == false)
            {
                // �e�̕������v�Z
                if (animationTimer >= nowAnimationLength)
                {
                    isAnimation = !isAnimation;
                    animationTimer = 0.0f;


                    nowAnimationNumber = (int)HumanoidAnimation.Reload;

                    AnimationPlay();                                   //�֐�"AnimationPlay"�����s����
                }


            }

            if (animationTimer >= nowAnimationLength)
            {
                isReload = false;
                animationTimer = 0.0f;
                nowAnimationNumber = (int)HumanoidAnimation.GunPlay;
                AnimationPlay();                                    //�֐�"AnimationPlay"�����s����
            }

        }
    }


    public override void OnTriggerEnter(Collider collision)
    {
        base.OnTriggerEnter(collision);
    }
}