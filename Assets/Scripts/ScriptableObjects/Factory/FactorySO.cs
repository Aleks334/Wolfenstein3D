using System.Collections;
using UnityEngine;

public abstract class FactorySO<T> : ScriptableObject, IFactory<T>
{
    public abstract T Create();
    public Transform ObjParent { get; set; }
}