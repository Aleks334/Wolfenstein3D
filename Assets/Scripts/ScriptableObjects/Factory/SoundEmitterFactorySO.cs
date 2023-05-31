using UnityEngine;

[CreateAssetMenu(fileName = "NewSoundEmitterFactory", menuName = "Factory/SoundEmitter Factory")]
public class SoundEmitterFactorySO : FactorySO<SoundEmitter>
{
    [SerializeField] private SoundEmitter _prefab;

    public override SoundEmitter Create()
    {
        return Instantiate(_prefab, ObjParent);
    }
}