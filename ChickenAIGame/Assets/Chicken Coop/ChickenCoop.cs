using System.Collections.Generic;
using UnityEngine;

public class ChickenCoop : MonoBehaviour {

    private int chickenCount = 0;
    //This just keeps track of how many chickens we have generated and is used when assigning the 
    //chickens a unique ID.

    public Dictionary<int, PlayerChicken> playerChickens = new Dictionary<int, PlayerChicken>();

    public void Start() {
        List<PlayerChicken> chickens = ChickenGenerator.genPChicken(6);
        //Generate the starting 6 chickens for the player.
        foreach (PlayerChicken chicken in chickens) {
            playerChickens.Add(chickenCount, chicken);
            chickenCount++;
            //Add the chickens to the map giving them a unique ID for when we need to retrieve data
            //from a specific chicken.
        }
        Debug.Log("Chicken Count: " + chickenCount);
        Debug.Log("Chicken 1 Strength: " + playerChickens[0].strengthStat);
        Debug.Log("Chicken 2 Strength: " + playerChickens[1].strengthStat);
        Debug.Log("Chicken 3 Strength: " + playerChickens[2].strengthStat);
        Debug.Log("Chicken 4 Strength: " + playerChickens[3].strengthStat);
        Debug.Log("Chicken 5 Strength: " + playerChickens[4].strengthStat);
        Debug.Log("Chicken 6 Strength: " + playerChickens[5].strengthStat);
    }
}
