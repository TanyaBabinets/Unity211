using UnityEngine;

public class ClockScript : MonoBehaviour
{
    
    public TMPro.TextMeshProUGUI content;
    private float countTime=0f;
    void Start()
    {
        content = GetComponentInChildren<TMPro.TextMeshProUGUI>();
    }

    
    void Update()
    {
      //  Debug.Log($"Time: {countTime}");
        countTime += Time.deltaTime;
        int hours = (int)(countTime / 3600);
        int min = (int)((countTime % 3600) / 60);
        int sec = (int)(countTime % 60);

        content.text = $"{hours:00}:{min:00}:{sec:00}";
    }
}
