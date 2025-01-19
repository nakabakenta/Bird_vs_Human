using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdGroup : MonoBehaviour
{
    //コンポーネント
    private Transform playerTransform;//Transform(プレイヤー)

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.Find("Player").transform;//
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = playerTransform.position;

        if (PlayerController.status == "Normal")
        {
            Destroy(this.gameObject);//このオブジェクトを消す
        }
    }
}
