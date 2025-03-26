using UnityEngine;
using UnityEngine.EventSystems;

public class PositionTile : MonoBehaviour, /*IPointerEnterHandler, IPointerExitHandler,*/ IPointerClickHandler
{
    MasterClient mc;

    Color alpha = new Color(0f, 0f, 0f, 0.35f);

    void Awake()
    {
        mc = GameObject.Find("Canvas").GetComponent<MasterClient>();
    }

    /*
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (mc.IsTurn)
        {
            GetComponent<SpriteRenderer>().color += alpha;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (mc.IsTurn)
        {
            GetComponent<SpriteRenderer>().color -= alpha;
        }
    }
    */
    public void OnPointerClick(PointerEventData eventData)
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(eventData.position);
        if (mc.IsTurn)
        {
            if(mousePos.x < transform.position.x + 0.5f && mousePos.x > transform.position.x - 0.5f &&
                mousePos.y < transform.position.y + 0.5f && mousePos.y > transform.position.y - 0.5f)
            {
                GetComponent<SpriteRenderer>().color += alpha;
            }
            else
            {
                GetComponent <SpriteRenderer>().color -= alpha;
            }
        }
    }
}
