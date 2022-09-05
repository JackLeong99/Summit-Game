using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pool<T> : MonoBehaviour where T: MonoBehaviour
{
    [SerializeField] private int initialSize = 5;
    [SerializeField] private T prefab;

    Queue<T> pool = new Queue<T>();

    void Start ()
    {
        for (int i = 0; i < initialSize; i++)
        {
            T instance = Instantiate(prefab, transform);
            OnInstantiate(instance);
            Return(instance);
        }
    }

    public T Get ()
    {
        T instance;

        if (pool.Count > 0)
        {
            instance = pool.Dequeue();
        }
        else
        {
            instance = Instantiate(prefab, transform);
            OnInstantiate(instance);
        }

        OnGet(instance);
        return instance;
    }

    public void Return (T instance)
    {
        OnReturn(instance);
        pool.Enqueue(instance);
    }

    protected virtual void OnInstantiate (T instance)
    {
    }

    protected virtual void OnGet (T instance)
    {
        instance.gameObject.SetActive(true);
    }

    protected virtual void OnReturn (T instance)
    {
        instance.gameObject.SetActive(false);
    }
}
