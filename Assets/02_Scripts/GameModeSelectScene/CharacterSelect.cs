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
        // 선택확정
        gameObject.SetActive(false);
    }

    public void CloseCharacterSelectPopupUI()
    {
        gameObject.SetActive(false);
    }
}
