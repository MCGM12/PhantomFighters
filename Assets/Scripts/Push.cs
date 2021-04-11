using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.PlayerConnection;
using UnityEngine.SceneManagement;

public class Push : MonoBehaviour
{
    public bool timer;
    public bool timerActive;
    public float statTimer = 5f;
    public int row;

    GameManager mang;
    
   
    public GameObject Statue;
    public GameObject statue1;
    public GameObject statue2;
    public GameObject statue3;


    string URL = "http://vgdapi.basmati.org/mods4.php";
    string pushURL = "http://vgdapi.basmati.org/gets4.php?groupid=pm38&row=7";
    string pushURL1 = "http://vgdapi.basmati.org/gets4.php?groupid=pm38&row=8";
    string pushURL2 = "http://vgdapi.basmati.org/gets4.php?groupid=pm38&row=9";
    string pushURL3 = "http://vgdapi.basmati.org/gets4.php?groupid=pm38&row=10";
    public string thisPushURL;

    void Start()
    {
        mang = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        statTimer = 5f;
        
        string m_Path;
        //Get the path of the Game data folder
        m_Path = Application.dataPath;

        //Output the Game data path to the console
        Debug.Log("dataPath : " + m_Path);
        if(mang.N_Player == 1)
        {
            thisPushURL = pushURL;
        }
        else if(mang.N_Player == 2)
        {
            thisPushURL = pushURL1;
        }
        else if(mang.N_Player == 3)
        {
            thisPushURL = pushURL2;
        }
        else if(mang.N_Player == 4)
        {
            thisPushURL = pushURL3;
        } else if(mang.N_Player <= 0)
        {
            thisPushURL = pushURL;
        }

    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            SceneManager.LoadScene("Title");
        }
        //transform.position = player.position;
        if (timer == false)
        {
            timer = true;
            StartCoroutine(Timer());
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            Debug.Log("PUSHING DATA....");
            Statue.transform.position = transform.position;
            StartCoroutine(Upload());
            Debug.Log(Statue.transform.position);
        }
        if(Input.GetKeyDown(KeyCode.P))
        {
            StartCoroutine(Pull(thisPushURL, Statue));
        }
        //After timer
        if(timer == false)
        {
            StartCoroutine(Pull(pushURL1, statue1));
            StartCoroutine(Pull(pushURL2, statue2));
            StartCoroutine(Pull(pushURL3, statue3));
        }
    }
    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(statTimer - 1f);
        //if (timerActive) StartCoroutine(Upload()); //transform.position = Statue.position; //WHATEVER YOU WANT TO HAPPEN BASICALLY GOES HERE
        yield return new WaitForSeconds(1f);
        timer = false;
        
    }
    IEnumerator Upload()
    {
        WWWForm form = new WWWForm();
       // form.AddField("myField", "myData");
        form.AddField("groupid", "pm38");
        form.AddField("grouppw", "2yy67vZFEU");
        form.AddField("row", row);
        string statXY = Statue.transform.position.x.ToString() + "|" + Statue.transform.position.y.ToString();

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
    private void MoveStatue(string webText, GameObject statue) //THIS GRABS THE POS FROM THE PULL AND MOVES PLAYER ACCORDINGLY
    {
        Debug.Log("Moving player..." + webText);
        //Data comes in ROW,X|Y
        string[] webData = webText.Split(',');
        string[] pos = webData[1].Split('|');
        float x1 = float.Parse(pos[0]);
        float y1 = float.Parse(pos[1]);
        Debug.Log(x1.ToString() + "," + x1.ToString());
        statue.transform.position = new Vector3(x1, y1, 0);
    }


    IEnumerator Pull(string uri, GameObject statue)
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
                MoveStatue(webRequest.downloadHandler.text, statue);

            }
        }

    }


}
