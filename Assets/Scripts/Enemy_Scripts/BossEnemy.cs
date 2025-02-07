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
                Move();
                SmoothPlayerDirection();//関数"SmoothPlayerDirection"を実行する

                actionChangeTimer += Time.deltaTime;

                if(nowAnimationNumber == (int)Enemy.HumanoidAnimation.Walk)
                {
                    if (this.transform.position.x + actionRange.x > playerTransform.position.x &&
                        this.transform.position.x - actionRange.x < playerTransform.position.x &&
                        this.transform.position.y + actionRange.y > playerTransform.position.y)
                    {
                        isAnimation = true;
                        nowAnimationNumber = (int)Enemy.HumanoidAnimation.Jump;
                        AnimationPlay();
                    }
                    else if(this.transform.position.x + actionRange.x > playerTransform.position.x &&
                            this.transform.position.x - actionRange.x < playerTransform.position.x &&
                            this.transform.position.y + actionRange.y < playerTransform.position.y)
                    {
                        isAnimation = true;
                        nowAnimationNumber = (int)Enemy.HumanoidAnimation.Throw;
                        AnimationPlay();
                    }
                }
                else if (nowAnimationNumber == (int)Enemy.HumanoidAnimation.CrazyRun)
                {
                    bulletPutTimer += Time.deltaTime;

                    if (bulletPutTimer >= bulletPutInterval)
                    {
                        Instantiate(bulletPut, this.transform.position, Quaternion.identity);
                        bulletPutTimer = 0.0f;
                    }
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
            SmoothPlayerDirection();
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
        if (nowAnimationNumber == (int)Enemy.HumanoidAnimation.Walk)
        {
            nowAnimationNumber = (int)Enemy.HumanoidAnimation.Battlecry;
        }
        else if (nowAnimationNumber == (int)Enemy.HumanoidAnimation.CrazyRun)
        {
            nowAnimationNumber = (int)Enemy.HumanoidAnimation.JumpAttack;//現在のアニメーションを"ジャンプ攻撃"にする
        }

        AnimationPlay();//関数"AnimationPlay"を実行する
    }

    public override void ActionWait()
    {
        if (nowAnimationNumber == (int)Enemy.HumanoidAnimation.Throw)
        {
            if (animationTimer >= 2.5f)
            {
                if (nowBullet > 0)
                {
                    Vector3 bulletDirection = (playerTransform.position - this.transform.position).normalized;
                    Instantiate(bulletShot, positionShot.transform.position, Quaternion.LookRotation(bulletDirection));
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