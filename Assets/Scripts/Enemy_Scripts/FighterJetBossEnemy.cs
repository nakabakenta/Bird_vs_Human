using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterJetBossEnemy : EnemyBase
{
    private Stage stage;

    // Start is called before the first frame update
    void Start()
    {
        //ステータスを設定
        enemyType = Enemy.EnemyType.Vehicle.ToString();//敵の型
        action = true;
        bossEnemy = true;
        //関数を実行する
        GetComponent();//コンポーネントを所得する
        BaseStart();   //関数"BaseStart"を実行する
    }

    // Update is called once per frame
    void Update()
    {
        if (Stage.nowStage == 5 && Stage.killCount >= 8)
        {
            BaseUpdate();
        }
    }

    public override void BaseUpdate()
    {
        base.BaseUpdate();

        if (viewPortPosition.y < moveRange[0].range[0].y && hp <= 0)
        {
            Destroy();//関数"Destroy"を実行する
        }
    }

    //関数"Action"
    public override void Action()
    {
        Move();

        if (viewPortPosition.x > moveRange[0].range[0].x && viewPortPosition.x < moveRange[0].range[1].x)
        {
            bulletShotTimer += Time.deltaTime;//攻撃間隔に"Time.deltaTime(経過時間)"を足す

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
        bossEnemy = false;
        base.DeathEnemy();
        rigidBody.useGravity = true;//RigidBodyの重力を"有効"にする
        boxCollider.isTrigger = false;
    }
}