using System.Collections.Generic;
using UnityEngine;

public class ChickenCoop : MonoBehaviour {

    private int chickenCount = 0;
    //This just keeps track of how many chickens we have generated and is used when assigning the 
    //chickens a unique ID.

    private DayManager dayManager = DayManager.Instance();
    private CoopTraining training = CoopTraining.Instance();
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


        Debug.Log("Day: " + dayManager.currentDay);
        Debug.Log("Age: " + playerChickens[0].ageInDays);
        Debug.Log("Status: " + playerChickens[0].currentStatus);
        Debug.Log("Days until Active: " + playerChickens[0].daysLeftUntilActive);
        Debug.Log("Strength: " + playerChickens[0].strengthStat);
        Debug.Log("Intelligence: " + playerChickens[0].intelligenceStat);

        training.AssignChicken(playerChickens[0], CoopTraining.TrainingType.Str);
        Debug.Log("Chicken is assigned to Strength training.");
        Debug.Log("Status: " + playerChickens[0].currentStatus);
        Debug.Log("Days until Active: " + playerChickens[0].daysLeftUntilActive);
        Debug.Log("Strength: " + playerChickens[0].strengthStat);
        Debug.Log("Dexterity: " + playerChickens[0].intelligenceStat);

        dayManager.AdvanceDay();
        Debug.Log("Next Day.");
        ChickenDecrement();
        Debug.Log("Day: " + dayManager.currentDay);
        Debug.Log("Status: " + playerChickens[0].currentStatus);
        Debug.Log("Days until Active: " + playerChickens[0].daysLeftUntilActive);

        dayManager.AdvanceDay();
        Debug.Log("Next Day.");
        ChickenDecrement();
        Debug.Log("Day: " + dayManager.currentDay);
        Debug.Log("Status: " + playerChickens[0].currentStatus);
        Debug.Log("Days until Active: " + playerChickens[0].daysLeftUntilActive);

        dayManager.AdvanceDay();
        Debug.Log("Next Day.");
        ChickenDecrement();
        Debug.Log("Day: " + dayManager.currentDay);
        Debug.Log("Status: " + playerChickens[0].currentStatus);
        Debug.Log("Days until Active: " + playerChickens[0].daysLeftUntilActive);
    }

    private void ChickenDecrement() {
        foreach (PlayerChicken chicken in playerChickens.Values) {
            chicken.ageInDays++;

            if (chicken.daysLeftUntilActive != 0) {
                chicken.daysLeftUntilActive -= 1;

                if(chicken.daysLeftUntilActive == 0) {
                    chicken.currentStatus = PlayerChicken.Status.Idle;
                }
            }
        }
    }
}
