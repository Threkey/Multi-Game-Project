using UnityEngine;
using UnityEngine.UI;

public class CharacterSelect : MonoBehaviour
{
    [SerializeField]
    Button[] btnCharacters;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void Confirm()
    {
        // ����Ȯ��
        gameObject.SetActive(false);
    }

    public void CloseCharacterSelectPopupUI()
    {
        gameObject.SetActive(false);
    }
}
