using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManualManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> _gameManualPages;
    [SerializeField] TextMeshProUGUI _pageText;

    private int _currentPage = 1;

    private const int MIN_PAGE_NUM = 1;
    private const int MAX_PAGE_NUM = 11;

    void Start()
    {
        ChangePage(_currentPage);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _currentPage--;
            ValidateCurrentPageIndex();
            ChangePage(_currentPage);
        }
        else if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            _currentPage++;
            ValidateCurrentPageIndex();
            ChangePage(_currentPage);
        }
    }

    private void ChangePage(int currentPage)
    {
        foreach (var page in _gameManualPages)
        {
            if(_gameManualPages.IndexOf(page) == (currentPage - 1))
            {
                page.SetActive(true);
            }
            else
                page.SetActive(false);
        }

        _pageText.text = string.Format("pg {0} of {1}", _currentPage, MAX_PAGE_NUM);
    }

    private void ValidateCurrentPageIndex()
    {
        if(_currentPage < MIN_PAGE_NUM)
        {
            _currentPage = MAX_PAGE_NUM;
        }
        else if(_currentPage > MAX_PAGE_NUM)
        {
            _currentPage = MIN_PAGE_NUM;
        }
    }
}
