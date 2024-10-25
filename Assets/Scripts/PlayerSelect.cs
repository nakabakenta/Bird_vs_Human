using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelect : MonoBehaviour
{
    //
    public enum PlayerList
    {
        SPARROW,  //スズメ
        CROW,　　 //カラス
        CHICKADEE,//コガラ
        PENGUIN,  //ペンギン
    }

    //
    public enum PlayerStatus
    {
        NAME, //名前
        HP,   //体力
        POWER,//攻撃力
        SPEED,//移動速度
    }

    private SceneLoader sceneLoader;//

    // Start is called before the first frame update
    void Start()
    {
        sceneLoader = GetComponent<SceneLoader>();//
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Sparrow()
    {
        PlayerList type = PlayerList.SPARROW;



        sceneLoader.StageSelect();
    }
}
