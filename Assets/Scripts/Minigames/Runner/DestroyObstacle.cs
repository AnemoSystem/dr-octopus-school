using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObstaclee : MonoBehaviour
{
      void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("ObstacleRunner"))
        {
            Destroy(gameObject);
        }
    }
}
