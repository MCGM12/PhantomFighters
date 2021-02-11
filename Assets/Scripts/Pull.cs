using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Pull : MonoBehaviour
{
    public GameObject mainPlayer;

    //https://docs.unity3d.com/ScriptReference/Networking.UnityWebRequest.Get.html
    string URL = "http://vgdapi.basmati.org/gets4.php?groupid=DUMMY&row=7";

    void Start()
    {
        string m_Path;
        //Get the path of the Game data folder
        m_Path = Application.dataPath;

        //Output the Game data path to the console
        Debug.Log("dataPath : " + m_Path);

        // A non-existing page.
        // StartCoroutine(GetRequest("https://error.html"));
    }

    private void OnMouseDown()
    {
        // A correct website page.
        Debug.Log("Calling Website");
        StartCoroutine(GetRequest(URL));
    }

    private void MovePlayer(string webText)
    {
        Debug.Log("Moving player..." + webText);
        //Data comes in ROW,X|Y
        string[] webData = webText.Split(',');
        string[] pos = webData[1].Split('|');
        float x = float.Parse(pos[0]);
        float y = float.Parse(pos[1]);
        Debug.Log(x.ToString() + "," + y.ToString());
        mainPlayer.transform.position = new Vector3(x, y, 0);
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
                MovePlayer(webRequest.downloadHandler.text);
                
            }
        }

        
    }


}
