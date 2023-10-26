using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class SUSHIZARA : MonoBehaviour
{
    [SerializeField] GameObject[] routepoints;
    [SerializeField] float speed;
    bool isconmplete = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(movepoint());
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
        
    }
}
