using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : CharacteBase
{
    //ステータス
    public static int attackPower;//攻撃力
    public static int remain;     //残機
    public static string status;  //状態
    public float attackSpeed;     //攻撃速度
    //座標
    private Vector3 mousePosition;
    //処理
    public static float[] attackTimer = new float[2];//攻撃タイマー([前方],[下方])
    public static float attackInterval;              //攻撃間隔
    public static float gageTimer;                   //ゲージタイマー
    public static float gageInterval = 20.0f;        //ゲージ間隔
    public static int exp;                           //経験値
    public static int nowAlly;                       //現在の味方数
    private float invincibleTimer = 0.0f;            //無敵タイマー
    private float invincibleInterval = 10.0f;        //無敵間隔
    private float blinkingInterval = 1.0f;           //点滅間隔
    private float rendererTimer;                     //Renderer切り替えの経過時間
    private float rendererInterval = 0.05f;          //Renderer切り替え時間
    private float rendererTotalTime;                 //Renderer切り替えの合計経過時間
    private bool isRenderer;                         //Rendererの可否
    //このオブジェクトのコンポーネント
    public GameObject[] player = new GameObject[3];//"GameObject(プレイヤー)"
    protected GameObject playerObject;             //"GameObject(プレイヤー)"
    protected GameObject groupObject;

    //関数"BaseStart"
    public void BaseStart()
    {
        GetComponent();

        //選択したプレイヤーのステータスを設定する
        hp = Player.hp[GameManager.selectPlayer];                                              //体力
        attackPower = Player.attackPower[GameManager.selectPlayer];                            //攻撃力
        attackSpeed = (Player.maxStatus - Player.attackSpeed[GameManager.selectPlayer]) / 2.0f;
        attackTimer[0] = attackSpeed;
        attackTimer[1] = attackSpeed;
        status = Player.Status.Normal.ToString();//プレイヤーの状態を"Normal"にする
        //処理を初期化する
        gageTimer = 0.0f;
        nowAlly = 0;
        exp = 0;

        //ゲームの状態が"Menu"の場合
        if (GameManager.playBegin == false)
        {
            remain = 3;                  //
            GameManager.playBegin = true;//
        }
    }

    public void UpdatePlayer()
    {
        //ゲームの状態が"Play"の場合
        if (hp > 0 && Stage.gameStatus == "Play")
        {
            //攻撃タイマーに経過時間を足す
            attackTimer[0] += Time.deltaTime;//攻撃タイマー[前方]
            attackTimer[1] += Time.deltaTime;//攻撃タイマー[下方]
            //マウスの位置を取得する
            mousePosition = Input.mousePosition;
            //マウスの位置(スクリーン座標)をビューポイント座標に変換する
            viewPortPosition = Camera.main.ScreenToViewportPoint(new Vector3(mousePosition.x, mousePosition.y, 13.0f));
            //移動の限界位置を設定する
            viewPortPosition.x = Mathf.Clamp(viewPortPosition.x, moveRange[0].range[0].x, moveRange[0].range[1].x);
            viewPortPosition.y = Mathf.Clamp(viewPortPosition.y, moveRange[0].range[0].y, moveRange[0].range[1].y);
            //ビューポイント座標をワールド座標に変換する
            this.transform.position = Camera.main.ViewportToWorldPoint(new Vector3(viewPortPosition.x, viewPortPosition.y, 13.0f));

            //プレイヤーの状態が"Normal"の場合
            if (status == Player.Status.Normal.ToString())
            {
                attackInterval = attackSpeed;

                gageTimer += Time.deltaTime;//ゲージタイマーに経過時間を足す
            }
            //プレイヤーの状態が"Invincible"の場合
            else if (status == Player.Status.Invincible.ToString())
            {
                //無敵時の攻撃間隔を設定する
                attackInterval = PlayerBase.InvincibleStatus.attackSpeed;
            }

            InputButton();
        }

        //プレイヤーの状態が"Invincible"の場合
        if (status == Player.Status.Invincible.ToString())
        {
            Invincible();//関数"Invincible"を実行する
        }

        //経験値が最大経験値と等しい場合
        if (exp >= Player.maxExp)
        {
            Heal();//関数"Heal"を実行する
        }

        if (status == Player.Status.Death.ToString())
        {
            if (this.transform.position.y <= 0.5f && Stage.nowStage != 5)
            {
                this.transform.position = new Vector3(this.transform.position.x, 0.5f, this.transform.position.z);
            }
        }
    }

    public virtual void InputButton()
    {
        
    }

    //関数"Heal"
    public void Heal()
    {
        hp += 1;
        exp = 0;//経験値を初期化する
    }

    //関数"Invincible"
    void Invincible()
    {
        invincibleTimer += Time.deltaTime;        //無敵タイマーに経過時間を足す
        //無敵タイマーが無敵持続時間以上の場合
        if (invincibleTimer >= invincibleInterval)
        {
            invincibleTimer = 0.0f;                  //無敵タイマーを初期化する
            status = Player.Status.Normal.ToString();//プレイヤーの状態を"Normal"にする
            Destroy(groupObject);                    //このオブジェクトを消す
        }
    }

    //関数"Damage"
    public void Damage()
    {
        //ダメージを"受けている"場合
        if (isDamage == true)
        {
            return;//返す
        }

        //味方数が"0より上"の場合
        if (nowAlly > 0)
        {
            nowAlly -= 1;//味方数を"-1"する
        }
        //味方数が"0以下"の場合
        else if (nowAlly <= 0)
        {
            hp -= 1;//体力を"-1"する
            audioSource.PlayOneShot(damage);
        }

        //体力が"0より上"の場合
        if (hp > 0)
        {
            StartCoroutine("Blinking");//コルーチン"Blinking"を実行する
        }
        //体力が"0以下"だったら
        else if (hp <= 0)
        {
            Death();
        }
    }

    //関数"SetObjRenderer"
    void SetObjRenderer(bool set)
    {
        for (int i = 0; i < thisRenderer.Length; i++)
        {
            thisRenderer[i].enabled = set;//RendererをthisRendererにセットする
        }
    }

    //コルーチン"Blinking"
    IEnumerator Blinking()
    {
        isDamage = true;         //ダメージを"受けている"にする
        //タイマーを初期化
        rendererTimer = 0.0f;
        rendererTotalTime = 0.0f;

        while (true)
        {
            //タイマーに経過時間を足す
            rendererTimer += Time.deltaTime;
            rendererTotalTime += Time.deltaTime;
            //Renderer切り替えの経過時間がRenderer切り替え時間以上の場合
            if (rendererInterval <= rendererTimer)
            {
                rendererTimer = 0.0f;          //Renderer切り替えの経過時間を初期化する
                isRenderer = !isRenderer;//"objRenderer"を"true"の場合は"false"、"false"の場合は"true"にする
                SetObjRenderer(isRenderer); //関数"SetObjRenderer"を実行する
            }
            //Renderer切り替えの合計経過時間が点滅持続時間以上の場合
            if (blinkingInterval <= rendererTotalTime)
            {
                isDamage = false;    //ダメージを"受けていない"にする
                isRenderer = true;//Rendererを有効化する
                SetObjRenderer(true);//関数"SetObjRenderer"を実行する
                yield break;         //コルーチンを停止する
            }
            yield return null;
        }
    }

    //関数"Death"
    public virtual void Death()
    {
        rigidBody.useGravity = true;        //RigidBodyの重力を"有効"にする
        animator.SetInteger("Animation", 1);
        audioSource.PlayOneShot(sEDeath);
    }

    //衝突判定(OnTriggerEnter)
    public virtual void OnTriggerEnter(Collider collision)
    {
        //(衝突したオブジェクトのタグが"Enemy" || "BossEnemy" || "EnemyBullet" ) && プレイヤーの状態が"Normal"の場合
        if ((collision.gameObject.tag == "Enemy" || 
             collision.gameObject.tag == "BossEnemy" || 
             collision.gameObject.tag == "EnemyBullet" ||
             collision.gameObject.tag == "Damage") && 
             status == Player.Status.Normal.ToString())
        {
            Damage();//関数"Damage"を実行する
        }
    }

    public static class Player
    {
        public enum Name
        {
            Sparrow = 0,
            Crow = 1,
            Chickadee = 2,
            Penguin = 3,
        }

        public enum Status
        {
            Normal,
            Invincible,
            Death,
        }

        public static int maxStatus = 8;//最大ステータス
        public static int maxExp = 8;   //最大経験値
        public static int maxAlly = 2;  //最大味方数

        //体力
        public static int[] hp = new int[] 
        { 3, 3, 3, 0 };
        //攻撃力
        public static int[] attackPower = new int[]
        { 4, 6, 2, 0 };
        //攻撃速度
        public static float[] attackSpeed = new float[]
        { 5.0f, 2.0f, 7.0f, 0.0f };
    }

    public static class InvincibleStatus
    {
        public static float attackSpeed = 0.5f;
    }
}