using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FinishHandler : MonoBehaviour
{
    DateTime lapStart = DateTime.Now;

    TimeSpan fastestLap = TimeSpan.Zero;

    TimeSpan currentLap = TimeSpan.Zero;

    GameObject timer = null;
    // Start is called before the first frame update
    void Start()
    {
        timer = GameObject.Find("Canvas/Time");
        Debug.Log(timer);
    }

    // Update is called once per frame
    void Update()
    {
        currentLap = lapStart.Subtract(DateTime.Now);
        timer.GetComponent<TextMeshPro>().SetText(currentLap.ToString() + " | " + fastestLap.ToString());
    }

    void OnTriggerExit2D() {
        if (currentLap < fastestLap) {
            fastestLap = currentLap;
        }

    }
}
