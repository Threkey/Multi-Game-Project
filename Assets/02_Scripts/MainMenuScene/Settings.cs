using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    GameManager gm;

    [SerializeField]
    AudioMixer audioMixer;

    [SerializeField]
    TextMeshProUGUI textMasterVolumePercent;
    [SerializeField]
    TextMeshProUGUI textSfxVolumePercent;
    [SerializeField]
    TextMeshProUGUI textBgmVolumePercent;

    [SerializeField]
    Slider sliderMasterVolume;
    [SerializeField]
    Slider sliderSfxVolume;
    [SerializeField]
    Slider sliderBgmVolume;

    [SerializeField]
    Toggle toggleMasterMute;
    [SerializeField]
    Toggle toggleSfxMute;
    [SerializeField]
    Toggle toggleBgmMute;

    [SerializeField]
    ToggleGroup tgLanguage;
    [SerializeField]
    Toggle[] togglesLanguage;

    void Start()
    {
        gm = Managers.Game;

        // 볼륨 퍼센트 텍스트
        textMasterVolumePercent.text = string.Format("{0:p0}", gm.MasterVolume);
        textSfxVolumePercent.text = string.Format("{0:p0}", gm.SfxVolume);
        textBgmVolumePercent.text = string.Format("{0:p0}", gm.BgmVolume);

        // 볼륨 슬라이더
        sliderMasterVolume.value = gm.MasterVolume;
        sliderSfxVolume.value = gm.SfxVolume;
        sliderBgmVolume.value = gm.BgmVolume;

        // 사운드 뮤트
        toggleMasterMute.isOn = gm.MasterMute;
        toggleSfxMute.isOn = gm.SfxMute;
        toggleBgmMute.isOn = gm.BgmMute;

        // 언어
        togglesLanguage[gm.Language].isOn = true;
    }

    public void ShowVersionAndInformation()
    {

    }

    public void Cancel()
    {
        // 설정내용을 변경 전으로 되돌리고 팝업창 닫음
        // 볼륨
        sliderMasterVolume.value = gm.MasterVolume;
        sliderSfxVolume.value = gm.SfxVolume;
        sliderBgmVolume.value = gm.BgmVolume;

        // 사운드 뮤트
        toggleMasterMute.isOn = gm.MasterMute;
        toggleSfxMute.isOn = gm.SfxMute;
        toggleBgmMute.isOn = gm.BgmMute;

        // 언어
        togglesLanguage[gm.Language].isOn = true;

        gameObject.SetActive(false);
    }

    public void Confirm()
    {
        // 게임매니저에 설정 변경내용 저장하고 팝업창 닫음
        // 볼륨
        gm.MasterVolume = sliderMasterVolume.value;
        gm.SfxVolume = sliderSfxVolume.value;
        gm.BgmVolume = sliderBgmVolume.value;

        // 사운드 뮤트
        gm.MasterMute = toggleMasterMute.isOn;
        gm.SfxMute = toggleSfxMute.isOn;
        gm.BgmMute = toggleBgmMute.isOn;

        // 언어
        gm.Language = GetLanguageIndex();

        gm.Save();
        gameObject.SetActive(false);
    }

    public void ChangeVolume(string mixerName)
    {
        Slider sliderVolume = transform.Find("Image_Settings").Find("Go_Sound").Find("Slider_" +  mixerName + "Volume").GetComponent<Slider>();
        TextMeshProUGUI textVolume = transform.Find("Image_Settings").Find("Go_Sound").Find("Text_" + mixerName + "VolumePercent").GetComponent<TextMeshProUGUI>();
        Toggle toggleMute = transform.Find("Image_Settings").Find("Go_Sound").Find("Toggle_" + mixerName + "Mute").GetComponent<Toggle>();

        textVolume.text = string.Format("{0:p0}", sliderVolume.value);

        if(!toggleMute.isOn)
            audioMixer.SetFloat(mixerName, Mathf.Log10(sliderVolume.value) * 20);

    }

    //------------------------------------------------//
    /*
     * 2025.01.13
     * 이후에 더 깔끔하게 뮤트시킬 수 있는 방법을 찾으면 바꾸기
     */
    public void MuteMasterVolume(bool isOn)
    {
        if(isOn)
        {
            audioMixer.SetFloat("Master", -80.0f);
        }
        else
        {
            audioMixer.SetFloat("Master", Mathf.Log10(sliderMasterVolume.value) * 20);
        }
    }

    public void MuteSfxVolume(bool isOn)
    {
        if (isOn)
        {
            audioMixer.SetFloat("SFX", -80.0f);
        }
        else
        {
            audioMixer.SetFloat("SFX", Mathf.Log10(sliderSfxVolume.value) * 20);
        }
    }

    public void MuteBgmVolume(bool isOn)
    {
        if (isOn)
        {
            audioMixer.SetFloat("BGM", -80.0f);
        }
        else
        {
            audioMixer.SetFloat("BGM", Mathf.Log10(sliderBgmVolume.value) * 20);
        }
    }
    //--------------------------------------------//

    byte GetLanguageIndex()
    {
        for(byte i = 0; i < togglesLanguage.Length; i++)
        {
            if(togglesLanguage[i].isOn == true)
            {
                return i;
            }
        }

        return 0;
    }
}
