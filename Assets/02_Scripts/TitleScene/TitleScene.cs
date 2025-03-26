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
         * 인터넷 연결, 서버 연결 등을 체크하고 메인메뉴로 넘어감
         */

        sm.CreateLoadingUI(1);

        // 인터넷 연결 확인
        if(Application.internetReachability == NetworkReachability.NotReachable)
        {
            Debug.Log("인터넷 연결 끊김");
            btnInternetDisconnected.gameObject.SetActive(true);
        }
        else
        {
            Debug.Log("인터넷 연결 확인");
        }

        // 포톤서버 연결
        PhotonNetwork.ConnectUsingSettings();
    }

    override public void OnConnectedToMaster()
    {
        Debug.Log("서버 연결 확인");
        sm.LoadScene("MainMenuScene", 1);
    }

    public void RestartGame()
    {
        PhotonNetwork.LoadLevel("TitleScene");
    }
}
