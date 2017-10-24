using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class ObjectPool<T> where T : class, new()
{
    private Stack<T> m_objectStack;

    private Action<T> m_resetAction;
    private Action<T> m_onetimeInitAction;

    public ObjectPool(int initialBufferSize, Action<T>
        ResetAction = null, Action<T> OnetimeInitAction = null)
    {
        m_objectStack = new Stack<T>(initialBufferSize);
        m_resetAction = ResetAction;
        m_onetimeInitAction = OnetimeInitAction;
    }

    public T Spawn()
    {
        if (m_objectStack.Count > 0)
        {
            T t = m_objectStack.Pop();

            if (m_resetAction != null)
                m_resetAction(t);

            return t;
        }
        else
        {
            T t = new T();

            if (m_onetimeInitAction != null)
                m_onetimeInitAction(t);

            return t;
        }
    }

    public void Despawn(T obj)
    {
        m_objectStack.Push(obj);
    }
}