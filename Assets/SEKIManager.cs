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

    IEnumerator StartCus()
    {
        yield return new WaitForSeconds(100);
        string textname = "./Assets/TextData" + gameObject.name + ".txt";
        File.WriteAllText(textname, "");
        customerNum = Random.Range(0, 7);
        for (int i = 0; i < customerNum; i++)
        {
            sushiselect();
        }
    }

    void Start()
    {
        StartCoroutine(StartCus());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        foreach (string sushiwantname in sushiWant)
        {
            Debug.Log("hit");
            if (collision.gameObject.CompareTag(sushiwantname))
            {
                SUSHIZARA sUSHIZARA = collision.gameObject.GetComponent<SUSHIZARA>();
                writeTex(sushiwantname,((int)sUSHIZARA.time).ToString());
                Destroy(collision.gameObject);
                sushicount++;
                sushiWant.Remove(sushiwantname);
                StartCoroutine(addsushi());
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
    }

    void writeTex(string sushiname, string sushitime)
    {
        result.Add(sushicount.ToString() + ":" +  sushiname, sushitime);
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
