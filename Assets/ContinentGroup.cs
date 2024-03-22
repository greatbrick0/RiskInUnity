using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinentGroup
{
    public List<CountryNode> nodes = new();
    public Color color { get; private set; }
    public int bonus { get; private set; }

    public ContinentGroup(Color newColor, int newBonus)
    { 
        color = newColor;
        bonus = newBonus;
    }

    public void Add(CountryNode node)
    {
        if(!nodes.Contains(node)) nodes.Add(node);
    }

    public bool IsFullyControlled()
    {
        foreach (CountryNode ii in nodes)
        {
            if (ii.owner != nodes[0].owner)
            {
                return false;
            }
        }
        return true;
    }
}
