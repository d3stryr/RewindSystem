using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Rewind : MonoBehaviour
{
    [Serializable]
    struct TransFormData
    {
        public Vector3 position;
        public Quaternion rotation;
        public Vector3 scale;
    }
    
    [SerializeField] private float m_maxRewindTime;
    [SerializeField] private float m_delayToReset;
    
    private Queue<TransFormData> m_rewindQueue = new Queue<TransFormData>();
    private Queue<TransFormData> m_reversedQueue = new Queue<TransFormData>();
    private bool m_canRewind = false;
    private bool m_isRewinding = false;
    private int m_maxQueueSize;

    private void Start()
    {
        Application.targetFrameRate = 60;
        m_maxQueueSize = (int)m_maxRewindTime * 60;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            m_canRewind = true;
        }

        if (Input.GetKeyUp(KeyCode.R))
        {
            m_canRewind = false;
            m_isRewinding = false;
            SwapQueues(ref m_reversedQueue, ref m_rewindQueue);
        }

        if (m_canRewind)
        {
            if (!m_isRewinding)
            {
                m_isRewinding = true;
                SwapQueues(ref m_rewindQueue, ref m_reversedQueue);
            }
            ApplyRewind();
        }
        else
        {
            UpdateQueue();
        }
    }

    private void UpdateQueue()
    {
        if (m_rewindQueue.Count == m_maxQueueSize)
        {
            m_rewindQueue.Dequeue();
        }

        TransFormData newTransform = new TransFormData();
        newTransform.position = transform.position;
        newTransform.rotation = transform.rotation;
        newTransform.scale = transform.localScale;
        m_rewindQueue.Enqueue(newTransform);
    }

    private void ApplyRewind()
    {
        if (m_reversedQueue.Count != 0)
        {
            TransFormData newTransform = m_reversedQueue.Dequeue();
            transform.position = newTransform.position;
            transform.rotation = newTransform.rotation;
            transform.localScale = newTransform.scale;
        }
    }

    private void SwapQueues(ref Queue<TransFormData> firstQueue, ref Queue<TransFormData> secondQueue)
    {
        IEnumerable<TransFormData> reversedQueue = firstQueue.Reverse();
        secondQueue = new Queue<TransFormData>(reversedQueue);
    }
}
