using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaveGunEnemy : EnemyBase
{
    public int maxMagazine;
    private int nowMagazine;
    //このオブジェクトのコンポーネント
    public GameObject gun;   //"GameObject(銃)"
    public GameObject bullet;//"GameObject(弾)"

    // Start is called before the first frame update
    void Start()
    {
        //ステータスを設定
        enemyType = Enemy.EnemyType.Human.ToString();   //敵の型
        enemyOption = Enemy.EnemyOption.Wait.ToString();//
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