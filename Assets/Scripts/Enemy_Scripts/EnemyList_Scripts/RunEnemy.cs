using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunEnemy : EnemyBase
{
    // Start is called before the first frame update
    void Start()
    {
        //�X�e�[�^�X��ݒ�
        enemyName = EnemyName.RunEnemy.ToString();//���O
        hp = EnemyList.RunEnemy.hp;               //�̗�
        speed = EnemyList.RunEnemy.speed;         //�ړ����x
        jump = EnemyList.RunEnemy.jump;           //�W�����v��
        //�����̃A�j���[�V������ݒ肷��
        defaultAnimationNumber = (int)HumanoidAnimation.Run;
        //�֐������s����
        GetComponent();//�R���|�[�l���g������
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