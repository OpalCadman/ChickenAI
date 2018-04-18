using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ChickenCoop : MonoBehaviour {

    private ButtonManager buttonManagerRef;

    private int chickenCount = 0;
    //This just keeps track of how many chickens we have generated and is used when assigning the 
    //chickens a unique ID.

    private DayManager dayManager = DayManager.Instance();
    private CoopTraining training = CoopTraining.Instance();
    private CoopBreeding breeding = CoopBreeding.Instance();
    //Basically pointers or shortcuts to our other files rather than sticking everything in one file.

    //public Dictionary<int, PlayerChicken> playerChickens = new Dictionary<int, PlayerChicken>();
    public Dictionary<int, BaseChicken> enemyChickensEasy = new Dictionary<int, BaseChicken>();
    public Dictionary<int, BaseChicken> enemyChickensMedium = new Dictionary<int, BaseChicken>();
    public Dictionary<int, BaseChicken> enemyChickensHard = new Dictionary<int, BaseChicken>();

    public List<PlayerChicken> playerChickens = new List<PlayerChicken>();

    public void Start() {
        List<PlayerChicken> chickens = ChickenGenerator.genPChicken(6);
        //Generate the starting 6 chickens for the player.
        foreach (PlayerChicken chicken in chickens) {
            chicken.uniqueID = chickenCount;
            playerChickens.Add(chicken);
            chickenCount+=1;
            //Add the chickens to the map giving them a unique ID for when we need to retrieve data
            //from a specific chicken.
        }

        List<BaseChicken> eChickens = ChickenGenerator.genOpposingChicken(16, ChickenGenerator.StatTier.Easy);

        foreach (BaseChicken chicken in eChickens) {
            chicken.uniqueID = chickenCount;
            enemyChickensEasy.Add(chickenCount, chicken);
            chickenCount+=1;
        }

        List<BaseChicken> mChickens = ChickenGenerator.genOpposingChicken(16, ChickenGenerator.StatTier.Medium);

        foreach (BaseChicken chicken in mChickens)
        {
            chicken.uniqueID = chickenCount;
            enemyChickensMedium.Add(chickenCount, chicken);
            chickenCount += 1;
        }

        List<BaseChicken> hChickens = ChickenGenerator.genOpposingChicken(16, ChickenGenerator.StatTier.Hard);

        foreach (BaseChicken chicken in hChickens)
        {
            chicken.uniqueID = chickenCount;
            enemyChickensHard.Add(chickenCount, chicken);
            chickenCount += 1;
        }

        GameObject temp = GameObject.Find("ButtonManager");
        buttonManagerRef = temp.GetComponent<ButtonManager>();
        buttonManagerRef.Initialise();
        buttonManagerRef.ActivateButtons();

        buttonManagerRef.coopAlive = true;
    }

    public void CleanUp()
    {
        buttonManagerRef.DeactivateButtons();
    }

    public void ChickenDecrement() {
        dayManager.AdvanceDay();
        foreach (PlayerChicken chicken in playerChickens) {
            chicken.ageInDays+=1;

            if (chicken.daysLeftUntilActive != 0) {
                chicken.daysLeftUntilActive -= 1;

                if(chicken.daysLeftUntilActive == 0) {
                    chicken.currentStatus = PlayerChicken.Status.Idle;
                }
            }
        }
    }

    public void BreedChickens()
    { 
        List<PlayerChicken> newChickens = new List<PlayerChicken>();
        int chickenIndex = 0;
        for (int i = 0; i < 9; i++)
        {
            if(chickenIndex + 1 <= playerChickens.Count - 1)
            {
                int[] newChickenStats = breeding.AssignChickens(playerChickens[chickenIndex], playerChickens[chickenIndex + 1]);
                PlayerChicken babyChicken = ChickenGenerator.genSetChicken(newChickenStats);
                babyChicken.uniqueID = chickenCount;
                newChickens.Add(babyChicken);
                chickenCount += 1;
                chickenIndex += 2;
            }
            else { break; }
        }

        while(newChickens.Count != 0)
        {
            if(playerChickens.Count + newChickens.Count > 18)
            {
                playerChickens.RemoveRange(0, (playerChickens.Count + newChickens.Count) - 18);
            }
            playerChickens.Add(newChickens[newChickens.Count - 1]);
            newChickens.RemoveAt(newChickens.Count - 1);
        }
    }

    public List<PlayerChicken> SelectChickens()
    {
        List<PlayerChicken> selectedChickens = new List<PlayerChicken>();

        for(int i = 0; i < 3; i++)
        {
            int chickenIndex = Random.Range(0, playerChickens.Count);
            selectedChickens.Add(playerChickens[chickenIndex]);
        }
        
        return selectedChickens;
    }

    private bool CheckChickenIdle(int chickenID) {
        if(playerChickens[chickenID].currentStatus == PlayerChicken.Status.Idle) {
            return true;
        } else {
            return false;
        }
    }

    private bool CheckChickenIdle(PlayerChicken chicken) {
        if (chicken.currentStatus == PlayerChicken.Status.Idle) { 
            return true;
        } else {
            return false;
        }
    }

    /*private int CalculatePercentile(int chickenID) {

        Dictionary<int, int> allChickensStats = new Dictionary<int, int>();

        foreach (PlayerChicken chicken in playerChickens) {
            int statTotal = chicken.strengthStat + chicken.dexterityStat + chicken.intelligenceStat + chicken.enduranceStat;
            allChickensStats.Add(chicken.uniqueID, statTotal);
        }

        foreach (BaseChicken chicken in enemyChickensEasy.Values)
        {
            int statTotal = chicken.strengthStat + chicken.dexterityStat + chicken.intelligenceStat + chicken.enduranceStat;
            allChickensStats.Add(chicken.uniqueID, statTotal);
        }

        foreach (BaseChicken chicken in enemyChickensMedium.Values)
        {
            int statTotal = chicken.strengthStat + chicken.dexterityStat + chicken.intelligenceStat + chicken.enduranceStat;
            allChickensStats.Add(chicken.uniqueID, statTotal);
        }

        foreach (BaseChicken chicken in enemyChickensHard.Values)
        {
            int statTotal = chicken.strengthStat + chicken.dexterityStat + chicken.intelligenceStat + chicken.enduranceStat;
            allChickensStats.Add(chicken.uniqueID, statTotal);
        }

        var sortedChickenStats = from statTotal in allChickensStats orderby statTotal.Value ascending select statTotal;

        var sortedList = sortedChickenStats.ToList();
        var index = sortedList.FindIndex(key => key.Key == chickenID);

        return ((index + 1) * 100) / sortedList.Count;
    }*/
}
