using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunEnemy : MonoBehaviour
{
    //ステータス
    private int hp = EnemyList.RunEnemy.hp;        //体力
    private float speed = EnemyList.RunEnemy.speed;//移動速度
    private float jump = EnemyList.RunEnemy.jump;  //ジャンプ力
    //処理
    private float viewPointX;        //ビューポイント座標.X
    private float interval = 0.0f;   //間隔
    private int nowAnimation;        //現在のアニメーション
    private bool isAnimation = false;//アニメーションの可否
    //このオブジェクトのコンポーネント(public)
    public AudioClip damage;
    public AudioClip scream;
    //このオブジェクトのコンポーネント(private)
    private Animator animator = null;//"Animator"
    private AudioSource audioSource; //"AudioSource"
    //他のオブジェクトのコンポーネント(private)
    private Transform playerTransform;//"Transform"(プレイヤー)

    // Start is called before the first frame update
    void Start()
    {
        //このオブジェクトのコンポーネントを取得
        animator = this.GetComponent<Animator>();      //"Animator"
        audioSource = this.GetComponent<AudioSource>();//"AudioSource"
        //他のオブジェクトのコンポーネントを取得
        playerTransform = GameObject.Find("Player").transform;//"Transform"(プレイヤー)
        //
        nowAnimation = EnemyList.HumanoidAnimation.run;//"nowAnimation"を"walk(走る)"にする
        Animation();                                   //関数"Animation"を実行
    }

    // Update is called once per frame
    void Update()
    {
        //このオブジェクトのビューポート座標を取得
        viewPointX = Camera.main.WorldToViewportPoint(this.transform.position).x;//ビューポート座標.X

        //"hp"が"0"より上 && "viewPointX"が"1"より上であれば
        if (hp > 0 && viewPointX < 1)
        {
            Behavior();//行動関数を呼び出す
        }
        //体力が0以下 && ビューポート座標.Xが0未満であれば
        else if (hp <= 0 && viewPointX < 0)
        {
            Destroy(this.gameObject);//このオブジェクトを消す
        }
    }

    //関数"Behavior"
    void Behavior()
    {
        if (this.transform.position.x + EnemyList.RunEnemy.rangeX > playerTransform.position.x &&
            this.transform.position.x - EnemyList.RunEnemy.rangeX < playerTransform.position.x &&
            this.transform.position.y + EnemyList.RunEnemy.rangeY < playerTransform.position.y &&
            this.transform.position.y == 0.0f && nowAnimation == EnemyList.HumanoidAnimation.run && 
            isAnimation == false)
        {
            isAnimation = true;
            nowAnimation = EnemyList.HumanoidAnimation.jump;
            Animation();
        }

        if(nowAnimation == EnemyList.HumanoidAnimation.run)
        {
            this.transform.position = new Vector3(this.transform.position.x, 0.0f, 1.0f);
        }
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

        //
        if (nowAnimation == EnemyList.HumanoidAnimation.run)
        {
            this.transform.position += speed * transform.forward * Time.deltaTime;//前方向に移動する
        }
        //
        else if (nowAnimation != EnemyList.HumanoidAnimation.run && isAnimation == true)
        {
            Wait();
        }

        //
        if (PlayerController.hp <= 0 && isAnimation == false)
        {
            nowAnimation = EnemyList.HumanoidAnimation.dance;
            Animation();//アニメーション関数を実行
        }
    }

    //関数"Animation"
    void Animation()
    {
        animator.SetInteger("Motion", nowAnimation);//"animator(Motion)"に"nowAnimation"を設定する
    }

    //関数"Wait"
    void Wait()
    {
        interval += Time.deltaTime;//クールタイムにTime.deltaTimeを足す

        if (nowAnimation == EnemyList.HumanoidAnimation.punch ||
            nowAnimation == EnemyList.HumanoidAnimation.kick)
        {
            //
            if (nowAnimation == EnemyList.HumanoidAnimation.punch)
            {
                //
                if (interval >= 2.0f)
                {
                    interval = 0.0f;
                    isAnimation = false;
                    nowAnimation = EnemyList.HumanoidAnimation.run;
                    Animation();
                }
            }
            //
            else if (nowAnimation == EnemyList.HumanoidAnimation.kick)
            {
                //
                if (interval >= 1.5f)
                {
                    interval = 0.0f;
                    isAnimation = false;
                    nowAnimation = EnemyList.HumanoidAnimation.run;
                    Animation();
                }
            }
        }
        //
        else if(nowAnimation == EnemyList.HumanoidAnimation.jump)
        {
            if (interval >= 0.75f)
            {
                this.transform.position += jump * transform.up * Time.deltaTime;

                if (interval >= 2.0f)
                {
                    animator.SetFloat("MoveSpeed", 1.0f);            //"animator(MoveSpeed)"を"1.0f(再生)"にする
                    interval = 0.0f;
                    isAnimation = false;
                    nowAnimation = EnemyList.HumanoidAnimation.run;
                    Animation();
                }
                else if (interval >= 1.0f)
                {
                    animator.SetFloat("MoveSpeed", 0.0f);            //"animator(MoveSpeed)"を"0.0f(停止)"にする
                }
            }
        }
    }

    //関数"Damage"
    void Damage()
    {
        hp -= PlayerList.Player.power[GameManager.playerNumber];//

        //"hp"が"0"より上だったら
        if (hp > 0)
        {
            audioSource.PlayOneShot(damage);
        }
        //"hp"が"0"以下だったら
        else if (hp <= 0)
        {
            Invoke("Death", 0.01f);//関数"Death"を"0.01f"後に実行
        }
    }

    //関数"Death"
    void Death()
    {
        this.transform.position = new Vector3(this.transform.position.x, 0.0f, this.transform.position.z);//
        this.tag = "Untagged";                           //このタグを"Untagged"に変更する
        hp = 0;                                          //"hp"を"0"にする
        GameManager.score += EnemyList.WalkEnemy.score;  //"score"を足す
        nowAnimation = EnemyList.HumanoidAnimation.death;//"nowAnimation"を"death(死亡)"にする
        audioSource.PlayOneShot(scream);                 //"scream"を鳴らす
        Animation();                                     //関数"Animation"を実行
    }

    //当たり判定(OnTriggerEnter)
    void OnTriggerEnter(Collider collision)
    {
        //衝突したオブジェクトのタグが"Player" && "action"が"false"だったら
        if (collision.gameObject.tag == "Player" && isAnimation == false)
        {
            isAnimation = true;
            nowAnimation = (int)Random.Range(EnemyList.HumanoidAnimation.punch, EnemyList.HumanoidAnimation.kick + 1);//ランダム"10(パンチ)"～"12(キック)"
            Animation();
        }
        //タグBulletの付いたオブジェクトに衝突したら
        if (collision.gameObject.tag == "Bullet" && hp > 0)
        {
            Damage();//関数Damageを呼び出す
        }
    }
}