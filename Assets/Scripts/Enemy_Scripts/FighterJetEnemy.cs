using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterJetEnemy : EnemyBase
{
    // Start is called before the first frame update
    void Start()
    {
        //�X�e�[�^�X��ݒ�
        enemyType = Enemy.EnemyType.Vehicle.ToString();//�G�̌^
        action = true;
        //�֐������s����
        GetComponent();//�R���|�[�l���g����������
        BaseStart();   //�֐�"BaseStart"�����s����
    }

    // Update is called once per frame
    void Update()
    {
        BaseUpdate();
    }

    public override void BaseUpdate()
    {
        base.BaseUpdate();
    }

    //�֐�"Action"
    public override void Action()
    {
        Move();

        if (viewPortPosition.x > moveRange[0].range[0].x && viewPortPosition.x < moveRange[0].range[1].x)
        {
            bulletShotTimer += Time.deltaTime;//�U���Ԋu��"Time.deltaTime(�o�ߎ���)"�𑫂�

            if (bulletShotTimer >= bulletShotInterval)
            {
                audioSource.PlayOneShot(sEShot);
                Instantiate(bulletShot, positionShot.transform.position, this.transform.rotation);
                bulletShotTimer = 0.0f;
            }
        }
        else if (viewPortPosition.x < moveRange[0].range[0].x || viewPortPosition.x > moveRange[0].range[1].x)
        {
            CoarsePlayerDirection();
        }  
    }

    public override void DeathEnemy()
    {
        base.DeathEnemy();

        rigidBody.useGravity = true;//RigidBody�̏d�͂�"�L��"�ɂ���
        boxCollider.isTrigger = false;
    }
}
