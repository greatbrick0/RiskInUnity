using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData
{
    public int countryCount = 0;
    public int continentBonus = 0;
    public int armyCount;
    public Color playerColour;

    public PlayerData(Color newColour, int newArmyCount)
    {
        playerColour = newColour;
        armyCount = newArmyCount;
    }

    public void GainReserves()
    {
        armyCount += Mathf.Max(3, Mathf.FloorToInt(armyCount / 3));
        armyCount += continentBonus;
    }
}
