using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class ComponentPoolSO<T> : ScriptableObject where T : Component
{
    [SerializeField] protected uint _initalPoolSize;
    [SerializeField] protected T _pooledObject;

    private Stack<T> _stack;

    private List<T> _takenFromPool;

    public List<T> TakenFromPool
    {
        get => _takenFromPool;
    }

    public abstract IFactory<T> Factory { get; set; }

    public Func<Transform> InitPoolParent;

    
    public void ReturnAllToPool()
    {
        List<T> list = new();

        foreach (var soundEmitter in TakenFromPool)
        {
            //skip sound emitter with background music
            if (soundEmitter.GetComponent<AudioSource>().loop)
                continue;

            list.Add(soundEmitter);
            
        }

        foreach (var item in list)
        {
            Return(item);
        }
    }

    public void SetupPool()
    {
        _stack = new Stack<T>();
        _takenFromPool = new List<T>();

        T newInstance;

        SetPoolParent();

        for (int i = 0; i < _initalPoolSize; i++)
        {
            newInstance = Create();
            newInstance.gameObject.SetActive(false);
            _stack.Push(newInstance);
        }
    }

    public void Return(T pooledObj)
    {
        _stack.Push(pooledObj);
        _takenFromPool.Remove(pooledObj);
        pooledObj.gameObject.SetActive(false);
    }

    public T Request()
    {
        if (_stack.Count == 0)
        {
            T additionalInstance = Create();
            _takenFromPool.Add(additionalInstance);
            return additionalInstance;
        }

        T retrievedInstance = _stack.Pop();
        _takenFromPool.Add(retrievedInstance);
        retrievedInstance.gameObject.SetActive(true);
        return retrievedInstance;
    }

    protected virtual T Create()
    {
        return Factory.Create();
    }

    private void SetPoolParent()
    {
        Factory.ObjParent = InitPoolParent();
    }
}