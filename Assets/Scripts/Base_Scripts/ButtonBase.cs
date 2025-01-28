using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonBase : UIBase
{
    //���̃I�u�W�F�N�g�̃R���|�[�l���g
    public GameObject alpha;           //"GameObject(������)"
    public GameObject selectMark;      //"GameObject(�I���}�[�N)"
    public AudioClip enter;            //"AudioClip(����)"
    public AudioClip click;            //"AudioClip(�N���b�N)"
    public Button button;              //"Button" 
    public RectTransform rectTransform;//"RectTransform"
    public AudioSource audioSource;    //"AudioSource"
    //����
    public Vector2 buttonPosition;     //�{�^���̈ʒu
    public bool setActive;             //�I�u�W�F�N�g�\���̉�

    public void GetComponent()
    {
        //���̃I�u�W�F�N�g�̃R���|�[�l���g���擾����
        button = this.GetComponent<Button>();
        rectTransform = this.GetComponent<RectTransform>();
        audioSource = this.GetComponent<AudioSource>();
        //���̃I�u�W�F�N�g�̃R���|�[�l���g��������
        selectMark.SetActive(false);
        //������������
        buttonPosition = rectTransform.anchoredPosition;
    }

    public void ResetButton()
    {

    }

    public void EnterButton()
    {
        rectTransform.anchoredPosition = new Vector2(buttonPosition.x + 150, rectTransform.anchoredPosition.y);
        alpha.SetActive(false);
        selectMark.SetActive(true);
        audioSource.PlayOneShot(enter);//"����"��炷
    }

    public void ClickButton()
    {
        audioSource.PlayOneShot(click);       //"�N���b�N"��炷

        if(Stage.status == "Pause")
        {
            Stage.status = null;
            LoadScene();
        }

        InvokeRepeating("Flash", 0.0f, 0.25f);//�֐�"Flash"��"0.0f"��Ɏ��s�A"0.25f"���ɌJ��Ԃ�
        Invoke("LoadScene", 2.0f);            //�֐�"LoadScene"��"2.0f"��Ɏ��s
    }

    //�֐�"Flash"
    public void Flash()
    {
        //"setActive"��"true"�̏ꍇ��"false"�A"false"�̏ꍇ��"true"�ɂ���
        setActive = !setActive;

        //�q�I�u�W�F�N�g���擾
        foreach (Transform child in transform)
        {
            //�q�I�u�W�F�N�g�̖��O��"Alpha"�̏ꍇ
            if (child.name == "Alpha_UI_Base_64_03")
            {
                //�q�I�u�W�F�N�g���\���ɂ���
                child.gameObject.SetActive(false);
            }
            //����ȊO�̏ꍇ
            else
            {
                //�q�I�u�W�F�N�g��"setActive"�ɂ���
                child.gameObject.SetActive(setActive);
            }
        }
    }
}
