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
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(movepoint());
    }

    public void Add(List<GameObject> s)
    {
        foreach(GameObject route in s)
        {
            routepoints.Add(route);
        }
    }

    IEnumerator movepoint()
    {
        foreach(var routepoint in routepoints)
        {
            isconmplete = false;
            float length = Vector2.Distance(transform.position,routepoint.transform.position);
            float time = Mathf.Abs(length) / speed;
            gameObject.transform.DOMove(routepoint.transform.position, time / 5).SetEase(Ease.Linear).OnComplete(() => isconmplete = true);
            yield return new WaitUntil(() => isconmplete);
        }
        StartCoroutine(movepoint());
    }


    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
    }
}
