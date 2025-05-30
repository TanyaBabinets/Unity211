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
        Time.timeScale = 0.0f;//������ �� �����
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
         //������ ���������� ����������� ������ ��� ������� � ���������(��� ����� ��� ����� Builds ���� ��������)
    UnityEditor.EditorApplication.isPlaying = false;
#endif


#if UNITY_STANDALONE
        Application.Quit();

        //������ ���������� ����������� ������ ��� ������ �������
#endif
    }
    public void OnResumeClick()
    {
        Hide();
    }



 
    //    //(Exit).... in Unity ���� 2 ������ ������� - � ��������� � �������������� (EXE)
    //    //� ������ ��������� ������� (��������� ) ���� ����� � ������� ������� ������� �������� � ���������� ��������� 
    //    //������ ��� ����������. 
    //    //� ������ ����, � ����� �� ������� ����� ��������� � �� ���������
    //    //�� ����� ���������� ������� ���������.

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
////������������ - ����� �������� ����� ���������� �������. ������, ���� ������ ���� ������ �����, �� ��� �������� ������������.
///
//� MenuCanvas ������� Content, ������ ������ ���������� �� �������� �������, �� ���������������� ������ ������ ������
//������������ - ��� ������ ���������� 
//������ ����� ���������� �� ����������, ������� ������ ��������, � ������� ����� ���������� ��� ��������, � ������� �� ��� 
//�� ����� - �.�. ����� �������������� � ���������.