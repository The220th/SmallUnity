using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleScript : MonoBehaviour {

    private Vector3 v; //координаты
    private Vector3 Phi; // угол
    private float U = 0.1f; //скорость Paddle
    private float Tilt = 3; //Костыль
    private int RightBack = 0, LeftBack = 0; //костыль и костыль
    private bool Slow = false; //Если попал мячик на Freezer

    public GameObject test; //какая-то хрень
    
    void Start()
    {
        //test.GetComponent<BallScript>().UpSpeed();
    }
    void FixedUpdate() //Тут я думаю всё понятно
    {
        v = gameObject.transform.position;
        Phi = gameObject.transform.eulerAngles;
        FastMove();
        TurnLeft();
        TurnRight();
        
    }

    public void SlowPaddle() //метод вызывается извне
    {
        Slow = true;
        U = 0.05f;
        Invoke("InvertSlow", 2f); 
    }
    void InvertSlow()
    {
        Slow = !Slow;
    }

    void TurnRight()
    {
        if (Input.GetKey(KeyCode.D) && v.x > -8.2f) // Turn Right
        {
            v.x -= U;
            gameObject.transform.position = v;

            if ((Phi.z < 180) ? (Phi.z < 30f) : (Phi.z - 360 < 30f))
            {
                Phi.z += Tilt;
                gameObject.transform.eulerAngles = Phi;
                RightBack += 2;
            }
        }
        else
        {
            if (RightBack > 0)
            {
                Phi.z -= Tilt / 2;
                gameObject.transform.eulerAngles = Phi;
                RightBack--;
            }
        }
    }

    void TurnLeft()
    {
        if (Input.GetKey(KeyCode.A) && v.x < 8.2f) // Turn Left
        {
            v.x += U;
            gameObject.transform.position = v;

            if ((Phi.z > 180) ? (Phi.z - 360 > -30f) : (Phi.z > -30f))
            {
                Phi.z -= Tilt;
                gameObject.transform.eulerAngles = Phi;
                LeftBack += 2;
            }
        }
        else
        {
            if (LeftBack > 0)
            {
                Phi.z += Tilt / 2;
                gameObject.transform.eulerAngles = Phi;
                LeftBack--;
            }
        }
    }

    void FastMove()
    {
        if (!Slow)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                U = 0.7f;
            }
            else
            {
                U = 0.1f;
            }
        }
    }
}
