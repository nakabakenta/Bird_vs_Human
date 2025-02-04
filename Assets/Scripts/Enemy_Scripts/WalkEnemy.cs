using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkEnemy : EnemyBase
{
    // Start is called before the first frame update
    void Start()
    {
        //�X�e�[�^�X��ݒ肷��
        enemyType = Enemy.EnemyType.Human.ToString();     //�G�̌^
        enemyOption = Enemy.EnemyOption.Normal.ToString();//
        hp = EnemyList.WalkEnemy.hp;                      //�̗�
        moveSpeed = EnemyList.WalkEnemy.speed;            //�ړ����x
        //�����̃A�j���[�V�����ԍ���ݒ肷��
        defaultAnimationNumber = (int)Enemy.HumanoidAnimation.Walk;
        //�֐������s����
        GetComponent();//�R���|�[�l���g����������
        BaseStart();   //�֐�"BaseStart"�����s����
    }

    // Update is called once per frame
    void Update()
    {
        BaseUpdate();
    }
}