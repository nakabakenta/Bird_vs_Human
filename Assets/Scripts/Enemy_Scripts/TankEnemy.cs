using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankEnemy : EnemyBase
{
    //このオブジェクトのコンポーネント
    public GameObject shotPosition, bullet;

    // Start is called before the first frame update
    void Start()
    {
        enemyType = Enemy.EnemyType.Vehicle.ToString(); //敵の型
        enemyOption = Enemy.EnemyOption.Find.ToString();//
        //関数を実行する
        GetComponent();//コンポーネントを所得する
        BaseStart();   //関数"BaseStart"を実行する
    }

    // Update is called once per frame
    void Update()
    {
        BaseUpdate();
    }

    //関数"Action"
    public override void Action()
    {
        Attack();
        Move();
        Direction();//関数"Direction"を実行する
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
        Invoke("Destroy", 1.0f);                                                             //関数"Destroy"を"5.0f"後に実行
    }
}