using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInstPlayer : MonoBehaviour
{

    public int score = 0;

    public void Start()
    {
        StartCoroutine(time());
    }

    public void Update()
    {
        
    }

    IEnumerator time()
    {
        while (true)
        {
            timeCount();
            yield return new WaitForSeconds(1);
        }
    }

    void timeCount()
    {
        score += 1;
    }
}
