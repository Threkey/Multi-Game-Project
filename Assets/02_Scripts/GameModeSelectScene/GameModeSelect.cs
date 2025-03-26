using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;
using TMPro;
using System.Collections;

public class GameModeSelect : MonoBehaviourPunCallbacks
{
    LoadSceneManager sm;

    int gameMode = -1;              // 0: 멀티, 1: 싱글, 2: 커스텀생성, 3: 커스텀 입장
    string customCode;
    bool isSearching = false;
    string lobbyName = "";
    float timer = 0f;               // 매치 서칭 시간

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
    Image imageCharacterPortrait;               // 캐릭터 선택 버튼 이미지
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
        // 매칭 시간 체크
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
                textGameMode.text = "멀티 게임";
                lobbyName = "Multi";
                roomOptions.MaxPlayers = 2;
                break;
            case 1:
                textGameMode.text = "싱글 게임";
                lobbyName = "Single";
                roomOptions.MaxPlayers = 1;
                break;
            case 2:
                textGameMode.text = "커스텀 생성";
                lobbyName = "Custom";
                roomOptions.MaxPlayers = 2;
                break;
            case 3:
                textGameMode.text = "커스텀 입장";
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

        // 로비 입장
        if(gameMode != -1)
        {
            isSearching = true;
            goButtonStart.SetActive(false);

            TypedLobby typedLobby = new TypedLobby(lobbyName, LobbyType.Default);

            PhotonNetwork.JoinLobby(typedLobby);
        }
        else
        {
            textGameMode.text = "모드를 선택하세요";
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
            Debug.Log("왜안되지");
            PhotonNetwork.LeaveLobby();
        }
    }

    IEnumerator coSearchMatch()
    {
        while (isSearching)
        {
            textTimer.text = string.Format("{0:D2} : {1:D2}", (int)(timer / 60f), (int)(timer % 60f));

            // Room에 입장해서 플레이어가 2명이면 게임룸Scene으로 이동
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