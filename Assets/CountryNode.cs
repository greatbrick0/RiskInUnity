using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountryNode
{
    public string name;
    public Vector2 position { get; private set; }
    List<CountryNode> neighbors = new();
    public ContinentGroup group { get; private set; }
    public int armyCount = 0;
    public PlayerData owner;

    private CountryVisualHandler visualsRef;
    private Generator mapRef;

    public CountryNode(string newName, Vector2 newPos, ContinentGroup newGroup, Generator map)
    {
        name = newName;
        position = newPos;
        group = newGroup;
        group.Add(this);
        mapRef = map;
    }

    public void CreateVisual(CountryVisualHandler settings)
    {
        visualsRef = settings;
        settings.Initialize(name, group.color);
        settings.button.onClick.AddListener(Action);
    }

    public void UpdateVisual()
    {
        visualsRef.UpdateArmyLabel(armyCount, owner != null ? owner.playerColour : Color.white);
    }

    public void Action()
    {
        Debug.Log(name + " clicked");
        switch (mapRef.gameState)
        {
            case "Prep: Fill":
                if (armyCount != 0) return;
                BecomeOccupied();
                mapRef.AttemptExitPrepFill();
                mapRef.playerIndex += 1;
                break;
            case "Prep: More":
                if (owner != mapRef.GetCurrentPlayer()) return;
                IncreaseArmy(1);
                mapRef.playerIndex += 1;
                break;
            case "Growing":
                if (owner != mapRef.GetCurrentPlayer()) return;
                IncreaseArmy(1);
                mapRef.UpdateReservesLabel(mapRef.GetCurrentPlayer().armyCount);
                if (mapRef.GetCurrentPlayer().armyCount == 0) mapRef.AttemptExitGrowing();
                break;
            default:
                break;
        }
    }

    public void HalfAddNeighbor(CountryNode neighbor)
    {
        if (neighbor == this) return;
        if (neighbors.Contains(neighbor)) return;
        neighbors.Add(neighbor);
        //Debug.Log(neighbor.name + " added as neighbor of " + name);
    }

    public void AddNeighbor(CountryNode neighbor)
    {
        this.HalfAddNeighbor(neighbor);
        neighbor.HalfAddNeighbor(this);
    }

    private void BecomeOccupied()
    {
        armyCount += 1;
        owner = mapRef.GetCurrentPlayer();
        owner.armyCount -= 1;
        owner.countryCount += 1;
        UpdateVisual();
        if(group.IsFullyControlled()) owner.continentBonus += group.bonus;
    }

    private void IncreaseArmy(int amount)
    {
        armyCount += amount;
        owner.armyCount -= amount;
        UpdateVisual();
    }
}
