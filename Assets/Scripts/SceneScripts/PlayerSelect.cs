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
        GameManager.hp = 6;
        GameManager.power = 6;
        GameManager.speed = 8;
        GameManager.playerSelect = "Sparrow";
        sceneLoader.StageSelect();
    }

    //
    public void Crow()
    {
        GameManager.hp = 8;
        GameManager.power = 10;
        GameManager.speed = 5;
        GameManager.playerSelect = "Crow";
        sceneLoader.StageSelect();
    }

    //
    public void Chickadee()
    {
        GameManager.hp = 6;
        GameManager.power = 5;
        GameManager.speed = 10;
        GameManager.playerSelect = "Chickadee";
        sceneLoader.StageSelect();
    }

    //
    public void Penguin()
    {
        GameManager.hp = 10;
        GameManager.power = 10;
        GameManager.speed = 10;
        GameManager.playerSelect = "Penguin";
        sceneLoader.StageSelect();
    }
}
