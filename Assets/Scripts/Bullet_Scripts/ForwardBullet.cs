using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardBullet : BulletBase
{
    // Update is called once per frame
    void Update()
    {
        BaseUpdate();

        this.transform.position += moveSpeed * transform.right * Time.deltaTime;//
    }
}
