using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class LoadingScene2 : MonoBehaviour
{
    LoadSceneManager sm;
    GameManager gm;

    private void Awake()
    {
        sm = Managers.LoadScene;
        gm = Managers.Game;
    }
    void Start()
    {
        PhotonNetwork.NickName = gm.PlayerName;

        // Ŀ����������Ƽ�� ĳ���� ���غ���

        PhotonNetwork.LoadLevel(sm.SceneName);
    }
}
