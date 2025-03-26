using UnityEngine;
using Photon.Pun;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;


public class LoadSceneManager : MonoBehaviourPunCallbacks
{
    private string sceneName;
    public string SceneName { get { return sceneName; } }

    /// <summary>
    /// 씬을 이동하기 전 로딩 UI를 생성한다.
    /// </summary>
    public void CreateLoadingUI(int mode)
    {
        Addressables.InstantiateAsync("Canvas_Loading" + mode);
    }


    /// <summary>
    /// 로딩씬을 거쳐 입력한 씬으로 이동한다.
    /// </summary>
    /// <param name="sceneName"></param>
    /// 이동할 씬 이름
    /// <param name="mode"></param>
    /// 거쳐갈 로딩씬 번호, 0은 로딩씬 없이 바로 이동한다.
    public void LoadScene(string sceneName, int mode = 0)
    {
        this.sceneName = sceneName;

        if(mode == 0)
            PhotonNetwork.LoadLevel(this.sceneName);
        else
            PhotonNetwork.LoadLevel("LoadingScene" + mode);
    }
}
