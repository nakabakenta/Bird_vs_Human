using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour
{
    //このオブジェクトのコンポーネント
    private SceneLoader sceneLoader;//"Script(SceneLoader)"

    // Start is called before the first frame update
    void Start()
    {
        sceneLoader = this.GetComponent<SceneLoader>();//この"Script(SceneLoader)"を取得する
    }

    // Update is called once per frame
    void Update()
    {
        //マウスを(左 || 右)クリックをしたら
        if(Input.GetMouseButton(0) || Input.GetMouseButton(1))
        {
            sceneLoader.PlayerSelect();//"Script(SceneLoader)"の"関数(PlayerSelect)"を実行する
        }
    }
}
