using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FinishHandler : MonoBehaviour
{
    DateTime lapStart = DateTime.Now;

    TimeSpan fastestLap = TimeSpan.FromMinutes(1);

    TimeSpan currentLap = TimeSpan.Zero;

    GameObject timer = null;

    TextMeshProUGUI textMeshPro = null;

    CheckpointHandler checkpointHandler;
    // Start is called before the first frame update
    void Start()
    {
        timer = GameObject.Find("Canvas/Time");
        textMeshPro = timer.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        currentLap = DateTime.Now.Subtract(lapStart);
        textMeshPro.SetText("Lap time " + currentLap.TotalSeconds/* + " | " + fastestLap.TotalSeconds*/);
    }

    void OnTriggerEnter2D() {
        if(checkpointHandler.ValidFinish()) {
            checkpointHandler.lastCheckpointIndex = 0;
            if (currentLap < fastestLap) {
                fastestLap = currentLap;
            }
            lapStart = DateTime.Now;
            currentLap = TimeSpan.Zero;
            Debug.Log("fastest lap " + fastestLap.TotalSeconds);
        }
        else {
            Debug.Log("checkpoint missed");
        }
    }

    public void SetCheckpointHandler(CheckpointHandler checkpointHandler) {
        this.checkpointHandler = checkpointHandler;
    }
}
