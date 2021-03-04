﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class PullPlayerCount : MonoBehaviour
{
    public float numPlayers1;
    public float numPlayers2;
    public float numPlayers3;
    string url1 = "http://vgdapi.basmati.org/gets4.php?groupid=pm38&row=0";
    string url2 = "http://vgdapi.basmati.org/gets4.php?groupid=pm38&row=1";
    string url3 = "http://vgdapi.basmati.org/gets4.php?groupid=pm38&row=2";

    void Start()
    {
        string m_Path;
        m_Path = Application.dataPath;
        Debug.Log("data path: " + m_Path);
        UpdatePlayerCount();
    }

    public void UpdatePlayerCount()
    {
        Debug.Log("Calling website");
        StartCoroutine(GetRequest());
    }

    private void GetCurrentPlayers1(string webText)
    {
        //Data comes in ROW,DATA
        string[] webData = webText.Split(',');
        //if (webData[1] is null)
        //    Debug.Log("Was null");
        //webData[1] = "0";
        numPlayers1 = float.Parse(webData[1]);
        Debug.Log("1: " + numPlayers1.ToString());
    }

    private void GetCurrentPlayers2(string webText)
    {
        string[] webData = webText.Split(',');
        //if (webData[1] is null)
        //    Debug.Log("Was null");
        //    webData[1] = "0";
        numPlayers2 = float.Parse(webData[1]);
        Debug.Log("2: " + numPlayers2.ToString());
    }

    private void GetCurrentPlayers3(string webText)
    {
        string[] webData = webText.Split(',');
        //if (webData[1] is null)
        //    Debug.Log("Was null");
        //webData[1] = "0";
        numPlayers3 = float.Parse(webData[1]);
        Debug.Log("3: " + numPlayers3.ToString());
    }


    IEnumerator GetRequest()
    {
        //Room 1
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url1))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = url1.Split('/');
            int page = pages.Length - 1;

            if (webRequest.isNetworkError)
            {
                Debug.Log(pages[page] + ": Error: " + webRequest.error);
            }
            else
            {
                Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                GetCurrentPlayers1(webRequest.downloadHandler.text);

            }
        }

        //Room 2
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url2))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = url2.Split('/');
            int page = pages.Length - 1;

            if (webRequest.isNetworkError)
            {
                Debug.Log(pages[page] + ": Error: " + webRequest.error);
            }
            else
            {
                Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                GetCurrentPlayers2(webRequest.downloadHandler.text);

            }
        }

        // Room 3
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url3))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = url3.Split('/');
            int page = pages.Length - 1;

            if (webRequest.isNetworkError)
            {
                Debug.Log(pages[page] + ": Error: " + webRequest.error);
            }
            else
            {
                Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                GetCurrentPlayers3(webRequest.downloadHandler.text);

            }
        }
    }
}
