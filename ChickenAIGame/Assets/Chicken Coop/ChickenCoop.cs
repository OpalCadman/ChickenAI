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

    public Dictionary<int, PlayerChicken> playerChickens = new Dictionary<int, PlayerChicken>();
    public Dictionary<int, BaseChicken> enemyChickensEasy = new Dictionary<int, BaseChicken>();
    public Dictionary<int, BaseChicken> enemyChickensMedium = new Dictionary<int, BaseChicken>();
    public Dictionary<int, BaseChicken> enemyChickensHard = new Dictionary<int, BaseChicken>();

    public void Start() {
        List<PlayerChicken> chickens = ChickenGenerator.genPChicken(6);
        //Generate the starting 6 chickens for the player.
        foreach (PlayerChicken chicken in chickens) {
            chicken.uniqueID = chickenCount;
            playerChickens.Add(chickenCount, chicken);
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
    }

    public void Update()
    {
        
    }

    public void CleanUp()
    {
        buttonManagerRef.DeactivateButtons();
    }

    public void ChickenDecrement() {
        dayManager.AdvanceDay();
        foreach (PlayerChicken chicken in playerChickens.Values) {
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
        int[] newChickenStats = breeding.AssignChickens(playerChickens[0], playerChickens[1]);
        PlayerChicken babyChicken = ChickenGenerator.genSetChicken(newChickenStats);
        babyChicken.uniqueID = chickenCount;
        playerChickens.Add(chickenCount, babyChicken);
        chickenCount += 1;
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

    private int CalculatePercentile(int chickenID) {

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
    }
}
