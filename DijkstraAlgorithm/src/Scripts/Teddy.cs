using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teddy : MonoBehaviour
{

    public static bool ForceSort = false;
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "pickup") { Destroy(other.gameObject); }
        MainForWeek1.Pickups[0] = null;
        MainForWeek1.TrueCountOfPickups--;
        ForceSort = true;
        
    }
}
