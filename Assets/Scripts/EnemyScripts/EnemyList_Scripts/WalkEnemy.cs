using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkEnemy : MonoBehaviour
{
    //ステータス
    private int hp = EnemyList.WalkEnemy.hp;        //敵の体力
    private float speed = EnemyList.WalkEnemy.speed;//敵の移動速度
    //処理
    private float viewPointX;        //ビューポイント座標.X
    private float interval = 0.0f;   //間隔
    private int nowAnimation;        //現在のアニメーション
    private bool isAnimation = false;//アニメーションの可否
    //このオブジェクトのコンポーネント(public)
    public AudioClip damage;
    public AudioClip scream;
    //このオブジェクトのコンポーネント(private)
    private Transform thisTransform; //"Transform"
    private Animator animator = null;//"Animator"
    private AudioSource audioSource; //"AudioSource"

    // Start is called before the first frame update
    void Start()
    {
        //このオブジェクトのコンポーネントを取得
        thisTransform = this.gameObject.GetComponent<Transform>();//"Transform"
        animator = this.GetComponent<Animator>();                 //"Animator"
        audioSource = this.GetComponent<AudioSource>();           //"AudioSource"
        
        nowAnimation = EnemyList.HumanoidAnimation.walk;//"nowAnimation"を"walk(歩く)"にする        
        Animation();                                    //関数"Animation"を実行
    }

    // Update is called once per frame
    void Update()
    {
        //ビューポート座標を取得
        viewPointX = Camera.main.WorldToViewportPoint(this.transform.position).x;//画面座標.X

        //体力が0より上 && ビューポート座標.Xが1より上であれば
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

    void Behavior()
    {
        Vector3 localPosition = thisTransform.localPosition;//オブジェクトの
        Vector3 localAngle = thisTransform.localEulerAngles;//
        localPosition.y = 0.0f;  //
        localPosition.z = 1.0f;  //
        localAngle.y = -EnemyList.rotation;//
        thisTransform.localPosition = localPosition;       //ローカル座標での座標を設定
        thisTransform.localEulerAngles = localAngle;       //

        //
        if (nowAnimation == EnemyList.HumanoidAnimation.walk)
        {
            this.transform.position += speed * transform.forward * Time.deltaTime;//前方向に移動する
        }
        //
        else if (nowAnimation != EnemyList.HumanoidAnimation.walk && isAnimation == true)
        {
            Wait();
        }

        //
        if (PlayerController.hp <= 0 && isAnimation == false)
        {
            nowAnimation = EnemyList.HumanoidAnimation.dance;
            Animation();//関数"Animation"を実行
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
                    nowAnimation = EnemyList.HumanoidAnimation.walk;
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
                    nowAnimation = EnemyList.HumanoidAnimation.walk;
                    Animation();
                }
            }
        }
    }

    //関数"Damage"
    void Damage()
    {
        hp -= PlayerList.Player.power[GameManager.playerNumber];//

        //"hp"が"0"より上だったら
        if(hp > 0)
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
        this.tag = "Untagged";                           //このタグを"Untagged"に変更する
        hp = 0;                                          //敵の体力を"0"にする
        GameManager.score += EnemyList.WalkEnemy.score;  //
        nowAnimation = EnemyList.HumanoidAnimation.death;
        audioSource.PlayOneShot(scream);
        Animation();                                     //関数"Animation"を実行
    }

    //当たり判定(OnTriggerEnter)
    void OnTriggerEnter(Collider collision)
    {
        //衝突したオブジェクトのタグが"Player" && "isAnimation"が"false"の場合
        if (collision.gameObject.tag == "Player" && isAnimation == false)
        {
            isAnimation = true;
            nowAnimation = (int)Random.Range(EnemyList.HumanoidAnimation.punch, EnemyList.HumanoidAnimation.kick + 1);//ランダム"10(パンチ)"～"12(キック)"
            Animation();
        }
        //衝突したオブジェクトのタグが"Bullet"の場合
        if (collision.gameObject.tag == "Bullet" && this.tag != "Untagged")
        {
            Damage();//関数Damageを呼び出す
        }
    }
}
