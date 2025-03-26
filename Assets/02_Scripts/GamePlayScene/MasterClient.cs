using Photon.Pun;
using Photon.Realtime;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.UI;

public class MasterClient : MonoBehaviour
{
    [SerializeField]
    PhotonView pv;

    [SerializeField]
    Image imageTimerBar;
    [SerializeField]
    Button btnEndTurn;

    const float timeMax = 30f;      // �� ���ѽð�
    float time;                     // �� �����ð�
    int currentTurn = 0;            // ���� ��

    [SerializeField]
    bool isTurn = false;
    public bool IsTurn { get { return isTurn; } set { isTurn = value; } }

    void Awake()
    {

    }

    void Start()
    {
        
    }

    void Update()
    {
        // ������ Ŭ���̾�Ʈ(����)���� ó���� �͵�
        if (PhotonNetwork.IsMasterClient)
        {
            // ���ѽð� ����ȭ
            if(isTurn)
            {
                time -= Time.deltaTime;
                pv.RPC("SyncTimeRPC", RpcTarget.AllBuffered, time);
                
                // �ð��� ������ �� ����
                if(time <= 0f)
                {
                    pv.RPC("EndTurnPRC", RpcTarget.AllBuffered);
                }
            }
        }
    }

    [PunRPC]
    void StartTurnRPC()
    {
        IsTurn = true;
        btnEndTurn.interactable = true;
        time = timeMax;
    }

    [PunRPC]
    void EndTurnRPC()
    {
        isTurn = false;
        btnEndTurn.interactable = false;
    }

    [PunRPC]
    void SyncTimeRPC(float time)
    {
        imageTimerBar.fillAmount = time / timeMax;
    }
}