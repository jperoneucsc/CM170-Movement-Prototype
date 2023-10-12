using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    // Components
    TopDownPlayerController TopDownPlayerController;

    // Start is called before the first frame update
    void Start()
    {
        TopDownPlayerController = GetComponent<TopDownPlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 inputVector = Vector2.zero;

        inputVector.x = Input.GetAxis("Horizontal");
        inputVector.y = Mathf.Max(0, Input.GetAxis("Vertical")); // Clamp vertical input to be non-negative for no backward movement

        TopDownPlayerController.SetInputVector(inputVector);
    }
}
