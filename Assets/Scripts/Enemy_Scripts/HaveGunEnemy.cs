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
        enemyType = Enemy.EnemyType.Human.ToString();   //�G�̌^
        enemyOption = Enemy.EnemyOption.Wait.ToString();//
        hp = EnemyList.HaveGunEnemy.hp;                 //�̗�
        nowMagazine = maxMagazine;
        //����������������
        playerFind = true;
        //�����̃A�j���[�V�����ԍ���ݒ肷��
        defaultAnimationNumber = (int)Enemy.HumanoidAnimation.HaveGunIdle;
        //�֐������s����
        GetComponent();  //�R���|�[�l���g����������
        StartAnimation();//�J�n���̃A�j���[�V������ݒ肷��
    }

    // Update is called once per frame
    void Update()
    {
        UpdateEnemy();
    }

    public override void AddAction()
    {
        if (nowAnimationNumber == (int)Enemy.HumanoidAnimation.HaveGunIdle)
        {
            nowAnimationNumber = (int)Enemy.HumanoidAnimation.GunPlay;
            AnimationPlay();                                    //�֐�"AnimationPlay"�����s����
        }

        Direction();    //�֐�"Direction"�����s����
        AnimationFind();//�֐�"AnimationFind"�����s����
    }


    public override void AddAnimationChange()
    {
        if (nowMagazine <= 0)
        {
            nowAnimationNumber = (int)Enemy.HumanoidAnimation.Reload;
            nowMagazine = maxMagazine;
        }
        else if (nowMagazine <= maxMagazine)
        {
            nowAnimationNumber = (int)Enemy.HumanoidAnimation.GunPlay;
            Instantiate(bullet, gun.transform.position, Quaternion.identity);
            nowMagazine -= 1;
        }
    }

    public override void OnTriggerEnter(Collider collision)
    {
        base.OnTriggerEnter(collision);
    }
}