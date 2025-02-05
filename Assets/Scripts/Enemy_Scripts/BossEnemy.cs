using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : EnemyBase
{
    // Start is called before the first frame update
    void Start()
    {
        //ステータスを設定する
        enemyType = Enemy.EnemyType.Human.ToString();   //敵の型
        bossEnemy = true;
        //初期のアニメーションを設定する
        defaultAnimationNumber = (int)Enemy.HumanoidAnimation.Walk;
        //関数を実行する
        GetComponent();//コンポーネントを所得する
        BaseStart();   //関数"BaseStart"を実行する
    }

    // Update is called once per frame
    void Update()
    {
        BaseUpdate();
    }

    public override void BaseUpdate()
    {
        base.BaseUpdate();

        if (viewPortPosition.x < moveRange[0].range[1].x)
        {
            action = true;
        }
    }

    public override void Action()
    {
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
                PlayerDirection();//関数"PlayerDirection"を実行する
                Move();

                actionChangeTimer += Time.deltaTime;

                if (this.transform.position.x + actionRange.x > playerTransform.position.x &&
                    this.transform.position.x - actionRange.x < playerTransform.position.x &&
                    this.transform.position.y + actionRange.y < playerTransform.position.y &&
                    nowAnimationNumber == defaultAnimationNumber)
                {
                    isAnimation = true;
                    nowAnimationNumber = (int)Enemy.HumanoidAnimation.Jump;
                    AnimationPlay();
                }

                if (actionChangeTimer >= actionChangeInterval)
                {
                    isAnimation = true;
                    actionChangeTimer = 0.0f;
                    ActionChange();
                }
            }
            else if (PlayerBase.status == "Death")
            {
                nowAnimationNumber = (int)Enemy.HumanoidAnimation.Dance;
                AnimationPlay();                                        //関数"AnimationPlay"を実行する
            }
        }

        if(nowAnimationNumber == (int)Enemy.HumanoidAnimation.Throw)
        {
            PlayerDirection();
        }

        if (nowAnimationNumber != (int)Enemy.HumanoidAnimation.Jump &&
            nowAnimationNumber != (int)Enemy.HumanoidAnimation.JumpAttack)
        {
            this.transform.position = new Vector3(this.transform.position.x, 0.0f, playerTransform.position.z);
        }
    }

    public override void ActionChange()
    {
        //
        if (nowAnimationNumber == defaultAnimationNumber)
        {
            int[] randomAnimation = { 14, 23 };
            nowAnimationNumber = randomAnimation[Random.Range(0, randomAnimation.Length)];
        }
        if (nowAnimationNumber == (int)Enemy.HumanoidAnimation.CrazyRun)
        {
            nowAnimationNumber = (int)Enemy.HumanoidAnimation.JumpAttack;//現在のアニメーションを"ジャンプ攻撃"にする
        }

        AnimationPlay();//関数"AnimationPlay"を実行する
    }

    public override void ActionWait()
    {
        if (nowAnimationNumber == (int)Enemy.HumanoidAnimation.Throw)
        {
            if (animationTimer >= 2.25f)
            {
                if (nowBullet > 0)
                {
                    Instantiate(bullet, shotPosition.transform.position, Quaternion.identity);
                    nowBullet -= 1;
                }
            }

            if (animationTimer >= nowAnimationLength)
            {
                if(nowBullet <= 0)
                {
                    nowBullet = maxBullet;
                }
            }
        }

        base.ActionWait();
    }

    public override void DeathEnemy()
    {
        bossEnemy = false;
        base.DeathEnemy();
    }
}