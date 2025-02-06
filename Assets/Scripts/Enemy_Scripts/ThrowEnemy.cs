using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowEnemy : EnemyBase
{
    // Start is called before the first frame update
    void Start()
    {
        //ステータスを設定する
        enemyType = Enemy.EnemyType.Human.ToString();//敵の型
        //初期のアニメーションを設定する
        defaultAnimationNumber = (int)Enemy.HumanoidAnimation.Run;
        //関数を実行する
        GetComponent();//コンポーネントを所得
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

        if (viewPortPosition.x < moveRange[0].range[0].x && hp <= 0)
        {
            Destroy();//関数"Destroy"を実行する
        }
    }

    public override void Action()
    {
        if (isAnimation == true)
        {
            AnimationFind();        //関数"AnimationFind"を実行する
        }
        //
        else if (isAnimation == false)
        {
            if (PlayerBase.status != "Death")
            {
                SmoothPlayerDirection();//関数"SmoothPlayerDirection"を実行する
                Move();

                actionChangeTimer += Time.deltaTime;

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

        this.transform.position = new Vector3(this.transform.position.x, 0.0f, playerTransform.position.z);
    }

    public override void ActionChange()
    {
        //
        if (nowAnimationNumber == defaultAnimationNumber)
        {
            nowAnimationNumber = (int)Enemy.HumanoidAnimation.Throw;
        }

        isAnimation = true;
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
                if (nowBullet <= 0)
                {
                    nowBullet = maxBullet;
                }
            }
        }

        base.ActionWait();
    }
}
