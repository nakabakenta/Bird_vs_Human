using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : EnemyBase
{
    // Start is called before the first frame update
    void Start()
    {
        //�X�e�[�^�X��ݒ肷��
        enemyType = Enemy.EnemyType.Human.ToString();   //�G�̌^
        enemyOption = Enemy.EnemyOption.Boss.ToString();//
        hp = EnemyList.BossEnemy.hp[Stage.nowStage - 1];      
        speed = EnemyList.BossEnemy.speed[Stage.nowStage - 1];
        jump = EnemyList.BossEnemy.jump[Stage.nowStage - 1];
        playerFind = true;
        bossEnemy = true;

        //�����̃A�j���[�V������ݒ肷��
        defaultAnimationNumber = (int)Enemy.HumanoidAnimation.Walk;
        //�֐������s����
        GetComponent();  //�R���|�[�l���g����������
        StartAnimation();//�J�n���̃A�j���[�V������ݒ肷��
    }

    // Update is called once per frame
    void Update()
    {
        UpdateEnemy();
    }

    public override void DeathEnemy()
    {
        bossEnemy = false;
        base.DeathEnemy();
    }

    //�����蔻��(OnTriggerEnter)
    public override void OnTriggerEnter(Collider collision)
    {
        base.OnTriggerEnter(collision);
    }
}