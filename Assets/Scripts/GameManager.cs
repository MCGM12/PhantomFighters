using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class GameManager : MonoBehaviour
{
    public float N_Player;
    public float N_Player1;
    public float N_Player2;
    public float N_Player3;
    public bool p1;
    public bool p2;
    public bool p3;
    public bool p4;
    
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    
    void Update()
    {
        if (N_Player == 1)
        {
            p1 = true;
        }
        else p1 = false;

        if (N_Player1 == 1)
        {
            p2 = true;
        }
        else p2 = false;

        if (N_Player2 == 1)
        {
            p3 = true;
        }
        else p3 = false;

        if (N_Player3 == 1)
        {
            p4 = true;
        }
        else p4 = false;

    }
}
