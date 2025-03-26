using UnityEngine;
using Photon.Pun;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;


public class LoadSceneManager : MonoBehaviourPunCallbacks
{
    private string sceneName;
    public string SceneName { get { return sceneName; } }

    /// <summary>
    /// ���� �̵��ϱ� �� �ε� UI�� �����Ѵ�.
    /// </summary>
    public void CreateLoadingUI(int mode)
    {
        Addressables.InstantiateAsync("Canvas_Loading" + mode);
    }


    /// <summary>
    /// �ε����� ���� �Է��� ������ �̵��Ѵ�.
    /// </summary>
    /// <param name="sceneName"></param>
    /// �̵��� �� �̸�
    /// <param name="mode"></param>
    /// ���İ� �ε��� ��ȣ, 0�� �ε��� ���� �ٷ� �̵��Ѵ�.
    public void LoadScene(string sceneName, int mode = 0)
    {
        this.sceneName = sceneName;

        if(mode == 0)
            PhotonNetwork.LoadLevel(this.sceneName);
        else
            PhotonNetwork.LoadLevel("LoadingScene" + mode);
    }
}
