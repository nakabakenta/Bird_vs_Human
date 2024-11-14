using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdPlayer : MonoBehaviour
{
    //コンポーネント取得用
    private Transform playerTransform;//Transform(プレイヤー)

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.Find("Player").transform;//"Player"のTransformを取得
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = playerTransform.position;
    }
}
