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

    const float timeMax = 30f;      // 턴 제한시간
    float time;                     // 턴 남은시간
    int currentTurn = 0;            // 현재 턴

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
        // 마스터 클라이언트(서버)에서 처리할 것들
        if (PhotonNetwork.IsMasterClient)
        {
            // 제한시간 동기화
            if(isTurn)
            {
                time -= Time.deltaTime;
                pv.RPC("SyncTimeRPC", RpcTarget.AllBuffered, time);
                
                // 시간이 지나면 턴 종료
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