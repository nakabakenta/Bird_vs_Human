using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : CharacteBase
{
    //ステータス
    public static int attackPower;//攻撃力
    public static int remain;     //残機
    public static string status;  //状態
    //座標
    public Vector3 mousePosition;
    //処理
    public static float[] attackTimer = new float[2];       //攻撃タイマー([前方],[下方])
    public static float[] attackTimeInterval = new float[2];//攻撃時間間隔([前方],[下方])
    public static float gageTimer;                          //ゲージタイマー
    public static float gageTimeInterval;                   //ゲージ時間間隔
    public static int maxExp = 10;                          //最大経験値
    public static int exp;                                  //経験値
    public static int ally;                                 //味方数
    public float invincibleTimer = 0.0f;                    //無敵タイマー
    public float invincibleInterval = 10.0f;                //無敵持続時間
    public float blinkingTime = 1.0f;                       //点滅持続時間
    public float rendererSwitch = 0.05f;                    //Renderer切り替え時間
    public float rendererTimer;                             //Renderer切り替えの経過時間
    public float rendererTotalTime;                         //Renderer切り替えの合計経過時間
    public bool isRenderer;                               　//Rendererの可否
    public float levelAttackInterval = 0.0f;                //レベルアップ時の攻撃間隔短縮
    public bool isAction = false;      //行動の可否
    public bool isAnimation = false;   //アニメーションの可否
    //このオブジェクトのコンポーネント
    public GameObject nowPlayer;                       //"GameObject(現在のプレイヤー)"
    public GameObject[] playerAlly = new GameObject[2];//"GameObject(プレイヤーの味方)"

    //関数"StartPlayer"
    public void StartPlayer()
    {
        //選択したプレイヤーのステータスを設定する
        hp = Player.hp[GameManager.selectPlayer];                     //体力
        attackPower = Player.attackPower[GameManager.selectPlayer];   //攻撃力

        if(GameManager.selectPlayer == 0)
        {
            attackTimer[0] = 2.0f;
            attackTimer[1] = 2.0f;
        }
        else if (GameManager.selectPlayer == 1)
        {
            attackTimer[0] = 3.0f;
            attackTimer[1] = 3.0f;
        }
        else if (GameManager.selectPlayer == 2)
        {
            attackTimer[0] = 1.0f;
            attackTimer[1] = 1.0f;
        }

        status = "Normal";                                            //プレイヤーの状態を"Normal"にする

        //処理を初期化する
        gageTimer = 0.0f;
        gageTimeInterval = 20.0f;
        ally = 0;
        exp = 0;

        //ゲームの状態が"Menu"の場合
        if (GameManager.status == "Menu")
        {
            remain = 3;                 //残機
            GameManager.status = "Play";//ゲームの状態を"Play"にする
        }
    }

    public virtual void UpdatePlayer()
    {

    }

    //関数"DamagePlayer"
    public virtual void DamagePlayer()
    {
        //ダメージを"受けている"場合
        if (isDamage == true)
        {
            return;//返す
        }

        hp -= 1;//体力を"-1"する
    }

    //関数"DeathPlayer"
    public void DeathPlayer()
    {
        boxCollider.enabled = false;    //BoxColliderを"無効"にする
        rigidBody.useGravity = true;    //RigidBodyの重力を"有効"にする
        animator.SetBool("Death", true);//Animatorを"Death"にする
        hp = 0;                         //体力を"0"にする
        remain -= 1;                    //残機を"-1"する
        status = "Death";
    }

    public static class Player
    {
        public enum PlayerName
        {
            Sparrow = 0,
            Crow = 1,
            Chickadee = 2,
            Penguin = 3,
        }

        //体力
        public static int[] hp = new int[] 
        { 4, 4, 4, 4 };
        //攻撃力
        public static int[] attackPower = new int[]
        { 3, 6, 1, 5 };
        //攻撃速度
        public static float[] attackSpeed = new float[]
        { 4.0f, 2.0f, 6.0f, 6.0f };
    }

    public static class InvincibleStatus
    {
        public static float attackSpeed = 0.5f;
    }
}