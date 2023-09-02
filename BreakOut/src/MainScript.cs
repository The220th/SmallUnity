using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainScript : MonoBehaviour {

    float i, j;
    public GameObject TestBlock; //хз что это, не помню уже( (Подключается извне)
    public GameObject Ball; //Это мячик (Подключается извне)
    public Text ScoreUI; //Это текст со счётом (Подключается извне)
    public Text AmountOfBallsUI; //Показывает сколько мячей осталось (Подключается извне)
    public Text GameOver; //Это выводится в случае проигрыша или победы (Подключается извне)
    public GameObject PanelOfGameOver; //Это нужно для public Text GameOver (Подключается извне)
    static public int Score = 0; //Счёт игрока  (Подключается извне)

    static private int TimeSpawnBalls; //Через сколько новый мяч спаунить
    static public int AmountOfBalls; //Сколько их может быть
    static public int TrueAmountOfBalls = 0; //Сколько сейчас мячиков
    static public int LifesLeft; //Сколько жизней осталось

    public GameObject SoundWin; //Музыка победы (Подключается извне)
    public GameObject SoundLose; //Музыка поражения (Подключается извне)
    public GameObject SoundGame; // Музыка в самой игре (Подключается извне)
    GameObject del; //Потом удалить, чтобы public GameObject SoundGame

    bool STOP = false; //Конец игры

    void Start ()
    {
        del = Instantiate(SoundGame, Ball.transform.position, Ball.transform.rotation); //Спауню, чтобы потом удалить(см. строку 70 и 81)

        Score = 0;
        TrueAmountOfBalls = 0;
        for (i = 15; i >= 6; i -= 1.5f) //Спауню все блоки
        {
            for(j = 8; j >= -8; j -= 2) //Спауню все блоки
            {
                Instantiate(TestBlock, new Vector3(j, i, 0f), new Quaternion(0f, 0f, 0f, 0f)); //Спауню все блоки
            }
        }

        LifesLeft = BissnessLogic.Settings.MissedBall; //Сколько жизней у игрока осталось
        TimeSpawnBalls = 50 * BissnessLogic.Settings.TimeSpawnBalls; //через сколько заспавнить новый блок
        AmountOfBalls = BissnessLogic.Settings.AmountOfBalls; // Сколько мячиков всего может быть

        Instantiate(Ball, Ball.transform.position, Ball.transform.rotation); //спауню мячик
        TrueAmountOfBalls++; // Увеличиваю текущее количество мячей
    }
	void FixedUpdate ()
    {
        if (TimeSpawnBalls <= 0) //Если пора спаунить новый мяч
        {
            if (TrueAmountOfBalls < AmountOfBalls) // если их количество позволяем
            {
                Instantiate(Ball, Ball.transform.position, Ball.transform.rotation); //спауню мяч
                TrueAmountOfBalls++;// Увеличиваю текущее количество мячей
            }

            TimeSpawnBalls = 50 * BissnessLogic.Settings.TimeSpawnBalls; //Обновляю таймер спауна мячей
        }

        if (LifesLeft <= 0 && !STOP) //Поражение
        {
            STOP = true; //Конец игры = да
            GameOver.text = "You lose! You get nothing!!! Good day, sir!"; //подготовка сообщения о проигрыше
            PanelOfGameOver.SetActive(true); //включаю панель, на которой будет показано сообщения о проигрыше
            //Score = 0; TrueAmountOfBalls = 0;
            Instantiate(SoundLose, Ball.transform.position, Ball.transform.rotation); //Включаю грусный звук
            Destroy(del); //убираю основную музыку (см. строку 31)
            Invoke("GotoLoadScreen", 5f); //Таймер отсчёта выброса в меню
        }

        if (BlocksScrypt.AmountOfBlocks <= 0 && !STOP)
        {
            STOP = true; //Конец игры = да
            GameOver.text = "NICE!\nYou WIN! \n My congratulations!"; //подготовка сообщения о победе
            PanelOfGameOver.SetActive(true); //включаю панель, на которой будет показано сообщения о победе
            //Score = 0; TrueAmountOfBalls = 0;
            Instantiate(SoundWin, Ball.transform.position, Ball.transform.rotation);//Включаю звук победы
            Destroy(del); //убираю основную музыку (см. строку 31)
            Invoke("GotoLoadScreen", 5f); //Таймер отсчёта выброса в меню
        }

        //Debug.Log(TimeSpawnBalls);
        ScoreUI.text = "Score: " + Score; //Показываю очки игрока
        AmountOfBallsUI.text = "Balls: " + TrueAmountOfBalls + "\nLifes: " + LifesLeft; //Показываю сколько осталось жизней и мячей
        TimeSpawnBalls--; //Уменьшаю таймер спауна мячей

    }

    private void GotoLoadScreen()
    {
        Application.LoadLevel(0); //Переход в меню
    }
}
