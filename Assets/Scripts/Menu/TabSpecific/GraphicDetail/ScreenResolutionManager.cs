using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScreenResolutionManager : MonoBehaviour
{
    [SerializeField] private GraphicDetailSO _database;
    private SpriteToggler[] _btns;

    [SerializeField] private RectTransform _resolutionBtnPrefab;
    [SerializeField] private Transform _btnsParent;

    private void Start()
    {
        _btns = new SpriteToggler[_database.AvailableResolutions.Count];
        InitResolutionBtns();

        RestoreResolutionOnMenuPageLoad();
        Debug.Log(_database.CurrentResolution);
    }

    private void InitResolutionBtns()
    {
        if (_database.AvailableResolutions.Count == 0)
            return;

        for (int i = 0; i < _database.AvailableResolutions.Count; i++)
        {
            var btn = Instantiate(_resolutionBtnPrefab, _btnsParent);
            var posY = i == 0 ? btn.localPosition.y : btn.localPosition.y - btn.rect.height * i;
            string ResText = $"{_database.AvailableResolutions[i].Width} x {_database.AvailableResolutions[i].Height}";
            
            btn.GetChild(0).GetComponent<TextMeshProUGUI>().text = ResText;
            btn.localPosition = new Vector3(0, posY);
            _btns[i] = btn.GetComponent<SpriteToggler>();

            _btns[i].ToggledBtn += ToggledBtnEventHandler;        
        }
    }
    
    private void ToggledBtnEventHandler(SpriteToggler clickedToggle)
    {
        for (int i = 0; i < _btns.Length; i++)
        {
            if (_btns[i] != clickedToggle)
                _btns[i].SwitchOff(_btns[i].transform.GetChild(1).GetComponent<Image>());
            else
            {
                ChangeResolution(i);
            }  
        }
    }

    private void RestoreResolutionOnMenuPageLoad()
    {
        for (int i = 0; i < _database.AvailableResolutions.Count; i++)
        {
            if (_database.AvailableResolutions[i] == _database.CurrentResolution)
            {
                _btns[i].SwitchOn(_btns[i].transform.GetChild(1).GetComponent<Image>());
                break;
            }
        }
    }

    private void ChangeResolution(int i)
    {
        if (Screen.currentResolution.width == _database.AvailableResolutions[i].Width &&
               Screen.currentResolution.height == _database.AvailableResolutions[i].Height)
            return;
        

        Screen.SetResolution(_database.AvailableResolutions[i].Width, _database.AvailableResolutions[i].Height, false);
        _database.CurrentResolution = _database.AvailableResolutions[i];

        //Debug.Log("changed res: " + _database.CurrentResolution);
    }
}