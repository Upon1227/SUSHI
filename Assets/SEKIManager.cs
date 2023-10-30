using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class SEKIManager : MonoBehaviour
{
    [SerializeField] int customerNum;
    [SerializeField] List<string> sushiKind = new List<string>();
    [SerializeField] List<string> sushiWant = new List<string>();
    [SerializeField] Dictionary<string, string> result = new Dictionary<string, string>();
    int sushicount = 1;
    [SerializeField]
    int eatsushicount = 0;
    [SerializeField]
    int maxeatcount;

    IEnumerator StartCus(int waittime)
    {
        yield return new WaitForSeconds(waittime);
        Debug.Log("Start!");
        string textname = "./Assets/TextData" + gameObject.name + ".txt";
        File.WriteAllText(textname, "");
        customerNum = Random.Range(0, 7);
        maxeatcount = Random.Range(5 * customerNum, 12 * customerNum);
        StartCoroutine(UpdateTime());
        for (int i = 0; i < customerNum; i++)
        {
            sushiselect();
        }
    }

    void Start()
    {
        int waittime = Random.Range(100, 200);
        StartCoroutine(StartCus(waittime));
    }

    IEnumerator re(int waittime)
    {
        Debug.Log("ha!");
        customerNum = 0;
        yield return new WaitForSeconds(waittime);
        customerNum = Random.Range(0, 7);
        maxeatcount = Random.Range(5 * customerNum, 12 * customerNum);
        for (int i = 0; i < customerNum; i++)
        {
            sushiselect();
        }
    }

    IEnumerator UpdateTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            if(eatsushicount >= maxeatcount)
            {
                int waittime = Random.Range(100, 600);
                StartCoroutine(re(waittime));
            }
            for(int i = 0;i < sushiWant.Count; i++)
            {
                if(i % 2 != 0)
                {
                    int time = int.Parse(sushiWant[i]);
                    time++;
                    sushiWant[i] = time.ToString();
                }
            }

        }
    }

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        for (int i = 0;i < sushiWant.Count;i++)
        {
            string sushiwantname = sushiWant[i];
            Debug.Log("hit");
            if(i % 2 == 0)
            {
                if (collision.gameObject.CompareTag(sushiwantname))
                {
                    SUSHIZARA sUSHIZARA = collision.gameObject.GetComponent<SUSHIZARA>();
                    writeTex(sushiwantname, ((int)sUSHIZARA.time).ToString(), sushiWant[i + 1]);
                    Destroy(collision.gameObject);
                    eatsushicount++;
                    sushicount++;
                    sushiWant.RemoveAt(i + 1);
                    sushiWant.Remove(sushiwantname);
                    StartCoroutine(addsushi());
                }

            }
  
        }
       
    }

    IEnumerator addsushi()
    {
        int waittime = Random.Range(5, 15);
        yield return new WaitForSeconds(waittime);
        sushiselect();
    }

    void sushiselect()
    {
        int selectsushinum = Random.Range(0, sushiKind.Count);
        sushiWant.Add(sushiKind[selectsushinum]);
        sushiWant.Add("0");
    }

    void writeTex(string sushiname, string sushitime,string waittime)
    {
        result.Add(sushicount.ToString() + ":" +  sushiname + ":" + waittime, sushitime);
    }

    void OnApplicationQuit()
    {
        string textname = "./Assets/TextData" + gameObject.name + ".txt";
        foreach(string textnum in result.Keys)
        {
            File.AppendAllText(textname, textnum + ":" + result[textnum] + "\n");
        }
    }
}
