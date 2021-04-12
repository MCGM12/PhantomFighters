using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class JoinRoom : MonoBehaviour
{
    public GameObject currentPlayers;
    string url = "http://vgdapi.basmati.org/mods4.php";
    int player;

    public void JoinGame(float roomNum)
    {
        Debug.Log("Calling website");
        currentPlayers.GetComponent<PullPlayerCount>().UpdatePlayerCount();
        StartCoroutine(Upload(roomNum));
    }

    IEnumerator Upload(float roomNum)
    {
        WWWForm form = new WWWForm();
        // form.AddField("myField", "myData");
        form.AddField("groupid", "pm38");
        form.AddField("grouppw", "2yy67vZFEU");
        form.AddField("row", roomNum.ToString());

        if (roomNum == 0)
        {
            float numPlayers1 = currentPlayers.GetComponent<PullPlayerCount>().numPlayers1;
            if (numPlayers1 < 1)
            {
                numPlayers1++;
                //GameObject.FindWithTag("GameManager").GetComponent<GameManager>().N_Player = numPlayers1;
                currentPlayers.GetComponent<PullPlayerCount>().roomNumber = 0;
                SceneManager.LoadScene("LightingTest");
            } else
            {
                Debug.Log("Room was full");
            }
            form.AddField("s4", numPlayers1.ToString());
        } 
        if (roomNum == 1)
        {
            float numPlayers2 = currentPlayers.GetComponent<PullPlayerCount>().numPlayers2;
            if (numPlayers2 < 1)
            {
                numPlayers2++;
                //GameObject.FindWithTag("GameManager").GetComponent<GameManager>().N_Player = numPlayers1;
                currentPlayers.GetComponent<PullPlayerCount>().roomNumber = 1;
                SceneManager.LoadScene("LightingTest");
            }
            else
            {
                Debug.Log("Room was full");
            }
            form.AddField("s4", numPlayers2.ToString());
        }
        if (roomNum == 2)
        {
            float numPlayers3 = currentPlayers.GetComponent<PullPlayerCount>().numPlayers3;
            if (numPlayers3 < 1)
            {
                numPlayers3++;
                //GameObject.FindWithTag("GameManager").GetComponent<GameManager>().N_Player = numPlayers1;
                currentPlayers.GetComponent<PullPlayerCount>().roomNumber = 2;
                SceneManager.LoadScene("LightingTest");
            }
            else
            {
                Debug.Log("Room was full");
            }
            form.AddField("s4", numPlayers3.ToString());
        }
        if (roomNum == 3)
        {
            float numPlayers4 = currentPlayers.GetComponent<PullPlayerCount>().numPlayers4;
            if (numPlayers4 < 1)
            {
                numPlayers4++;
                //GameObject.FindWithTag("GameManager").GetComponent<GameManager>().N_Player = numPlayers1;
                currentPlayers.GetComponent<PullPlayerCount>().roomNumber = 3;
                SceneManager.LoadScene("LightingTest");
            }
            else
            {
                Debug.Log("Room was full");
            }
            form.AddField("s4", numPlayers4.ToString());
        }

        currentPlayers.GetComponent<PullPlayerCount>().UpdatePlayerCount();
        //form.AddField("s4", "");

        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
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
