using UnityEngine;
using System.Collections;

public class CameraTracking : MonoBehaviour {
    private const float HALFTURN = 180f;

    public Quaternion r;

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

    /// <summary>
    /// The angle that the camera changes on each fixed update while moving.
    /// </summary>
    public float deltaAngle = 120f;

    /// <summary>
    /// Whether or not the camera is moving to the other side.
    /// </summary>
    private bool flipping = false;

    /// <summary>
    /// The total angle moved during movement.
    /// </summary>
    public float angleFlipped = 0f;

    /// <summary>
    /// Use this for initialization.
    /// </summary>
    void Start () {
        transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
        transform.position = new Vector3(0f, 0f, -distance);

        // TODO: If we don't have a tracked object, find the player and track
        // him.
    }
	
    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    void Update () {
        if (Input.GetKeyDown(KeyCode.F) && !flipping) {
            startFlipping();
        }

        flip();
        track();

        if (Input.GetKey(KeyCode.F)) {
            startFlipping();
        }

        r = transform.rotation;
    }

    /// <summary>
    /// FixedUpdate is called at a fixed interval.
    /// </summary>
    void FixedUpdate() {
        // Nothing happening in here!
    }

    /// <summary>
    /// Updates the variables used to track movement.
    /// </summary>
    void startFlipping() {
        flipping = true;
        angleFlipped = 0f;
    }

    /// <summary>
    /// Updates the variables used to track movement and st=naps the camera to its destination.
    /// </summary>
    void stopFlipping() {
        flipping = false;
    }

    /// <summary>
    /// Flips the camera around the scene.
    /// </summary>
    void flip() {
        if (flipping) {
            // Flip 180 degrees.
            if (angleFlipped < HALFTURN
                && transform.rotation.x >= 0f - double.Epsilon
                && transform.rotation.w >= 0f - double.Epsilon) {
                float lastrotation = transform.rotation.x;
                transform.RotateAround(
                    new Vector3(transform.position.x, 0f, 0f),
                    Vector3.right,
                    deltaAngle * Time.deltaTime);
                angleFlipped += deltaAngle * Time.deltaTime;
            }
            else {
                stopFlipping();
            }
        }
        else {
            // Return to flat.
            float lastx = transform.position.x;
            if (transform.rotation.x > 0.5f) {
                // We're upside down.
                transform.rotation = new Quaternion(
                    1f,
                    transform.rotation.y,
                    transform.rotation.z,
                    0f);
                transform.position = new Vector3(lastx, 0f, distance);
            }
            else {
                // we're right side up.

                transform.rotation = new Quaternion(
                    0f,
                    transform.rotation.y,
                    transform.rotation.z,
                    1f);
                transform.position = new Vector3(lastx, 0f, -distance);
            }
        }
    }

    /// <summary>
    /// Tracks the tracked object.
    /// </summary>
    void track() {
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
