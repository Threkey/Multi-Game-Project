using UnityEngine;
using UnityEngine.Audio;

public class PlayerPrefsEx : PlayerPrefs
{
    public static void SetBool(string key, bool value)
    {
        if (value)
        {
            SetInt(key, 1);
        }
        else
        {
            SetInt (key, 0);
        }
    }

    public static void SetByte(string Key, byte Value)
    {
        SetInt(Key, (int)Value);
    }

    public static bool GetBool(string Key, bool defaultValue = false)
    {
        bool tmp = GetInt(Key, 0) == 1 ? true : false;
        return tmp;
    }

    public static byte GetByte(string Key, byte defaultValue = 0)
    {
        return (byte)GetInt(Key, 0);
    }
}

public class GameManager : MonoBehaviour
{
    string playerName;
    public string PlayerName { get { return playerName; } set { playerName = value; } }

    float masterVolume;
    public float MasterVolume { get { return masterVolume; } set { masterVolume = value; } }
    float sfxVolume;
    public float SfxVolume { get {return sfxVolume; } set { sfxVolume = value; } }
    float bgmVolume;
    public float BgmVolume { get {return bgmVolume; } set { bgmVolume = value; } }

    bool masterMute;
    public bool MasterMute { get { return masterMute; } set {masterMute = value; } }
    bool sfxMute;
    public bool SfxMute { get {return sfxMute; } set {sfxMute = value; } }
    bool bgmMute;
    public bool BgmMute { get { return bgmMute; } set { bgmMute = value; } }

    /// <summary>
    /// ���
    /// 0: �ѱ���, 1: ����, 2: �Ϻ���, 3: �߱���
    /// </summary>
    byte language;
    public byte Language { get { return language; } set { language = value; } }

    public void Init()
    {
        // �������� �ҷ��� �� ����

        PlayerName = PlayerPrefsEx.GetString("PlayerName", "Player");
        MasterVolume = PlayerPrefsEx.GetFloat("MasterVolume", 1.0f);
        SfxVolume = PlayerPrefsEx.GetFloat("SfxVolume", 1.0f);
        BgmVolume = PlayerPrefsEx.GetFloat("BgmVolume", 1.0f);
        MasterMute = PlayerPrefsEx.GetBool("MasterMute", false);
        SfxMute = PlayerPrefsEx.GetBool("SfxMute", false);
        BgmMute = PlayerPrefsEx.GetBool("BgmMute", false);
        Language = PlayerPrefsEx.GetByte("Language", 0);
    }
    
    public void Save()
    {
        // ���������� �����ϰ� ���ÿ� ����

        PlayerPrefsEx.SetString("PlayerName", PlayerName);
        PlayerPrefsEx.SetFloat("MasterVolume", MasterVolume);
        PlayerPrefsEx.SetFloat("SfxVolume", SfxVolume);
        PlayerPrefsEx.SetFloat("BgmVolume", BgmVolume);
        PlayerPrefsEx.SetBool("MasterMute", MasterMute);
        PlayerPrefsEx.SetBool("SfxMute", SfxMute);
        PlayerPrefsEx.SetBool("BgmMute", BgmMute);
        PlayerPrefsEx.SetByte("Language", Language);

        PlayerPrefsEx.Save();
    }

    public void Reset()
    {
        // ��� ���� �ʱⰪ���� ����

        PlayerPrefsEx.DeleteAll();
        Init();
        Save();
    }
}
