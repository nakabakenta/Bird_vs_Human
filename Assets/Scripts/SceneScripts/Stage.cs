using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    public static int stage;
    public static bool[] BossEnemy = new bool[5];//�{�X

    private SceneLoader sceneLoader;//

    // Start is called before the first frame update
    void Start()
    {
        BossEnemy[0] = true;

        sceneLoader = GetComponent<SceneLoader>();//Script"SceneLoader"���擾����
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerController.hp <= 0)
        {
            sceneLoader.GameOver();
        }
    }
}
