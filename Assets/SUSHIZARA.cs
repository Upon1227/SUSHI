using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class SUSHIZARA : MonoBehaviour
{
    public List<GameObject> routepoints = new List<GameObject>();
    [SerializeField] float speed;
    bool isconmplete = false;
    public float time;
    float speedbuff = 1;
    public int startnum;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(movepoint(startnum));
    }

    public void Add(List<GameObject> s)
    {
        foreach(GameObject route in s)
        {
            routepoints.Add(route);
        }
    }

    IEnumerator movepoint(int startnum)
    {
        for (int i = startnum; i < routepoints.Count;i++)
        {
            isconmplete = false;
            float length = Vector2.Distance(transform.position, routepoints[i].transform.position);
            float time_a = (Mathf.Abs(length)) / (speed * speedbuff);
            gameObject.transform.DOMove(routepoints[i].transform.position, time_a / 5).SetEase(Ease.Linear).OnComplete(() => isconmplete = true);
            yield return new WaitUntil(() => isconmplete);
        }
        for (int i = 0; i < startnum; i++)
        {
            isconmplete = false;
            float length = Vector2.Distance(transform.position, routepoints[i].transform.position);
            float time_a = (Mathf.Abs(length)) / (speed * speedbuff);
            gameObject.transform.DOMove(routepoints[i].transform.position, time_a / 5).SetEase(Ease.Linear).OnComplete(() => isconmplete = true);
            yield return new WaitUntil(() => isconmplete);
        }
        StartCoroutine(movepoint(startnum));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Seki"))
        {
            SEKIManager seki = collision.gameObject.GetComponent<SEKIManager>();
            bool isenmpty = seki.isEnmpty;
            if (isenmpty == true)
            {
                speedbuff = 1.5f;
            }
            else
            {
                speedbuff = 1f;
            }
        }
        if (collision.gameObject.CompareTag("Safety"))
        {
            speedbuff = 1f;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Seki"))
        {

                speedbuff = 1.5f;
            
        }
    }



    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
    }
}
