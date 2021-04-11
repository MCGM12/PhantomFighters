using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueScript : MonoBehaviour
{
    public float Statue1Timer;
    public float statTimer;
    public bool timer;
    public bool timerActive;
    public Transform stat;
    public Transform player;

    void Start()
    {
        statTimer = 5f;
        stat = this.transform;
    }

    void Update()
    {
        stat = this.transform;
        //transform.position = player.position;
        if (timer == false)
        {
            timer = true;
            StartCoroutine(Timer());
        }
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(statTimer);
        timer = false;
        if(timerActive)transform.position = player.position;
    }

 
}
