using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour {

    private Vector3 v; //Какой-то вектор. Он будет дальше использоваться
    //private Vector3 vPrew;
    private int StartAcceleration = 0; //Это нужно, потому, что шарик сбрасывает сам скорость. Дурацкая встроенная физика юнити

    //private bool StartUpSpeed = false;
    //private int timer = 0;
    //private float UofSpeedUp = 100f;


    private int TimeReproductionOfBalls; //Через это время, будет копировать сам себя мяч

    private Rigidbody rb;

    void Start()
    {

        TimeReproductionOfBalls = 50 * BissnessLogic.Settings.TimeReproductionOfBalls; //Берем время "репродукции"

        //vPrew = new Vector3(0, 0, 0);
        v = new Vector3(Random.Range(-5, 5), -5, 0); //Задаём начальное вектор скорости для шарика
        //gameObject.GetComponent<Rigidbody>().AddForce(v, ForceMode.Impulse);

        rb = GetComponent<Rigidbody>(); //Берём Rigidbody
        Invoke("PushAndCountTileLife", 2f); //толкаем и запускаем таймер жизни шарика (он должен исчезнуть потом)
    }

    
    void FixedUpdate ()
    {
        if (StartAcceleration == 500) { rb.velocity += (0.2f * rb.velocity); StartAcceleration = 0; } //Это костыль, надо как-то сделать так, чтобы шарик сам не останавливался. Вот как я это сделал.
        if (TimeReproductionOfBalls <= 0) //Пора размножать шарики
        {
            if (MainScript.TrueAmountOfBalls < MainScript.AmountOfBalls) //Если их меньше, чем их максимальное количество, то заспавнить
            {
                Instantiate(gameObject, gameObject.transform.position, gameObject.transform.rotation); //спавним
                MainScript.TrueAmountOfBalls++; //Увеличиваем их реальное количество
            }
            TimeReproductionOfBalls = 50 * BissnessLogic.Settings.TimeReproductionOfBalls; //Обновляем таймер репродукции
        }

        /*(Comment) It is plab B*/
        /*if (StartUpSpeed == true) { timer++; rb.velocity -= BackU; }
        if (timer == 100) { StartUpSpeed = false;  }

        if (StopAcceleration > 0)
        {
            v = gameObject.transform.position;
            gameObject.GetComponent<Rigidbody>().AddForce((new Vector3(v.x - vPrew.x, v.y - vPrew.y, v.z - vPrew.z)) * 5, ForceMode.Force);
            vPrew = gameObject.transform.position;
            StopAcceleration--;
        }*/


        TimeReproductionOfBalls--; //Умельшаем таймер репродукции
        StartAcceleration++; //Увеличиваем таймер, когда снова ускорить, чтобы не замедлился
    }

    private void PushAndCountTileLife()
    {
        rb.velocity += v; //Толкаем
        Invoke("Kill", 13f); //И запускаем таймер жизни шарика
    }
    private void Kill()
    {
        Destroy(gameObject); //Убиваем шарик
        MainScript.TrueAmountOfBalls--; //Уменьшаем их реальное количество
    }

    public void UpSpeed() //Этот метод надо вызвать извне. Он увиличивает на время скорость шарика в 2 раза.
    {
        //timer = 0;
        //Debug.Log(123);
        //gameObject.GetComponent<Rigidbody>().AddForce((new Vector3((v.x - vPrew.x), (v.y - vPrew.y), (v.z - vPrew.z))) * UofSpeedUp, ForceMode.Impulse);
        //rb.AddForce(gameObject.transform.forward * UofSpeedUp);
        rb.velocity += rb.velocity; //Увеличиваем скорость в 2 раза

        Invoke("BackAcceleration", 2f); //Вернём её назад через 2 сек.
    }

    private void BackAcceleration()
    {
        rb.velocity = (0.5f * rb.velocity); //Возврашаем скорость назад
    }

    void OnTriggerEnter(Collider other) //Если коснётся дна, то удавить шарик
    {
        if (other.tag == "dno")
        {
            Kill();
            MainScript.LifesLeft--; //Это счётчик жизней изрока уменьшается.
        }
    }


}
