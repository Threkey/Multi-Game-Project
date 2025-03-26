using UnityEngine;
using Photon.Pun;
using UnityEngine.EventSystems;

public class Player : MonoBehaviourPunCallbacks
{
    MasterClient mc;

    const float maxHealth = 3.0f;
    float currentHealth;

    Vector2 mousePos = Vector2.zero;
    Vector3 movePos = new Vector3(0f, -2f, 0);
    Vector3 atkPos = new Vector3(0f, -2f, 0f);

    void Awake()
    {
        mc = GameObject.Find("Canvas").GetComponent<MasterClient>();
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if( mc.IsTurn == true )
        {
            // 클릭하면 공격, 이동위치 지정
            if (Input.GetMouseButtonUp(0) /*&& !EventSystem.current.IsPointerOverGameObject()*/)
            {
                mousePos = Camera.main.ScreenToWorldPoint( Input.mousePosition );

                if(mousePos.x > -1.5f && mousePos.x < 1.5f)
                {
                    // atkPos
                    if (mousePos.y > 0.5f && mousePos.y < 3.5f)
                    {
                        atkPos.y = Mathf.Round(mousePos.y);
                        atkPos.x = Mathf.Round(mousePos.x);
                    }
                    // movePos
                    else if (mousePos.y < -0.5f && mousePos.y > -3.5f)
                    {
                        movePos.y = Mathf.Round(mousePos.y);
                        movePos.x = Mathf.Round(mousePos.x);
                    }

                    Debug.Log(string.Format("Mouse : {0}, {1}", mousePos.x, mousePos.y));
                    Debug.Log(string.Format("Atk : {0}, {1}", atkPos.x, atkPos.y));
                    Debug.Log(string.Format("Move : {0}, {1}", movePos.x, movePos.y));
                }

                transform.position = movePos;
            }
        }
    }

    public float GetPlayerHealthPercentage()
    {
        return currentHealth / maxHealth;
    }
}
