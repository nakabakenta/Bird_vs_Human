using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunEnemy : EnemyBase
{
    // Start is called before the first frame update
    void Start()
    {
        //ステータスを設定する
        enemyType = EnemyType.RunEnemy.ToString();//型
        hp = EnemyList.RunEnemy.hp;               //体力
        speed = EnemyList.RunEnemy.speed;         //移動速度
        jump = EnemyList.RunEnemy.jump;           //ジャンプ力
        //初期のアニメーションを設定する
        defaultAnimationNumber = (int)HumanoidAnimation.Run;
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