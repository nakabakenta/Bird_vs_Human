using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkEnemy : EnemyBase
{

    // Start is called before the first frame update
    void Start()
    {
        GetComponent();
        base.StartEnemy();
        //�X�e�[�^�X��ݒ�
        hp = EnemyList.WalkEnemy.hp;      //�̗�
        speed = EnemyList.WalkEnemy.speed;//�ړ����x
        //
        Direction();
        //
        nowAnimationNumber = (int)HumanoidAnimation.Walk;
        nowAnimationName = HumanoidAnimation.Walk.ToString();
        AnimationPlay();                                     //�֐�"AnimationPlay"�����s����
    }

    // Update is called once per frame
    void Update()
    {
        base.UpdateEnemy();
    }

    public override void OnTriggerEnter(Collider collision)
    {
        base.OnTriggerEnter(collision);
    }
}