using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_instance;
    public static Managers Instance { get { Init(); return s_instance; } }

    // 매니저들
    LoadSceneManager _loadScene = new LoadSceneManager();
    public static LoadSceneManager LoadScene { get { return Instance._loadScene; } }

    GameManager _game = new GameManager();
    public static GameManager Game { get { return Instance._game; } }

    void Awake()
    {
        Init();
    }

    static void Init()
    {
        if(s_instance == null)
        {
            GameObject obj = GameObject.Find("@");
            if(obj == null)
            {
                obj = new GameObject("@");
                obj.AddComponent<Managers>();
            }

            DontDestroyOnLoad(obj);
            s_instance = obj.GetComponent<Managers>();
        }
    }
}
