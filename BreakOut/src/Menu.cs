using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour {

    public GameObject PanelOfSettings; //Панель настроек
    public GameObject PanelOfRules; // Панель помощи в управлении

    public void StartGame()
    {
        BissnessLogic.LoadSettings(); //Определение настроек
        Application.LoadLevel(1); //Загрузка игры
    }

    public void DifficultOfGame()
    {
        PanelOfSettings.SetActive(!PanelOfSettings.activeSelf); //Включить панель настроек, если не была велючена
        PanelOfRules.SetActive(false); //Выключить другую панель
    }

    public void QuitFromGame()
    {
        Application.Quit(); //выход из игры
    }

    public void SetDifficultOfGame(float Value)
    {
        BissnessLogic.Difficult = Value; //Меняем сложность извне
    }

    public void ShowHelp()
    {
        PanelOfRules.SetActive(!PanelOfRules.activeSelf); //Включить панель помощи в управлении, если не была велючена
        PanelOfSettings.SetActive(false); //Выключить другую панель
    }
}
