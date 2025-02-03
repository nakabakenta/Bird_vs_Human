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
        enemyOption = Enemy.EnemyOption.Boss.ToString();//
        playerFind = true;
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

    public override void DeathEnemy()
    {
        bossEnemy = false;
        base.DeathEnemy();
    }

    //当たり判定(OnTriggerEnter)
    public override void OnTriggerEnter(Collider collision)
    {
        base.OnTriggerEnter(collision);
    }
}