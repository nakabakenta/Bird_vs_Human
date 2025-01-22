using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaveGunEnemy : MonoBehaviour
{
    //ステータス
    private int hp;                     //体力
    //処理
    private Vector2 viewPoint;          //ビューポイント座標.X
    private bool isAction = false;      //行動の可否
    private bool isReload = false;
    private int nowAnimation;           //現在のアニメーション
    private float animationTimer = 0.0f;//アニメーションタイマー
    //このオブジェクトのコンポーネント
    public GameObject gun;           //"GameObject(銃)"
    public GameObject bullet;        //"GameObject(弾)"
    public AudioClip damage;         //"AudioClip(ダメージ)"
    public AudioClip scream;         //"AudioClip(叫び声)"
    private Animator animator = null;//"Animator"
    private AudioSource audioSource; //"AudioSource"
    //他のオブジェクトのコンポーネント
    private Transform playerTransform;//"Transform(プレイヤー)"

    // Start is called before the first frame update
    void Start()
    {
        //ステータスを設定
        hp = EnemyList.HaveGunEnemy.hp;                //体力
        //このオブジェクトのコンポーネントを取得
        animator = this.GetComponent<Animator>();      //"Animator"
        audioSource = this.GetComponent<AudioSource>();//"AudioSource"
        //他のオブジェクトのコンポーネントを取得
        playerTransform = GameObject.Find("Player").transform;//"Transform(プレイヤー)"

        nowAnimation = EnemyList.HumanoidAnimation.haveGunIdle;
        Animation();   //関数"Animation"を実行
    }

    // Update is called once per frame
    void Update()
    {
        //このオブジェクトのビューポート座標を取得
        viewPoint.x = Camera.main.WorldToViewportPoint(this.transform.position).x;//画面座標.X

        if(isAction == true)
        {
            //"hp > 0"
            if (hp > 0)
            {
                Horizontal();
            }
        }
        else if(isAction == false)
        {
            if (viewPoint.x < 1)
            {
                isAction = true;
                nowAnimation = EnemyList.HumanoidAnimation.gunPlay;
                Animation();//関数"Animation"を実行
            }
        }

        //"viewPointX < 0"の場合
        if (viewPoint.x < 0)
        {
            Destroy();//関数"Destroy"を実行
        }
    }

    //関数"Horizontal"
    void Horizontal()
    {
        if (PlayerController.status == "Death")
        {
            nowAnimation = EnemyList.HumanoidAnimation.dance;
            Animation();//関数"Animation"を実行
        }
        else
        {
            //
            if (this.transform.position.x > playerTransform.position.x)
            {
                this.transform.eulerAngles = new Vector3(this.transform.rotation.x, -EnemyList.rotation, this.transform.rotation.z);
            }
            //
            else if (this.transform.position.x < playerTransform.position.x)
            {
                this.transform.eulerAngles = new Vector3(this.transform.rotation.x, EnemyList.rotation, this.transform.rotation.z);
            }

            Wait();//関数"Wait"を実行
        }

        this.transform.position = new Vector3(this.transform.position.x, 0.0f, playerTransform.position.z);
    }

    //関数"Animation"
    void Animation()
    {
        animator.SetInteger("Motion", nowAnimation);//"animator(Motion)"に"nowAnimation"を設定して再生
    }

    //関数"Wait"
    void Wait()
    {
        animationTimer += Time.deltaTime;//" animationTimer"に"Time.deltaTime(経過時間)"を足す

        //
        if (nowAnimation == EnemyList.HumanoidAnimation.punch)
        {
            //
            if (animationTimer >= 2.12f)
            {
                animationTimer = 0.0f;
            }
        }
        //
        else if (nowAnimation == EnemyList.HumanoidAnimation.kick)
        {
            //
            if (animationTimer >= 1.15f)
            {
                animationTimer = 0.0f;
            }
        }
        //
        else if (nowAnimation == EnemyList.HumanoidAnimation.gunPlay)
        {
            if(isReload == false)
            {
                Instantiate(bullet, gun.transform.position, Quaternion.identity);
                isReload = true;
            }

            // 弾の方向を計算
            if (animationTimer >= 0.34f)
            {
                animationTimer = 0.0f;
                nowAnimation = EnemyList.HumanoidAnimation.reload;
                Animation();//関数"Animation"を実行
            }
        }
        //
        else if (nowAnimation == EnemyList.HumanoidAnimation.reload)
        {
            if (animationTimer >= 3.09f)
            {
                isReload = false;
                animationTimer = 0.0f;
                nowAnimation = EnemyList.HumanoidAnimation.gunPlay;
                Animation();//関数"Animation"を実行
            }
        }
    }

    //関数"Damage"
    void Damage()
    {
        hp -= PlayerList.Player.power[GameManager.playerNumber];//

        //"hp > 0"の場合
        if (hp > 0)
        {
            audioSource.PlayOneShot(damage);                  //"damage"を鳴らす
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
        this.tag = "Untagged";                           //"this.tag = Untagged"にする
        hp = 0;                                          //"hp = 0"にする
        GameManager.score += EnemyList.WalkEnemy.score;  //"score"を足す
        nowAnimation = EnemyList.HumanoidAnimation.death;//"nowAnimation = death(死亡)"にする
        audioSource.PlayOneShot(scream);                 //"scream"を鳴らす
        Animation();                                     //関数"Animation"を実行
    }

    //関数"Destroy"
    void Destroy()
    {
        Destroy(this.gameObject);//このオブジェクトを消す
    }

    //当たり判定(OnTriggerEnter)
    void OnTriggerEnter(Collider collision)
    {
        //衝突したオブジェクトの"tag == Player" && "isAnimation == false"の場合
        if (collision.gameObject.tag == "Player")
        {
            //ランダム"10(パンチ)"～"12(キック)"
            nowAnimation = (int)Random.Range(EnemyList.HumanoidAnimation.punch,
                                             EnemyList.HumanoidAnimation.kick + 1);
            Animation();//関数"Animation"を実行
        }
        //衝突したオブジェクトのタグが"Bullet" && "hp > 0"の場合
        if (collision.gameObject.tag == "Bullet" && hp > 0)
        {
            Damage();//関数"Damage"を実行
        }
    }
}