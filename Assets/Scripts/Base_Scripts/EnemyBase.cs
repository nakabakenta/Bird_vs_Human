using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : CharacteBase
{
    //このオブジェクトのコンポーネント
    public AudioClip death;//"AudioClip(死亡)"
    //ステータス
    public string enemyType;     //敵の型
    public string enemyOption;   //敵の設定
    public float jump;           //ジャンプ力
    public static bool bossEnemy;//
    //処理
    public bool action = false;                           //行動の可否
    public bool playerFind;                               //プレイヤー探しの可否
    public int defaultAnimationNumber, nowAnimationNumber;//標準のアニメーション番号, 現在のアニメーション番号
    public bool isAnimation = false;                      //アニメーションの可否
    private string nowAnimationName;                      //現在のアニメーションの名前
    private float nowAnimationLength;                     //現在のアニメーションの長さ
    private float animationTimer = 0.0f;                  //アニメーションタイマー
    private float animationChangeTimer = 0.0f;            //アニメーション切り替えタイマー
    private float jumpTimer = 0.0f;                       //ジャンプタイマー
    private Enemy.HumanoidAnimation humanoidAnimation;    //"enum(HumanoidAnimation)"

    //関数"StartAnimation"
    public void StartAnimation()
    {
        //このオブジェクトのコンポーネントを取得
        animator = this.GetComponent<Animator>();
        runtimeAnimatorController = animator.runtimeAnimatorController;
        //処理を初期化する
        nowAnimationNumber = defaultAnimationNumber;//現在のアニメーション番号に標準のアニメーション番号を設定する
        AnimationPlay();                            //関数"AnimationPlay"を実行する
    }

    //関数"UpdateEnmey"
    public void UpdateEnemy()
    {
        //このオブジェクトのワールド座標をビューポート座標に変換して取得する
        viewPortPosition.x = Camera.main.WorldToViewportPoint(this.transform.position).x;

        if(enemyType == Enemy.EnemyType.Human.ToString())
        {
            if (action == false)
            {
                if (viewPortPosition.x < 1)
                {
                    action = true;
                }
            }
            //体力が"0より上" && 行動"する"の場合
            if (hp > 0 && action == true)
            {
                Action();//関数"Action"を実行する
            }

            //ビューポート座標が"0未満"の場合
            if (viewPortPosition.x < 0)
            {
                if (enemyOption == Enemy.EnemyOption.Normal.ToString() ||
                    enemyOption == Enemy.EnemyOption.Wait.ToString())
                {
                    Destroy();//関数"Destroy"を実行する
                }
                else if (enemyOption == Enemy.EnemyOption.Find.ToString() ||
                         enemyOption == Enemy.EnemyOption.Boss.ToString())
                {
                    if (hp <= 0)
                    {
                        Destroy();//関数"Destroy"を実行する
                    }
                }
            }
        }
        else if(enemyType == Enemy.EnemyType.Vehicle.ToString())
        {
            if (enemyOption == Enemy.EnemyOption.Normal.ToString())
            {
                if (action == false)
                {
                    if (rotation == (int)Characte.Direction.Vertical)
                    {
                        if (this.thisTransform.position.x < playerTransform.position.x + 5.0f)
                        {
                            action = true;
                        }
                    }
                    else if(rotation == -(int)Characte.Direction.Horizontal)
                    {
                        if (viewPortPosition.x < 1.25)
                        {
                            action = true;
                        }
                    }
                }

                //体力が"0より上" && 行動"する"の場合
                if (hp > 0 && action == true)
                {
                    Action();//関数"Action"を実行する
                }
            }
            else if(enemyOption == Enemy.EnemyOption.Find.ToString() ||
                    enemyOption == Enemy.EnemyOption.Boss.ToString())
            {
                //体力が"0より上" && 行動"する"の場合
                if (hp > 0)
                {
                    Action();//関数"Action"を実行する
                }
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
        if (enemyType == Enemy.EnemyType.Human.ToString())
        {
            //
            if (isAnimation == true)
            {
                AnimationFind();//関数"AnimationFind"を実行する
            }
            //
            else if (isAnimation == false)
            {
                //
                if (PlayerBase.status != "Death")
                {
                    Direction();//関数"Direction"を実行する

                    animationChangeTimer += Time.deltaTime;

                    if (enemyOption != Enemy.EnemyOption.Normal.ToString())
                    {
                        if (enemyOption == Enemy.EnemyOption.Find.ToString() || enemyOption == Enemy.EnemyOption.Boss.ToString())
                        {
                            if (this.transform.position.x + EnemyList.RunEnemy.range.x > playerTransform.position.x &&
                            this.transform.position.x - EnemyList.RunEnemy.range.x < playerTransform.position.x &&
                            this.transform.position.y + EnemyList.RunEnemy.range.y < playerTransform.position.y &&
                            this.transform.position.y == 0.0f && nowAnimationNumber == defaultAnimationNumber)
                            {
                                isAnimation = true;
                                nowAnimationNumber = (int)Enemy.HumanoidAnimation.Jump;
                                AnimationPlay();
                            }
                        }
                    }

                    if (enemyOption != Enemy.EnemyOption.Wait.ToString())
                    {
                        Move();
                    }

                    //アニメーション切り替えタイマーが"5.0f以上"の場合
                    if (animationChangeTimer >= 5.0f && enemyOption == Enemy.EnemyOption.Boss.ToString())
                    {
                        isAnimation = true;
                        animationChangeTimer = 0.0f;//アニメーション切り替えタイマーを初期化する

                        //
                        if (nowAnimationNumber == defaultAnimationNumber)
                        {
                            nowAnimationNumber = (int)Enemy.HumanoidAnimation.Battlecry;//現在のアニメーションを"雄叫び"にする
                        }
                        //
                        if (nowAnimationNumber == (int)Enemy.HumanoidAnimation.CrazyRun)
                        {
                            nowAnimationNumber = (int)Enemy.HumanoidAnimation.JumpAttack;//現在のアニメーションを"ジャンプ攻撃"にする
                        }

                        AnimationPlay();//関数"AnimationPlay"を実行する
                    }
                }
                else if (PlayerBase.status == "Death")
                {
                    nowAnimationNumber = (int)Enemy.HumanoidAnimation.Dance;
                    AnimationPlay();                                  //関数"AnimationPlay"を実行する
                }

                AddAction();//関数"AddAction"を実行する
            }

            if (nowAnimationNumber != (int)Enemy.HumanoidAnimation.Jump &&
                nowAnimationNumber != (int)Enemy.HumanoidAnimation.JumpAttack)
            {
                this.transform.position = new Vector3(this.transform.position.x, 0.0f, playerTransform.position.z);
            }
        }
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
            rotation = (int)Characte.Direction.Vertical;
        }
        //
        if (this.transform.position.z >= playerTransform.position.z - 0.1f &&
            this.transform.position.z <= playerTransform.position.z + 0.1f)
        {
            rotation = -(int)Characte.Direction.Horizontal;
        }

        if (playerFind == true)
        {
            //
            if (this.transform.position.x > playerTransform.position.x)
            {
                rotation = -(int)Characte.Direction.Horizontal;
            }
            //
            if (this.transform.position.x < playerTransform.position.x)
            {
                rotation = (int)Characte.Direction.Horizontal;
            }
        }

        this.transform.eulerAngles = new Vector3(this.transform.rotation.x, rotation, this.transform.rotation.z);
    }

    //関数"AddAction"
    public virtual void AddAction()
    {
        
    }

    //関数"Animation"
    public void AnimationPlay()
    {
        humanoidAnimation = (Enemy.HumanoidAnimation)nowAnimationNumber;
        nowAnimationName = humanoidAnimation.ToString();
        animator.SetInteger("Animation", nowAnimationNumber);        //"animator(Motion)"に"nowAnimation"を設定して再生
    }

    //関数"AnimationFind"
    public void AnimationFind()
    {
        animationTimer += Time.deltaTime;//"animationTimer"に"Time.deltaTime(経過時間)"を足す

        foreach (AnimationClip clip in runtimeAnimatorController.animationClips)
        {
            if (clip.name == nowAnimationName)
            {
                nowAnimationLength = clip.length;
            }
        }

        AnimationWait();//関数"AnimationWait"を実行する
    }

    //関数"AnimationWait"
    public void AnimationWait()
    {
        //
        if (nowAnimationNumber == (int)Enemy.HumanoidAnimation.Jump)
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
                    nowAnimationNumber = defaultAnimationNumber;//
                    AnimationPlay();                            //関数"Animation"を実行
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
                animationTimer = 0.0f;                                //アニメーションタイマーを初期化する
                nowAnimationNumber = defaultAnimationNumber;          //現在のアニメーションを"歩く"にする
                moveSpeed /= 3.0f;
                isAnimation = false;
                AnimationPlay();
            }
        }
        //現在のアニメーションが"雄叫び"の場合
        else if (nowAnimationNumber == (int)Enemy.HumanoidAnimation.Battlecry)
        {
            //
            if (animationTimer >= nowAnimationLength / 2)
            {
                animationTimer = 0.0f;                               //アニメーションタイマーを初期化する
                nowAnimationNumber = (int)Enemy.HumanoidAnimation.CrazyRun;//現在のアニメーションを"走る"にする
                moveSpeed *= 3.0f;                                       //移動速度を"*3"する
                isAnimation = false;
                AnimationPlay();
            }
        }
        else if (nowAnimationNumber == (int)Enemy.HumanoidAnimation.ExitCar)
        {
            if (animationTimer >= nowAnimationLength / 2)
            {   //アニメーションタイマーを初期化する
                defaultAnimationNumber = (int)Enemy.HumanoidAnimation.Run;//現在のアニメーションを"走る"にする
                AnimationChange();
            }
        }
        else if (animationTimer >= nowAnimationLength)
        {
            AddAnimationChange();
            AnimationChange();
        }
    }

    public virtual void AddAnimationChange()
    {

    }

    public void AnimationChange()
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
        if (hp > 0 && enemyType == Enemy.EnemyType.Human.ToString() && nowAnimationNumber == defaultAnimationNumber)
        {
            isAnimation = true;                                      //"isAnimation = true"にする
            nowAnimationNumber = (int)Enemy.HumanoidAnimation.Damage;
            audioSource.PlayOneShot(damage);                         //"damage"を鳴らす
            AnimationPlay();                                         //関数"Animation"を実行
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
        audioSource.PlayOneShot(death);//"death"を鳴らす

        if (enemyType == Enemy.EnemyType.Human.ToString())
        {
            //位置(.Y)を"0.0f"にする
            this.transform.position
                = new Vector3(this.transform.position.x, 0.0f, this.transform.position.z);
            nowAnimationNumber = (int)Enemy.HumanoidAnimation.Death;
            AnimationPlay();                                 //関数"Animation"を実行
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
        //衝突したオブジェクトのタグが"Bullet" && 体力が"0より上"の場合
        if (collision.gameObject.tag == "Bullet" && hp > 0)
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

        public enum EnemyOption
        {
            Normal,
            Find,
            Wait,
            Boss,
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
