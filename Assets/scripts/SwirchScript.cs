using UnityEngine;

public class SwirchScript : MonoBehaviour
{
    // ������������ ������ ����� FPV � ������� ��������

    public static bool isFpv;
    void Start()
    {
        isFpv = false;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            isFpv = !isFpv;//������������ ������
            GameEvents.EmitEvent(new GameEvent
            {
                name = "FPV",
            });

        }
    }
}
