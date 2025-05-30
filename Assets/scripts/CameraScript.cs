using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private GameObject camPos;
    float angleX;
    float angleY;
    void Start()
    {
        camPos = GameObject.Find("CameraPos");
        angleX=this.transform.eulerAngles.x;//rotation
        angleY=this.transform.eulerAngles.y;
    }

    void Update()
    {
        float mx = 0.5f*Input.GetAxis("Mouse X");
        float my = 0.5f*Input.GetAxis("Mouse Y");// это не координаты мыши а ее смещение вдоль осей, если она не движется, то сигналы 0

        angleX -= my;//изменение углов поворота камеры
        angleY += mx;
        //мышь по Х, камера по Y
        this.transform.eulerAngles = SwirchScript.isFpv
            ? new Vector3(angleX, angleY, 0f)
            : camPos.transform.eulerAngles;

      //  angleX = Mathf.Clamp(angleX, 20f, 60f);//Mathf.Clamp(значение, минимум, максимум)
    }
    
}

//идея поворота камеры - накопление данных про движение мыши вдоль разных осей (X, Y)
