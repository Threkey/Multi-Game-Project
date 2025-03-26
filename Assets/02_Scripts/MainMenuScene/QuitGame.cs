using UnityEngine;

public class QuitGame : MonoBehaviour
{
    public void ClosePopupUI()
    {
        gameObject.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
