using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankEnemy : EnemyBase
{
    // Start is called before the first frame update
    void Start()
    {
        enemyType = Enemy.EnemyType.Vehicle.ToString(); //�G�̌^
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

        if (viewPortPosition.x < 1)
        {
            action = true;
        }

        if (viewPortPosition.x < 0 && hp <= 0)
        {
            Destroy();//�֐�"Destroy"�����s����
        }
    }

    //�֐�"Action"
    public override void Action()
    {
        if (PlayerBase.status != "Death")
        {
            Move();

            if(viewPortPosition.x < 0 || viewPortPosition.x > 1)
            {
                direction = playerTransform.position - this.transform.position;
                direction.y = 0.0f;

                this.transform.rotation = Quaternion.LookRotation(direction);
            }

            attackTimer += Time.deltaTime;

            if (this.transform.position.x + 3.0f > playerTransform.position.x &&
                this.transform.position.x - 3.0f < playerTransform.position.x)
            {
                if (attackTimer > attackInterval)
                {
                    Instantiate(bullet, shotPosition.transform.position, this.transform.rotation);
                    attackTimer = 0.0f;
                }
            }
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