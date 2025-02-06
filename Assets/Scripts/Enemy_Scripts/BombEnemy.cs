using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombEnemy : EnemyBase
{
    // Start is called before the first frame update
    void Start()
    {
        //ステータスを設定する
        enemyType = Enemy.EnemyType.Human.ToString();//敵の型
        //初期のアニメーションを設定する
        defaultAnimationNumber = (int)Enemy.HumanoidAnimation.Run;
        //関数を実行する
        GetComponent();//コンポーネントを所得する
        BaseStart();   //関数"BaseStart"を実行する
    }

    // Update is called once per frame
    void Update()
    {
        BaseUpdate();
    }

    public override void BaseUpdate()
    {
        base.BaseUpdate();

        if (viewPortPosition.x < moveRange[0].range[1].x)
        {
            action = true;
        }

        if (viewPortPosition.x < moveRange[0].range[0].x && hp <= 0)
        {
            Destroy();//関数"Destroy"を実行する
        }
    }

    public override void Action()
    {
        if (isAnimation == true)
        {
            AnimationFind();//関数"AnimationFind"を実行する
        }
        //
        else if (isAnimation == false)
        {
            //
            if (PlayerBase.status != "Death")
            {
                Move();

                if (viewPortPosition.x > moveRange[0].range[0].x && viewPortPosition.x < moveRange[0].range[1].x)
                {
                    bulletPutTimer += Time.deltaTime;

                    if (bulletPutTimer >= bulletPutInterval)
                    {
                        Instantiate(bulletPut, this.transform.position, Quaternion.identity);
                        bulletPutTimer = 0.0f;
                    }
                }
                else if (viewPortPosition.x < moveRange[0].range[0].x || viewPortPosition.x > moveRange[0].range[1].x)
                {
                    CoarsePlayerDirection();
                }
            }
            else if (PlayerBase.status == "Death")
            {
                nowAnimationNumber = (int)Enemy.HumanoidAnimation.Dance;
                AnimationPlay();                                        //関数"AnimationPlay"を実行する
            }
        }

        this.transform.position = new Vector3(this.transform.position.x, 0.0f, playerTransform.position.z);
    }
}
