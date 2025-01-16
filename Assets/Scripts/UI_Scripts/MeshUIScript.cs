using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshUIScript : MonoBehaviour
{
    //�N���X
    public RotateUIClass rotateUIClass;//��]UI�N���X

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //��]UI�N���X��"�g�p����"�̏ꍇ
        if (rotateUIClass.use == true)
        {
            RotateUI();//�֐�"RotateUI"�����s
        }
    }

    //�֐�"RotateUI"
    void RotateUI()
    {
        //���̃I�u�W�F�N�g�𖈕b��]������
        this.gameObject.transform.Rotate(new Vector3(rotateUIClass.speed.x, rotateUIClass.speed.y, rotateUIClass.speed.z) * Time.deltaTime);
    }

    //��]UI�N���X
    [System.Serializable]
    public class RotateUIClass
    {
        //����
        public bool use;     //�g�p�̉�
        public Vector3 speed;//��]���x
    }
}
