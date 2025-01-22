using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkEnemy : EnemyBase
{
    // Start is called before the first frame update
    void Start()
    {
        //�X�e�[�^�X��ݒ肷��
        enemyName = EnemyName.WalkEnemy.ToString();//���O
        hp = EnemyList.WalkEnemy.hp;               //�̗�
        speed = EnemyList.WalkEnemy.speed;         //�ړ����x
        //�����̃A�j���[�V�����ԍ���ݒ肷��
        defaultAnimationNumber = (int)HumanoidAnimation.Walk;
        //�֐������s����
        GetComponent();//�R���|�[�l���g����������
        StartEnemy();  //�G�̐ݒ������
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