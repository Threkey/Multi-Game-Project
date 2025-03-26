using System.Collections;
using UnityEngine;

public class MatchStartUI : MonoBehaviour
{
    [SerializeField]
    RectTransform imageReady;

    Vector3 imagePos = Vector3.zero;

    [SerializeField]
    float speed;
    void Start()
    {
        imagePos = imageReady.anchoredPosition;
        StartCoroutine(coMoveImage());
    }

    IEnumerator coMoveImage()
    {
        while(imageReady.anchoredPosition.x < 595f)
        {
            yield return null;

            if(imageReady.anchoredPosition.x < -0.01f)
            {
                imagePos.x = Mathf.Lerp(imagePos.x, 0f, Time.deltaTime * speed);
            }
            else
            {
                imagePos.x = Mathf.Lerp(imagePos.x, 600f, Time.deltaTime * speed);
            }

            imageReady.anchoredPosition = imagePos;
        }

        Destroy(gameObject);
    }
}
