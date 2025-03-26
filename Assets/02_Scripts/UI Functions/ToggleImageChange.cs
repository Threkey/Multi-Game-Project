using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ToggleImageChange : MonoBehaviour
{
    [SerializeField]
    Sprite[] sprites = new Sprite[2];   // 0: mute, 1: unmute

    [SerializeField]
    Image imgToggle;
    void Start()
    {
        imgToggle.sprite = GetComponent<Toggle>().isOn ? sprites[0] : sprites[1];
    }

    public void ChangeImage(bool isOn)
    {
        imgToggle.sprite = isOn ? sprites[0] : sprites[1];
    }
}
