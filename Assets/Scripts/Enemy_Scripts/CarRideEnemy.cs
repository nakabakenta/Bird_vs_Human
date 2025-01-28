using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarRideEnemy : EnemyBase
{
    // Start is called before the first frame update
    void Start()
    {
        //�X�e�[�^�X��ݒ肷��
        enemyType = EnemyType.Find.ToString();//�G�̌^
        hp = EnemyList.CarRideEnemy.hp;       //�̗�
        speed = EnemyList.CarRideEnemy.speed; //�ړ����x
        jump = EnemyList.CarRideEnemy.jump;   //�W�����v��
        //����������������
        playerFind = true;
        isAnimation = true;
        //�����̃A�j���[�V������ݒ肷��
        defaultAnimationNumber = (int)HumanoidAnimation.ExitCar;
        //�֐������s����
        GetComponent();  //�R���|�[�l���g����������
        StartAnimation();//�J�n���̃A�j���[�V������ݒ肷��
    }

    // Update is called once per frame
    void Update()
    {
        base.UpdateEnemy();
    }

    //�����蔻��(OnTriggerEnter)
    public override void OnTriggerEnter(Collider collision)
    {
        base.OnTriggerEnter(collision);
    }
}