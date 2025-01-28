using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterJetEnemy : EnemyBase
{
    //処理
    private float attackTimer = 0.5f;   //攻撃間隔タイマー
    private float attackInterval = 0.5f;//攻撃間隔
    private float bulletRotation;       //弾の回転
    //このオブジェクトのコンポーネント
    public GameObject bullet;       //"GameObject(弾)"
    public GameObject effect;       //"GameObject(エフェクト)"
    public AudioClip explosion;     //"AudioClip(爆発)"

    // Start is called before the first frame update
    void Start()
    {
        //ステータスを設定
        enemyType = EnemyType.Vehicle.ToString();//敵の型
        hp = EnemyList.FighterJetEnemy.hp;      //体力
        speed = EnemyList.FighterJetEnemy.speed;//移動速度

        //関数を実行する
        GetComponent();//コンポーネントを所得する
    }

    // Update is called once per frame
    void Update()
    {
        UpdateEnemy();
    }

    //関数"Action"
    public override void Action()
    {
        attackTimer += Time.deltaTime;//攻撃間隔に"Time.deltaTime(経過時間)"を足す

        if (viewPortPosition.x < -0.5)
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
            this.transform.eulerAngles = new Vector3(this.transform.rotation.x, EnemyList.rotation, this.transform.rotation.z);
            bulletRotation = EnemyList.rotation;
        }
        else if (viewPortPosition.x > 1.5)
        {
            this.transform.position = new Vector3(this.transform.position.x, playerTransform.position.y, this.transform.position.z);
            this.transform.eulerAngles = new Vector3(this.transform.rotation.x, -EnemyList.rotation, this.transform.rotation.z);
            bulletRotation = -EnemyList.rotation;
        }

        this.transform.position += speed * transform.forward * Time.deltaTime;//前方向に移動する

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
        Instantiate(effect, this.transform.position, this.transform.rotation, thisTransform);
        audioSource.PlayOneShot(explosion);                                                  //"explosion"を鳴らす
        Invoke("Destroy", 1.0f);                                                             //関数"Destroy"を"5.0f"後に実行
    }

    public override void OnTriggerEnter(Collider collision)
    {
        base.OnTriggerEnter(collision);
    }
}
