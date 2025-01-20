using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : MonoBehaviour
{
    //ステータス
    private int hp;     //体力
    private float speed;//移動速度
    private float jump; //ジャンプ力
    //処理
    private Vector3 viewPoint;                //ビューポイント座標
    private int nowAnimation;                 //現在のアニメーション
    private float animationTimer = 0.0f;      //アニメーションタイマー
    private float changeAnimationTimer = 0.0f;//アニメーション切り替えタイマー
    private float jumpTimer = 0.0f;           //ジャンプタイマー
    private bool isAnimation = false;         //アニメーションの可否
    //このオブジェクトのコンポーネント                               
    public AudioClip damage;                                    //"AudioClip(ダメージ)"
    public AudioClip scream;                                    //"AudioClip(叫び声)"
    private Animator animator = null;                           //"Animator"
    private RuntimeAnimatorController runtimeAnimatorController;//RuntimeAnimatorController
    private AudioSource audioSource;                            //"AudioSource"
    //他のオブジェクトのコンポーネント
    private Transform playerTransform;//"Transform"(プレイヤー)

    // Start is called before the first frame update
    void Start()
    {
        //ステータスを設定する
        hp = EnemyList.BossEnemy.hp[Stage.nowStage - 1];      
        speed = EnemyList.BossEnemy.speed[Stage.nowStage - 1];
        jump = EnemyList.BossEnemy.jump[Stage.nowStage - 1];
        //このオブジェクトのコンポーネントを取得する
        animator = this.GetComponent<Animator>();      
        runtimeAnimatorController = animator.runtimeAnimatorController;
        audioSource = this.GetComponent<AudioSource>();
        //他のオブジェクトのコンポーネントを取得する
        playerTransform = GameObject.Find("Player").transform;
        //アニメーションを設定する
        nowAnimation = EnemyList.HumanoidAnimation.walk;//現在のアニメーションを"歩く"にする
        Animation();                                    //関数"Animation"を実行する
    }

    // Update is called once per frame
    void Update()
    {
        //このオブジェクト位置(ワールド座標)をビューポイント座標に変換する
        viewPoint.x = Camera.main.WorldToViewportPoint(this.transform.position).x;//ビューポート座標.X

        //ビューポイント座標.xが"0未満"の場合
        if (viewPoint.x < 0)
        {
            Destroy();//関数"Destroy"を実行する
        }
        //体力が"0より上" && ビューポイント座標.xが"1未満"の場合
        else if (hp > 0 && viewPoint.x < 1)
        {
            Action();//関数"Action"を実行する
        }
    }

    //関数"Action"
    void Action()
    {
        if (nowAnimation != EnemyList.HumanoidAnimation.jumpAttack && 
            nowAnimation != EnemyList.HumanoidAnimation.jump)
        {
            this.transform.position = new Vector3(this.transform.position.x, 0.0f, 1.0f);
        }

        //アニメーションを"再生していない"場合
        if (isAnimation == true)
        {
            Wait();//関数"Wait"を実行する
        }
        //アニメーションを"再生している"場合
        else if (isAnimation == false)
        {
            //プレイヤーの体力が"0より上"の場合
            if (PlayerController.hp > 0)
            {
                if (this.transform.position.x + EnemyList.BossEnemy.range[Stage.nowStage - 1].x > playerTransform.position.x &&
                    this.transform.position.x - EnemyList.BossEnemy.range[Stage.nowStage - 1].x < playerTransform.position.x &&
                    this.transform.position.y + EnemyList.BossEnemy.range[Stage.nowStage - 1].y < playerTransform.position.y &&
                    this.transform.position.y == 0.0f && nowAnimation == EnemyList.HumanoidAnimation.walk)
                {
                    isAnimation = true;//アニメーションを"再生している"にする
                    nowAnimation = EnemyList.HumanoidAnimation.jump;
                    Animation();//関数"Animation"を実行する
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

                changeAnimationTimer += Time.deltaTime;
                this.transform.position = new Vector3(this.transform.position.x, 0.0f, 1.0f);
                this.transform.position += speed * transform.forward * Time.deltaTime;//前方向に移動する

                //アニメーション切り替えタイマーが"5.0f以上"の場合
                if (changeAnimationTimer >= 5.0f)
                {
                    changeAnimationTimer = 0.0f;//アニメーション切り替えタイマーを初期化する
                    ChangeAnimation();          //関数"ChangeAnimation"を実行する
                }
            }
            //プレイヤーの体力が"0以下"の場合
            else if (PlayerController.hp <= 0)
            {
                nowAnimation = EnemyList.HumanoidAnimation.dance;//現在のアニメーションを"ダンス"にする
                Animation();//関数"Animation"を実行
            }
        }
    }

    //関数"ChangeAnimation"
    void ChangeAnimation()
    {
        //現在のアニメーションが"歩くと等しい"場合
        if (nowAnimation == EnemyList.HumanoidAnimation.walk)
        {
            isAnimation = true;                                  //アニメーションを"再生している"にする
            nowAnimation = EnemyList.HumanoidAnimation.battlecry;//現在のアニメーションを"雄叫び"にする
            Animation();                                         //関数"Animation"を実行する
        }
        //現在のアニメーションが"走ると等しい"場合
        else if (nowAnimation == EnemyList.HumanoidAnimation.crazyRun)
        {
            isAnimation = true;                                   //アニメーションを"再生している"にする
            nowAnimation = EnemyList.HumanoidAnimation.jumpAttack;//現在のアニメーションを"ジャンプ攻撃"にする
            Animation();                                          //関数"Animation"を実行する
        }
    }

    //関数"Animation"
    void Animation()
    {
        animator.SetInteger("Motion", nowAnimation);//"アニメーターに"現在のアニメーション"を設定して再生する
    }

    //関数"Wait"
    void Wait()
    {
        animationTimer += Time.deltaTime;//アニメーションタイマーに経過時間を足す

        //現在のアニメーションが"パンチ"の場合
        if (nowAnimation == EnemyList.HumanoidAnimation.punch)
        {
            foreach (AnimationClip clip in runtimeAnimatorController.animationClips)
            {
                if (clip.name == "")//指定した名前のアニメーションを検索
                {
                    //Debug.Log($"アニメーション名: {clip.name}, 長さ: {clip.length}秒");
                    //return;

                    //
                    if (animationTimer >= 2.12f)
                    {
                        animationTimer = 0.0f;                          //アニメーションタイマーを初期化する
                        nowAnimation = EnemyList.HumanoidAnimation.walk;//現在のアニメーションを"歩く"にする
                    }
                }
            }
        }
        //現在のアニメーションが"キック"の場合
        else if (nowAnimation == EnemyList.HumanoidAnimation.kick)
        {
            //
            if (animationTimer >= 1.15f)
            {
                animationTimer = 0.0f;                          //アニメーションタイマーを初期化する
                nowAnimation = EnemyList.HumanoidAnimation.walk;//現在のアニメーションを"歩く"にする
            }
        }
        //現在のアニメーションが"ジャンプ攻撃"の場合
        else if (nowAnimation == EnemyList.HumanoidAnimation.jumpAttack)
        {
            //
            if (animationTimer >= 1.605f)
            {
                animationTimer = 0.0f;                                //アニメーションタイマーを初期化する
                nowAnimation = EnemyList.HumanoidAnimation.walk;      //現在のアニメーションを"歩く"にする
                speed = EnemyList.BossEnemy.speed[Stage.nowStage - 1];
            }
        }
        //現在のアニメーションが"ジャンプ"の場合
        else if (nowAnimation == EnemyList.HumanoidAnimation.jump)
        {
            jumpTimer += Time.deltaTime;//"jumpTimer"に"Time.deltaTime(経過時間)"を足す
            //アニメーションタイマーが"0.8f以上" && ジャンプタイマーが"0.1f以上"の場合
            if (animationTimer >= 0.8f && jumpTimer >= 0.1f)
            {
                jump -= 1.0f;    //ジャンプを"-1"する
                jumpTimer = 0.0f;//ジャンプタイマーを初期化する
            }
            //アニメーションタイマーが"0.8f以上"の場合
            if (animationTimer >= 0.8f)
            {
                this.transform.position += jump * transform.up * Time.deltaTime;//このオブジェクトに"ジャンプ"をかける

                if (animationTimer >= 2.7f)
                {
                    jump = EnemyList.RunEnemy.jump;
                    animationTimer = 0.0f;
                    jumpTimer = 0.0f;
                    nowAnimation = EnemyList.HumanoidAnimation.walk;//現在のアニメーションを"歩く"にする
                }
                else if (animationTimer >= 2.3f)
                {
                    animator.SetFloat("MoveSpeed", 1.0f);//アニメーターの"動く速度"を"1.0f(等速)"にする
                }
                else if (animationTimer >= 1.2f)
                {
                    animator.SetFloat("MoveSpeed", 0.0f);//アニメーターの"動く速度"を"0.0f(停止)"にする
                }
            }
        }
        //現在のアニメーションが"雄叫び"の場合
        else if (nowAnimation == EnemyList.HumanoidAnimation.battlecry)
        {
            //
            if (animationTimer >= 1.125f)
            {
                animationTimer = 0.0f;                               //アニメーションタイマーを初期化する
                nowAnimation = EnemyList.HumanoidAnimation.crazyRun;//現在のアニメーションを"走る"にする
                speed *= 3.0f;                                       //移動速度を"*3"する
            }
        }
        //現在のアニメーションが"ダメージ"の場合
        else if (nowAnimation == EnemyList.HumanoidAnimation.damage)
        {
            //
            if (animationTimer >= 1.13f)
            {
                animationTimer = 0.0f;                          //アニメーションタイマーを初期化する
                nowAnimation = EnemyList.HumanoidAnimation.walk;//現在のアニメーションを"歩く"にする
            }
        }
        //アニメーションタイマーが"0.0fと等しい" && "アニメーションが"再生している"場合
        if (animationTimer == 0.0f && isAnimation == true)
        {
            isAnimation = false;//アニメーションを"再生していない"にする
            Animation();        //関数"Animation"を実行
        }
    }

    //関数"Damage"
    void Damage()
    {
        hp -= PlayerList.Player.power[GameManager.playerNumber];//体力を"-プレイヤーの攻撃力"する

        //体力が"0より上" && 現在のアニメーションが"歩く"の場合
        if (hp > 0 && nowAnimation == EnemyList.HumanoidAnimation.walk)
        {
            isAnimation = true;                               //アニメーションを"再生している"にする
            nowAnimation = EnemyList.HumanoidAnimation.damage;//現在のアニメーションを"ダメージ"にする
            audioSource.PlayOneShot(damage);                  //"ダメージ"を鳴らす
            Animation();                                      //関数"Animation"を実行する
        }
        //体力が"0以下"の場合
        else if (hp <= 0)
        {
            Invoke("Death", 0.01f);//関数"Death"を"0.01f"後に実行する
        }
    }

    //関数"Death"
    void Death()
    {
        hp = 0;                                     //体力を"0"にする
        Stage.bossEnemy[Stage.nowStage - 1] = false;//ボスを"倒した"にする
        this.tag = "Untagged";                      //このタグを"Untagged"にする
        //スコアにボスのスコアを足す
        GameManager.score += EnemyList.BossEnemy.score[Stage.nowStage - 1];
        //このオブジェクトの位置を固定する
        this.transform.position = new Vector3(this.transform.position.x, 0.0f, this.transform.position.z);
        audioSource.PlayOneShot(scream);                 //"叫び声"を鳴らす
        nowAnimation = EnemyList.HumanoidAnimation.death;//現在のアニメーションを"死亡"にする
        Animation();                                     //関数"Animation"を実行する
    }

    //関数"Destroy"
    void Destroy()
    {
        Destroy(this.gameObject);//このオブジェクトを消す
    }

    //当たり判定(OnTriggerEnter)
    void OnTriggerEnter(Collider collision)
    {
        //衝突したオブジェクトのタグが"Player" && アニメーションが"再生されていない"場合
        if (collision.gameObject.tag == "Player" && isAnimation == false)
        {
            isAnimation = true;//アニメーションを"再生している"にする

            //ランダムな値"10(パンチ)～12(キック)"を現在のアニメーションに入れる
            nowAnimation = (int)Random.Range(EnemyList.HumanoidAnimation.punch,
                                             EnemyList.HumanoidAnimation.kick + 1);
            Animation();//関数"Animation"を実行する
        }
        //衝突したオブジェクトのタグが"Bullet" && 体力が"0より上"の場合
        if (collision.gameObject.tag == "Bullet" && hp > 0)
        {
            Damage();//関数"Damage"を実行する
        }
    }
}