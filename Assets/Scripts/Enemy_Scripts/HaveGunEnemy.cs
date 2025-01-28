using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaveGunEnemy : EnemyBase
{
    public int nowMagazine;
    public int maxMagazine = 3;
    //このオブジェクトのコンポーネント
    public GameObject gun;           //"GameObject(銃)"
    public GameObject bullet;        //"GameObject(弾)"

    // Start is called before the first frame update
    void Start()
    {
        //ステータスを設定
        enemyType = EnemyType.Wait.ToString();//敵の型
        hp = EnemyList.HaveGunEnemy.hp;       //体力
        nowMagazine = maxMagazine;
        //処理を初期化する
        playerFind = true;
        //初期のアニメーション番号を設定する
        defaultAnimationNumber = (int)HumanoidAnimation.HaveGunIdle;
        //関数を実行する
        GetComponent();//コンポーネントを所得する
        StartEnemy();  //敵の設定をする
    }

    // Update is called once per frame
    void Update()
    {
        UpdateEnemy();
    }

    public override void AddAction()
    {
        if (nowAnimationNumber == (int)HumanoidAnimation.HaveGunIdle)
        {
            nowAnimationNumber = (int)HumanoidAnimation.GunPlay;
            AnimationPlay();                                    //関数"AnimationPlay"を実行する
        }

        Direction();    //関数"Direction"を実行する
        AnimationFind();//関数"AnimationFind"を実行する
    }


    public override void AddAnimationChange()
    {
        if (nowMagazine <= 0)
        {
            nowAnimationNumber = (int)HumanoidAnimation.Reload;
            nowMagazine = maxMagazine;
        }
        else if (nowMagazine <= maxMagazine)
        {
            nowAnimationNumber = (int)HumanoidAnimation.GunPlay;
            Instantiate(bullet, gun.transform.position, Quaternion.identity);
            nowMagazine -= 1;
        }
    }

    public override void OnTriggerEnter(Collider collision)
    {
        base.OnTriggerEnter(collision);
    }
}