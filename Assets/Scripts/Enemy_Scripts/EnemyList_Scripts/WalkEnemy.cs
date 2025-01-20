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
        hp = EnemyList.WalkEnemy.hp;                   //体力
        speed = EnemyList.WalkEnemy.speed;             //移動速度
        //
        Direction();
        //
        nowAnimationNumber = (int)HumanoidAnimation.Walk;
        nowAnimationName = HumanoidAnimation.Walk.ToString();
        AnimationPlay();//関数"AnimationPlay"を実行する
    }

    // Update is called once per frame
    void Update()
    {
        base.UpdateEnemy();

        Debug.Log(isAnimation);
        Debug.Log(nowAnimationNumber);

        if (isAction == true)
        {
            Direction();
        }
        else if (isAction == false)
        {
            if (viewPortPosition.x < 1)
            {
                isAction = true;
            }
        }

        //"hp > 0 && isAction == true"
        if (hp > 0 && isAction == true)
        {
            nowDirection();
        }
    }

    //関数"Direction"
    void Direction()
    {
        //
        if (this.transform.position.z > playerTransform.position.z + 0.5f)
        {
            nowDirection = Vertical;//
        }
        //
        if (this.transform.position.z >= playerTransform.position.z - 0.5f &&
            this.transform.position.z <= playerTransform.position.z + 0.5f)
        {
            nowDirection = Horizontal;//
        }
    }

    //関数"Vertical"
    void Vertical()
    {
        //
        if (isAnimation == true)
        {
            Wait();//関数"Wait"を実行
        }
        else if(isAnimation == false)
        {
            //
            if (PlayerController.hp > 0)
            {
                this.transform.position += speed * transform.forward * Time.deltaTime;                                                 //前方向に移動する
                this.transform.eulerAngles = new Vector3(this.transform.rotation.x, -EnemyList.rotation * 2, this.transform.rotation.z);
            }
            else if (PlayerController.hp <= 0)
            {
                nowAnimationNumber = (int)HumanoidAnimation.Dance;
                nowAnimationName = HumanoidAnimation.Dance.ToString();
                AnimationPlay();//関数"AnimationPlay"を実行する
            }
        }

        this.transform.position = new Vector3(this.transform.position.x, 0.0f, this.transform.position.z);
    }

    //関数"Horizontal"
    void Horizontal()
    {
        //
        if (isAnimation == true)
        {
            Wait();//関数"Wait"を実行
        }
        else if (isAnimation == false)
        {
            //
            if (PlayerController.hp > 0)
            {
                this.transform.position += speed * transform.forward * Time.deltaTime;                                              //前方向に移動する
                this.transform.eulerAngles = new Vector3(this.transform.rotation.x, -EnemyList.rotation, this.transform.rotation.z);
            }
            else if (PlayerController.hp <= 0)
            {
                nowAnimationNumber = (int)HumanoidAnimation.Dance;
                nowAnimationName = HumanoidAnimation.Dance.ToString();
                AnimationPlay();//関数"AnimationPlay"を実行する
            }
        }

        this.transform.position = new Vector3(this.transform.position.x, 0.0f, playerTransform.position.z);
    }

    public override void OnTriggerEnter(Collider collision)
    {
        base.OnTriggerEnter(collision);
    }
}