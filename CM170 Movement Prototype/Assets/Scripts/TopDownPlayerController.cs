using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownPlayerController : MonoBehaviour
{

    [Header("Temp Settings")]
    public float accelerationFactor = 10.0f;
    public float turnFactor = 3.5f;     // Bigger the number the quicker the turn
    public float driftFactor = 0.5f;
    public float dragFactor = 5.0f;     // Bigger the number = the less slippery the floor
    public float maxSpeed = 20;
    public AnimationCurve turnFactorCurve = AnimationCurve.Linear(0, 0, 1, 1);     // How much turnFactor scales with velocity. Adjust graph as needed Larger num = more significant scaling = turning less sensitive at higher speeds. 

    // Local variables
    private float accelerationInput = 0;
    private float steeringInput = 0;

    private float rotationAngle = 0;

    private float velocityVsUp = 0;

    // Components
    Rigidbody2D playerRigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody2D = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        ApplyForce();
        ApplySteering();
        KillOrthogonalVelocity();
    }

    void ApplyForce()
    {
        // Calculate how much "forward we are going in terms of the direction of our velocity
        velocityVsUp = Vector2.Dot(transform.up, playerRigidbody2D.velocity);

        // Limit so we cant go faster than the max speed in the "forward" direction
        if (velocityVsUp > maxSpeed && accelerationInput > 0) return;

        // Limit so we cant go faster in any direction while accelerating
        if (playerRigidbody2D.velocity.sqrMagnitude > maxSpeed * maxSpeed && accelerationInput > 0) return;

        // Apply drag if there is no accelerationInput. --------- In the future this drag value will change depending on the surface
        if (accelerationInput == 0)
        {
            playerRigidbody2D.drag = Mathf.Lerp(playerRigidbody2D.drag, dragFactor, Time.fixedDeltaTime * 3);        // Second parameter: Smaller number = More Slippery floor
        } else playerRigidbody2D.drag = 0;

        // Create a vector for the forward force
        Vector2 engineForceVector = transform.up * accelerationInput * accelerationFactor;

        // Apply force and push the sprite forward
        playerRigidbody2D.AddForce(engineForceVector, ForceMode2D.Force);
    }

    void ApplySteering()
    {
        float scaledTurnFactor = turnFactor + (turnFactor * turnFactorCurve.Evaluate(playerRigidbody2D.velocity.magnitude / maxSpeed));

        // Update the rotation angle based on input
        rotationAngle -= steeringInput * scaledTurnFactor;

        // Apply steering by rotating the player object
        playerRigidbody2D.MoveRotation(rotationAngle);
    }

    void KillOrthogonalVelocity()
    {
        Vector2 forwardVelocity = transform.up * Vector2.Dot(playerRigidbody2D.velocity, transform.up);
        Vector2 rightVelocity = transform.right * Vector2.Dot(playerRigidbody2D.velocity, transform.right);

        playerRigidbody2D.velocity = forwardVelocity + rightVelocity * driftFactor;
    }

    public void SetInputVector(Vector2 inputVector)
    {
        steeringInput = inputVector.x;
        accelerationInput = inputVector.y;
    }
}
