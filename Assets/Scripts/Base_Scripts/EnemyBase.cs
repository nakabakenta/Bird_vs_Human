using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : CharacteBase
{
    //このオブジェクトのコンポーネント
    public AudioClip scream;//"AudioClip(叫び声)"
    //ステータス
    public string enemyType;//敵の型
    public float jump;      //ジャンプ力
    //処理
    public float rotation = 90.0f;
    public int defaultAnimationNumber, nowAnimationNumber;//標準のアニメーション番号, 現在のアニメーション番号
    public string nowAnimationName;                       //現在のアニメーションの名前
    public float nowAnimationLength;                      //現在のアニメーションの長さ
    public bool isAction = false;                         //行動の可否
    public float animationTimer = 0.0f;                   //アニメーションタイマー
    public float changeAnimationTimer = 0.0f;
    public float jumpTimer = 0.0f;
    public bool isAnimation = false;                      //アニメーションの可否
    public HumanoidAnimation humanoidAnimation;

    //関数"StartEnmey"
    public void StartEnemy()
    {
        //このオブジェクトのコンポーネントを取得
        animator = this.GetComponent<Animator>();
        runtimeAnimatorController = animator.runtimeAnimatorController;
        //
        nowAnimationNumber = defaultAnimationNumber;
        AnimationPlay();                            //関数"AnimationPlay"を実行する
    }

    //関数"UpdateEnmey"
    public virtual void UpdateEnemy()
    {
        //このオブジェクトのビューポート座標を取得
        viewPortPosition.x = Camera.main.WorldToViewportPoint(this.transform.position).x;

        //ビューポート座標が"0未満"の場合
        if (viewPortPosition.x < 0)
        {
            Destroy();//関数"Destroy"を実行
        }

        if (isAction == false)
        {
            if (viewPortPosition.x < 1)
            {
                isAction = true;
            }
        }

        //"hp > 0 && isAction == true"
        if (hp > 0 && isAction == true)
        {
            Action();
        }
    }

    //関数"Action"
    public void Action()
    {
        //
        if (this.transform.position.z > playerTransform.position.z + 0.5f)
        {
            rotation = 180.0f;
        }
        //
        if (this.transform.position.z >= playerTransform.position.z - 0.5f &&
            this.transform.position.z <= playerTransform.position.z + 0.5f)
        {
            rotation = 90.0f;//
        }

        if (isAnimation == false)
        {
            //
            if (PlayerController.status != "Death")
            {
                changeAnimationTimer += Time.deltaTime;

                if (enemyType == EnemyType.WalkEnemy.ToString())
                {
                    this.transform.eulerAngles = new Vector3(this.transform.rotation.x, -rotation, this.transform.rotation.z);
                }
                else if (enemyType != EnemyType.WalkEnemy.ToString())
                {
                    if (enemyType == EnemyType.RunEnemy.ToString() || enemyType == EnemyType.BossEnemy.ToString())
                    {
                        if (this.transform.position.x + EnemyList.RunEnemy.range.x > playerTransform.position.x &&
                        this.transform.position.x - EnemyList.RunEnemy.range.x < playerTransform.position.x &&
                        this.transform.position.y + EnemyList.RunEnemy.range.y < playerTransform.position.y &&
                        this.transform.position.y == 0.0f && nowAnimationNumber == defaultAnimationNumber)
                        {
                            isAnimation = true;
                            nowAnimationNumber = (int)HumanoidAnimation.Jump;
                            AnimationPlay();
                        }
                    }

                    //
                    if (this.transform.position.x > playerTransform.position.x)
                    {
                        this.transform.eulerAngles = new Vector3(this.transform.rotation.x, -rotation, this.transform.rotation.z);
                    }
                    //
                    else if (this.transform.position.x < playerTransform.position.x)
                    {
                        this.transform.eulerAngles = new Vector3(this.transform.rotation.x, rotation, this.transform.rotation.z);
                    }
                }

                if (enemyType != EnemyType.HaveGunEnemy.ToString())
                {
                    this.transform.position += speed * transform.forward * Time.deltaTime;//前方向に移動する
                }

                //アニメーション切り替えタイマーが"5.0f以上"の場合
                if (changeAnimationTimer >= 5.0f && enemyType == EnemyType.BossEnemy.ToString())
                {
                    isAnimation = true;
                    changeAnimationTimer = 0.0f;//アニメーション切り替えタイマーを初期化する

                    //
                    if (nowAnimationNumber == defaultAnimationNumber)
                    {
                        nowAnimationNumber = (int)HumanoidAnimation.Battlecry;//現在のアニメーションを"雄叫び"にする
                    }
                    //
                    if (nowAnimationNumber == (int)HumanoidAnimation.CrazyRun)
                    {
                        nowAnimationNumber = (int)HumanoidAnimation.JumpAttack;//現在のアニメーションを"ジャンプ攻撃"にする
                    }

                    AnimationPlay();//関数"AnimationPlay"を実行する
                }
            }
            else if (PlayerController.status == "Death")
            {
                nowAnimationNumber = (int)HumanoidAnimation.Dance;
                AnimationPlay();                                  //関数"AnimationPlay"を実行する
            }
        }
        //
        else if (isAnimation == true)
        {
            AnimationChange();//関数"AnimationChange"を実行
        }

        if (nowAnimationNumber != (int)HumanoidAnimation.Jump && 
            nowAnimationNumber != (int)HumanoidAnimation.JumpAttack)
        {
            this.transform.position = new Vector3(this.transform.position.x, 0.0f, playerTransform.position.z);
        }
    }

    //関数"Animation"
    public void AnimationPlay()
    {
        humanoidAnimation = (HumanoidAnimation)nowAnimationNumber;
        nowAnimationName = humanoidAnimation.ToString();
        animator.SetInteger("Animation", nowAnimationNumber);        //"animator(Motion)"に"nowAnimation"を設定して再生
    }

    //関数"Wait"
    public virtual void AnimationChange()
    {
        animationTimer += Time.deltaTime;//"animationTimer"に"Time.deltaTime(経過時間)"を足す

        foreach (AnimationClip clip in runtimeAnimatorController.animationClips)
        {
            if (clip.name == nowAnimationName)
            {
                nowAnimationLength = clip.length;
            }
        }

        //
        if (nowAnimationNumber == (int)HumanoidAnimation.Jump)
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
        else if (nowAnimationNumber == (int)HumanoidAnimation.JumpAttack)
        {
            //
            if (animationTimer >= nowAnimationLength / 2)
            {
                animationTimer = 0.0f;                                //アニメーションタイマーを初期化する
                nowAnimationNumber = defaultAnimationNumber;          //現在のアニメーションを"歩く"にする
                speed = EnemyList.BossEnemy.speed[Stage.nowStage - 1];
                isAnimation = false;
                AnimationPlay();
            }
        }
        //現在のアニメーションが"雄叫び"の場合
        else if (nowAnimationNumber == (int)HumanoidAnimation.Battlecry)
        {
            //
            if (animationTimer >= nowAnimationLength / 2)
            {
                animationTimer = 0.0f;                               //アニメーションタイマーを初期化する
                nowAnimationNumber = (int)HumanoidAnimation.CrazyRun;//現在のアニメーションを"走る"にする
                speed *= 3.0f;                                       //移動速度を"*3"する
                isAnimation = false;
                AnimationPlay();
            }
        }
        else if (enemyType != EnemyType.HaveGunEnemy.ToString() && animationTimer >= nowAnimationLength)
        {
            animationTimer = 0.0f;
            isAnimation = false;
            nowAnimationNumber = defaultAnimationNumber;
            AnimationPlay();                            //関数"AnimationPlay"を実行する
        }
    }

    //関数"Enmey"
    public virtual void DamageEnemy()
    {
        hp -= PlayerBase.attack;

        //体力が"0より上" && 現在のアニメーション番号が初期のアニメーション番号と等しい場合
        if (hp > 0 && nowAnimationNumber == defaultAnimationNumber)
        {
            isAnimation = true;                                //"isAnimation = true"にする
            nowAnimationNumber = (int)HumanoidAnimation.Damage;
            audioSource.PlayOneShot(damage);                   //"damage"を鳴らす
            AnimationPlay();                                   //関数"Animation"を実行
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
        this.tag = "Untagged";                            //このタグを"Untagged"にする
        hp = 0;                                           //体力を"0"にする
        GameManager.score += EnemyList.WalkEnemy.score;   //スコアを足す
        PlayerController.exp += 10;                       //経験値を足す

        //位置(.Y)を"0.0f"にする
        this.transform.position
            = new Vector3(this.transform.position.x, 0.0f, this.transform.position.z);
        nowAnimationNumber = (int)HumanoidAnimation.Death;
        audioSource.PlayOneShot(scream);                  //"scream"を鳴らす
        AnimationPlay();                                  //関数"Animation"を実行
    }

    //当たり判定(OnTriggerEnter)
    public virtual void OnTriggerEnter(Collider collision)
    {
        //衝突したオブジェクトの"tag == Player" && "isAnimation == false"の場合
        if (collision.gameObject.tag == "Player" && isAnimation == false)
        {
            isAnimation = true;//"isAnimation = true"にする
            //ランダム"10(パンチ)"～"12(キック)"
            nowAnimationNumber = (int)Random.Range((int)HumanoidAnimation.Punch,
                                                   (int)HumanoidAnimation.Kick + 1);
            AnimationPlay();//関数"AnimationPlay"を実行する
        }
        //衝突したオブジェクトのタグが"Bullet" && 体力が"0より上"の場合
        if (collision.gameObject.tag == "Bullet" && hp > 0)
        {
            DamageEnemy();//関数"DamageEnemy"を実行する
        }
    }

    public enum EnemyType
    {
        WalkEnemy,
        RunEnemy,
        HaveGunEnemy,
        CarEnemy,
        BossEnemy,
    }

    public enum HumanoidAnimation
    {
        Walk        = 0,
        Run         = 1,
        CrazyRun    = 2,
        HaveGunIdle = 3,
        Punch       = 10,
        Kick        = 11,
        JumpAttack  = 12,
        GunPlay     = 13,
        Jump        = 20,
        Crouch      = 21,
        ExitCar     = 22,
        Battlecry   = 23,
        Reload      = 24,
        Dance       = 30,
        Damage      = 31,
        Death       = 32
    }
}
