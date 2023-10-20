using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointHandler : MonoBehaviour
{
    private List<Checkpoint> checkpointList;
    int nextCheckpointIndex;
    public int lastCheckpointIndex;

    void Awake() {
        Transform checkpointsTransform = transform.Find("Checkpoints");
        Transform finishTransform = transform.Find("Finish Line");

        FinishHandler finishLine = finishTransform.GetComponent<FinishHandler>();
        finishLine.SetCheckpointHandler(this);

        checkpointList = new List<Checkpoint>();
        foreach(Transform checkpointTransform in checkpointsTransform) {
            Checkpoint checkpoint = checkpointTransform.GetComponent<Checkpoint>();
            checkpoint.SetCheckpointHandler(this);
            checkpointList.Add(checkpoint);
        }

        nextCheckpointIndex = 0;
    }

    public void CheckpointCrossed(Checkpoint checkpoint) {
        if(nextCheckpointIndex == checkpointList.IndexOf(checkpoint)) {
            lastCheckpointIndex = nextCheckpointIndex;
            nextCheckpointIndex = (nextCheckpointIndex + 1) % checkpointList.Count;
        }
    }

    public bool ValidFinish() {
        return lastCheckpointIndex == checkpointList.Count - 1;
    }
}
