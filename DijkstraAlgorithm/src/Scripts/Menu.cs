using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour 
{
	public void Start()
	{
		if (GameObject.FindWithTag("Week2Sound") != null)
        {
            Destroy(GameObject.FindWithTag("Week2Sound"));
        }
	}
    public void Week1()
    {
        Application.LoadLevel(1);
    }
    public void Week2()
    {
        Application.LoadLevel(2);
    }
    public void Week3()
    {

    }
    public void Week4()
    {

    }
    public void Quit()
    {
        Application.Quit();
    }
}
