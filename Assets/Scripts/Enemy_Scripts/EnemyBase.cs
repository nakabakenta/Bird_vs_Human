using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : CharacteBase
{
    //このオブジェクトのコンポーネント
    public GameObject bulletShot, bulletPut;//"GameObject(弾)"
    public GameObject effectDeath;          //"GameObject(エフェクト)"
    public GameObject shotPosition;         //"GameObject(発射位置)"
    public GameObject sEMove;
    public AudioClip sEShot, sEDeath;
    //ステータス
    public float jump;         //ジャンプ力
    public float actionChangeInterval, shotBulletInterval, putBulletInterval;//行動変更間隔, 攻撃間隔
    public float rotationSpeed;//回転速度
    public int maxBullet;
    public Vector3 actionRange;
    public static bool bossEnemy;//
    //敵の型
    protected string enemyType;
    /*【処理】*/
    //標準のアニメーション番号, 現在のアニメーション番号
    protected int defaultAnimationNumber, nowAnimationNumber;
    //行動変更タイマー, 攻撃タイマー
    protected float actionChangeTimer = 0.0f, attackTimer = 0.0f, putBulletTimer = 0.0f;
    //行動の可否, アニメーションの可否
    protected int nowBullet;
    protected string nowAnimationName;       //現在のアニメーションの名前
    protected float nowAnimationLength;      //現在のアニメーションの長さ
    protected float animationTimer = 0.0f;   //アニメーションタイマー
    protected bool action = false, isAnimation = false;
    private float jumpTimer = 0.0f;          //ジャンプタイマー
    private float gravity = 0.0f;            //重力          
    private Enemy.HumanoidAnimation humanoidAnimation;//"enum(HumanoidAnimation)"

    //関数"BaseStart"
    public void BaseStart()
    {
        nowBullet = maxBullet;

        if (enemyType == Enemy.EnemyType.Human.ToString())
        {
            //このオブジェクトのコンポーネントを取得
            animator = this.GetComponent<Animator>();
            runtimeAnimatorController = animator.runtimeAnimatorController;
            //処理を初期化する
            nowAnimationNumber = defaultAnimationNumber;//現在のアニメーション番号に標準のアニメーション番号を設定する
            AnimationPlay();                            //関数"AnimationPlay"を実行する
        }
    }

    //関数"BaseUpdate"
    public virtual void BaseUpdate()
    {
        //このオブジェクトのワールド座標をビューポート座標に変換して取得する
        viewPortPosition.x = Camera.main.WorldToViewportPoint(this.transform.position).x;

        //体力が"0より上" && 行動"する"の場合
        if (hp > 0 && action == true)
        {
            Action();//関数"Action"を実行する
        }

        if (sEMove != null)
        {
            if(viewPortPosition.x > moveRange[0].range[0].x && viewPortPosition.x < moveRange[0].range[1].x)
            {
                sEMove.SetActive(true);
            }
            else if(viewPortPosition.x < moveRange[0].range[0].x || viewPortPosition.x > moveRange[0].range[1].x)
            {
                sEMove.SetActive(false);
            }
        }

        if (bossEnemy == false)
        {
            Destroy();//関数"Destroy"を実行する
        }
    }

    //関数"Action"
    public virtual void Action()
    {
        
    }

    //関数"Move"
    public void Move()
    {
        this.transform.position += moveSpeed * transform.forward * Time.deltaTime;//前方向に移動する
    }

    //関数"Direction"
    public void Direction()
    {
        //
        if (this.transform.position.z > playerTransform.position.z + 0.1f)
        {
            direction.y = (int)Characte.Direction.Vertical;
        }
        //
        if (this.transform.position.z >= playerTransform.position.z - 0.1f &&
            this.transform.position.z <= playerTransform.position.z + 0.1f)
        {
            direction.y = -(int)Characte.Direction.Horizontal;
        }
    }

    public void CoarsePlayerDirection()
    {
        direction = playerTransform.position - this.transform.position;
        direction.y = 0.0f;

        this.transform.rotation = Quaternion.LookRotation(direction);
    }

    public void SmoothPlayerDirection()
    {
        direction = playerTransform.position - this.transform.position;
        direction.y = 0.0f;

        if (direction != Vector3.zero)
        {
            Quaternion quaternion = Quaternion.LookRotation(direction);
            this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, quaternion, rotationSpeed * Time.deltaTime);
        }
    }

    //関数"Animation"
    public void AnimationPlay()
    {
        humanoidAnimation = (Enemy.HumanoidAnimation)nowAnimationNumber;
        nowAnimationName = humanoidAnimation.ToString();
        animator.SetInteger("Animation", nowAnimationNumber);           //"animator(Motion)"に"nowAnimation"を設定して再生
    }

    public virtual void ActionChange()
    {

    }

    //関数"AnimationFind"
    public void AnimationFind()
    {
        foreach (AnimationClip clip in runtimeAnimatorController.animationClips)
        {
            if (clip.name == nowAnimationName)
            {
                nowAnimationLength = clip.length;
            }
        }

        animationTimer += Time.deltaTime;//"animationTimer"に"Time.deltaTime(経過時間)"を足す
        ActionWait();                    //関数"ActionWait"を実行する
    }

    //関数"ActionWait"
    public virtual void ActionWait()
    {
        //
        if (nowAnimationNumber == (int)Enemy.HumanoidAnimation.Jump)
        {
            jumpTimer += Time.deltaTime;//"jumpTimer"に"Time.deltaTime(経過時間)"を足す

            if (animationTimer >= 0.8f && jumpTimer >= 0.1f)
            {
                gravity += 1.0f;
                jumpTimer = 0.0f;
            }

            if (animationTimer >= 0.8f)
            {
                this.transform.position += (jump - gravity) * transform.up * Time.deltaTime;

                if (animationTimer >= 2.7f)
                {
                    gravity = 0.0f;
                    jumpTimer = 0.0f;
                    ActionReset();
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
        //現在のアニメーションが"ジャンプ攻撃"の場合
        else if (nowAnimationNumber == (int)Enemy.HumanoidAnimation.JumpAttack)
        {
            //
            if (animationTimer >= nowAnimationLength / 2)
            {
                moveSpeed /= 3.0f;
                ActionReset();
            }
        }
        //現在のアニメーションが"雄叫び"の場合
        else if (nowAnimationNumber == (int)Enemy.HumanoidAnimation.Battlecry)
        {
            //
            if (animationTimer >= nowAnimationLength / 2)
            {
                animationTimer = 0.0f;                                     //アニメーションタイマーを初期化する
                nowAnimationNumber = (int)Enemy.HumanoidAnimation.CrazyRun;//現在のアニメーションを"走る"にする
                moveSpeed *= 3.0f;                                         //移動速度を"*3"する
                isAnimation = false;
                AnimationPlay();
            }
        }
        else if (nowAnimationNumber == (int)Enemy.HumanoidAnimation.ExitCar)
        {
            if (animationTimer >= nowAnimationLength / 2)
            {   //アニメーションタイマーを初期化する
                defaultAnimationNumber = (int)Enemy.HumanoidAnimation.Run;//現在のアニメーションを"走る"にする
                ActionReset();
            }
        }
        else if (animationTimer >= nowAnimationLength)
        {
            ActionReset();
        }
    }

    public void ActionReset()
    {
        animationTimer = 0.0f;

        if (isAnimation == true)
        {
            isAnimation = false;
            nowAnimationNumber = defaultAnimationNumber;
        }

        AnimationPlay();//関数"AnimationPlay"を実行する
    }


    //関数"Enmey"
    public void DamageEnemy()
    {
        hp -= PlayerBase.attackPower;

        //体力が"0より上" && 現在のアニメーション番号が初期のアニメーション番号と等しい場合
        if (hp > 0)
        {
            audioSource.PlayOneShot(damage);//"damage"を鳴らす

            if (enemyType == Enemy.EnemyType.Human.ToString() && nowAnimationNumber == defaultAnimationNumber)
            {
                isAnimation = true;                                      //"isAnimation = true"にする
                nowAnimationNumber = (int)Enemy.HumanoidAnimation.Damage;
                AnimationPlay();                                         //関数"Animation"を実行
            }
        }
        //"hp <= 0"の場合
        else if (hp <= 0)
        {
            Invoke("DeathEnemy", 0.01f);//関数"DeathEnemy"を"0.01f"後に実行する
        }
    }

    //関数"Enmey"
    public virtual void DeathEnemy()
    {
        this.tag = "Untagged";         //このタグを"Untagged"にする
        hp = 0;                        //体力を"0"にする
        GameManager.score += 1;        //スコアを足す
        PlayerBase.exp += 1;           //経験値を足す

        if (enemyType == Enemy.EnemyType.Human.ToString())
        {
            audioSource.PlayOneShot(sEDeath);//"death"を鳴らす

            //位置(.Y)を"0.0f"にする
            this.transform.position
                = new Vector3(this.transform.position.x, 0.0f, this.transform.position.z);
            nowAnimationNumber = (int)Enemy.HumanoidAnimation.Death;
            AnimationPlay();                                        //関数"Animation"を実行
        }
        else if(enemyType == Enemy.EnemyType.Vehicle.ToString())
        {
            Instantiate(effectDeath, this.transform.position, this.transform.rotation);
            Invoke("Destroy", 1.0f);//関数"Destroy"を"5.0f"後に実行
        }
    }

    //当たり判定(OnTriggerEnter)
    public virtual void OnTriggerEnter(Collider collision)
    {
        if(enemyType == Enemy.EnemyType.Human.ToString())
        {
            //衝突したオブジェクトの"tag == Player" && "isAnimation == false"の場合
            if (collision.gameObject.tag == "Player" && isAnimation == false)
            {
                isAnimation = true;//"isAnimation = true"にする
                                   //ランダム"10(パンチ)"～"12(キック)"
                nowAnimationNumber = (int)Random.Range((int)Enemy.HumanoidAnimation.Punch,
                                                       (int)Enemy.HumanoidAnimation.Kick + 1);
                AnimationPlay();//関数"AnimationPlay"を実行する
            }
        }
        //衝突したオブジェクトのタグが"PlayerBullet" && 体力が"0より上"の場合
        if (collision.gameObject.tag == "PlayerBullet" && hp > 0)
        {
            DamageEnemy();//関数"DamageEnemy"を実行する
        }
    }

    public static class Enemy
    {
        public enum EnemyType
        {
            Human,
            Vehicle,
        }

        public enum HumanoidAnimation
        {
            Walk = 0,
            Run = 1,
            CrazyRun = 2,
            HaveGunIdle = 3,
            Punch = 10,
            Kick = 11,
            JumpAttack = 12,
            GunPlay = 13,
            Throw = 14,
            Jump = 20,
            Crouch = 21,
            ExitCar = 22,
            Battlecry = 23,
            Reload = 24,
            Dance = 30,
            Damage = 31,
            Death = 32
        }
    }
}