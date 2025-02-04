using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaveGunEnemy : EnemyBase
{
    public int maxMagazine;
    private int nowMagazine;

    // Start is called before the first frame update
    void Start()
    {
        //ステータスを設定
        enemyType = Enemy.EnemyType.Human.ToString();//敵の型
        nowMagazine = maxMagazine;
        //初期のアニメーション番号を設定する
        defaultAnimationNumber = (int)Enemy.HumanoidAnimation.HaveGunIdle;
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

        if (viewPortPosition.x < 1)
        {
            action = true;
        }

        if (viewPortPosition.x < 0)
        {
            Destroy();//関数"Destroy"を実行する
        }
    }

    public override void Action()
    {
        if (PlayerBase.status != "Death")
        {
            PlayerFind();   //関数"PlayerFind"を実行する
            ActionChange();
            AnimationFind();//関数"AnimationFind"を実行する
        }
        else if (PlayerBase.status == "Death")
        {
            nowAnimationNumber = (int)Enemy.HumanoidAnimation.Dance;
            AnimationPlay();                                        //関数"AnimationPlay"を実行する
        }

        this.transform.position = new Vector3(this.transform.position.x, 0.0f, playerTransform.position.z);
    }

    public override void ActionChange()
    {
        if (nowAnimationNumber == (int)Enemy.HumanoidAnimation.HaveGunIdle)
        {
            nowAnimationNumber = (int)Enemy.HumanoidAnimation.GunPlay;
            AnimationPlay();                                          //関数"AnimationPlay"を実行する
        }
    }

    public override void ActionWait()
    {
        if(animationTimer >= nowAnimationLength)
        {
            if (nowMagazine <= 0)
            {
                nowAnimationNumber = (int)Enemy.HumanoidAnimation.Reload;
                nowMagazine = maxMagazine;
            }
            else if (nowMagazine <= maxMagazine)
            {
                nowAnimationNumber = (int)Enemy.HumanoidAnimation.GunPlay;
                Instantiate(bullet, shotPosition.transform.position, Quaternion.identity);
                nowMagazine -= 1;
            }
        }

        base.ActionWait();
    }
}