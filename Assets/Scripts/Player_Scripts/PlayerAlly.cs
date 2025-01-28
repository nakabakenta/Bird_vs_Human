using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAlly : CharacteBase
{
    //オブジェクト
    public GameObject[] ally = new GameObject[3];//仲間オブジェクト

    // Start is called before the first frame update
    void Start()
    {
        GetComponent();
        Instantiate(ally[GameManager.selectPlayer], this.transform.position, this.transform.rotation, thisTransform);
    }

    // Update is called once per frame
    void Update()
    {
        //このオブジェクトのワールド座標をビューポート座標に変換して取得する
        viewPortPosition.x = Camera.main.WorldToViewportPoint(this.transform.position).x;

        if (viewPortPosition.x < 0)
        {
            Destroy();//関数"Destroy"を実行する
        }
    }

    //衝突判定(OnTriggerEnter)
    void OnTriggerEnter(Collider collision)
    {
        //衝突したオブジェクトのタグが"Player"の場合
        if (collision.gameObject.tag == "Player" && PlayerBase.ally < 2)
        {
            Destroy();//関数"Destroy"を実行する
        }
    }
}