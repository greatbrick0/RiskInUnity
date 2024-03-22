using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData
{
    public int countryCount = 0;
    public int armyCount;
    public Color playerColour;

    public PlayerData(Color newColour, int newArmyCount)
    {
        playerColour = newColour;
        armyCount = newArmyCount;
    }
}
