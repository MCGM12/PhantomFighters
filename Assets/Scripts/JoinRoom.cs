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
            if (numPlayers1 < 4)
            {
                numPlayers1++;
                GameObject.FindWithTag("GameManager").GetComponent<GameManager>().N_Player = numPlayers1;
                currentPlayers.GetComponent<PullPlayerCount>().roomNumber = 0;
                SceneManager.LoadScene("LightingTest");
            } else
            {
                Debug.Log("Room was full");
            }
            form.AddField("s4", numPlayers1.ToString());
        } //else if (roomNum == 1)
        //{
        //    float numPlayers2 = currentPlayers.GetComponent<PullPlayerCount>().numPlayers2;
        //    if (numPlayers2 < 4)
        //    {
        //        numPlayers2++;
        //        currentPlayers.GetComponent<PullPlayerCount>().roomNumber = 1;
        //        SceneManager.LoadScene("LightingTest");
        //    }
        //    else
        //    {
        //        Debug.Log("Room was full");
        //    }
        //    form.AddField("s4", numPlayers2.ToString());
        //} else
        //{
        //    float numPlayers3 = currentPlayers.GetComponent<PullPlayerCount>().numPlayers3;
        //    if (numPlayers3 < 4)
        //    {
        //        numPlayers3++;
        //        currentPlayers.GetComponent<PullPlayerCount>().roomNumber = 2;
        //        SceneManager.LoadScene("LightingTest");
        //    }
        //    else
        //    {
        //        Debug.Log("Room was full");
        //    }
        //    form.AddField("s4", numPlayers3.ToString());
        //}

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
