using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class Push : MonoBehaviour
{
    public bool timer;
    public bool timerActive;
    public float statTimer = 5f;
    public int row;

    GameManager mang;
    
   
    public GameObject Statue; //player statue
    public GameObject statue1; //statue of player 1
    public GameObject statue2; //statue of player 2
    public GameObject statue3; //statue of player 3

    
    string URL = "http://vgdapi.basmati.org/mods4.php";
    string pullURL = "http://vgdapi.basmati.org/gets4.php?groupid=pm38&row=100";
    string pullURL1 = "http://vgdapi.basmati.org/gets4.php?groupid=pm38&row=101";
    string pullURL2 = "http://vgdapi.basmati.org/gets4.php?groupid=pm38&row=102";
    string pullURL3 = "http://vgdapi.basmati.org/gets4.php?groupid=pm38&row=103";
    public string thisPullURL; //finds the correct URL to push and pull from.

    void Start()
    {
       
        mang = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        statTimer = 5f;
        
        string m_Path;
        //Get the path of the Game data folder
        m_Path = Application.dataPath;

        //Output the Game data path to the console
        Debug.Log("dataPath : " + m_Path);

        //Finds what player slot you are, adjusts things accordingly.
        if(mang.p1)
        {
            row = 100;
            thisPullURL = pullURL;
        }
        if (mang.p2)
        {
            row = 101;
            thisPullURL = pullURL1;
        }
        if (mang.p3)
        {
            row = 102;
            thisPullURL = pullURL2;
        }
        if (mang.p4)
        {
            row = 103;
            thisPullURL = pullURL3;
        }



    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            SceneManager.LoadScene("Title");
        }
        transform.position = this.transform.position;
        if (timer == false)
        {
            timer = true;
            StartCoroutine(Timer());
        }
        // Push statue location
        if (Input.GetKeyDown(KeyCode.U)) //Pull after pushing as well
        {
            Debug.Log("PUSHING DATA....");
            Statue.transform.position = transform.position;
            StartCoroutine(Upload());
            Debug.Log(Statue.transform.position);

            // Pull other statue locations
            StartCoroutine(Pull(thisPullURL, Statue));
            StartCoroutine(Pull(pullURL1, statue1));
            StartCoroutine(Pull(pullURL2, statue2));
            StartCoroutine(Pull(pullURL3, statue3));
        }
        if(Input.GetKeyDown(KeyCode.P))
        {
            StartCoroutine(Pull(thisPullURL, Statue));
        }
        //After timer
        if(timer == false)
        {
            //StartCoroutine(Pull(pullURL1, statue1));
            //StartCoroutine(Pull(pullURL2, statue2));
            //StartCoroutine(Pull(pullURL3, statue3));
        }
    }
    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(statTimer - 0.5f);
        if (timerActive) StartCoroutine(Upload()); //transform.position = Statue.position; //WHATEVER YOU WANT TO HAPPEN BASICALLY GOES HERE
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(Pull(thisPullURL, Statue));
        StartCoroutine(Pull(pullURL1, statue1));
        StartCoroutine(Pull(pullURL2, statue2));
        StartCoroutine(Pull(pullURL3, statue3));
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
