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
        StartCoroutine(UpdateTime());
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
                eatsushicount = 0;
                sushiWant.Clear();
                maxeatcount = 0;
                break;
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

    bool compwrite;
    bool addsushicomp;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        for (int i = 0;i < sushiWant.Count;i++)
        {
            string sushiwantname = sushiWant[i];
            Debug.Log("hit");
            if(i % 2 == 0)
            {
                if (collision.gameObject.CompareTag(sushiwantname) && compwrite == false && addsushicomp == false)
                {
                    compwrite = true;
                    addsushicomp = true;
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
        addsushicomp = false;
        sushiselect();
    }

    void sushiselect()
    {
        if(eatsushicount < maxeatcount)
        {
            int selectsushinum = Random.Range(0, sushiKind.Count * 10);
            if(selectsushinum <= 2)
            {
                sushiWant.Add(sushiKind[9]);
            }
            else if (selectsushinum > 2 && selectsushinum <= 17)
            {
                sushiWant.Add(sushiKind[0]);
            }
            else if(selectsushinum > 17 && selectsushinum <= 32)
            {
                sushiWant.Add(sushiKind[1]);
            }
            else if(selectsushinum > 32 && selectsushinum <= 47)
            {
                sushiWant.Add(sushiKind[2]);
            }
            else if(selectsushinum > 47 && selectsushinum <= 62)
            {
                sushiWant.Add(sushiKind[3]);
            }
            else if (selectsushinum > 62 && selectsushinum <= 74)
            {
                sushiWant.Add(sushiKind[4]);
            }
            else if (selectsushinum > 74 && selectsushinum <= 84)
            {
                sushiWant.Add(sushiKind[5]);
            }
            else if (selectsushinum > 84 && selectsushinum <= 92)
            {
                sushiWant.Add(sushiKind[6]);
            }
            else if (selectsushinum > 92 && selectsushinum <= 97)
            {
                sushiWant.Add(sushiKind[7]);
            }
            else if (selectsushinum > 97 && selectsushinum <= 100)
            {
                sushiWant.Add(sushiKind[8]);
            }
            sushiWant.Add("0");
        }
    }

    void writeTex(string sushiname, string sushitime,string waittime)
    {
        result.Add(sushicount.ToString() + ":" +  sushiname + ":" + waittime, sushitime);
        compwrite = false;
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
