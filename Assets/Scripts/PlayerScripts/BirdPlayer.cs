using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdPlayer : MonoBehaviour
{
    //�R���|�[�l���g�擾�p
    private Transform playerTransform;//Transform(�v���C���[)

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.Find("Player").transform;//"Player"��Transform���擾
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = playerTransform.position;
    }
}
