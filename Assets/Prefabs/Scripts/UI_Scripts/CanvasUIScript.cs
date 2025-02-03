using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasUIScript : MonoBehaviour
{
    //�N���X
    public FlashUIClass flashUIClass;//�_��UI�N���X
    public TitleUIClass titleUIClass;//�^�C�g��UI�N���X
    //���̃I�u�W�F�N�g�̃R���|�[�l���g
    private Image[] image;                //"Image"
    private RectTransform[] rectTransform;//"RectTransform"

    // Start is called before the first frame update
    void Start()
    {
        //���̃I�u�W�F�N�g�̃R���|�[�l���g���擾
        image = this.gameObject.GetComponentsInChildren<Image>();
        rectTransform = this.gameObject.GetComponentsInChildren<RectTransform>();

        //�_��UI�g�p�̉ۂ�"true"�̏ꍇ
        if (flashUIClass.use == true)
        {
            //�R���[�`��"FlashUI"�����s����
            StartCoroutine("FlashUI");
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

    //�R���[�`��"FlashUI"
    IEnumerator FlashUI()
    {
        while (true)
        {
            for (int down = 0; down < 25; down++)
            {
                for (int count = 0; count < image.Length; count++)
                {
                    image[count].color = image[count].color - new Color32(0, 0, 0, 10);
                }

                yield return new WaitForSeconds(flashUIClass.interval);
            }

            for (int up = 0; up < 25; up++)
            {
                for (int count = 0; count < image.Length; count++)
                {
                    image[count].color = image[count].color + new Color32(0, 0, 0, 10);
                }

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
            for (int count = 0; count < image.Length; count++)
            {
                //
                if (rectTransform[count].anchoredPosition.x >= titleUIClass.limitPosition.x)
                {
                    rectTransform[count].anchoredPosition = new Vector2(-titleUIClass.limitPosition.x, rectTransform[count].anchoredPosition.y);
                }
                else
                {
                    rectTransform[count].anchoredPosition = new Vector2(rectTransform[count].anchoredPosition.x + titleUIClass.speed * Time.deltaTime, rectTransform[count].anchoredPosition.y);
                }
            }
        }
        //�^�C�g��UI�ړ�����(�E����[1])�̉ۂ�"true"�̏ꍇ
        else if (titleUIClass.direction[1] == true)
        {
            for (int count = 0; count < image.Length; count++)
            {
                //
                if (rectTransform[count].anchoredPosition.x <= -titleUIClass.limitPosition.x)
                {
                    rectTransform[count].anchoredPosition = new Vector2(titleUIClass.limitPosition.x, rectTransform[count].anchoredPosition.y);
                }
                else
                {
                    rectTransform[count].anchoredPosition = new Vector2(rectTransform[count].anchoredPosition.x - titleUIClass.speed * Time.deltaTime, rectTransform[count].anchoredPosition.y);
                }
            }
        }
    }

    //�_��UI�N���X
    [System.Serializable]
    public class FlashUIClass
    {
        //����
        public bool use;      //�g�p�̉�
        public float interval;//�Ԋu
    }
    
    //�^�C�g��UI�N���X
    [System.Serializable]
    public class TitleUIClass
    {
        //����
        public bool use;                      //�g�p�̉�
        public bool[] direction = new bool[2];//�ړ�����(������[0],�E����[1]�̉�
        public float speed;                   //�ړ����x
        public Vector2 limitPosition;         //�ړ����E�ʒu
    }
}
