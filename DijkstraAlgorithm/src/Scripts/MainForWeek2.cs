using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainForWeek2 : MonoBehaviour {

    public GameObject Point; //out
    public GameObject Bridge; //out
    public Text Cost; //out
    public Camera cam; //out
    public GameObject Canvas; //out
    public GameObject TeddyObj; //out
    public GameObject Snow; //out
    public GameObject Sound; //out

    static GameObject[] V;
    static int MaxAmountV;
    static GameObject[] E;
    static int AmountE;
    static int[, ] AdjacencyMatrix;
    static int [, ] IncidenceMatrix;
    public static Stack<GameObject> Way;

    static Text[] Costs;

    static Vqp[] Vqps;

    static bool STOP;

    public static bool START;

    private GameObject sound;
    private GameObject snow;
    //private static bool Moved;

    void Start() {
        STOP = false;
        START = false;
        SpawnAllPoint(12);
        MakeAdjacenctMatrix();
        BuildBrigdes();
        ShowIncidenceMatrix();
        DijkstraAlgorithm();
        FindWay();
        Invoke("StartGame", 2f);

        
        if (GameObject.FindWithTag("Week2Sound") == null) { sound = Instantiate(Sound, new Vector3(-50, 102, 70), new Quaternion()); GameObject.DontDestroyOnLoad(sound);}
        if (GameObject.FindWithTag("snow") == null)
        {
            snow = Instantiate(Snow, new Vector3(-44, 77, 30), new Quaternion());
            snow.transform.eulerAngles = new Vector3(90, 180, -180);
			GameObject.DontDestroyOnLoad(snow);
        }
    }
	
	
	void FixedUpdate ()
    {
        if (Way.Count != 0 && START == true)
        {
            MoveTeddyObj(Way.Peek());
        }
        if(ForPoints.Done == true) { BreakBridge(); }
        if(Way.Count == 0 && STOP == false) { STOP = true; Invoke("Quit", 5f); }
        if (Input.GetKey(KeyCode.Escape)) { QuitFromLevel2(); }
    }
    private void MoveTeddyObj(GameObject Target)
    {
        Vector3 v = TeddyObj.transform.position;
        Vector3 a = Target.transform.position;
        v.x = a.x - v.x;
        v.z = a.z - v.z;
        v.y = 0;
        TeddyObj.transform.position += v * Time.deltaTime;
        TeddyObj.transform.forward = v;
    }

    private void BreakBridge()
    {

        ForPoints.Done = false;
    }

    private void SpawnAllPoint(int MaxV)
    {
        int i = 0;
        MaxAmountV = MaxV;
        V = new GameObject[MaxAmountV];
        V[i] = Instantiate(Point, new Vector3(10f, 0f, 0f), new Quaternion());
        int x = -30;
        for (int j = 0; j < 3; j++)
        {
            i++;
            V[i] = Instantiate(Point, new Vector3(-20f, 0f, x), new Quaternion());
            x += 30;
        }
        x = -30;
        for (int j = 0; j < 4; j++)
        {
            i++;
            V[i] = Instantiate(Point, new Vector3(-50f, 0f, x), new Quaternion());
            x += 30;
        }
        x = -30;
        for (int j = 0; j < 3; j++)
        {
            i++;
            V[i] = Instantiate(Point, new Vector3(-80f, 0f, x), new Quaternion());
            x += 30;
        }
        i++;
        V[i] = Instantiate(Point, new Vector3(-110f, 0f, 0), new Quaternion());
    }

    private void MakeAdjacenctMatrix()
    {
        int i, j;
        AdjacencyMatrix = new int[MaxAmountV, MaxAmountV];
        AmountE = 0;

        // set null 
        for (i = 0; i < MaxAmountV; i++)
        {
            for (j = 0; j < MaxAmountV; j++)
            {
                AdjacencyMatrix[i, j] = 777;
            }
        }

        //set 0 on main diagonal
        for (i = 0; i < MaxAmountV; i++)
        {
            AdjacencyMatrix[i, i] = 0;
        }

        //Set Upper Triangular Matrix
        AdjacencyMatrix[0, 1] = 1;
        AdjacencyMatrix[1, 4] = 1;
        AdjacencyMatrix[4, 8] = 1;
        AdjacencyMatrix[8, 11] = 1;
        AdjacencyMatrix[0, 2] = 1;
        AdjacencyMatrix[2, 5] = 1;
        AdjacencyMatrix[5, 9] = 1;
        AdjacencyMatrix[9, 11] = 1;
        AdjacencyMatrix[0, 3] = 1;
        AdjacencyMatrix[3, 6] = 1;
        AdjacencyMatrix[6, 10] = 1;
        AdjacencyMatrix[10, 11] = 1;
        AdjacencyMatrix[3, 7] = 1;
        AdjacencyMatrix[7, 10] = 1;
        AdjacencyMatrix[1, 2] = Random.Range(0, 2);
        AdjacencyMatrix[1, 5] = Random.Range(0, 2);
        AdjacencyMatrix[2, 4] = Random.Range(0, 2);
        AdjacencyMatrix[4, 5] = Random.Range(0, 2);
        AdjacencyMatrix[4, 9] = Random.Range(0, 2);
        AdjacencyMatrix[5, 8] = Random.Range(0, 2);
        AdjacencyMatrix[8, 9] = Random.Range(0, 2);
        AdjacencyMatrix[2, 3] = Random.Range(0, 2);
        AdjacencyMatrix[2, 6] = Random.Range(0, 2);
        AdjacencyMatrix[3, 5] = Random.Range(0, 2);
        AdjacencyMatrix[5, 6] = Random.Range(0, 2);
        AdjacencyMatrix[5, 10] = Random.Range(0, 2);
        AdjacencyMatrix[6, 9] = Random.Range(0, 2);
        AdjacencyMatrix[9, 10] = Random.Range(0, 2);
        AdjacencyMatrix[6, 7] = Random.Range(0, 2);
        for (i = 0; i < MaxAmountV; i++)
        {
            for(j = i; j < MaxAmountV; j++)
            {
                if(i != j && AdjacencyMatrix[i, j] == 777)
                {
                    AdjacencyMatrix[i, j] = 0/*Random.Range(0, 2)*/;
                }
                if(AdjacencyMatrix[i, j] == 1) { AmountE++; }
            }
        }

        //Copy Upper Triangular Matrix in Lower Triangular Matrix
        for (i = 0; i < MaxAmountV; i++)
        {
            for (j = i; j < MaxAmountV; j++)
            {
                AdjacencyMatrix[j, i] = AdjacencyMatrix[i, j];
            }
        }

        /*
        //Show Adjacency Matrix
        for (i = 0; i < MaxAmountV; i++)
        {
            for (j = 0; j < MaxAmountV; j++)
            {
                Debug.Log("M[" + i + ", " + j +"] = " + AdjacencyMatrix[i, j]);
            }
        }
        */
    }

    private void BuildBrigdes()
    {
        int i, j, i_forE = 0; ;
        E = new GameObject[AmountE];
        Costs = new Text[AmountE];
        IncidenceMatrix = new int[MaxAmountV, AmountE];

        //Spawn
        for(i = 0; i < MaxAmountV; i++)
        {
            for(j = i; j < MaxAmountV; j++)
            {
                if(AdjacencyMatrix[i, j] == 1)
                {
                    E[i_forE] = Instantiate(Bridge, GetPosition(V[i], V[j]), new Quaternion());
                    E[i_forE].transform.eulerAngles = new Vector3(0, GetAngle(V[i], V[j]), 0);
                    E[i_forE].transform.localScale = GetScale(V[i], V[j]);
                    IncidenceMatrix[i, i_forE] = 1; IncidenceMatrix[j, i_forE] = 1;

                    Costs[i_forE] = Instantiate(Cost, GetScreenPosition(E[i_forE], V[i]), new Quaternion());

                    i_forE++;
                }
            }
        }

        //Give cost for E
        for(i = 0; i < AmountE; i++)
        {
            Debug.Log(i + "- i: " + Costs[i]);
            //Costs[i] = Instantiate(Cost, GetScreenPosition(E[i]), new Quaternion());
            Costs[i].text = "" + Random.Range(1, 17);
            Costs[i].transform.SetParent(Canvas.transform);
        }

    }

    private float GetAngle(GameObject v1, GameObject v2)
    {
        Vector3 a1 = v1.transform.position;
        Vector3 a2 = v2.transform.position;
        return ( ( Mathf.Atan2( (a2.x-a1.x), (a2.z - a1.z) ) ) * 180 ) / Mathf.PI;
    }
    private Vector3 GetPosition(GameObject v1, GameObject v2)
    {
        Vector3 a1 = v1.transform.position;
        Vector3 a2 = v2.transform.position;
        return new Vector3(0.5f*(a1.x+a2.x), 0, 0.5f * (a1.z + a2.z));
    }
    
    private Vector3 GetScale(GameObject v1, GameObject v2)
    {
        Vector3 a1 = v1.transform.position;
        Vector3 a2 = v2.transform.position;
        return new Vector3(3f, 0.5f, Mathf.Sqrt( Mathf.Pow((a1.x-a2.x), 2) + Mathf.Pow((a1.z - a2.z), 2)) );
    }

    private Vector3 GetScreenPosition(GameObject Bridge, GameObject Point)
    {
        Vector3 vB = Bridge.transform.position;
        Vector3 vP = Point.transform.position;
        Vector3 v = new Vector3();
        v.x = 0.5f*(vB.x + vP.x);
        v.y = vB.y;
        v.z = 0.5f * (vB.z + vP.z);
        v.x -= 11;
        return cam.WorldToScreenPoint(v);

    }

    private void ShowIncidenceMatrix()
    {
        int i, j;
        for (i = 0; i < MaxAmountV; i++)
        {
            for(j = i; j < MaxAmountV; j++)
            {
                if (AdjacencyMatrix[i, j] == 1)
                {
                    Debug.Log("Between " + (i + 1) + " and " + (j + 1) + ": " + FindEBetween(i, j) + ". His Cost is " + System.Convert.ToInt32(Costs[FindEBetween(i, j)].text) );
                }
            }
        }
        
    }
    private int FindEBetween(int v1, int v2)
    {
        int it = 777, j;
        for(j = 0; j < AmountE; j++)
        {
            if(IncidenceMatrix[v1, j] == 1 && IncidenceMatrix[v2, j] == 1)
            {
                it = j;
                break;
            }
        }
        return it;
    }

    private void DijkstraAlgorithm()
    {
        int i, j;
        Vqps = new Vqp[MaxAmountV];

        //start
        Vqps[0] = new Vqp(V[0]); // Пусть V[0] - это начальная точка
        Vqps[0].Cost = 0;
        for(i =1; i < MaxAmountV; i++)
        {
            Vqps[i] = new Vqp(V[i]);
        }

        //Main
        int Next;
        for(i = 0; i < MaxAmountV; i++)
        {
            Next = FindMinCost(Vqps); //Ищу с минимальной стоимостью
            Vqps[Next].Done = true; // Когда выбрал, то сказать, что всё - точка больше не изменяется
            //Debug.Log("See " + (Next) + ": " + Vqps[Next].Cost + ". Her where " + Vqps[Next].Where);
            for (j = 0; j < MaxAmountV; j++) //Рассматриваю все точки
            {
                if(AdjacencyMatrix[Next, j] == 1 && j != Next && Vqps[j].Done == false) // выбираю те, с которыми можно соединиться и которые не были ранее рассмотрены
                {

                        if (Vqps[j].Cost > Vqps[Next].Cost + System.Convert.ToInt32(Costs[FindEBetween(Next, j)].text)) //Если новая стоимость меньше
                        {
                            Vqps[j].Cost = Vqps[Next].Cost + System.Convert.ToInt32(Costs[FindEBetween(Next, j)].text); //То переприсваиваю её
                            Vqps[j].Where = Vqps[Next].Main;
                            Vqps[j].Prew = Next;
                        }                        
                    
                }
            }
            

        }
        Debug.Log(Vqps[11].Cost);


    }

    private void FindWay()
    {
        int i = 11;
        Way = new Stack<GameObject>();
        while(Vqps[i].Where != null)
        {
            Way.Push(Vqps[i].Main);
            i = Vqps[i].Prew;
        }
        Way.Push(Vqps[i].Main);
    }
    private void ShowWay()
    {
        Stack<GameObject> Buff = Way;
        GameObject buff;
        Vector3 v = new Vector3(0f, 0f, 0f);
        while(Buff.Count != 0)
        {
            buff = Buff.Pop();
            v = buff.transform.position;
            Debug.Log("-" + v.x + " " + v.z);
        }
    }

    private int FindMinCost(Vqp[] Vqps)
    {
        int i;
        float min = 999999999;
        int nmin = -1;
        for(i = 0; i < MaxAmountV; i++)
        {
            if(Vqps[i].Cost < min && Vqps[i].Done == false) { min = Vqps[i].Cost; nmin = i; }
        }
        return nmin;
    }

    class Vqp
    {
        public GameObject Main;
        public GameObject Where;
        public int Prew;
        public int Cost;
        public bool Done;
        public Vqp(GameObject V)
        {
            Main = V;
            Cost = 999999999; //Типа бесконечность
            Where = null;
            Prew = 777;
            Done = false;
        }
    }

    private void StartGame()
    {
        START = true;
    }
    private void Quit()
    {
        if (snow != null) { GameObject.DontDestroyOnLoad(snow); }
        if (sound != null) { GameObject.DontDestroyOnLoad(sound); }
        Application.LoadLevel(2);
    }
    private void QuitFromLevel2()
    {
        Destroy(snow);
        Destroy(sound);
        Application.LoadLevel(0);
        //SceneManager.LoadScene(0);
        
    }
        

}
