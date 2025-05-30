using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    private GameObject content;
    [SerializeField] private AudioMixer audioMixer;
    private Slider effectsSlider;
    private Slider musicSlider;
    private Slider gateSlider;
    private Toggle muteToggle;
    void Start()
    {
        content = transform.Find("Content").gameObject;
        effectsSlider = transform.Find("Content/EffectsSlider").GetComponent<Slider>();
        musicSlider = transform.Find("Content/MusicSlider").GetComponent<Slider>();
        gateSlider = transform.Find("Content/LoopSlider").GetComponent<Slider>();
        Toggle muteToggle = transform.Find("Content/MuteToggle").GetComponent<Toggle>();
       
      
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        }
        else {
            musicSlider.value = 0.8f; //0 dB
        }
        if (PlayerPrefs.HasKey("effectsVolume"))
        {
            effectsSlider.value = PlayerPrefs.GetFloat("effectsVolume");
        }
        else
        {
            effectsSlider.value = 0.8f; //0 dB
        }
        if (PlayerPrefs.HasKey("gateVolume"))
        {
            gateSlider.value = PlayerPrefs.GetFloat("gateVolume");
        }
        else
        {
            gateSlider.value = 0.8f; //0 dB
        }
        if (PlayerPrefs.HasKey("isMuted"))
        {
            muteToggle.isOn = PlayerPrefs.GetInt("isMuted")==1;
        }
        OnMuteValueChanged(muteToggle.isOn);
        Hide();
    }


    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (content.activeInHierarchy) Hide(); else Show();
        }
    }
    private void Hide()
    {
        content.SetActive(false);
        Time.timeScale = 1.0f;
        audioMixer.SetFloat("effectsVolume", 0f);
    }

    private void Show()
    {
        content.SetActive(true);
        Time.timeScale = 0.0f;//ставим на паузу
        audioMixer.SetFloat("effectsVolume", -80f);
    }
    public void OnEffectValueChange(float value)
    {
        //[0,1]->[-80,20]
        audioMixer.SetFloat("effectsVolume", Mathf.Lerp(-80f, 20f, value));
    }
    public void OnMusicValueChanged(float value)
    {
        audioMixer.SetFloat("musicVolume", Mathf.Lerp(-80f, 20f, value));
    }
    public void OnGateValueChanged(float value)
    {
        audioMixer.SetFloat("gateVolume", Mathf.Lerp(-80f, 20f, value));
    }
    public void OnMuteValueChanged(bool isMuted)
    {
        audioMixer.SetFloat("musicVolume", isMuted ? -80f : 0f);
    }

    public void OnExitClick()
    {
        #if UNITY_EDITOR
         //данные инструкции компилюются только при запуску з редактора(это нужно для папки Builds чтоб сбилдить)
    UnityEditor.EditorApplication.isPlaying = false;
#endif


#if UNITY_STANDALONE
        Application.Quit();

        //данные инструкции компилюются только при сборке проекта
#endif
    }
    public void OnResumeClick()
    {
        Hide();
    }



 
    //    //(Exit).... in Unity есть 2 режима запуска - в редакторе и самостоятельно (EXE)
    //    //В режиме редактора процесс (системний ) есть Юнити и попытка закрыть процесс приводит к выключения редактора 
    //    //причем без сохранения. 
    //    //З іншого боку, у збірку не входить пакет редактора і всі звернення
    //    //до нього викликають помилки компіляції.

    private void OnDestroy()
    {
        PlayerPrefs.SetFloat("musicVolume", musicSlider.value);
        PlayerPrefs.SetFloat("effectsVolume", effectsSlider.value);
        PlayerPrefs.SetFloat("gateVolume", gateSlider.value);
        PlayerPrefs.SetInt("isMuted", muteToggle.isOn ? 1 : 0);
        PlayerPrefs.Save();
    }
}


//}
////интерполяция - поиск значений между известными точками. График, если график дает прямую линию, то это линейная интерполяция.
///
//В MenuCanvas создаем Content, скрипт должен находиться на активном обьекте, на деактивироваться должен другой обьект
//инкапсуляция - все внутри МЕНЮКанвас 
//скрипт будет находиться на МЕНЮКАнвас, который всегда активный, а контент будет появляться или исчезать, и скрипта на нем 
//не будет - т.е. будет активироваться и деактивир.