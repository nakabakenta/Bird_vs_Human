using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaveGunEnemy : EnemyBase
{
    public bool isReload = false;
    //このオブジェクトのコンポーネント
    public GameObject gun;           //"GameObject(銃)"
    public GameObject bullet;        //"GameObject(弾)"

    // Start is called before the first frame update
    void Start()
    {
        //ステータスを設定
        enemyName = EnemyName.HaveGunEnemy.ToString();//名前
        hp = EnemyList.HaveGunEnemy.hp;            //体力
        //初期のアニメーション番号を設定する
        defaultAnimationNumber = (int)HumanoidAnimation.HaveGunIdle;
        //関数を実行する
        GetComponent();//コンポーネントを所得する
        StartEnemy();  //敵の設定をする
    }

    // Update is called once per frame
    void Update()
    {
        base.UpdateEnemy();

        if (isAction == true)
        {
            AnimationChange();//関数"Wait"を実行
        }
    }

    public override void AnimationChange()
    {
        base.AnimationChange();

        if (nowAnimationNumber == (int)HumanoidAnimation.HaveGunIdle)
        {
            nowAnimationNumber = (int)HumanoidAnimation.GunPlay;
            AnimationPlay();                                          //関数"AnimationPlay"を実行する
        }
        else
        {
            if(isReload == true)
            {
                Instantiate(bullet, gun.transform.position, Quaternion.identity);
                isReload = true;
            }
            else if(isReload == false)
            {
                // 弾の方向を計算
                if (animationTimer >= nowAnimationLength)
                {
                    isAnimation = !isAnimation;
                    animationTimer = 0.0f;


                    nowAnimationNumber = (int)HumanoidAnimation.Reload;

                    AnimationPlay();                                   //関数"AnimationPlay"を実行する
                }


            }

            if (animationTimer >= nowAnimationLength)
            {
                isReload = false;
                animationTimer = 0.0f;
                nowAnimationNumber = (int)HumanoidAnimation.GunPlay;
                AnimationPlay();                                    //関数"AnimationPlay"を実行する
            }

        }
    }


    public override void OnTriggerEnter(Collider collision)
    {
        base.OnTriggerEnter(collision);
    }
}