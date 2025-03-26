using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MyInfo : MonoBehaviour
{
    GameManager gm;

    Button btnConfirm;
    Button btnCancel;
    [SerializeField]
    TMP_InputField infieldPlayerName;
    [SerializeField]
    TextMeshProUGUI textButtonMyInfo;

    void Start()
    {
        gm = Managers.Game;
        infieldPlayerName.text = gm.PlayerName;
    }

    public void Confirm()
    {
        gm.PlayerName = infieldPlayerName.text;
        textButtonMyInfo.text = gm.PlayerName;

        gm.Save();
        ClosePopupUI();
    }

    public void ClosePopupUI()
    {
        gameObject.SetActive(false);
    }
}
