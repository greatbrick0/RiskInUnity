using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CountryVisualHandler : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI nameLabel;
    [SerializeField]
    private Image edge;
    [SerializeField]
    private TextMeshProUGUI armyLabel;
    [SerializeField]
    public Button button;

    public void Initialize(string newName, Color newColor)
    {
        nameLabel.text = newName;
        edge.color = newColor;
    }

    public void UpdateArmyLabel(int newArmyCount, Color newColor)
    {
        if (newArmyCount == 0) armyLabel.text = "";
        else armyLabel.text = newArmyCount.ToString();
        armyLabel.color = newColor;
    }
}
