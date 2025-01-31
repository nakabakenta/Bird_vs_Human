using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchEnemy : EnemyBase
{
    // Start is called before the first frame update
    void Start()
    {
        //ステータスを設定する
        enemyType = Enemy.EnemyType.Human.ToString();   //敵の型
        enemyOption = Enemy.EnemyOption.Wait.ToString();//
        hp = EnemyList.CrouchEnemy.hp;                  //体力
        //初期のアニメーションを設定する
        defaultAnimationNumber = (int)Enemy.HumanoidAnimation.Crouch;
        playerFind = false;

        //関数を実行する
        GetComponent();  //コンポーネントを所得
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
