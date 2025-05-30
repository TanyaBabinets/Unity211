
using UnityEngine;

public class Gates1 : MonoBehaviour
{

  
    public bool isOpen { get; private set; } = false;
    private GameObject content;
    private float openingTime= 5.0f; //seconds to open
    [SerializeField]  private float openSize = 1.0f;//size of gates
    private bool destroyGate = false;
    [SerializeField] private Vector3 openDirection = Vector3.right;
    public int keyNumber;

    void Start()
    {
        content = transform.Find("Content").gameObject;  //для внутр.обьектов - transform исп.
        GameEvents.Subscribe(OnGameEvent);//подписка на много событий
    }


    void Update()
    {
        if (isOpen)
        {
            if (content.transform.localPosition.magnitude < openSize)
            {
                content.transform.Translate(
                    openSize * Time.deltaTime / openingTime * openDirection);

                if (content.transform.localPosition.magnitude >= openSize)
                {
                    GameEvents.EmitEvent(new GameEvent
                    {
                        name = $"Gates{keyNumber} opened"
                    });
                    destroyGate = true;
                    Destroy(gameObject);
                }
            }
            //if (isOpen && !destroyGate && content.transform.localPosition.x >= openSize)
            //{
            //    destroyGate = true;
            //    Destroy(gameObject);
            //}
        }
    }

  

    private void OnGameEvent(GameEvent e) { //подписка на событие
        if (e.name == $"Key{keyNumber}") {
            Debug.Log(e);
            isOpen = true;
            GameEvents.EmitEvent(new GameEvent
            {
                name = $"Gates {keyNumber} opening"
            });
        }
    }
    private void OnDestroy()//отписка обязательно!!!!!!!!!!!!!!
    {
        GameEvents.UnSubscribe(OnGameEvent);
    }
}
