using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BissnessLogic
{
    public static float Difficult = 0; //Это значение менятся в Menu.cs

    public static void LoadSettings() //Это вызывается извне (в Menu.cs)
    {
        if (Difficult >= 0 && Difficult < 0.33) { Easy(); }
        if (Difficult > 0.34 && Difficult < 0.66) { Medium(); }
        if (Difficult > 0.67 && Difficult <= 1) { Hard(); }
    }

    public static class Settings //Класс с настройками.
    {
        static public int TimeSpawnBalls; //Через сколько новый шарик заспауниться
        static public int AmountOfBalls; // Сколько шаров вообще может быть
        static public int TimeReproductionOfBalls; //Время репродукции (см. BallScrypt.cs)
        static public int MissedBall;
    }

    private static void Easy()
    {
        Debug.Log("Difficult is Easy. ");
        Settings.TimeSpawnBalls = Random.Range(7, 12);
        Settings.AmountOfBalls = 3;
        Settings.TimeReproductionOfBalls = 10;
        Settings.MissedBall = 15;

    }

    private static void Medium()
    {
        Debug.Log("Difficult is Normal. ");
        Settings.TimeSpawnBalls = Random.Range(5, 9);
        Settings.AmountOfBalls = 5;
        Settings.TimeReproductionOfBalls = 7;
        Settings.MissedBall = 7;
    }

    private static void Hard()
    {
        Debug.Log("Difficult is Hard. ");
        Settings.TimeSpawnBalls = Random.Range(3, 7);
        Settings.AmountOfBalls = 9;
        Settings.TimeReproductionOfBalls = 3;
        Settings.MissedBall = 4;
    }

}
