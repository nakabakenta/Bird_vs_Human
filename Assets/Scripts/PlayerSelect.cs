using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelect : MonoBehaviour
{
    //
    public enum PlayerList
    {
        SPARROW,  //�X�Y��
        CROW,�@�@ //�J���X
        CHICKADEE,//�R�K��
        PENGUIN,  //�y���M��
    }

    //
    public enum PlayerStatus
    {
        NAME, //���O
        HP,   //�̗�
        POWER,//�U����
        SPEED,//�ړ����x
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
