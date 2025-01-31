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
        enemyType = Enemy.EnemyType.Human.ToString();   //敵の型
        enemyOption = Enemy.EnemyOption.Wait.ToString();//
        hp = EnemyList.HaveGunEnemy.hp;                 //体力
        nowMagazine = maxMagazine;
        //処理を初期化する
        playerFind = true;
        //初期のアニメーション番号を設定する
        defaultAnimationNumber = (int)Enemy.HumanoidAnimation.HaveGunIdle;
        //関数を実行する
        GetComponent();  //コンポーネントを所得する
        StartAnimation();//開始時のアニメーションを設定する
    }

    // Update is called once per frame
    void Update()
    {
        UpdateEnemy();
    }

    public override void AddAction()
    {
        if (nowAnimationNumber == (int)Enemy.HumanoidAnimation.HaveGunIdle)
        {
            nowAnimationNumber = (int)Enemy.HumanoidAnimation.GunPlay;
            AnimationPlay();                                    //関数"AnimationPlay"を実行する
        }

        Direction();    //関数"Direction"を実行する
        AnimationFind();//関数"AnimationFind"を実行する
    }


    public override void AddAnimationChange()
    {
        if (nowMagazine <= 0)
        {
            nowAnimationNumber = (int)Enemy.HumanoidAnimation.Reload;
            nowMagazine = maxMagazine;
        }
        else if (nowMagazine <= maxMagazine)
        {
            nowAnimationNumber = (int)Enemy.HumanoidAnimation.GunPlay;
            Instantiate(bullet, gun.transform.position, Quaternion.identity);
            nowMagazine -= 1;
        }
    }

    public override void OnTriggerEnter(Collider collision)
    {
        base.OnTriggerEnter(collision);
    }
}