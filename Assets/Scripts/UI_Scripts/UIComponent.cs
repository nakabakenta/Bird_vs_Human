using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIComponent : MonoBehaviour
{
    //����
    public TitleUIClass titleUIClass;   //�^�C�g��UI�N���X
    //���̃I�u�W�F�N�g�̃R���|�[�l���g
    private RectTransform rectTransform;//"RectTransform"

    // Start is called before the first frame update
    void Start()
    {
        //���̃I�u�W�F�N�g�̃R���|�[�l���g���擾
        rectTransform = this.gameObject.GetComponent<RectTransform>();//"RectTransform"
    }

    // Update is called once per frame
    void Update()
    {
        //�^�C�g��UI�g�p�̉ۂ�"true"�̏ꍇ
        if(titleUIClass.use == true)
        {
            TitleUI();//�֐�"TitleUI"�����s
        }
    }

    //�֐�"TitleUI"
    void TitleUI()
    {
        //�^�C�g��UI�ړ�����(������[0])�̉ۂ�"true"�̏ꍇ
        if (titleUIClass.direction[0] == true)
        {
            //
            if (rectTransform.anchoredPosition.x >= titleUIClass.limitPosition.x)
            {
                rectTransform.anchoredPosition = new Vector2(-titleUIClass.limitPosition.x, rectTransform.anchoredPosition.y);
            }
            else
            {
                rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x + titleUIClass.speed * Time.deltaTime, rectTransform.anchoredPosition.y);
            }
        }
        //�^�C�g��UI�ړ�����(�E����[1])�̉ۂ�"true"�̏ꍇ
        else if (titleUIClass.direction[1] == true)
        {
            //
            if (rectTransform.anchoredPosition.x <= -titleUIClass.limitPosition.x)
            {
                rectTransform.anchoredPosition = new Vector2(titleUIClass.limitPosition.x, rectTransform.anchoredPosition.y);
            }
            else
            {
                rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x - titleUIClass.speed * Time.deltaTime, rectTransform.anchoredPosition.y);
            }
        }
    }

    //�^�C�g��UI�N���X
    [System.Serializable]
    public class TitleUIClass
    {
        //����
        public bool use;                                          //�g�p�̉�
        public bool[] direction = new bool[2];                    //�ړ�����(������[0],�E����[1]�̉�
        public float speed = 100.0f;                              //�ړ����x
        public Vector2 limitPosition = new Vector2(1176.0f, 0.0f);//�ړ��̌��E�l
    }
}
