using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankEnemy : EnemyBase
{
    //処理
    private float bulletRotation;//弾の回転
    //このオブジェクトのコンポーネント
    public GameObject turret, muzzle, bullet, shotPosition;

    // Start is called before the first frame update
    void Start()
    {
        enemyType = Enemy.EnemyType.Vehicle.ToString();   //敵の型
        enemyOption = Enemy.EnemyOption.Normal.ToString();//
        //関数を実行する
        GetComponent();//コンポーネントを所得する
        BaseStart();   //関数"BaseStart"を実行する
        Direction();
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

        if(muzzle.transform.rotation.x >= -30.0f)
        {
            
        }

        direction = (playerTransform.position - muzzle.transform.position).normalized;
        //direction.x = 0.0f;
        //direction.y = 0f;
        //direction.z = 0;
        direction.Normalize();

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        muzzle.transform.rotation = Quaternion.Slerp(muzzle.transform.rotation, targetRotation, Time.deltaTime * 1.0f);


        //turret.transform.Rotate(new Vector3(rotateSpeed.x, rotateSpeed.y, rotateSpeed.z) * Time.deltaTime);


        if (this.transform.position.x < playerTransform.position.x)
        {
            bulletRotation = (int)Characte.Direction.Horizontal;
        }
        else if (this.transform.position.x > playerTransform.position.x)
        {
            bulletRotation = -(int)Characte.Direction.Horizontal;
        }

        if (attackTimer > attackInterval)
        {
            Instantiate(bullet, shotPosition.transform.position, Quaternion.Euler(this.transform.rotation.x, bulletRotation, this.transform.rotation.z));
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