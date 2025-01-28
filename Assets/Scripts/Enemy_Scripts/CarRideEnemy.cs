using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarRideEnemy : EnemyBase
{
    // Start is called before the first frame update
    void Start()
    {
        //ステータスを設定する
        enemyType = EnemyType.Find.ToString();//敵の型
        hp = EnemyList.CarRideEnemy.hp;       //体力
        speed = EnemyList.CarRideEnemy.speed; //移動速度
        jump = EnemyList.CarRideEnemy.jump;   //ジャンプ力
        //処理を初期化する
        playerFind = true;
        isAnimation = true;
        //初期のアニメーションを設定する
        defaultAnimationNumber = (int)HumanoidAnimation.ExitCar;
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