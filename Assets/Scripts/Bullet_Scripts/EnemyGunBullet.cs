using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGunBullet : BulletBase
{
    //処理
    private Vector3 direction;//オブジェクトの方向
    //他のオブジェクトのコンポーネント
    private Transform playerTransform;//"Transform(プレイヤー)"

    // Start is called before the first frame update
    void Start()
    {
        //他のオブジェクトのコンポーネントを取得
        playerTransform = GameObject.Find("Player").transform;//"Transform(プレイヤー)"

        direction = (playerTransform.position - this.transform.position).normalized;
        this.transform.rotation = Quaternion.LookRotation(direction);
    }

    // Update is called once per frame
    void Update()
    {
        BaseUpdate();

        this.transform.position += moveSpeed * transform.forward * Time.deltaTime;//前方向に移動する
    }

    //衝突判定(OnTriggerEnter)
    public override void OnTriggerEnter(Collider collision)
    {
        base.OnTriggerEnter(collision);
    }
}
