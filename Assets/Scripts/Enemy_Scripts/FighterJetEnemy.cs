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

        if (viewPortPosition.y < moveRange[0].range[0].y && hp <= 0)
        {
            Destroy();//�֐�"Destroy"�����s����
        }
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
            this.transform.position = new Vector3(this.transform.position.x, playerTransform.position.y, this.transform.position.z);
            CoarsePlayerDirection();
        }  
    }

    public override void DeathEnemy()
    {
        base.DeathEnemy();

        rigidBody.useGravity = true;  //RigidBody�̏d�͂�"�L��"�ɂ���
        boxCollider.isTrigger = false;

        if(Stage.nowStage == 5)
        {
            Stage.killCount += 1;
        }
    }
}
