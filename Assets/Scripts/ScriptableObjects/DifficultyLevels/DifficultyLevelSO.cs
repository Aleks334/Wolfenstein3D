using UnityEngine;

[CreateAssetMenu(fileName = "NewDifficultyLevel", menuName = "DifficultyLevel")]
public class DifficultyLevelSO : ScriptableObject
{
    [Header("difficulty lvl information")]
	[SerializeField] private string _difficultyLevel;

	public string Name
    {
		get => _difficultyLevel;
		set => _difficultyLevel = value;
	}
}