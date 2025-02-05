using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterEnemy : EnemyBase
{
    public GameObject aaa;

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
        aaa.transform.Rotate(transform.right, rotationSpeed * Time.deltaTime);

        if (PlayerBase.status != "Death")
        {
            Move();

            if (viewPortPosition.x < moveRange[0].range[0].x || viewPortPosition.x > moveRange[0].range[1].x)
            {
                audioMove.SetActive(false);
                CoarsePlayerDirection();
            }
            if (viewPortPosition.x > moveRange[0].range[0].x && viewPortPosition.x < moveRange[0].range[1].x)
            {
                audioMove.SetActive(true);
                attackTimer += Time.deltaTime;

                if (attackTimer >= shotBulletInterval)
                {
                    audioSource.PlayOneShot(shot);
                    //Instantiate(shotBullet, shotPosition.transform.position, this.transform.rotation);
                    attackTimer = 0.0f;
                }
            }
        }
    }

    public override void DeathEnemy()
    {
        base.DeathEnemy();
        //
        Instantiate(effect, this.transform.position, this.transform.rotation);
        Invoke("Destroy", 1.0f);                                              //�֐�"Destroy"��"1.0f"��Ɏ��s
    }
}
