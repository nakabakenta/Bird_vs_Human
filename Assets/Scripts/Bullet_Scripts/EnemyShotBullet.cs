using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShotBullet : BulletBase
{
    // Start is called before the first frame update
    void Start()
    {
        BaseStart();
    }

    // Update is called once per frame
    void Update()
    {
        BaseUpdate();

        this.transform.position += moveSpeed * transform.forward * Time.deltaTime;//前方向に移動する
    }
}
