using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainForWeek1 : MonoBehaviour {

    public GameObject TeddyObj;
    public GameObject pickup;
    public static GameObject[] Pickups;
    public static int AmountPickups;
    private static int timer = 50;
    private static int MaxPickups = 20;
    public static int TrueCountOfPickups;
    


    Vector3 v;
    Quaternion Phi;
    void Start () {
        TrueCountOfPickups = 0;

        v = new Vector3(0f, 0f, 0f);
        Phi = new Quaternion(0f, 0f, 0f, 0f);
        v.y = 1;

        Pickups = new GameObject[MaxPickups];
        for(int i = 0; i < MaxPickups; i++)
        {
            Pickups[i] = null;
            Debug.Log(Pickups[i]);
        }
	}
	
	void FixedUpdate () {
        if(Teddy.ForceSort == true) { SortPickup(); Teddy.ForceSort = true; }
		if(TrueCountOfPickups < MaxPickups && timer <= 0)
        {
            SpawnPickup();
            timer = 50;

            //---------------
            Debug.Log("Start");
            Vector3 v;
            for (int i = 0; i < MaxPickups; i++)
            {
                if (Pickups[i] != null)
                {
                    v = Pickups[i].transform.position;
                    Debug.Log( (i+1) + ": " + v.x + " " + v.z + " " + GetL(Pickups[i]) );
                }
                else { Debug.Log((i+1) + ": null"); }
            }
            //-----------

        }

        MoveTeddyObj();

        if (Input.GetKey(KeyCode.Escape)){ QuitFromLevel1(); }

        timer--;
	}

    public void SortPickup()
    {
        GameObject buff;
        float min = 9999999999;
        int i, j, nmin = MaxPickups-1;
        float Lbuff;
        for (i = 0; i < MaxPickups-1; i++)
        {
            for (j = i; j < MaxPickups; j++)
            {
                if (Pickups[j] == null) { Lbuff = 9999999999; }
                else { Lbuff = GetL(Pickups[j]); }
                if(Lbuff < min) { min = Lbuff; nmin = j; }
            }
            buff = Pickups[i];
            Pickups[i] = Pickups[nmin];
            Pickups[nmin] = buff;
            min = 9999999999;
            nmin = MaxPickups-1;

        }
    } 

    private float GetL(GameObject obj)
    {
        Vector3 v = obj.transform.position;
        Vector3 a = TeddyObj.transform.position;
        return Mathf.Sqrt(Mathf.Pow(a.x - v.x, 2) + Mathf.Pow(a.z - v.z, 2));
    }

    private void MoveTeddyObj()
    {
        if (Pickups[0] != null)
        {
            Vector3 v = TeddyObj.transform.position;
            Vector3 a = Pickups[0].transform.position;
            v.x = a.x - v.x;
            v.z = a.z - v.z;
            v.y = 0;
            TeddyObj.transform.position += v * Time.deltaTime;
            TeddyObj.transform.forward = v;
            SortPickup();
        }
    }

    private void SpawnPickup()
    {
        v.x = Random.Range(-23, 23);
        v.z = Random.Range(-23, 23);

        Pickups[MaxPickups-1] = Instantiate(pickup, v, Phi) as GameObject;
        TrueCountOfPickups++;

        SortPickup();
    }

    private void QuitFromLevel1()
    {
        Application.LoadLevel(0);
    }

}
