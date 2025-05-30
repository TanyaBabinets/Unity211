using System.Security.Cryptography;
using System.Text.RegularExpressions;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.WSA;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody rb;
    private GameObject camPos;
    private AudioSource collectSound;
    private AudioSource gateOpenSound;
    void Start()
    {
     rb=this.GetComponent<Rigidbody>();
        AudioSource[] audioSources = this.GetComponents<AudioSource>();
        collectSound=audioSources[0];
        gateOpenSound=audioSources[1];//компоненты нумеруются в порядке декларации в инспекторе Player
     camPos = GameObject.Find("CameraPos");
        GameEvents.Subscribe(OnGameEvent);
    }

    // Update is called once per frame
    void Update()///клавиатура
    {
        float fx = 2f*Input.GetAxis("Horizontal");//-1 влево, 0, +1 вправо
        float fy = 2f*Input.GetAxis("Vertical");

        // rb.AddForce(fx, 0, fy); // добавляем силу по разным осям, надо катить его, не важно
        // меняем строку на это, для ориентации камеры учитывать ее позиц.векторы. Селфи-палка)
        Vector3 fwd = Camera.main.transform.forward;
            Vector3 r= Camera.main.transform.right;//векторы это цветные стрелки на камере
        rb.AddForce(2.0f*Time.timeScale*(fwd * fy + r * fx)); //приложить силу в направлении
        //timeScale добавлено, потому что при паузе Esc накапливается сила, чтоб убрать накопление и рывок, умножить на 0(=timeScale)

        Camera.main.transform.position =SwirchScript.isFpv
            //если isFPV переносим камеру в центр сферы
            ? this.transform.position//this = sphere 
        :camPos.transform.position; //иначе переносим камеру вверх на позицию

        //когда игра на паузе, ворота останавливаются, но музыка ворот играет, чтоб она тоже останавливалась:
    //    if (gateOpenSound.isPlaying)
        //{
        //    gateOpenSound.volume = Time.timeScale == 0.0f ? 0.0f : 0.05f; // мы звук не останавливаем, но ставим громкость 0
        //}
    }

    //собираем батарейки


    //private void OnTriggerEnter(Collider other)
    //{
    //    collectSound.Play();
    //    GameEvents.EmitEvent(new GameEvent
    //    {
    //        name = "Collected",
    //        toast = other.CompareTag("Untagged") ? null : $"You find {other.tag}",
    //        payload = other.gameObject  //battery
    //    });
    //    Destroy(other.gameObject);//забрали батарейку и удаляем обьект
   // }
    private void OnTriggerEnter(Collider other)
    {
        collectSound.Play();
        string toast = null;

        if (other.CompareTag("Battery"))
        {
            Battery battery = other.GetComponent<Battery>();
            if (battery != null)
            {
                BatteryType type = battery.GetBatType();
                float charge = battery.GetCharge();

                toast = $"Battery {battery.GetBatType()} collected with charge {battery.GetCharge():F2}";

            }
        }
        else if (other.CompareTag("Key"))
        {
            toast = "You found a key!";
        }
      
        GameEvents.EmitEvent(new GameEvent
        {
            name = "Collected",
         //  toast = other.CompareTag("Untagged") ? null : $"You find {other.tag}",
            toast = toast,
            payload = other.gameObject  //battery
        });
        Destroy(other.gameObject);//забрали и удаляем обьект
    }
    private void OnGameEvent(GameEvent e)
    { //подписка на событие
        if (Regex.IsMatch(e.name, @"Gates(.+) opening"))
        {
            gateOpenSound.Play();
        }
        else if (Regex.IsMatch(e.name, @"Gates(.+) opened"))
        {
            gateOpenSound.Stop();
        }
    }
    private void OnDestroy()
    {
        GameEvents.UnSubscribe(OnGameEvent);
    }
}
