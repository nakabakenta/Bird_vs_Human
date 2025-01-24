using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchEnemy : EnemyBase
{
    // Start is called before the first frame update
    void Start()
    {
        //�X�e�[�^�X��ݒ肷��
        enemyType = EnemyType.Wait.ToString();//�G�̌^
        hp = EnemyList.CrouchEnemy.hp;        //�̗�
        //�����̃A�j���[�V������ݒ肷��
        defaultAnimationNumber = (int)HumanoidAnimation.Crouch;
        isPlayerFind = false;

        //�֐������s����
        GetComponent();//�R���|�[�l���g������
        StartEnemy();  //�G�̐ݒ������
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
