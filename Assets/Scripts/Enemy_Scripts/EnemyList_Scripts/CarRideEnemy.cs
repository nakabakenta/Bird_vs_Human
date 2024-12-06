using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarRideEnemy : MonoBehaviour
{
    //ステータス
    private int hp = EnemyList.CarRideEnemy.hp;        //体力
    private float speed = EnemyList.CarRideEnemy.speed;//移動速度
    private float jump = EnemyList.CarRideEnemy.jump;  //ジャンプ力
    //処理
    private float viewPointX;           //ビューポイント座標.X
    private int nowAnimation;           //現在のアニメーション
    private float animationTimer = 0.0f;//アニメーションタイマー
    private float jumpTimer = 0.0f;     //ジャンプタイマー
    private bool isAnimation = false;   //アニメーションの可否
    //このオブジェクトのコンポーネント
    private Animator animator = null;//"Animator"
    public AudioClip damage;         //"AudioClip(ダメージ)"
    public AudioClip scream;         //"AudioClip(叫び声)"
    private AudioSource audioSource; //"AudioSource"
    //他のオブジェクトのコンポーネント
    private Transform playerTransform;//"Transform(プレイヤー)"

    // Start is called before the first frame update
    void Start()
    {
        //このオブジェクトのコンポーネントを取得
        animator = this.GetComponent<Animator>();      //"Animator"
        audioSource = this.GetComponent<AudioSource>();//"AudioSource"
        //他のオブジェクトのコンポーネントを取得
        playerTransform = GameObject.Find("Player").transform;//"Transform(プレイヤー)"
        //
        nowAnimation = EnemyList.HumanoidAnimation.carExit;//"nowAnimation = carExit(車から出る)"にする
        Animation();                                       //関数"Animation"を実行
    }

    // Update is called once per frame
    void Update()
    {
        //このオブジェクトのビューポート座標を取得
        viewPointX = Camera.main.WorldToViewportPoint(this.transform.position).x;//ビューポート座標.X

        //"(hp <= 0 && viewPointX < 0) || (hp > 0 && Stage.bossEnemy[Stage.nowStage - 1] == false)"の場合
        if ((hp <= 0 && viewPointX < 0) || (hp > 0 && Stage.bossEnemy[Stage.nowStage - 1] == false))
        {
            Destroy();//関数"Destroy"を実行
        }
        //"hp > 0 && viewPointX < 1"の場合
        else if (hp > 0 && viewPointX < 1)
        {
            Behavior();//関数"Behavior"を実行
        }
    }

    //関数"Behavior"
    void Behavior()
    {
        //
        if (PlayerController.hp > 0)
        {
            if (nowAnimation == EnemyList.HumanoidAnimation.carExit)
            {
                Wait();
            }
            else if (nowAnimation != EnemyList.HumanoidAnimation.carExit)
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
                if (isAnimation == false)
                {
                    this.transform.position = new Vector3(this.transform.position.x, 0.0f, 1.0f);
                    this.transform.position += speed * transform.forward * Time.deltaTime;       //前方向に移動する
                }
                //
                else if (isAnimation == true)
                {
                    Wait();//関数"Wait"を実行
                }
            }
        }
        //
        else if (PlayerController.hp <= 0 && isAnimation == false)
        {
            nowAnimation = EnemyList.HumanoidAnimation.dance;
            Animation();//関数"Animation"を実行
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
            if (animationTimer >= 2.0f)
            {
                animationTimer = 0.0f;
                isAnimation = false;
                nowAnimation = EnemyList.HumanoidAnimation.run;
                Animation();
            }
        }
        //
        else if (nowAnimation == EnemyList.HumanoidAnimation.kick)
        {
            //
            if (animationTimer >= 1.5f)
            {
                animationTimer = 0.0f;
                isAnimation = false;
                nowAnimation = EnemyList.HumanoidAnimation.run;
                Animation();
            }
        }
        //
        else if (nowAnimation == EnemyList.HumanoidAnimation.jump)
        {
            jumpTimer += Time.deltaTime;//"jumpTimer"に"Time.deltaTime(経過時間)"を足す

            if (jumpTimer >= 0.1f)
            {
                jump -= 0.5f;
                jumpTimer = 0.0f;
            }

            if (animationTimer >= 0.75f)
            {
                this.transform.position += jump * transform.up * Time.deltaTime;

                if (animationTimer >= 3.5f)
                {
                    jump = EnemyList.RunEnemy.jump;
                    animationTimer = 0.0f;
                    jumpTimer = 0.0f;
                    isAnimation = false;
                    nowAnimation = EnemyList.HumanoidAnimation.run;//
                    Animation();                                   //関数"Animation"を実行
                }
                else if (animationTimer >= 3.0f)
                {
                    animator.SetFloat("MoveSpeed", 1.0f);//"animator(MoveSpeed)"を"1.0f(再生)"にする
                }
                else if (animationTimer >= 1.25f)
                {
                    animator.SetFloat("MoveSpeed", 0.0f);//"animator(MoveSpeed)"を"0.0f(停止)"にする
                }
            }
        }
        //
        else if(nowAnimation == EnemyList.HumanoidAnimation.carExit)
        {
            //
            if (animationTimer >= 3.0f)
            {
                animationTimer = 0.0f;
                isAnimation = false;
                nowAnimation = EnemyList.HumanoidAnimation.run;
                Animation();
            }
        }
        //
        else if (nowAnimation == EnemyList.HumanoidAnimation.damage)
        {
            //
            if (animationTimer >= 1.0f)
            {
                animationTimer = 0.0f;
                isAnimation = false;
                nowAnimation = EnemyList.HumanoidAnimation.run;
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
        this.transform.position = new Vector3(this.transform.position.x, 0.0f, this.transform.position.z);//
        this.tag = "Untagged";                           //この"this.tag = Untagged"にする
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
            //ランダム"10(パンチ)"～"12(キック)"
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