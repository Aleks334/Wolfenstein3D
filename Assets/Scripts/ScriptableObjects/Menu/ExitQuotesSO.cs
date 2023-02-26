using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Menu/ExitQuotesDB")]
public class ExitQuotesSO : ScriptableObject
{
    [Header("Sentences that appear when player tries to leave the game")]
    public string[] exitQuotes = new string[8];
}