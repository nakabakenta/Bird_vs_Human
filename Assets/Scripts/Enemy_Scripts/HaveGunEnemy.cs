using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaveGunEnemy : EnemyBase
{
    public int maxMagazine;
    private int nowMagazine;
    //���̃I�u�W�F�N�g�̃R���|�[�l���g
    public GameObject gun;   //"GameObject(�e)"
    public GameObject bullet;//"GameObject(�e)"

    // Start is called before the first frame update
    void Start()
    {
        //�X�e�[�^�X��ݒ�
        enemyType = Enemy.EnemyType.Human.ToString();   //�G�̌^
        enemyOption = Enemy.EnemyOption.Wait.ToString();//
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