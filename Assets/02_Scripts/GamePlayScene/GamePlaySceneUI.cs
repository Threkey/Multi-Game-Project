using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GamePlaySceneUI : MonoBehaviour
{
    GameManager gm;

    [SerializeField]
    TextMeshProUGUI textPlayerName;
    [SerializeField]
    TextMeshProUGUI textOpponentName;
    [SerializeField]
    Image imagePlayerHp;
    [SerializeField]
    Image imageOpponentHp;

    void Awake()
    {
        gm = Managers.Game;
    }

    private void Start()
    {
        // 플레이어 닉네임
        textPlayerName.text = PhotonNetwork.LocalPlayer.NickName;

        // 상대 닉네임
        if (PhotonNetwork.PlayerListOthers.Length > 1)
            textOpponentName.text = PhotonNetwork.PlayerListOthers[0].NickName;
        else
            textOpponentName.text = "CPU";
    }
}
