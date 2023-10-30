using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SARAADD : MonoBehaviour
{
    [SerializeField] GameObject sara;
    [SerializeField] List<GameObject> route = new List<GameObject>();
    [SerializeField] string[] tagsname;
    [SerializeField] Color[] colors;
    bool isDontAdd;
    int haikinum;
    private void Start()
    {
        StartCoroutine(addSara());
    }

    IEnumerator addSara()
    {
        while (true)
        {
            int RandomTime = Random.Range(3, 6);
            yield return new WaitForSeconds(RandomTime);
            if(isDontAdd == false)
            {
                GameObject saras = Instantiate(sara);
                int tagnum = Random.Range(0, tagsname.Length);
                saras.tag = tagsname[tagnum];
                SpriteRenderer sprite = saras.GetComponent<SpriteRenderer>();
                sprite.color = colors[tagnum];
                SUSHIZARA sushizara = saras.GetComponent<SUSHIZARA>();
                sushizara.Add(route);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.name == "sara(Clone)")
        {
            isDontAdd = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "sara(Clone)")
        {
            SUSHIZARA sushizara = collision.gameObject.GetComponent<SUSHIZARA>();
            float sushitime = sushizara.time;
            if((int)sushitime >= 7200)
            {
                Destroy(collision.gameObject);
                haikinum++;
            }
        }
    }

    void OnApplicationQuit()
    {
        string textname = "./Assets/haiki.txt";
        File.AppendAllText(textname,haikinum.ToString());
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "sara(Clone)")
        {
            isDontAdd = false;
        }
    }
}
