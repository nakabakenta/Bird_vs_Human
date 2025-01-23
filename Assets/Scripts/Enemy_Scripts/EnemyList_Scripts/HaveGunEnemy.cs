using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaveGunEnemy : EnemyBase
{
    public int nowMagazine;
    public int maxMagazine = 1;
    //���̃I�u�W�F�N�g�̃R���|�[�l���g
    public GameObject gun;           //"GameObject(�e)"
    public GameObject bullet;        //"GameObject(�e)"

    // Start is called before the first frame update
    void Start()
    {
        //�X�e�[�^�X��ݒ�
        enemyType = EnemyType.HaveGunEnemy.ToString();//�^
        hp = EnemyList.HaveGunEnemy.hp;               //�̗�
        nowMagazine = maxMagazine;
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
        if (nowAnimationNumber == (int)HumanoidAnimation.HaveGunIdle)
        {
            nowAnimationNumber = (int)HumanoidAnimation.GunPlay;
            AnimationPlay();                                         //�֐�"AnimationPlay"�����s����
        }

        base.AnimationChange();

        if (animationTimer >= nowAnimationLength)
        {
            isAnimation = !isAnimation;
            animationTimer = 0.0f;

            if (nowMagazine > maxMagazine)
            {
                nowAnimationNumber = (int)HumanoidAnimation.GunPlay;
                Instantiate(bullet, gun.transform.position, Quaternion.identity);
                nowMagazine -= 1;
            }
            else if (nowMagazine <= maxMagazine)
            {
                nowAnimationNumber = (int)HumanoidAnimation.Reload;
                nowMagazine = maxMagazine;
            }

            AnimationPlay();                                       //�֐�"AnimationPlay"�����s����
        }
    }

    public override void OnTriggerEnter(Collider collision)
    {
        base.OnTriggerEnter(collision);
    }
}