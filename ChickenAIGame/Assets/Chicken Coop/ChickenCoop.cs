using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ChickenCoop : MonoBehaviour {

    private int chickenCount = 0;
    //This just keeps track of how many chickens we have generated and is used when assigning the 
    //chickens a unique ID.

    private DayManager dayManager = DayManager.Instance();
    private CoopTraining training = CoopTraining.Instance();
    //Basically pointers or shortcuts to our other files rather than sticking everything in one file.

    public Dictionary<int, PlayerChicken> playerChickens = new Dictionary<int, PlayerChicken>();
    public Dictionary<int, BaseChicken> enemyChickensEasy = new Dictionary<int, BaseChicken>();

    public void Start() {
        List<PlayerChicken> chickens = ChickenGenerator.genPChicken(6);
        //Generate the starting 6 chickens for the player.
        foreach (PlayerChicken chicken in chickens) {
            playerChickens.Add(chickenCount, chicken);
            chickenCount++;
            //Add the chickens to the map giving them a unique ID for when we need to retrieve data
            //from a specific chicken.
        }

        List<BaseChicken> eChickens = ChickenGenerator.genEChicken(16);

        foreach (BaseChicken chicken in eChickens) {
            enemyChickensEasy.Add(chickenCount, chicken);
            chickenCount++;
        }

        CalculatePercentile(0);
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

    private void CalculatePercentile(int chickenID) {

        Dictionary<int, int> allChickensStats = new Dictionary<int, int>();

        foreach (PlayerChicken chicken in playerChickens.Values) {
            int statTotal = chicken.strengthStat + chicken.dexterityStat + chicken.intelligenceStat + chicken.enduranceStat;
            allChickensStats.Add(chicken.uniqueID, statTotal);
        }

        foreach (BaseChicken chicken in enemyChickensEasy.Values)
        {
            int statTotal = chicken.strengthStat + chicken.dexterityStat + chicken.intelligenceStat + chicken.enduranceStat;
            allChickensStats.Add(chicken.uniqueID, statTotal);
        }

        var sortedChickenStats = from statTotal in allChickensStats orderby statTotal.Value ascending select statTotal;

        foreach (var stat in sortedChickenStats)
        {
            Debug.Log(stat.Key + stat.Value);
        }
    }
}
