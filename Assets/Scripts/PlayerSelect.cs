using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelect : MonoBehaviour
{
    private GameManager playerStatus;
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

    //
    public void Sparrow()
    {
        playerStatus.name = "Sparrow";

        playerStatus.hp = 6;
        playerStatus.power = 6;
        playerStatus.speppd = 8;
        sceneLoader.StageSelect();
    }

    //
    public void Crow()
    {
        GameManager.playerSelect = "Crow";
        GameManager.playerHp = 8;
        GameManager.playerPower = 10;
        GameManager.playerSeppd = 5;
        sceneLoader.StageSelect();
    }

    //
    public void Chickadee()
    {
        GameManager.playerSelect = "Chickadee";
        GameManager.playerHp = 6;
        GameManager.playerPower = 5;
        GameManager.playerSeppd = 10;
        sceneLoader.StageSelect();
    }

    //
    public void Penguin()
    {
        GameManager.playerSelect = "Penguin";
        sceneLoader.StageSelect();
    }

    void SetStatus()
    {

        GameManager.playerHp = hp;
        GameManager.playerPower = power;
        GameManager.playerSeppd = seppd;
    }
}
