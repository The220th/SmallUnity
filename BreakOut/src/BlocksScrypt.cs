using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlocksScrypt : MonoBehaviour {

    int Type; //Тип блока
    /*
     1 = standart
     2 = bonus
     3 = frezzer
     4 = Speedup
      */
    int rand;
	//Материалы для типов блоков
    public Material StandartM; 
    public Material BonusM;
    public Material FreezeM;
    public Material SpeedM;
    private GameObject Paddle;

    public static int AmountOfBlocks = 63; //Сколько будет блоков
    
    void Start () {
        rand = Random.Range(0, 100); //Выбор типа блока
        if (rand >= 0  && rand < 70) { Type = 1; gameObject.GetComponent<Renderer>().material = StandartM; }
        if (rand >= 70 && rand < 90) { Type = 2; gameObject.GetComponent<Renderer>().material = BonusM; }
        if (rand >= 90 && rand < 95) { Type = 3; gameObject.GetComponent<Renderer>().material = FreezeM; }
        if (rand >= 95 && rand <100) { Type = 4; gameObject.GetComponent<Renderer>().material = SpeedM; }

        Paddle = GameObject.FindGameObjectWithTag("paddle"); //Ищем Paddle
    }

    void OnCollisionEnter(Collision other) //Если мяч коснулся блока
    {
        if (other.gameObject.tag == "ball")
        {
            switch(Type)
            { // MainScript.Score - это счём игрока
                case 1: { MainScript.Score++; break; }
                case 2: { MainScript.Score += 2; break; }
                case 3: { MainScript.Score += 5; Paddle.gameObject.GetComponent<PaddleScript>().SlowPaddle(); break; } //Тут вызываем метод из Paddle
                case 4: { MainScript.Score += 5; other.gameObject.GetComponent<BallScript>().UpSpeed() ; break; } //Тут вызываем метод из шара
            }
            Destroy(gameObject); //Убираем блок
            AmountOfBlocks--; //Уменьшаем их количество
        }
    }

}
