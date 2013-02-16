using UnityEngine;
using System.Collections;

public class CameraTracking : MonoBehaviour {

    /// <summary>
    /// The object that the camera is tracking.
    /// </summary>
    public GameObject trackedObject;
    public bool trackingDelay = true;
    public float trackingSpeed = 0.05f;
    public float maxTracking = 5f;

    /// <summary>
    /// The distance the camera stays from the scene.
    /// </summary>
    public float distance = 10f;
    private float lastDistance = 0f;

    /// <summary>
    /// The angle that the camera changes on each fixed update while moving.
    /// </summary>
    public float deltaAngle = 0.01f;
    private float lastDelta = 0f;

    /// <summary>
    /// The distance for the camera to travel in order to resemble an arc.
    /// </summary>
    public float deltaDistance {
        get {
            // Do we need to change the precalculated delta distance?
            if (lastDistance != distance || lastDelta != deltaAngle) {
                lastDelta = distance;
                lastDelta = deltaAngle;
                _deltaDistance = distance * Mathf.Sin(deltaAngle);
            }

            // Return the precalcualted delta distance.
            return _deltaDistance;
        }
    }
    private float _deltaDistance;

    /// <summary>
    /// Whether or not the camera is moving to the other side.
    /// </summary>
    private bool moving = false;

    /// <summary>
    /// The total angle moved during movement.
    /// </summary>
    private float angleMoved = 0f;

    /// <summary>
    /// The side that the camera is on (+ or -).
    /// </summary>
    private int side = -1;

    /// <summary>
    /// Use this for initialization.
    /// </summary>
    void Start () {
        transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
        transform.position = new Vector3(0f, 0f, side * distance);

        // TODO: If we don't have a tracked object, find the player and track
        // him.
    }
	
    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    void Update () {
        if (trackingDelay) {
            // Move faster the farther away the camera is from the tracked object.
            transform.Translate(
                Vector3.left
                * trackingSpeed
                * (transform.position.x - trackedObject.transform.position.x));

            // Clip the distance to the max tracking distance.
            transform.position = new Vector3(
                bound(transform.position.x, trackedObject.transform.position.x, maxTracking),
                transform.position.y,
                transform.position.z);
        }
        else {
            transform.Translate(
                Vector3.left
                * (transform.position.x - trackedObject.transform.position.x));
        }
    }

    /// <summary>
    /// FixedUpdate is called at a fixed interval.
    /// </summary>
    void FixedUpdate() {
        // We don't need to fixed update when we're not flipping.
        if (!moving) {
            return;
        }

        // If camera is not at desired position (rotation isn't pi).
        //   rotate deltaAngle down
        //   move deltaDistance up
        // Else
        //   center camera (0 0 (pi | 0))
        //   force to flat (0 0 distance * side)
        //   stop moving
    }

    /// <summary>
    /// Updates the variables used to track movement.
    /// </summary>
    void startMoving() {
        moving = true;
        angleMoved = 0f;
        side = -side;
    }

    /// <summary>
    /// Updates the variables used to track movement and st=naps the camera to its destination.
    /// </summary>
    void stopMoving() {
        moving = false;
        transform.position = Vector3.forward * distance * side;
    }

    /// <summary>
    /// Limits the value to being within the given deviation from the desired value.
    /// </summary>
    /// <param name="value">The value to bound.</param>
    /// <param name="desired">The desired value to consider.</param>
    /// <param name="deviation">The allowed deviation from the desired value.</param>
    /// <returns>The bounded value.</returns>
    float bound(float value, float desired, float deviation) {
        if (value > desired + deviation) {
            return desired + deviation;
        }

        if (value < desired - deviation) {
            return desired - deviation;
        }

        return value;
    }
}
