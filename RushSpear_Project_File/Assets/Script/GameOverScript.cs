using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScript : MonoBehaviour
{
    public TimeScript timeSc;

    private void OnCollisionEnter(Collision collision)
    {
        if (timeSc.time > 20)
        {
            timeSc.time = 20.0f;
        }
    }
}
