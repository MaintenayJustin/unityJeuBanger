using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectedBehaviour : MonoBehaviour
{
    public void Destroy()
    {
        Destroy(gameObject);
    }
}
