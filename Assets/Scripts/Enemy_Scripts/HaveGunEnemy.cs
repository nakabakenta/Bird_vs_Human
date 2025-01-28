using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaveGunEnemy : EnemyBase
{
    public int nowMagazine;
    public int maxMagazine = 3;
    //���̃I�u�W�F�N�g�̃R���|�[�l���g
    public GameObject gun;           //"GameObject(�e)"
    public GameObject bullet;        //"GameObject(�e)"

    // Start is called before the first frame update
    void Start()
    {
        //�X�e�[�^�X��ݒ�
        enemyType = EnemyType.Wait.ToString();//�G�̌^
        hp = EnemyList.HaveGunEnemy.hp;       //�̗�
        nowMagazine = maxMagazine;
        //����������������
        playerFind = true;
        //�����̃A�j���[�V�����ԍ���ݒ肷��
        defaultAnimationNumber = (int)HumanoidAnimation.HaveGunIdle;
        //�֐������s����
        GetComponent();//�R���|�[�l���g����������
        StartEnemy();  //�G�̐ݒ������
    }

    // Update is called once per frame
    void Update()
    {
        UpdateEnemy();
    }

    public override void AddAction()
    {
        if (nowAnimationNumber == (int)HumanoidAnimation.HaveGunIdle)
        {
            nowAnimationNumber = (int)HumanoidAnimation.GunPlay;
            AnimationPlay();                                    //�֐�"AnimationPlay"�����s����
        }

        Direction();    //�֐�"Direction"�����s����
        AnimationFind();//�֐�"AnimationFind"�����s����
    }


    public override void AddAnimationChange()
    {
        if (nowMagazine <= 0)
        {
            nowAnimationNumber = (int)HumanoidAnimation.Reload;
            nowMagazine = maxMagazine;
        }
        else if (nowMagazine <= maxMagazine)
        {
            nowAnimationNumber = (int)HumanoidAnimation.GunPlay;
            Instantiate(bullet, gun.transform.position, Quaternion.identity);
            nowMagazine -= 1;
        }
    }

    public override void OnTriggerEnter(Collider collision)
    {
        base.OnTriggerEnter(collision);
    }
}