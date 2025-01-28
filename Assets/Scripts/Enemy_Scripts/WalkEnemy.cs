using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkEnemy : EnemyBase
{
    // Start is called before the first frame update
    void Start()
    {
        //ステータスを設定する
        enemyType = EnemyType.Normal.ToString();//敵の型
        hp = EnemyList.WalkEnemy.hp;            //体力
        speed = EnemyList.WalkEnemy.speed;      //移動速度

        playerFind = false;

        //初期のアニメーション番号を設定する
        defaultAnimationNumber = (int)HumanoidAnimation.Walk;
        //関数を実行する
        GetComponent();//コンポーネントを所得する
        StartEnemy();  //敵の設定をする
    }

    // Update is called once per frame
    void Update()
    {
        base.UpdateEnemy();
    }

    public override void OnTriggerEnter(Collider collision)
    {
        base.OnTriggerEnter(collision);
    }
}