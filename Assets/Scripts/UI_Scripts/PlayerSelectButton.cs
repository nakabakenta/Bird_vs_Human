using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerSelectButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    //����
    public static bool buttonSelect;//�{�^������������
    //���̃I�u�W�F�N�g�̃R���|�[�l���g
    public AudioClip cursor;        //"AudioClip(�J�[�\��)"
    public AudioClip select;        //"AudioClip(�I��)"
    private Button button;          //"Button" 
    private AudioSource audioSource;//"AudioSource"
    private SceneLoader sceneLoader;//"Script(SceneLoader)"

    // Start is called before the first frame update
    void Start()
    {
        //������������
        buttonSelect = false;                          //�{�^����"�����Ă��Ȃ�"�ɂ���
        //���̃I�u�W�F�N�g�̃R���|�[�l���g���擾
        button = this.GetComponent<Button>();
        audioSource = this.GetComponent<AudioSource>();//"AudioSource"
        sceneLoader = this.GetComponent<SceneLoader>();//"Script(SceneLoader)"
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //�{�^����"�����Ă��Ȃ�"�ꍇ
        if (buttonSelect == false)
        {
            audioSource.PlayOneShot(cursor);//"select(�J�[�\��)"��炷

            //�X�Y��
            if (button.gameObject.name == "Button_Sparrow")
            {
                GameManager.playerNumber = PlayerList.Player.number[0];//"GameManager"��"playerNumber"��"�X�Y��(0)"�ɂ���
            }
            //�J���X
            else if (button.gameObject.name == "Button_Crow")
            {
                GameManager.playerNumber = PlayerList.Player.number[1];//"GameManager"��"playerNumber"��"�J���X(1)"�ɂ���
            }
            //�R�K��
            else if (button.gameObject.name == "Button_Chickadee")
            {
                GameManager.playerNumber = PlayerList.Player.number[2];//"GameManager"��"playerNumber"��"�R�K��(2)"�ɂ���
            }
            //�y���M��
            else if (button.gameObject.name == "Button_Penguin")
            {
                GameManager.playerNumber = PlayerList.Player.number[3];//"GameManager"��"playerNumber"��"�y���M��(3)"�ɂ���
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //�{�^����"�����Ă��Ȃ�"�ꍇ
        if (buttonSelect == false)
        {
            //�X�Y��
            if (button.gameObject.name == "Button_Sparrow")
            {
                GameManager.playerNumber = PlayerList.Player.number[0];//"GameManager"��"playerNumber"��"�X�Y��(0)"�ɂ���
            }
            //�J���X
            else if (button.gameObject.name == "Button_Crow")
            {
                GameManager.playerNumber = PlayerList.Player.number[1];//"GameManager"��"playerNumber"��"�J���X(1)"�ɂ���
            }
            //�R�K��
            else if (button.gameObject.name == "Button_Chickadee")
            {
                GameManager.playerNumber = PlayerList.Player.number[2];//"GameManager"��"playerNumber"��"�R�K��(2)"�ɂ���
            }
            //�y���M��
            else if (button.gameObject.name == "Button_Penguin")
            {
                GameManager.playerNumber = PlayerList.Player.number[3];//"GameManager"��"playerNumber"��"�y���M��(3)"�ɂ���
            }

            buttonSelect = true;            //�{�^����"������"�ɂ���
            audioSource.PlayOneShot(select);//"select(�I��)"��炷
            Invoke("SceneLoad", 1.515f);    //�֐�"SceneLoad"��"1.515f"��Ɏ��s����
        }
    }

    //�֐�"SceneLoad"
    void SceneLoad()
    {
        sceneLoader.StageSelect();//"SceneLoader"�̊֐�"StageSelect"�����s����
    }
}