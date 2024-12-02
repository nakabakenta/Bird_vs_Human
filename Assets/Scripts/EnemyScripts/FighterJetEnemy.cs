using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterJetEnemy : MonoBehaviour
{
    public GameObject enemyBullet;
    //ステータス
    private int hp = EnemyList.FighterJetEnemy.hp;        //体力
    private float speed = EnemyList.FighterJetEnemy.speed;//移動速度
    //処理
    public float attackTimer = 0.25f;   //攻撃間隔タイマー
    public float attackInterval = 0.25f;//攻撃間隔
    private float viewPointX;           //ビューポイント座標.X
    //他のオブジェクトのコンポーネント
    private Transform playerTransform;//"Transform(プレイヤー)"

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.Find("Player").transform;//ゲームオブジェクト"Player"を探して"Transform"を取得
    }

    // Update is called once per frame
    void Update()
    {
        attackTimer += Time.deltaTime;

        //ビューポート座標を取得
        viewPointX = Camera.main.WorldToViewportPoint(this.transform.position).x;//画面座標.X

        //体力が0より上 && ビューポート座標.Xが1より上であれば
        if (hp > 0)
        {
            Behavior();//行動関数"Behavior"を実行する
        }
        //(体力が"0以下" && ビューポート座標.Xが"0未満"であれば
        else if (hp <= 0 && viewPointX < 0)
        {
            Destroy();//関数"Destroy"を実行する
        }
    }

    //関数"Behavior"
    void Behavior()
    {
        this.transform.position += speed * transform.forward * Time.deltaTime;//前方向に移動する

        if (attackTimer > attackInterval)
        {
            Instantiate(enemyBullet, this.transform.position, Quaternion.Euler(this.transform.rotation.x, this.transform.rotation.y, 90));
            attackTimer = 0.0f;
        }
    }

    //関数"Damage"
    void Damage()
    {
        hp -= 1;//体力を"-1"する

        //体力が0以下だったら
        if (hp <= 0)
        {
            Death();//関数"Death"死亡を呼び出す
        }
    }

    //関数"Death"
    void Death()
    {
        hp = 0;                                          //体力を"0"にする
        GameManager.score += EnemyList.WalkEnemy.score;//
        this.tag = "Death";                              //このタグを"Death"に変更する
    }

    //関数"Destroy"
    void Destroy()
    {
        Destroy(this.gameObject);//このオブジェクトを消す
    }

    //当たり判定(OnTriggerEnter)
    void OnTriggerEnter(Collider collision)
    {
        //タグBulletの付いたオブジェクトに衝突したら
        if (collision.gameObject.tag == "Bullet" && this.tag != "Death")
        {
            Damage();//関数Damageを呼び出す
        }
    }
}
