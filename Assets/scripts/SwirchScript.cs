using UnityEngine;

public class SwirchScript : MonoBehaviour
{
    // переключение камеры между FPV и третьей позицией

    public static bool isFpv;
    void Start()
    {
        isFpv = false;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            isFpv = !isFpv;//переключение камеры
            GameEvents.EmitEvent(new GameEvent
            {
                name = "FPV",
            });

        }
    }
}
