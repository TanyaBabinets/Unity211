using System.Security.Cryptography;
using UnityEngine;
public enum BatteryType { D, E }
public class Battery : MonoBehaviour
{
    public BatteryType type;
    private float charge;
  
    void Start()
    {
        switch (type)
        {
            case BatteryType.E:
                charge= Random.Range(0.5f, 1.0f);
                break;
            case BatteryType.D:
              charge= Random.Range(1.5f, 2.0f); 
                break;
            default: charge= 0.0f; 
                break;
        }
    }
    public float GetCharge()
    {
       return charge;
    }
    public BatteryType GetBatType()
    {
        return type;
    }
}
