using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunEnemy : EnemyBase
{
    // Start is called before the first frame update
    void Start()
    {
        //ステータスを設定する
        enemyType = Enemy.EnemyType.Human.ToString();   //敵の型
        enemyOption = Enemy.EnemyOption.Find.ToString();//
        hp = EnemyList.RunEnemy.hp;                     //体力
        speed = EnemyList.RunEnemy.speed;               //移動速度
        jump = EnemyList.RunEnemy.jump;                 //ジャンプ力
        //処理を初期化する
        playerFind = true;
        //初期のアニメーションを設定する
        defaultAnimationNumber = (int)Enemy.HumanoidAnimation.Run;
        //関数を実行する
        GetComponent();  //コンポーネントを所得する
        StartAnimation();//開始時のアニメーションを設定する
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