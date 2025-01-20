using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkEnemy : EnemyBase
{

    // Start is called before the first frame update
    void Start()
    {
        GetComponent();
        base.StartEnemy();
        //ステータスを設定
        hp = EnemyList.WalkEnemy.hp;      //体力
        speed = EnemyList.WalkEnemy.speed;//移動速度
        //
        Direction();
        //
        nowAnimationNumber = (int)HumanoidAnimation.Walk;
        nowAnimationName = HumanoidAnimation.Walk.ToString();
        AnimationPlay();                                     //関数"AnimationPlay"を実行する
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