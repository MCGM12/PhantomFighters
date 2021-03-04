using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class JoinRoom : MonoBehaviour
{
    public float numPlayers;
    string url = "http://vgdapi.basmati.org/gets4.php?groupid=pm38&row=0";
    
    void Start()
    {
        string m_Path;
        m_Path = Application.dataPath;
        Debug.Log("data path: " + m_Path);
    }

    public void JoinRoom1()
    {
        Debug.Log("Calling website");
        StartCoroutine(GetRequest(url));
    }

    private void GetCurrentPlayers(string webText)
    {
        //Data comes in ROW,DATA
        string[] webData = webText.Split(',');
        numPlayers = float.Parse(webData[1]);
        Debug.Log(numPlayers.ToString());
    }

    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            if (webRequest.isNetworkError)
            {
                Debug.Log(pages[page] + ": Error: " + webRequest.error);
            }
            else
            {
                Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                GetCurrentPlayers(webRequest.downloadHandler.text);

            }
        }


    }
}
