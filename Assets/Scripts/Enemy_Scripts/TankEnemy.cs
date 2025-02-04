using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankEnemy : EnemyBase
{
    //���̃I�u�W�F�N�g�̃R���|�[�l���g
    public GameObject shotPosition, bullet;

    // Start is called before the first frame update
    void Start()
    {
        enemyType = Enemy.EnemyType.Vehicle.ToString(); //�G�̌^
        enemyOption = Enemy.EnemyOption.Find.ToString();//
        //�֐������s����
        GetComponent();//�R���|�[�l���g����������
        BaseStart();   //�֐�"BaseStart"�����s����
    }

    // Update is called once per frame
    void Update()
    {
        BaseUpdate();
    }

    //�֐�"Action"
    public override void Action()
    {
        Attack();
        Move();
        Direction();//�֐�"Direction"�����s����
    }

    public override void Attack()
    {
        base.Attack();

        if (attackTimer > attackInterval)
        {
            Instantiate(bullet, shotPosition.transform.position, this.transform.rotation);
            attackTimer = 0.0f;
        }
    }

    public override void DeathEnemy()
    {
        base.DeathEnemy();
        //
        Instantiate(effect, this.transform.position, this.transform.rotation, thisTransform);
        Invoke("Destroy", 1.0f);                                                             //�֐�"Destroy"��"5.0f"��Ɏ��s
    }
}