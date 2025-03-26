using UnityEngine;
using Photon.Pun;

public class LoadingScene1 : MonoBehaviourPunCallbacks
{
    LoadSceneManager sm;

    void Start()
    {
        sm = Managers.LoadScene;

        PhotonNetwork.LoadLevel(sm.SceneName);
    }
}
