using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;

public class MainMenuButtons : MonoBehaviourPunCallbacks
{
    LoadSceneManager sm;
    GameManager gm;

    [SerializeField]
    GameObject goSettings;
    [SerializeField]
    GameObject goMyInfo;
    [SerializeField]
    GameObject goQuitGame;
    [SerializeField]
    TextMeshProUGUI textButtonMyInfo;

    void Start()
    {
        sm = Managers.LoadScene;
        gm = Managers.Game;
        textButtonMyInfo.text = gm.PlayerName;
    }

    public void LoadModeSelectScene()
    {
        sm.LoadScene("GameModeSelectScene");
    }

    public void ShowSettingsPopupUI()
    {
        goSettings.SetActive(true);
    }

    public void ShowQuitGamePopupUI()
    {
        goQuitGame.SetActive(true);
    }

    public void ShowMyInfoPopupUI()
    {
        goMyInfo.SetActive(true);
    }
}
