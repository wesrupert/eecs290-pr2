/// Wes Rupert - wkr3
/// EECS 290   - Project 02
/// Purgatory  - BackgroundManager.cs
/// Script to control the tiling of the background.

using UnityEngine;
using System.Collections.Generic;

public class BackgroundManager : MonoBehaviour {
    private class TwoWayQueue<T> {
        private class Node<N> {
            public N val;
            public Node<N> next, prev;
            public Node() : this(default(N), null, null) { }
            public Node(N val, Node<N> next, Node<N> prev) {
                this.val = val;
                this.next = next;
                this.prev = prev;
            }
        }

        private Node<T> first, last;

        public TwoWayQueue() {
            first = null;
            last = null;
        }

        public T PeekFront() { return first == null ? default(T) : first.val; }

        public T PeekBack() { return last == null ? default(T) : last.val; }

        public T PushFront(T val) {
            if (first == null || last == null) {
                first = last = new Node<T>(val, null, null);
            }
            else {
                Node<T> node = new Node<T>(val, first, null);
                first.prev = node;
                first = node;
            }

            return val;
        }

        public T PushBack(T val) {
            if (first == null || last == null) {
                first = last = new Node<T>(val, null, null);
            }
            else {
                Node<T> node = new Node<T>(val, null, last);
                last.next = node;
                last = node;
            }

            return val;
        }

        public T PopFront() {
            if (first == null) {
                return default(T);
            }

            Node<T> node = first;
            first.next.prev = null;
            first = first.next;
            return node.val;
        }

        public T PopBack() {
            if (last == null) {
                return default(T);
            }

            Node<T> node = last;
            last.prev.next = null;
            last = last.prev;
            return node.val;
        }
    }

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

    public float lastPosition, nextPosition;
    private TwoWayQueue<Transform> objectQueue;

    /// <summary>
    /// Use this for initialization
    /// </summary>
    void Start () {
        // Calculate the object count.
        int objectCount = 3 + (int)Mathf.Ceil((2f * recycleOffset) / size);

        // Generate the tiles. Credit for design goes to:
        // http://catlikecoding.com/unity/tutorials/runner/
        objectQueue = new TwoWayQueue<Transform>();
        nextPosition = transform.position.x - (recycleOffset + size);
        lastPosition = nextPosition - size;
        for (int i = 0; i < objectCount; i++) {
            Transform next = (Transform)Instantiate(prefab);
            next.position = new Vector3(nextPosition, 0f, transform.position.z);
            next.localScale = new Vector3(size, size, transform.localScale.z);
            next.rotation = transform.rotation;
            nextPosition += next.localScale.x;
            objectQueue.PushFront(next);
        }
    }
    
    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update () {
        // Recycle the out of frame tile. Credit for design goes to:
        // http://catlikecoding.com/unity/tutorials/runner/
        if (objectQueue.PeekFront().position.x - recycleOffset < trackedObject.transform.position.x) {
            Transform last = objectQueue.PopBack();
            last.position = new Vector3(nextPosition, 0f, transform.position.z);
            nextPosition += last.localScale.x;
            lastPosition += last.localScale.x;
            objectQueue.PushFront(last);
        }

        if (objectQueue.PeekBack().position.x + recycleOffset > trackedObject.transform.position.x) {
            Transform first = objectQueue.PopFront();
            first.position = new Vector3(lastPosition, 0f, transform.position.z);
            nextPosition -= first.localScale.x;
            lastPosition -= first.localScale.x;
            objectQueue.PushBack(first);
        }
    }
}
