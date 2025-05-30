using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class ToasterScript : MonoBehaviour
{
    private GameObject bg;
    private TMPro.TextMeshProUGUI content;
    private float timeout;
    private float showtime = 3.0f;

    
    void Start()
    {
        Transform t = transform.Find("Background");
        bg = t.gameObject;
        content=t.Find("Content")
       .GetComponent<TMPro.TextMeshProUGUI>();
        content.text = "";
        bg.SetActive(false);
        GameEvents.Subscribe(OnGameEvent);
    }

   
    void Update()
    {
        if (timeout > 0) {
            timeout -= Time.deltaTime;
            if (timeout <= 0)
            {
                content.text = "";
                bg.SetActive(false);
            }
        }
      

    }
    private void OnGameEvent(GameEvent e)
    { 
        if (e.toast != null)
        {
            content.text = e.toast;
            timeout = showtime;
            bg.SetActive(true);
        }
    }
    private void OnDestroy()
    {
        GameEvents.UnSubscribe(OnGameEvent);
    }
}
