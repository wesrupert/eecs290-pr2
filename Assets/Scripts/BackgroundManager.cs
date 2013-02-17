/// Wes Rupert - wkr3
/// EECS 290   - Project 02
/// Purgatory  - BackgroundManager.cs
/// Script to control the tiling of the background.

// Credit for design goes to:
// http://catlikecoding.com/unity/tutorials/runner/

using UnityEngine;
using System.Collections.Generic;

public class BackgroundManager : MonoBehaviour {
    /// <summary>
    /// The object the tiles track for tiling.
    /// </summary>
    public MonoBehaviour trackedObject;

    /// <summary>
    /// The prefab for the background tile.
    /// </summary>
    public Transform prefab;

    /// <summary>
    /// The size of the bacground tiles.
    /// </summary>
    public float size;

    /// <summary>
    /// The offset after which a tile recycles.
    /// </summary>
    public float recycleOffset;

    private float lastPosition, nextPosition;
    private Queue<Transform> objectQueue;

    /// <summary>
    /// Use this for initialization
    /// </summary>
    void Start () {
        // Calculate the object count.
        int objectCount = 1 + (int)Mathf.Ceil((2f * recycleOffset) / size);

        // Generate the tiles.
        objectQueue = new Queue<Transform>(objectCount);
        lastPosition = nextPosition = transform.position.x - recycleOffset;
        for (int i = 0; i < objectCount; i++) {
            Transform next = (Transform)Instantiate(prefab);
            next.position = new Vector3(nextPosition, 0f, transform.position.z);
            next.localScale = new Vector3(size, size, transform.localScale.z);
            nextPosition += next.localScale.x;
            objectQueue.Enqueue(next);
        }
    }
    
    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update () {
        // Recycle the out of frame tile.
        if (objectQueue.Peek().position.x + recycleOffset < trackedObject.transform.position.x) {
            Transform last = objectQueue.Dequeue();
            last.position = new Vector3(nextPosition, 0f, transform.position.z);
            nextPosition += last.localScale.x;
            objectQueue.Enqueue(last);
        }

        if (objectQueue.Peek().position.x - recycleOffset > trackedObject.transform.position.x) {
            Transform last = objectQueue.Dequeue();
            last.position = new Vector3(lastPosition, 0f, transform.position.z);
            nextPosition -= last.localScale.x;
            objectQueue.Enqueue(last);
        }
    }
}
