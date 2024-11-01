using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelect : MonoBehaviour
{
    private SceneLoader sceneLoader;//SceneLoader

    // Start is called before the first frame update
    void Start()
    {
        sceneLoader = GetComponent<SceneLoader>();//Script"SceneLoader"を取得する
    }

    //スズメ
    public void Sparrow()
    {
        GameManager.playerSelect = PlayerStatus.Sparrow.name;
        sceneLoader.StageSelect();
    }
    //カラス
    public void Crow()
    {
        GameManager.playerSelect = PlayerStatus.Crow.name;
        sceneLoader.StageSelect();
    }
    //コガラ
    public void Chickadee()
    {
        GameManager.playerSelect = PlayerStatus.Chickadee.name;
        sceneLoader.StageSelect();
    }
    //ペンギン
    public void Penguin()
    {
        GameManager.playerSelect = PlayerStatus.Penguin.name;
        sceneLoader.StageSelect();
    }
}
