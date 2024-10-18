using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkEnemy : MonoBehaviour
{
    //ステータス変数
    public int hp;     //体力
    public float speed;//移動速度

    private Animator animator = null;//アニメーター

    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += speed * transform.forward * Time.deltaTime;
    }

    //画面外処理
    void OnBecameInvisible()
    {
        Destroy(this.gameObject);//消す
    }
}
