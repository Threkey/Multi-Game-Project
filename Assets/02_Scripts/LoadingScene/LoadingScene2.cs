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

        // 커스텀프로퍼티로 캐릭터 정해보기

        PhotonNetwork.LoadLevel(sm.SceneName);
    }
}
