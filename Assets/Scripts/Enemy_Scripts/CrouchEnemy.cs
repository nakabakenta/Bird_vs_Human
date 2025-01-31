using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchEnemy : EnemyBase
{
    // Start is called before the first frame update
    void Start()
    {
        //�X�e�[�^�X��ݒ肷��
        enemyType = Enemy.EnemyType.Human.ToString();   //�G�̌^
        enemyOption = Enemy.EnemyOption.Wait.ToString();//
        hp = EnemyList.CrouchEnemy.hp;                  //�̗�
        //�����̃A�j���[�V������ݒ肷��
        defaultAnimationNumber = (int)Enemy.HumanoidAnimation.Crouch;
        playerFind = false;

        //�֐������s����
        GetComponent();  //�R���|�[�l���g������
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
