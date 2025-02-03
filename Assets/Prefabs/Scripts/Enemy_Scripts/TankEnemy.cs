using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankEnemy : EnemyBase
{
    //このオブジェクトのコンポーネント
    public float turretMoveSeppd, muzzleMoveSeppd;
    public GameObject turret, muzzle, bullet, shotPosition;
    private Vector3 turretRotation, muzzleRotation;

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
        Move();

        turretRotation = turret.transform.localRotation.eulerAngles;

        Debug.Log(turretRotation.y);

        //
        if (this.transform.position.x > playerTransform.position.x)
        {
            if(turretRotation.y < 180.0f)
            {
                turret.transform.Rotate(new Vector3(0.0f, turret.transform.rotation.y - turretMoveSeppd * Time.deltaTime, 0.0f));
            }
        }
        
        //if (this.transform.position.x < playerTransform.position.x)
        //{
            
        //}

        //turretRotation = playerTransform.position - this.transform.position;
        //muzzleRotation = playerTransform.position - this.transform.position;
        //turretRotation.y = 0.0f;

        //if (turretRotation != Vector3.zero)
        //{
        //    Quaternion turretQuaternion = Quaternion.LookRotation(turretRotation);
        //    turret.transform.rotation = Quaternion.RotateTowards(turret.transform.rotation, turretQuaternion, turretMoveSeppd * Time.deltaTime);
        //}
        //else
        //{
        //    Quaternion muzzleQuaternion = Quaternion.LookRotation(muzzleRotation);
        //    muzzle.transform.rotation = Quaternion.RotateTowards(muzzle.transform.rotation, muzzleQuaternion, muzzleMoveSeppd * Time.deltaTime);
        //}

        if (attackTimer > attackInterval)
        {
            Instantiate(bullet, shotPosition.transform.position, Quaternion.identity);
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