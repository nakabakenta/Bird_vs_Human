using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelect : MonoBehaviour
{
    private SceneLoader sceneLoader;//SceneLoader

    // Start is called before the first frame update
    void Start()
    {
        sceneLoader = GetComponent<SceneLoader>();//Script"SceneLoader"���擾����
    }

    //�X�Y��
    public void Sparrow()
    {
        GameManager.playerSelect = PlayerStatus.Sparrow.name;
        sceneLoader.StageSelect();
    }
    //�J���X
    public void Crow()
    {
        GameManager.playerSelect = PlayerStatus.Crow.name;
        sceneLoader.StageSelect();
    }
    //�R�K��
    public void Chickadee()
    {
        GameManager.playerSelect = PlayerStatus.Chickadee.name;
        sceneLoader.StageSelect();
    }
    //�y���M��
    public void Penguin()
    {
        GameManager.playerSelect = PlayerStatus.Penguin.name;
        sceneLoader.StageSelect();
    }
}
