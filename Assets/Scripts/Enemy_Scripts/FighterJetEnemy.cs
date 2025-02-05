using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterJetEnemy : EnemyBase
{
    //処理
    private float bulletRotation;       //弾の回転

    // Start is called before the first frame update
    void Start()
    {
        //ステータスを設定
        enemyType = Enemy.EnemyType.Vehicle.ToString();//敵の型
        //関数を実行する
        GetComponent();//コンポーネントを所得する
        BaseStart();   //関数"BaseStart"を実行する
    }

    // Update is called once per frame
    void Update()
    {
        BaseUpdate();
    }

    public override void BaseUpdate()
    {
        base.BaseUpdate();

        action = true;
    }

    //関数"Action"
    public override void Action()
    {
        attackTimer += Time.deltaTime;//攻撃間隔に"Time.deltaTime(経過時間)"を足す

        if (viewPortPosition.x < moveRange[0].range[0].x)
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
            this.transform.eulerAngles = new Vector3(this.transform.rotation.x, (int)Characte.Direction.Horizontal, this.transform.rotation.z);
            bulletRotation = (int)Characte.Direction.Horizontal;
        }
        else if (viewPortPosition.x > moveRange[0].range[1].x)
        {
            this.transform.position = new Vector3(this.transform.position.x, playerTransform.position.y, this.transform.position.z);
            this.transform.eulerAngles = new Vector3(this.transform.rotation.x, -(int)Characte.Direction.Horizontal, this.transform.rotation.z);
            bulletRotation = -(int)Characte.Direction.Horizontal;
        }

        this.transform.position += moveSpeed * transform.forward * Time.deltaTime;//前方向に移動する

        if (attackTimer > attackInterval)
        {
            Instantiate(bullet, this.transform.position, Quaternion.Euler(this.transform.rotation.x, bulletRotation, this.transform.rotation.z));
            attackTimer = 0.0f;
        }
    }

    public override void DeathEnemy()
    {
        base.DeathEnemy();
        //
        Instantiate(effect, this.transform.position, this.transform.rotation);
        Invoke("Destroy", 1.0f);
    }
}
