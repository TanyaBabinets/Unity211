using System.Threading;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class KeysScript : MonoBehaviour
{
    private Image key1Image;


    void Start()
    {
        key1Image = transform.Find("Key1Image").GetComponent<Image>();
        key1Image.enabled = false;
       
        GameEvents.Subscribe(OnGameEvent);
        
    }
    private void OnGameEvent(GameEvent e)
    { //подписка на событие
        if (e.name == "Collected")
        {
            if (e.payload is GameObject go && go.CompareTag("Key"))
            {
                key1Image.enabled = true;
            }

        }

    }
    private void OnDestroy() { 
        GameEvents.UnSubscribe(OnGameEvent);
    }
}
