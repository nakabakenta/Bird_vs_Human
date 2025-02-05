using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterEnemy : EnemyBase
{
    public GameObject[] propeller;

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

        if (viewPortPosition.x < moveRange[0].range[1].x)
        {
            action = true;
        }

        if (viewPortPosition.x < moveRange[0].range[0].x && hp <= 0)
        {
            Destroy();//�֐�"Destroy"�����s����
        }
    }

    //�֐�"Action"
    public override void Action()
    {
        propeller[0].transform.Rotate(transform.right, rotationSpeed * Time.deltaTime);
        propeller[1].transform.Rotate(transform.forward, rotationSpeed * Time.deltaTime);

        if (PlayerBase.status != "Death")
        {
            Move();

            if (viewPortPosition.x > moveRange[0].range[0].x && viewPortPosition.x < moveRange[0].range[1].x)
            {
                bulletShotTimer += Time.deltaTime;

                if (this.transform.position.y + actionRange.y > playerTransform.position.y &&
                    this.transform.position.y - actionRange.y < playerTransform.position.y)
                {
                    if (bulletShotTimer >= bulletShotInterval)
                    {
                        audioSource.PlayOneShot(sEShot);
                        Instantiate(bulletShot, positionShot.transform.position, this.transform.rotation);
                        bulletShotTimer = 0.0f;
                    }
                }
            }
            else if (viewPortPosition.x < moveRange[0].range[0].x || viewPortPosition.x > moveRange[0].range[1].x)
            {
                CoarsePlayerDirection();
            }
        }
    }

    public override void DeathEnemy()
    {
        base.DeathEnemy();
    }
}
