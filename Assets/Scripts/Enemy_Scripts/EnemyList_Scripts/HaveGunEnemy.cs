using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaveGunEnemy : EnemyBase
{
    public int nowMagazine;
    public int maxMagazine = 1;
    //このオブジェクトのコンポーネント
    public GameObject gun;           //"GameObject(銃)"
    public GameObject bullet;        //"GameObject(弾)"

    // Start is called before the first frame update
    void Start()
    {
        //ステータスを設定
        enemyType = EnemyType.HaveGunEnemy.ToString();//型
        hp = EnemyList.HaveGunEnemy.hp;               //体力
        nowMagazine = maxMagazine;
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
        if (nowAnimationNumber == (int)HumanoidAnimation.HaveGunIdle)
        {
            nowAnimationNumber = (int)HumanoidAnimation.GunPlay;
            AnimationPlay();                                         //関数"AnimationPlay"を実行する
        }

        base.AnimationChange();

        if (animationTimer >= nowAnimationLength)
        {
            isAnimation = !isAnimation;
            animationTimer = 0.0f;

            if (nowMagazine > maxMagazine)
            {
                nowAnimationNumber = (int)HumanoidAnimation.GunPlay;
                Instantiate(bullet, gun.transform.position, Quaternion.identity);
                nowMagazine -= 1;
            }
            else if (nowMagazine <= maxMagazine)
            {
                nowAnimationNumber = (int)HumanoidAnimation.Reload;
                nowMagazine = maxMagazine;
            }

            AnimationPlay();                                       //関数"AnimationPlay"を実行する
        }
    }

    public override void OnTriggerEnter(Collider collision)
    {
        base.OnTriggerEnter(collision);
    }
}