using System.Collections.Generic;
using UnityEngine;

public class ChickenCoop : MonoBehaviour {

    private int chickenCount = 0;
    //This just keeps track of how many chickens we have generated and is used when assigning the 
    //chickens a unique ID.

    private DayManager dayManager = DayManager.Instance();
    //Keep a pointer to our dayManager instance for use in this class. Saves us from writing DayManager.Instance(). all the time.

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

        Debug.Log(dayManager.currentDay);
        
        for(int i = 0; i < 10; i++) {
            dayManager.AdvanceDay();
            Debug.Log(dayManager.currentDay);
        }
    }
}
