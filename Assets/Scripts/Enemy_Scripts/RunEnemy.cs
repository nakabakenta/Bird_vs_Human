using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunEnemy : EnemyBase
{
    // Start is called before the first frame update
    void Start()
    {
        //�X�e�[�^�X��ݒ肷��
        enemyType = Enemy.EnemyType.Human.ToString();   //�G�̌^
        enemyOption = Enemy.EnemyOption.Find.ToString();//
        hp = EnemyList.RunEnemy.hp;                     //�̗�
        speed = EnemyList.RunEnemy.speed;               //�ړ����x
        jump = EnemyList.RunEnemy.jump;                 //�W�����v��
        //����������������
        playerFind = true;
        //�����̃A�j���[�V������ݒ肷��
        defaultAnimationNumber = (int)Enemy.HumanoidAnimation.Run;
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