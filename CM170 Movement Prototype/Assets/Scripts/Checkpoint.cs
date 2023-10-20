using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    CheckpointHandler checkpointHandler;

    void OnTriggerEnter2D(Collider2D collider) {
        checkpointHandler.CheckpointCrossed(this);
    }

    public void SetCheckpointHandler(CheckpointHandler checkpointHandler) {
        this.checkpointHandler = checkpointHandler;
    }
}
