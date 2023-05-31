using UnityEngine;

public interface IFactory<T>
{
    T Create();
    Transform ObjParent { get; set; }
}