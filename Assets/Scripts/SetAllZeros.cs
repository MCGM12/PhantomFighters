using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SetAllZeros : MonoBehaviour
{
    string URL = "http://vgdapi.basmati.org/mods4.php";

    void Start()
    {
        StartCoroutine(Upload());
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha0))
        {
            StartCoroutine(Upload());
        }
    }

    IEnumerator Upload()
    {
        for (int i = 0; i <= 1000; i++)
        {
            WWWForm form = new WWWForm();
            // form.AddField("myField", "myData");
            form.AddField("groupid", "pm38");
            form.AddField("grouppw", "2yy67vZFEU");
            form.AddField("row", i);

            form.AddField("s4", "0");
            Debug.Log(i);

            using (UnityWebRequest www = UnityWebRequest.Post(URL, form))
            {
                yield return www.SendWebRequest();

                if (www.isNetworkError || www.isHttpError)
                {
                    Debug.Log(www.error);
                }
            }
        }
        Debug.Log("All reset to zeroes");
    }
}
