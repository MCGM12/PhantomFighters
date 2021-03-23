using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueScript : MonoBehaviour
{
    public float Statue1Timer;
    public float statTimer;
    public bool timer;
    public Transform stat;
    public Transform player;

    void Start()
    {
        statTimer = 5f;
    }

    void Update()
    {
        if(timer == false)
        {
            timer = true;
            StartCoroutine("Timer(statTimer)");
        }
    }

    private IEnumerable Timer(float time)
    {
        yield return new WaitForSeconds(time);
        stat.position = player.position;
        timer = false;
    }

 
}
