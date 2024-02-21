using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{

    void Update()
    {
        transform.Rotate(0, 0.02f, 1);
    }
}
