using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIComponent : MonoBehaviour
{
    //����
    public FlashUIClass flashUIClass;//�_��UI�N���X
    public TitleUIClass titleUIClass;//�^�C�g��UI�N���X
    //���̃I�u�W�F�N�g�̃R���|�[�l���g
    public Image image;                //"Image"
    public RectTransform rectTransform;//"RectTransform"

    // Start is called before the first frame update
    void Start()
    {
        //���̃I�u�W�F�N�g�̃R���|�[�l���g���擾

        //�_��UI�g�p�̉ۂ�"true"�̏ꍇ
        if (flashUIClass.use == true)
        {
            StartCoroutine("Flash");
        }
    }

    // Update is called once per frame
    void Update()
    {
        

        //�^�C�g��UI�g�p�̉ۂ�"true"�̏ꍇ
        if (titleUIClass.use == true)
        {
            TitleUI();//�֐�"TitleUI"�����s
        }
    }

    IEnumerator Flash()
    {
        while (true)
        {
            for (int i = 0; i < 25; i++)
            {
                image.color = image.color - new Color32(0, 0, 0, 10);
                yield return new WaitForSeconds(flashUIClass.interval);
            }

            for (int k = 0; k < 25; k++)
            {
                image.color = image.color + new Color32(0, 0, 0, 10);
                yield return new WaitForSeconds(flashUIClass.interval);
            }
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

    //�_��UI�N���X
    [System.Serializable]
    public class FlashUIClass
    {
        //����
        public bool use;              //�g�p�̉�
        public float interval = 0.05f;//�Ԋu
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
