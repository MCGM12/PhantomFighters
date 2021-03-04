using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Push : MonoBehaviour
{
    public GameObject statue;

    string URL = "http://vgdapi.basmati.org/mods4.php";

    void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("PUSHING DATA....");
            statue.transform.position = transform.position;
            StartCoroutine(Upload());
            Debug.Log(statue.transform.position);
        }
    }

    IEnumerator Upload()
    {
        WWWForm form = new WWWForm();
       // form.AddField("myField", "myData");
        form.AddField("groupid", "pm38");
        form.AddField("grouppw", "2yy67vZFEU");
        form.AddField("row", 7);
        string statXY = statue.transform.position.x.ToString() + "|" + statue.transform.position.y.ToString();

        form.AddField("s4", statXY);

        using (UnityWebRequest www = UnityWebRequest.Post(URL, form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
            }
        }
    }
}
