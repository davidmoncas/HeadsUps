using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjects : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        RainingPoint rainingPoint = collision.GetComponent<RainingPoint>();
        if (rainingPoint != null) Destroy(rainingPoint);
    }
}
