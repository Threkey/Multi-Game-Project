using UnityEngine;
using UnityEngine.UI;

public class ScreenFadeout : MonoBehaviour
{
    Color panelColor = Color.black;
    [SerializeField]
    float speed;
    void Start()
    {
        panelColor.a = 0f;
    }

    void Update()
    {
        if(panelColor.a < 1f)
        {
            panelColor.a += 0.1f * speed;
            GetComponent<Image>().color = panelColor;
        }
    }
}
