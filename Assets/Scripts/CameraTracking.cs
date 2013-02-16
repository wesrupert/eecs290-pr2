using UnityEngine;
using System.Collections;

public class CameraTracking : MonoBehaviour {

    /// <summary>
    /// The object that the camera is tracking.
    /// </summary>
    public GameObject trackedObject;

    /// <summary>
    /// The distance the camera stays from the scene.
    /// </summary>
    public float distance = 10f;
    private float _lastDistance = 0f;

    /// <summary>
    /// The angle that the camera changes on each fixed update while moving.
    /// </summary>
    public float deltaAngle = 0.01f;
    private float _lastDelta = 0f;

    /// <summary>
    /// The distance for the camera to travel in order to resemble an arc.
    /// </summary>
    public float deltaDistance {
        get {
            // Do we need to change the precalculated delta distance?
            if (_lastDistance != distance || _lastDelta != deltaAngle) {
                _lastDelta = distance;
                _lastDelta = deltaAngle;
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
    /// The side that the camera is on (+ or -).
    /// </summary>
    private short side = -1;

    /// <summary>
	/// Use this for initialization.
    /// </summary>
	void Start () {
        transform.position = new Vector3(0, 0, side * distance);
        // TODO: If we don't have a tracked object, find the player and track
        // him.
	}
	
    /// <summary>
    /// Update is called once per frame.
    /// </summary>
	void Update () {
        // TODO: Camera x = trackedObject x
	}

    /// <summary>
    /// FixedUpdate is called at a fixed interval.
    /// </summary>
    void FixedUpdate() {
        // We don't need to fixed update when we're not flipping.
        if (!moving) {
            return;
        }

        // TODO:
        // If camera is not at desired position (rotation isn't pi).
        //   rotate deltaAngle down
        //   move deltaDistance up
        // Else
        //   center camera (0 0 (pi | 0))
        //   force to flat (0 0 distance * side)
        //   stop moving
    }
}
