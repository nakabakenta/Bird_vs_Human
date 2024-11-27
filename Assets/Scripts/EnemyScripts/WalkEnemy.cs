using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkEnemy : MonoBehaviour
{
    //ステータス
    private int hp = EnemyStatus.WalkEnemy.hp;        //体力
    private float speed = EnemyStatus.WalkEnemy.speed;//移動速度
    //処理
    private int random = 0;       //ランダム
    private float interval = 0.0f;//間隔
    private string nowAction;     //現在の動作
    private bool action = false;  //動作フラグ
    private float viewPointX;     //ビューポイント座標.X
   //このオブジェクトのコンポーネント
    private Transform setTransform;  //"Transform"
    private Animator animator = null;//"Animator"

    // Start is called before the first frame update
    void Start()
    {
        setTransform = this.gameObject.GetComponent<Transform>();//このオブジェクトのTransformを取得
        animator = this.GetComponent<Animator>();                //このオブジェクトのAnimatorを取得
        animator.SetInteger("Motion", 0);                        //Animatorの"Motion, 0"(歩く)を有効にする
        nowAction = "Run";
    }

    // Update is called once per frame
    void Update()
    {
        //ビューポート座標を取得
        viewPointX = Camera.main.WorldToViewportPoint(this.transform.position).x;//画面座標.X

        //体力が0より上 && ビューポート座標.Xが1より上であれば
        if (hp > 0 && viewPointX < 1)
        {
            Behavior();//行動関数を呼び出す
        }
        //体力が0以下 && ビューポート座標.Xが0未満であれば
        else if (hp <= 0 && viewPointX < 0)
        {
            Destroy(this.gameObject);//このオブジェクトを消す
        }
    }


    void Behavior()
    {
        Vector3 localPosition = setTransform.localPosition;//オブジェクトの
        Vector3 localAngle = setTransform.localEulerAngles;//
        localPosition.y = 0.0f;//
        localPosition.z = 1.0f;//
        localAngle.y = -EnemyStatus.rotationY;//
        setTransform.localPosition = localPosition;       //ローカル座標での座標を設定
        setTransform.localEulerAngles = localAngle;       //

        //
        if (nowAction == "Run")
        {
            this.transform.position += speed * transform.forward * Time.deltaTime;//前方向に移動する
        }
        //
        else if (nowAction != "Run" && action == true)
        {
            Wait();
        }

        //
        if (PlayerController.hp <= 0 && action == false)
        {
            Animation();//アニメーション関数を実行
        }
    }

    //アニメーション関数
    void Animation()
    {
        if (PlayerController.hp > 0 && nowAction == "Attack")
        {
            random = (int)Random.Range(10, 12);     //ランダム処理(1～2)
            animator.SetInteger("Motion", random);//AnimatorのAttackMotion(1～2)を有効にする
            Debug.Log(random);                    //Debug.Log(random)
        }
        else if (PlayerController.hp <= 0)
        {
            nowAction = "Dance";
            animator.SetInteger("Motion", 30);//"Animator"の"Motion, 3"(ダンスモーション)を有効にする
        }
    }

    //待機関数
    void Wait()
    {
        interval += Time.deltaTime;//クールタイムにTime.deltaTimeを足す

        if (nowAction == "Attack")
        {
            //
            if (random == 10)
            {
                //
                if (interval >= 2.0f)
                {
                    interval = 0.0f;                      //
                    animator.SetInteger("Motion", random);//
                    action = false;
                    nowAction = "Run";
                }
            }
            //
            else if (random == 11)
            {
                //
                if (interval >= 1.5f)
                {
                    interval = 0.0f;
                    animator.SetInteger("Motion", random);//
                    action = false;
                    nowAction = "Run";
                }
            }
        }
    }

    //ダメージ判定関数
    void Damage()
    {
        hp -= 1;//体力を"-1"する

        //体力が0以下だったら
        if (hp <= 0)
        {
            Death();//関数"Death"死亡を呼び出す
        }
    }

    //死亡関数
    void Death()
    {
        hp = 0;                                          //体力を"0"にする
        GameManager.score += EnemyStatus.WalkEnemy.score;//
        this.tag = "Death";                              //タグを"Death"に変更する
        animator.SetInteger("Motion", 31);               //
    }

    //当たり判定(OnTriggerEnter)
    void OnTriggerEnter(Collider collision)
    {
        //タグPlayerの付いたオブジェクトに衝突したら
        if (collision.gameObject.tag == "Player" && action == false)
        {
            action = true;
            nowAction = "Attack";
            Animation();
        }
        //タグBulletの付いたオブジェクトに衝突したら
        if (collision.gameObject.tag == "Bullet" && this.tag != "Death")
        {
            Damage();//関数Damageを呼び出す
        }
    }
}
