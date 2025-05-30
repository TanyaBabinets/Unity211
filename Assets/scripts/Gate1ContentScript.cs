using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class Gate1ContentScript : MonoBehaviour
{
    private Gates1 gates1Script;
    void Start()
    {
       gates1Script = transform.parent.GetComponent<Gates1>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player" && !gates1Script.isOpen)
        {
            GameEvents.EmitEvent(new GameEvent
            {
                toast = $"Closed! Look for Key {gates1Script.keyNumber}"
            });
        }
    }
}
