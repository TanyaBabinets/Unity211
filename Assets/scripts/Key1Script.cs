using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class Key1Script : MonoBehaviour
{
    [SerializeField] private int keyNumber;
    
    void Start()
    {
        
    }

  
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (other.name == "Player")
           
        {
            Debug.Log(other.name);
            GameEvents.EmitEvent(new GameEvent
            {
                name = $"Key{keyNumber}",
                toast = $"Key {keyNumber} collected. Gate {keyNumber} opening"
            });
         //   Gates1.isOpen = true;//прямая команда но неудобно масштабировать, мы привязаны тут, потому делаем много событий
            GameObject.Destroy(this.gameObject);
        }
    }
    private void OnDestroy()//отписка обязательно!!!!!!!!!!!!!!
    {
        Debug.Log("Key1 destroyed");
    }
}
