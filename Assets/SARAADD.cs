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
    int haikinum = 0;
    private void Start()
    {
        StartCoroutine(addSara());
    }

    IEnumerator addSara()
    {
        while (true)
        {
            int RandomTime = Random.Range(4, 8);
            int RandomSaraNum = Random.Range(2, 5);
            yield return new WaitForSeconds(RandomTime);
            int tagnum = Random.Range(0, tagsname.Length * 10);
            for (int i = 0;i < RandomSaraNum; i++)
            {
                if (isDontAdd == false)
                {
                    GameObject saras = Instantiate(sara);
                    SpriteRenderer sprite = saras.GetComponent<SpriteRenderer>();
                    if (tagnum <= 2)
                    {
                        saras.tag = tagsname[9];
                        sprite.color = colors[9];
                    }
                    else if (tagnum > 2 && tagnum <= 17)
                    {
                        saras.tag = tagsname[0];
                        sprite.color = colors[0];
                    }
                    else if (tagnum > 17 && tagnum <= 32)
                    {
                        saras.tag = tagsname[1];
                        sprite.color = colors[1];
                    }
                    else if (tagnum > 32 && tagnum <= 47)
                    {
                        saras.tag = tagsname[2];
                        sprite.color = colors[2];
                    }
                    else if (tagnum > 47 && tagnum <= 62)
                    {
                        saras.tag = tagsname[3];
                        sprite.color = colors[3];
                    }
                    else if (tagnum > 62 && tagnum <= 74)
                    {
                        saras.tag = tagsname[4];
                        sprite.color = colors[4];
                    }
                    else if (tagnum > 74 && tagnum <= 84)
                    {
                        saras.tag = tagsname[5];
                        sprite.color = colors[5];
                    }
                    else if (tagnum > 84 && tagnum <= 92)
                    {
                        saras.tag = tagsname[6];
                        sprite.color = colors[6];
                    }
                    else if (tagnum > 92 && tagnum <= 97)
                    {
                        saras.tag = tagsname[7];
                        sprite.color = colors[7];
                    }
                    else if (tagnum > 97 && tagnum <= 100)
                    {
                        saras.tag = tagsname[8];
                        sprite.color = colors[8];
                    }
                    SUSHIZARA sushizara = saras.GetComponent<SUSHIZARA>();
                    sushizara.Add(route);
                }
                yield return new WaitForSeconds(1);
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
