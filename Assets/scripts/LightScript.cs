using System.Linq;
using UnityEngine;

public class LightScript : MonoBehaviour
{
    [SerializeField]
    private Material daySkyBox;
    [SerializeField]
    private Material nightSkyBox;

    private Light[] dayLights;
    private Light[] nightLights;
    public static bool isDay;
    void Start()
    {
        dayLights = GameObject.FindGameObjectsWithTag("DayLight")
            .Select(g => g.GetComponent<Light>())
            .ToArray();
       

        nightLights = GameObject.FindGameObjectsWithTag("NightLight")
           .Select(g => g.GetComponent<Light>())
           .ToArray();
        isDay = true;
        ResetLight();
        GameEvents.Subscribe(OnGameEvent);
    }
    private void OnGameEvent(GameEvent e)
    { //подписка на событие
        if (e.name == "FPV")
        {
            ResetLight();

        }
    }
    private void OnDestroy()
    {
        GameEvents.UnSubscribe(OnGameEvent);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))//toggle day/night
        {
            isDay = !isDay;
            ResetLight();
        }
        }
    private void ResetLight()
    {
        if (isDay)
        {
            foreach (Light light in dayLights)
            {
                light.intensity = 1.0f;
            }
            foreach (Light light in nightLights)
            {
                light.intensity = 0.0f;
            }
            RenderSettings.ambientIntensity = 1.0f;
            RenderSettings.reflectionIntensity = 1.0f;
            RenderSettings.skybox = daySkyBox;
        }
        else
        {
            foreach (Light light in dayLights)
            {
                light.intensity = 0.0f;
            }
            foreach (Light light in nightLights)
            {
               
                light.intensity = SwirchScript.isFpv ? 0.0f : 1.0f;
            }
            RenderSettings.ambientIntensity = 0.0f;
            RenderSettings.reflectionIntensity = 0.0f;
            RenderSettings.skybox = nightSkyBox;
        }
    }
}
    

/*
 directional light зависит от углов наклона, а не от позиций.
Источники света - от наклона зависит
Как выключить свет: источники и оточення. Выключить.И еще skyBox
-оточення (налаштування)
=ambient (Enviroment lighting)
=reflections (enviroment reflections)

skybox -изображает небо, можно отключить вообще none
 */