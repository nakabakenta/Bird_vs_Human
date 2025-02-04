using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkEnemy : EnemyBase
{
    // Start is called before the first frame update
    void Start()
    {
        //ステータスを設定する
        enemyType = Enemy.EnemyType.Human.ToString();     //敵の型
        enemyOption = Enemy.EnemyOption.Normal.ToString();//
        hp = EnemyList.WalkEnemy.hp;                      //体力
        moveSpeed = EnemyList.WalkEnemy.speed;            //移動速度
        //初期のアニメーション番号を設定する
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
}