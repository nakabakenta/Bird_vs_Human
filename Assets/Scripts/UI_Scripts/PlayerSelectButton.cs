using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerSelectButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Button[] button = new Button[3];
    public AudioClip cursor;               //"AudioClip(�J�[�\��)"
    private AudioSource audioSource;       //"AudioSource"
    private SceneLoader sceneLoader;       //"Script(SceneLoader)"

    // Start is called before the first frame update
    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();//"AudioSource"
        sceneLoader = this.GetComponent<SceneLoader>();//����"Script(SceneLoader)"���擾����

        button[0].onClick.AddListener(Sparrow);
        button[1].onClick.AddListener(Crow);
        button[2].onClick.AddListener(Chickadee);

        GameManager.gameStart = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerPress == button[0])
        {
            audioSource.PlayOneShot(cursor);
            Debug.Log("aaa");
        }

        if (eventData.pointerPress == button[1])
        {
            audioSource.PlayOneShot(cursor);
            Debug.Log("bbb");
        }

        if (eventData.pointerPress == button[2])
        {
            audioSource.PlayOneShot(cursor);
            Debug.Log("ccc");
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("�J�[�\�����{�^�����痣��܂����I");
    }

    //�v���C���[�Z���N�g�ꗗ
    //�X�Y��
    public void Sparrow()
    {
        GameManager.playerNumber = PlayerList.Player.number[0];//"GameManager"��"playerNumber"��"�X�Y��(0)"�ɂ���
        sceneLoader.StageSelect();                           //"Script(SceneLoader)"��"�֐�(StageSelect)"�����s����

         Debug.Log("1");
    }
    //�J���X
    public void Crow()
    {
        GameManager.playerNumber = PlayerList.Player.number[1];//"GameManager"��"playerNumber"��"�J���X(1)"�ɂ���
        sceneLoader.StageSelect();                             //"Script(SceneLoader)"��"�֐�(StageSelect)"�����s����

        Debug.Log("2");
    }
    //�R�K��
    public void Chickadee()
    {
        GameManager.playerNumber = PlayerList.Player.number[2];//"GameManager"��"playerNumber"��"�R�K��(2)"�ɂ���
        sceneLoader.StageSelect();                             //"Script(SceneLoader)"��"�֐�(StageSelect)"�����s����
        Debug.Log("3");
    }
    //�y���M��
    public void Penguin()
    {
        GameManager.playerNumber = PlayerList.Player.number[3];//"GameManager"��"playerNumber"��"�y���M��(3)"�ɂ���
        sceneLoader.StageSelect();                             //"Script(SceneLoader)"��"�֐�(StageSelect)"�����s����
    }
}
