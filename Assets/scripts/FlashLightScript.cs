using UnityEngine;

public class FlashLightScript : MonoBehaviour
{
    private Light spotLight;
    private float lifetime = 10.0f;
    private float charge;
    
    void Start()
    {
        spotLight = GetComponent<Light>();
        charge = 1.0f;
        GameEvents.Subscribe(OnGameEvent);
    }
    private void OnGameEvent(GameEvent e)
    { //подписка на событие
        if (e.name == "Collected")
        {
            if (e.payload is GameObject go)
            {
                if (go.CompareTag("Battery"))
                {
                    //charge += 1.0f;
                    Battery battery = go.GetComponent<Battery>();
                    if (battery != null)
                    {
                        charge += battery.GetCharge();

                        switch (battery.type)
                        {
                            case BatteryType.D:
                                GameEvents.EmitEvent(new GameEvent { name = "BatteryD" });
                                break;
                            case BatteryType.E:
                                GameEvents.EmitEvent(new GameEvent { name = "BatteryE" });
                                break;
                        }
                        Debug.Log($"Picked up battery of type {battery.type} with charge {battery.GetCharge()}");
                    }
                }
            }

        }
    }
    private void OnDestroy()
    {
        GameEvents.UnSubscribe(OnGameEvent);
    }

   
    void Update()
    {
        if (SwirchScript.isFpv && !LightScript.isDay)
        {
            spotLight.intensity = Mathf.Clamp01(charge);//если яркость больше 1 то возвр.1, если меньше то само число 
            transform.forward=Camera.main.transform.forward;//направление
            charge -= charge - Time.deltaTime / lifetime;//уменьшаем заряд
            if (charge < 0f) {
                charge = 0f;
            }
        }
        else
        {
            spotLight.intensity = 0.0f;
        }
    }

}
//фонарик работает ночью и когда все выкл.фонари