using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.Audio;

public class TitleScene : MonoBehaviourPunCallbacks
{
    LoadSceneManager sm;
    GameManager gm;

    [SerializeField]
    Button btnInternetDisconnected;
    [SerializeField]
    AudioMixer audioMixer;

    void Start()
    {
        sm = Managers.LoadScene;
        gm = Managers.Game;

        gm.Init();
    }

    public void CheckGameStarting()
    {
        /*
         * ���ͳ� ����, ���� ���� ���� üũ�ϰ� ���θ޴��� �Ѿ
         */

        sm.CreateLoadingUI(1);

        // ���ͳ� ���� Ȯ��
        if(Application.internetReachability == NetworkReachability.NotReachable)
        {
            Debug.Log("���ͳ� ���� ����");
            btnInternetDisconnected.gameObject.SetActive(true);
        }
        else
        {
            Debug.Log("���ͳ� ���� Ȯ��");
        }

        // ���漭�� ����
        PhotonNetwork.ConnectUsingSettings();
    }

    override public void OnConnectedToMaster()
    {
        Debug.Log("���� ���� Ȯ��");
        sm.LoadScene("MainMenuScene", 1);
    }

    public void RestartGame()
    {
        PhotonNetwork.LoadLevel("TitleScene");
    }
}
