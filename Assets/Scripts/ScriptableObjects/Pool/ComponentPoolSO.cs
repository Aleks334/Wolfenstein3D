using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class ComponentPoolSO<T> : ScriptableObject where T : Component
{
    [SerializeField] protected uint _initalPoolSize;
    [SerializeField] protected T _pooledObject;

    private Stack<T> _stack;

    public abstract IFactory<T> Factory { get; set; }

    public Func<Transform> InitPoolParent;

    public void SetupPool()
    {
        _stack = new Stack<T>();

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
        pooledObj.gameObject.SetActive(false);
    }

    public T Request()
    {
        if (_stack.Count == 0)
        {
            T additionalInstance = Create();
            return additionalInstance;
        }

        T retrievedInstance = _stack.Pop();
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