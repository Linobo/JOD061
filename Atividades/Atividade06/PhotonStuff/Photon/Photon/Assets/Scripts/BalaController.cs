using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaController : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 1);
    }

    void Update()
    {
        transform.Translate(0, 0, Time.deltaTime * 20f);
    }

    public void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
