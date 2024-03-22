using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Generator : MonoBehaviour
{
    [SerializeField]
    public GameObject countryVisual;

    [SerializeField]
    private Transform canvas;

    [SerializeField]
    private Image gameStatePanel;
    [SerializeField]
    private TextMeshProUGUI gameStateText;
    [SerializeField]
    private TextMeshProUGUI reservesLabel;
    [SerializeField]
    private GameObject centreButtonObj;

    [SerializeField]
    private List<CountryNode> everyCountry = new();
    [SerializeField]
    public List<PlayerData> players = new();

    private GameObject instanceRef;

    public string gameState = "Prep: Fill";
    private int _playerIndex = 0;
    public int playerIndex
    {
        get { return _playerIndex; }
        set 
        { 
            _playerIndex = value % players.Count;
            Turn();
        }
    }

    public PlayerData GetCurrentPlayer()
    {
        return players[_playerIndex];
    }

    void Start()
    {
        playerIndex = 0;

        MapInit();

        foreach (CountryNode ii in everyCountry)
        {
            instanceRef = Instantiate(countryVisual, ii.position, Quaternion.identity, canvas);
            instanceRef.transform.position += canvas.position;
            ii.CreateVisual(instanceRef.GetComponent<CountryVisualHandler>());
            ii.UpdateVisual();
        }
    }

    void Turn()
    {
        gameStatePanel.color = players[playerIndex].playerColour;
        AttemptExitPrepMore();
        if(gameState == "Growing")
        {
            GetCurrentPlayer().GainReserves();
        }
        UpdateReservesLabel(GetCurrentPlayer().armyCount);
    }

    private void MapInit()
    {
        ContinentGroup northAmerica = new ContinentGroup(Color.red, 3);
        ContinentGroup europe = new ContinentGroup(Color.blue, 4);
        ContinentGroup africa = new ContinentGroup(Color.green, 5);

        everyCountry.Add(new CountryNode("Canada", new Vector2(-100, 65), northAmerica, this));
        everyCountry.Add(new CountryNode("Alaska", new Vector2(-180, 65), northAmerica, this));
        everyCountry.Add(new CountryNode("USA", new Vector2(-100, -10), northAmerica, this));
        everyCountry.Add(new CountryNode("Mexico", new Vector2(-100, -85), northAmerica, this));

        everyCountry.Add(new CountryNode("Britain", new Vector2(100, 145), europe, this));
        everyCountry.Add(new CountryNode("France", new Vector2(100, 70), europe, this));
        everyCountry.Add(new CountryNode("Germany", new Vector2(180, 70), europe, this));
        everyCountry.Add(new CountryNode("Spain", new Vector2(100, -5), europe, this));

        everyCountry.Add(new CountryNode("Guniea", new Vector2(90, -110), africa, this));
        everyCountry.Add(new CountryNode("Egypt", new Vector2(170, -110), africa, this));
        everyCountry.Add(new CountryNode("South Africa", new Vector2(130, -185), africa, this));

        ConnectByDistance(100.0f);
        everyCountry[0].AddNeighbor(everyCountry[5]);
        everyCountry[2].AddNeighbor(everyCountry[8]);
        everyCountry[7].AddNeighbor(everyCountry[9]);
    }

    public bool AllCountriesControlled()
    {
        foreach (CountryNode ii in everyCountry)
        {
            if(ii.owner == null) return false;
        }
        return true;
    }

    public void AttemptExitPrepFill()
    {
        if (AllCountriesControlled())
        {
            gameState = "Prep: More";
            gameStateText.text = gameState;
        }
    }

    private bool AllPlayersEmpty()
    {
        foreach (PlayerData ii in players)
        {
            if(ii.armyCount > 0) return false;
        }
        return true;
    }

    public void AttemptExitPrepMore()
    {
        if (AllPlayersEmpty() && gameState == "Prep: More")
        {
            gameState = "Growing";
            gameStateText.text = gameState;
            centreButtonObj.SetActive(true);
        }
    }

    public void AttemptExitGrowing()
    {
        gameState = "Attacking";
        gameStateText.text = gameState;
    }

    private void ConnectByDistance(float distance)
    {
        foreach (CountryNode ii in everyCountry)
        {
            foreach (CountryNode jj in everyCountry)
            {
                if((ii.position - jj.position).magnitude <= distance) ii.AddNeighbor(jj);
            }
        }
    }

    public void CentreButtonAction()
    {
        switch (gameState)
        {
            case "Growing":
                gameState = "Attacking";
                break;
            case "Attacking":
                gameState = "Moving";
                break;
            case "Moving":
                gameState = "Growing";
                playerIndex += 1;
                break;
        }
        gameStateText.text = gameState;
    }

    public void UpdateReservesLabel(int value)
    {
        reservesLabel.text = "Reserves: " + value.ToString();
    }
}
