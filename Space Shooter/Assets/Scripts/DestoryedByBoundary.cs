using UnityEngine;
using System.Collections;

public class DestoryedByBoundary : MonoBehaviour
{
    void OnTriggerExit(Collider other)
    {
        Destroy(other.gameObject);
    }

}
