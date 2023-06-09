using UnityEngine;

[CreateAssetMenu(fileName = "NewEpisode", menuName = "Game_Data/Episode")]
public class Episode : ScriptableObject
{
    public const int FLOORS_NUM = 9;

    [Header("Episode Floors")]
    [SerializeField] private FloorSO[] _floors = new FloorSO[FLOORS_NUM];

    public FloorSO[] Floors
    {
        get => _floors;
    }

    public int CurrentFloorNum { get; private set; }

    [SerializeField] private FloorSO _currentFloor;
    public FloorSO CurrentFloor
    {
        get => _currentFloor;
        private set => _currentFloor = value;
    }

    public void SetFloorOnInit()
    {
        CurrentFloorNum = default;
        CurrentFloor = Floors[CurrentFloorNum];
    }

    public void GoToNextFloor()
    {
        if (CurrentFloorNum + 1 < FLOORS_NUM)
            CurrentFloorNum++;

        CurrentFloor = Floors[CurrentFloorNum];
    }
}