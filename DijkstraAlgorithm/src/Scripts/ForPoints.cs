using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForPoints : MonoBehaviour {

    public Material DoneM;
    public static bool Done;
	
    void Start(){
        Done = false;
    }

	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "teddy" && Done == false )
        {
            gameObject.GetComponent<Renderer>().material = DoneM;
            if (MainForWeek2.Way.Count != 0) { MainForWeek2.Way.Pop(); }
            Done = true;
        }
    }
}
