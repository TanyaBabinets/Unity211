using UnityEngine;

using UnityEngine.UI;

public class BatteriesScript : MonoBehaviour
{ 
    private Image batImage;
    void Start()
    {
        batImage = transform.Find("BatImage").GetComponent<Image>();
        batImage.enabled = false;
      
        GameEvents.Subscribe(OnGameEvent);

    }
    private void OnGameEvent(GameEvent e)
    {
        if (e.name == "Collected")
        {
            if (e.payload is GameObject go && go.CompareTag("Battery"))
            {
                batImage.enabled = true;
                Debug.Log("Battery taken");
                Debug.Log("Battery icon position: " + batImage.transform.position);
            }         
           
        }
    }
    private void OnDestroy()
    {
        GameEvents.UnSubscribe(OnGameEvent);
    }
}
