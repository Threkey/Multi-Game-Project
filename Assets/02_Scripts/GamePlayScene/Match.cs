using UnityEngine;
using Photon.Pun;

public class Match : MonoBehaviourPunCallbacks
{
    [SerializeField]
    GameObject goCharacter;
    [SerializeField]
    GameObject goMatchStartUI;

    bool isLoading = true;
    Vector3 characterInitPos = new Vector3(0f, -2f, 0f);

    void Awake()
    {
        DefaultPool pool = PhotonNetwork.PrefabPool as DefaultPool;
        pool.ResourceCache.Clear();
        pool.ResourceCache.Add("Character", goCharacter);
        pool.ResourceCache.Add("MatchStartUI", goMatchStartUI);
    }
    void Start()
    {
        PhotonNetwork.Instantiate("Character", characterInitPos, Quaternion.identity);
        PhotonNetwork.Instantiate("MatchStartUI", Vector3.zero, Quaternion.identity);

        if (PhotonNetwork.IsMasterClient)
        {

        }
    }
}
