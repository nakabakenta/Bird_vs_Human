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
    private float viewPointX;                 //ビューポイント座標.X
    private int nowAnimation;                 //現在のアニメーション
    private float animationTimer = 0.0f;      //アニメーションタイマー
    private float changeAnimationTimer = 0.0f;//チェンジアニメーションタイマー
    private float jumpTimer = 0.0f;           //ジャンプタイマー
    private bool isAnimation = false;         //アニメーションの可否
    //このオブジェクトのコンポーネント                               
    public AudioClip damage;         //"AudioClip(ダメージ)"
    public AudioClip scream;         //"AudioClip(叫び声)"
    private Animator animator = null;//"Animator"(このオブジェクト)
    private AudioSource audioSource; //"AudioSource"
    //他のオブジェクトのコンポーネント
    private Transform playerTransform;//"Transform"(プレイヤー)

    // Start is called before the first frame update
    void Start()
    {
        //ステータスを設定
        hp = EnemyList.BossEnemy.hp[Stage.nowStage - 1];      //体力
        speed = EnemyList.BossEnemy.speed[Stage.nowStage - 1];//移動速度
        jump = EnemyList.BossEnemy.jump[Stage.nowStage - 1];  //ジャンプ力
        //このオブジェクトのコンポーネントを取得
        animator = this.GetComponent<Animator>();      //"Animator"
        audioSource = this.GetComponent<AudioSource>();//"AudioSource"
        //他のオブジェクトのコンポーネントを取得
        playerTransform = GameObject.Find("Player").transform;//"Transform(プレイヤー)"
        //
        nowAnimation = EnemyList.HumanoidAnimation.walk;//"nowAnimation"を"walk(歩く)"にする
        Animation();                                    //関数"Animation"を実行
    }

    // Update is called once per frame
    void Update()
    {
        //このオブジェクトのビューポート座標を取得
        viewPointX = Camera.main.WorldToViewportPoint(this.transform.position).x;//ビューポート座標.X

        //"viewPointX < 0"の場合
        if (viewPointX < 0)
        {
            Destroy();//関数"Destroy"を実行
        }
        //"hp > 0 && viewPointX < 1"の場合
        else if (hp > 0 && viewPointX < 1)
        {
            Action();//関数"Action"を実行
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

        //
        if (isAnimation == true)
        {
            Wait();//関数"Wait"を実行
        }
        else if(isAnimation == false)
        {
            if (PlayerController.hp > 0)
            {
                if (this.transform.position.x + EnemyList.BossEnemy.range[Stage.nowStage - 1].x > playerTransform.position.x &&
                    this.transform.position.x - EnemyList.BossEnemy.range[Stage.nowStage - 1].x < playerTransform.position.x &&
                    this.transform.position.y + EnemyList.BossEnemy.range[Stage.nowStage - 1].y < playerTransform.position.y &&
                    this.transform.position.y == 0.0f && nowAnimation == EnemyList.HumanoidAnimation.walk)
                {
                    isAnimation = true;
                    nowAnimation = EnemyList.HumanoidAnimation.jump;
                    Animation();
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

                if (changeAnimationTimer >= 5.0f)
                {
                    changeAnimationTimer = 0.0f;
                    ChangeAnimation();
                }
            }
            //
            else if (PlayerController.hp <= 0)
            {
                nowAnimation = EnemyList.HumanoidAnimation.dance;
                Animation();//関数"Animation"を実行
            }
        }
    }

    //関数"ChangeAnimation"
    void ChangeAnimation()
    {
        if (nowAnimation == EnemyList.HumanoidAnimation.walk)
        {
            isAnimation = true;
            nowAnimation = EnemyList.HumanoidAnimation.battlecry;
            Animation();
        }
        else if (nowAnimation == EnemyList.HumanoidAnimation.mutantRun)
        {
            isAnimation = true;
            nowAnimation = EnemyList.HumanoidAnimation.jumpAttack;
            Animation();
        }
    }

    //関数"Animation"
    void Animation()
    {
        animator.SetInteger("Motion", nowAnimation);//"animator(Motion)"に"nowAnimation"を設定して再生
    }

    //関数"Wait"
    void Wait()
    {
        animationTimer += Time.deltaTime;//間隔に"Time.deltaTime(経過時間)"を足す

        //
        if (nowAnimation == EnemyList.HumanoidAnimation.punch)
        {
            //
            if (animationTimer >= 2.12f)
            {
                animationTimer = 0.0f;
                isAnimation = false;
                nowAnimation = EnemyList.HumanoidAnimation.walk;
                Animation();
            }
        }
        //
        else if (nowAnimation == EnemyList.HumanoidAnimation.kick)
        {
            //
            if (animationTimer >= 1.15f)
            {
                animationTimer = 0.0f;
                isAnimation = false;
                nowAnimation = EnemyList.HumanoidAnimation.walk;
                Animation();
            }
        }
        //
        else if (nowAnimation == EnemyList.HumanoidAnimation.jumpAttack)
        {
            //
            if (animationTimer >= 1.605f)
            {
                animationTimer = 0.0f;
                isAnimation = false;
                nowAnimation = EnemyList.HumanoidAnimation.walk;
                speed = EnemyList.BossEnemy.speed[Stage.nowStage - 1];
                Animation();
            }
        }
        //
        else if (nowAnimation == EnemyList.HumanoidAnimation.jump)
        {
            jumpTimer += Time.deltaTime;//"jumpTimer"に"Time.deltaTime(経過時間)"を足す

            if (animationTimer >= 0.8f && jumpTimer >= 0.1f)
            {
                jump -= 1.0f;
                jumpTimer = 0.0f;
            }

            if (animationTimer >= 0.8f)
            {
                this.transform.position += jump * transform.up * Time.deltaTime;

                if (animationTimer >= 2.7f)
                {
                    jump = EnemyList.RunEnemy.jump;
                    animationTimer = 0.0f;
                    jumpTimer = 0.0f;
                    isAnimation = false;
                    nowAnimation = EnemyList.HumanoidAnimation.walk;//
                    Animation();                                    //関数"Animation"を実行
                }
                else if (animationTimer >= 2.3f)
                {
                    animator.SetFloat("MoveSpeed", 1.0f);//"animator(MoveSpeed)"を"1.0f(再生)"にする
                }
                else if (animationTimer >= 1.2f)
                {
                    animator.SetFloat("MoveSpeed", 0.0f);//"animator(MoveSpeed)"を"0.0f(停止)"にする
                }
            }
        }
        //
        else if (nowAnimation == EnemyList.HumanoidAnimation.battlecry)
        {
            //
            if (animationTimer >= 1.125f)
            {
                animationTimer = 0.0f;
                isAnimation = false;
                nowAnimation = EnemyList.HumanoidAnimation.mutantRun;
                speed *= 3.0f;
                Animation();
            }
        }
        //
        else if (nowAnimation == EnemyList.HumanoidAnimation.damage)
        {
            //
            if (animationTimer >= 1.13f)
            {
                animationTimer = 0.0f;
                isAnimation = false;
                nowAnimation = EnemyList.HumanoidAnimation.walk;
                Animation();//関数"Animation"を実行
            }
        }
    }

    //関数"Damage"
    void Damage()
    {
        hp -= PlayerList.Player.power[GameManager.playerNumber];//

        //"hp > 0 && nowAnimation == EnemyList.HumanoidAnimation.walk"の場合
        if (hp > 0 && nowAnimation == EnemyList.HumanoidAnimation.walk)
        {
            isAnimation = true;                               //"isAnimation = true"にする
            nowAnimation = EnemyList.HumanoidAnimation.damage;//"nowAnimation = damage(ダメージ)"にする
            audioSource.PlayOneShot(damage);                  //"damage"を鳴らす
            Animation();                                      //関数"Animation"を実行
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
        Stage.bossEnemy[Stage.nowStage - 1] = false;     //
        this.transform.position = new Vector3(this.transform.position.x, 0.0f, this.transform.position.z);//
        this.tag = "Untagged";                           //この"this.tag == Untagged"にする
        hp = 0;                                          //"hp"を"0"にする
        GameManager.score += EnemyList.WalkEnemy.score;  //"score"を足す
        nowAnimation = EnemyList.HumanoidAnimation.death;//"nowAnimation"を"death(死亡)"にする
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
        //衝突したオブジェクトのタグが"Player" && "isAnimation == false"の場合
        if (collision.gameObject.tag == "Player" && isAnimation == false)
        {
            //ランダム"10(パンチ)"〜"12(キック)"
            isAnimation = true;
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