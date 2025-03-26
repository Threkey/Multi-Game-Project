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
        // �÷��̾� �г���
        textPlayerName.text = PhotonNetwork.LocalPlayer.NickName;

        // ��� �г���
        if (PhotonNetwork.PlayerListOthers.Length > 1)
            textOpponentName.text = PhotonNetwork.PlayerListOthers[0].NickName;
        else
            textOpponentName.text = "CPU";
    }
}
