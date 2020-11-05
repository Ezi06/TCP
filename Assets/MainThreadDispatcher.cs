using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainThreadDispatcher : MonoBehaviour
{
    public static MainThreadDispatcher Instance;

    private Queue<Action> QueuedActions = new Queue<Action>();

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        while (QueuedActions.Count>0)
        {
            QueuedActions.Dequeue().Invoke();
        } 
    }

    public void InvokeInMainThread(Action act)
    {
        QueuedActions.Enqueue(act);
    }
}
