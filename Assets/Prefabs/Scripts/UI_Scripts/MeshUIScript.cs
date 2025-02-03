using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshUIScript : MonoBehaviour
{
    //クラス
    public RotateUIClass rotateUIClass;//回転UIクラス

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //回転UIクラスが"使用する"の場合
        if (rotateUIClass.use == true)
        {
            RotateUI();//関数"RotateUI"を実行
        }
    }

    //関数"RotateUI"
    void RotateUI()
    {
        //このオブジェクトを毎秒回転させる
        this.gameObject.transform.Rotate(new Vector3(rotateUIClass.speed.x, rotateUIClass.speed.y, rotateUIClass.speed.z) * Time.deltaTime);
    }

    //回転UIクラス
    [System.Serializable]
    public class RotateUIClass
    {
        //処理
        public bool use;     //使用の可否
        public Vector3 speed;//回転速度
    }
}
