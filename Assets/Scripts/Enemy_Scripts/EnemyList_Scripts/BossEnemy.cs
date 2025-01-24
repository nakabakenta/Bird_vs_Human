using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : EnemyBase
{
    // Start is called before the first frame update
    void Start()
    {
        //ステータスを設定する
        enemyType = EnemyType.Boss.ToString();           //敵の型
        hp = EnemyList.BossEnemy.hp[Stage.nowStage - 1];      
        speed = EnemyList.BossEnemy.speed[Stage.nowStage - 1];
        jump = EnemyList.BossEnemy.jump[Stage.nowStage - 1];

        isPlayerFind = true;

        //初期のアニメーションを設定する
        defaultAnimationNumber = (int)HumanoidAnimation.Walk;
        //関数を実行する
        GetComponent();//コンポーネントを所得
        StartEnemy();  //敵の設定をする
    }

    // Update is called once per frame
    void Update()
    {
        base.UpdateEnemy();
    }

    //当たり判定(OnTriggerEnter)
    public override void OnTriggerEnter(Collider collision)
    {
        base.OnTriggerEnter(collision);
    }
}