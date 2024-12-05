using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarEnemy : MonoBehaviour
{
    //ステータス
    private int hp = EnemyList.CarEnemy.hp;        //体力
    private float speed = EnemyList.CarEnemy.speed;//移動速度
    //処理
    private float viewPointX;        //ビューポイント座標.X
    private bool isAnimation = false;//アニメーションの可否
    private bool carExit = false;    //
    //このオブジェクトのコンポーネント
    public GameObject enemy;        //"GameObject(敵)"
    public GameObject effect;       //"GameObject(エフェクト)"
    public AudioClip damage;        //"AudioClip(ダメージ)"
    public AudioClip brake;         //"AudioClip(ブレーキ)"
    public AudioClip horn;          //"AudioClip(クラクション)"
    public AudioClip explosion;     //"AudioClip(爆発)"
    private Transform thisTransform;//"Transform"
    private AudioSource audioSource;//"AudioSource"
    //他のオブジェクトのコンポーネント
    private Transform playerTransform;//"Transform(プレイヤー)"

    // Start is called before the first frame update
    void Start()
    {
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

        //"hp > 0" && "viewPointX < 1"の場合
        if (hp > 0 && viewPointX < 1)
        {
            Behavior();//関数"Behavior"を実行
        }
        //"hp <= 0" && "viewPointX < 0"の場合
        else if (hp <= 0 && viewPointX < 0)
        {
            Destroy();//関数"Destroy"を実行
        }
    }

    //関数"Behavior"
    void Behavior()
    {
        if(this.transform.position.x + EnemyList.CarEnemy.rangeX > playerTransform.position.x &&
            this.transform.position.x - EnemyList.CarEnemy.rangeX < playerTransform.position.x &&
            isAnimation == false)
        {
            isAnimation = true;
        }
        else if (this.transform.position.z > playerTransform.position.z && isAnimation == true)
        {
            this.transform.position += speed * transform.forward * Time.deltaTime;//前方向に移動する
        }
        else if(this.transform.position.z <= playerTransform.position.z && carExit == false)
        {
            Instantiate(enemy, this.transform.position, this.transform.rotation);
            audioSource.PlayOneShot(horn);
            audioSource.PlayOneShot(brake);
            carExit = true;
        }
    }

    //関数"Damage"
    void Damage()
    {
        hp -= PlayerList.Player.power[GameManager.playerNumber];//

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
        this.tag = "Untagged";                         //この"this.tag == Untagged"にする
        hp = 0;                                        //"hp"を"0"にする
        GameManager.score += EnemyList.WalkEnemy.score;//"score"を足す

        //
        Instantiate(effect, this.transform.position, this.transform.rotation, thisTransform);
        audioSource.PlayOneShot(explosion);//"explosion"を鳴らす
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
