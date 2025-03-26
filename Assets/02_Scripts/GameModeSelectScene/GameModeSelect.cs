using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;
using TMPro;
using System.Collections;

public class GameModeSelect : MonoBehaviourPunCallbacks
{
    LoadSceneManager sm;

    int gameMode = -1;              // 0: ��Ƽ, 1: �̱�, 2: Ŀ���һ���, 3: Ŀ���� ����
    string customCode;
    bool isSearching = false;
    string lobbyName = "";
    float timer = 0f;               // ��ġ ��Ī �ð�

    [SerializeField]
    GameObject goCharacterSelect;
    [SerializeField]
    GameObject goInputCustomCode;
    [SerializeField]
    GameObject goButtonStart;
    [SerializeField]
    GameObject goButtonStop;
    [SerializeField]
    TMP_InputField infieldCustomCode;
    [SerializeField]
    TextMeshProUGUI textGameMode;
    [SerializeField]
    TextMeshProUGUI textTimer;
    [SerializeField]
    Image imageCharacterPortrait;               // ĳ���� ���� ��ư �̹���
    [SerializeField]
    Sprite[] spriteCharacterPortraits;

    TypedLobby typedLobby;
    RoomOptions roomOptions = new RoomOptions();

    void Awake()
    {
        sm = Managers.LoadScene;
    }

    void Update()
    {
        // ��Ī �ð� üũ
        if (isSearching)
            timer += Time.deltaTime;
    }

    public void LoadMainMenuScene()
    {
        sm.LoadScene("MainMenuScene");
    }
    public void ChangeGameMode(int gameMode)
    {
        this.gameMode = gameMode;

        switch(gameMode)
        {
            case 0:
                textGameMode.text = "��Ƽ ����";
                lobbyName = "Multi";
                roomOptions.MaxPlayers = 2;
                break;
            case 1:
                textGameMode.text = "�̱� ����";
                lobbyName = "Single";
                roomOptions.MaxPlayers = 1;
                break;
            case 2:
                textGameMode.text = "Ŀ���� ����";
                lobbyName = "Custom";
                roomOptions.MaxPlayers = 2;
                break;
            case 3:
                textGameMode.text = "Ŀ���� ����";
                lobbyName = "Custom";
                roomOptions.MaxPlayers = 2;
                break;
            default:
                textGameMode.text = "";
                lobbyName = "";
                break;
        }
    }

    public void ShowCharacterSelectPopupUI()
    {
        goCharacterSelect.SetActive(true);
    }

    public void ShowCustomCodePopupUI()
    {
        goInputCustomCode.SetActive(true);
    }

    public void StartSearching()
    {
        Debug.Log("StartSearching");

        // �κ� ����
        if(gameMode != -1)
        {
            isSearching = true;
            goButtonStart.SetActive(false);

            TypedLobby typedLobby = new TypedLobby(lobbyName, LobbyType.Default);

            PhotonNetwork.JoinLobby(typedLobby);
        }
        else
        {
            textGameMode.text = "��带 �����ϼ���";
        }
    }

    public void StopSearching()
    {
        Debug.Log("StopSearching");
        isSearching = false;
        goButtonStop.SetActive(false);
        
        StopCoroutine("coSearchMatch");

        if (PhotonNetwork.CurrentRoom != null)
        {
            PhotonNetwork.LeaveRoom();
        }
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("OnJoinedLobby");

        if(PhotonNetwork.CurrentLobby.Name != "Custom")
            PhotonNetwork.JoinRandomOrCreateRoom(null, (byte)roomOptions.MaxPlayers, MatchmakingMode.FillRoom, null, null, null, roomOptions, null);
        else
        {
            if(gameMode == 2)
            {
                PhotonNetwork.CreateRoom(customCode);
            }
            else if(gameMode == 3)
            {
                PhotonNetwork.JoinRoom(customCode);
            }
        }
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom");

        goButtonStop.SetActive(true);
        StartCoroutine("coSearchMatch");
    }

    public override void OnLeftRoom()
    {
        Debug.Log("OnLeftRoom");

        timer = 0f;
        textTimer.text = "00 : 00";

        goButtonStart.SetActive(true);

        if (PhotonNetwork.CurrentLobby != null)
        {
            Debug.Log("�־ȵ���");
            PhotonNetwork.LeaveLobby();
        }
    }

    IEnumerator coSearchMatch()
    {
        while (isSearching)
        {
            textTimer.text = string.Format("{0:D2} : {1:D2}", (int)(timer / 60f), (int)(timer % 60f));

            // Room�� �����ؼ� �÷��̾ 2���̸� ���ӷ�Scene���� �̵�
            if (PhotonNetwork.CurrentRoom != null)
                if (PhotonNetwork.CurrentRoom.PlayerCount == roomOptions.MaxPlayers)
                {
                    Debug.Log("Opponent player found!");
                    sm.LoadScene("GamePlayScene", 2);
                    yield break;
                }
                else
                {
                    yield return new WaitForSeconds(1f);
                    Debug.Log("Searching opponent player");
                }
            else
                yield return new WaitForSeconds(1f);
        }

        yield return null;
    }
}