using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterJetBossEnemy : MonoBehaviour
{
    //ステータス
    private int hp;     //体力
    private float speed;//移動速度
    //処理
    private float attackTimer = 0.5f;   //攻撃間隔タイマー
    private float attackInterval = 0.5f;//攻撃間隔
    private float viewPointX;           //ビューポイント座標.X
    private float bulletRotation;       //弾の回転
    //このオブジェクトのコンポーネント
    public GameObject bullet;       //"GameObject(弾)"
    public GameObject effect;       //"GameObject(エフェクト)"
    public AudioClip damage;        //"AudioClip(ダメージ)"
    public AudioClip explosion;     //"AudioClip(爆発)"
    private Transform thisTransform;//"Transform"
    private AudioSource audioSource;//"AudioSource"
    //他のオブジェクトのコンポーネント
    private Transform playerTransform;//"Transform(プレイヤー)"

    // Start is called before the first frame update
    void Start()
    {
        //ステータスを設定
        hp = EnemyList.BossEnemy.hp[Stage.nowStage - 1];      //体力
        speed = EnemyList.BossEnemy.speed[Stage.nowStage - 1];//移動速度
        //このオブジェクトのコンポーネントを取得
        thisTransform = this.GetComponent<Transform>();//"Transform"
        audioSource = this.GetComponent<AudioSource>();//"AudioSource"
        //他のオブジェクトのコンポーネントを取得
        playerTransform = GameObject.Find("Player").transform;//"Transform(プレイヤー)"
    }

    // Update is called once per frame
    void Update()
    {
        //このオブジェクトのビューポート座標を取得
        viewPointX = Camera.main.WorldToViewportPoint(this.transform.position).x;//画面座標.X

        //"hp > 0"の場合
        if (hp > 0)
        {
            Behavior();//関数"Behavior"を実行
        }
    }

    //関数"Behavior"
    void Behavior()
    {
        attackTimer += Time.deltaTime;//攻撃間隔に"Time.deltaTime(経過時間)"を足す

        if (viewPointX < -0.5)
        {
            this.transform.position = new Vector3(this.transform.position.x, playerTransform.position.y, this.transform.position.z);
            this.transform.eulerAngles = new Vector3(this.transform.rotation.x, EnemyList.rotation, this.transform.rotation.z);
            bulletRotation = EnemyList.rotation;
        }
        else if (viewPointX > 1.5)
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

    //関数"Damage"
    void Damage()
    {
        hp -= PlayerBase.attackPower;

        //"hp > 0"の場合
        if (hp > 0)
        {
            audioSource.PlayOneShot(damage);
        }
        //"hp <= 0"の場合
        else if (hp <= 0)
        {
            Invoke("Death", 0.01f);//関数"Death"を"0.01f"後に実行
        }
    }

    //関数"Death"
    void Death()
    {
        Stage.bossEnemy[Stage.nowStage - 1] = false;   //
        this.tag = "Untagged";                         //この"this.tag == Untagged"にする
        hp = 0;                                        //"hp"を"0"にする
        GameManager.score += EnemyList.BossEnemy.score[Stage.nowStage - 1];//"score"を足す

        //
        Instantiate(effect, this.transform.position, this.transform.rotation, thisTransform);
        audioSource.PlayOneShot(explosion);            //"explosion"を鳴らす

        Invoke("Destroy", 1.0f);//関数"Destroy"を"5.0f"後に実行
    }

    //関数"Destroy"
    void Destroy()
    {
        Destroy(this.gameObject);//このオブジェクトを消す
    }

    //当たり判定(OnTriggerEnter)
    void OnTriggerEnter(Collider collision)
    {
        //衝突したオブジェクトのタグが"Bullet" && "hp > 0"の場合
        if (collision.gameObject.tag == "Bullet" && hp > 0)
        {
            Damage();//関数"Damage"を実行
        }
    }
}
