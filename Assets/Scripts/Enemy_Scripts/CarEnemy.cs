using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarEnemy : EnemyBase
{
    //処理
    private bool carExit = false;//
    //このオブジェクトのコンポーネント
    public GameObject enemy;     //"GameObject(敵)"
    public AudioClip brake;      //"AudioClip(ブレーキ)"
    public AudioClip horn;       //"AudioClip(クラクション)"

    // Start is called before the first frame update
    void Start()
    {
        enemyType = Enemy.EnemyType.Vehicle.ToString();//敵の型
        carExit = false;
        //関数を実行する
        GetComponent();//コンポーネントを所得する
        Direction();
    }                                   

    // Update is called once per frame
    void Update()
    {
        BaseUpdate();
    }

    public override void BaseUpdate()
    {
        base.BaseUpdate();

        if (direction.y == (int)Characte.Direction.Vertical)
        {
            if (this.thisTransform.position.x < playerTransform.position.x + 5.0f)
            {
                action = true;
            }
        }
        else if (direction.y == -(int)Characte.Direction.Horizontal)
        {
            if (viewPortPosition.x < moveRange[0].range[1].x)
            {
                action = true;
            }
        }

        if (viewPortPosition.x < moveRange[0].range[0].x)
        {
            Destroy();//関数"Destroy"を実行する
        }
    }

    public override void Action()
    {
        if(direction.y == (int)Characte.Direction.Vertical)
        {
            if (this.transform.position.z <= playerTransform.position.z && carExit == false)
            {
                Instantiate(enemy, this.transform.position, this.transform.rotation);
                audioSource.PlayOneShot(horn);
                audioSource.PlayOneShot(brake);
                carExit = true;
            }
            else if (this.transform.position.z > playerTransform.position.z)
            {
                Move();
            }
        }
        else if(direction.y == -(int)Characte.Direction.Horizontal)
        {
            Move();
        }
    }

    //関数"Death"
    public override void DeathEnemy()
    {
        base.DeathEnemy();

        Instantiate(effect, this.transform.position, this.transform.rotation, thisTransform);
    }
}
