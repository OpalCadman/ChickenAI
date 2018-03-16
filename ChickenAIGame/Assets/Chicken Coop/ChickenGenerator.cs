using UnityEngine;
using System.Collections.Generic;

static public class ChickenGenerator {

    public enum StatTier {
        Player = 0,
        Easy = 1,
        Medium = 2,
        Hard = 3
        //Just makes it easier to read, determines how strong the chickens base stats are.
    }

    static public List<PlayerChicken> genPChicken(int noOfChickens) {
        //Here we are just generating the base player chickens for the beginning of the game.
        //We should have a long list of chicken names that we can randomly assign to the generated chicken.

        List<PlayerChicken> chickens = new List<PlayerChicken>();

        for (int i = 0; i < noOfChickens; i++) {
            PlayerChicken chicken = new PlayerChicken();

            chicken.chickenName = "Test Dummy";

            List<int> baseStats = genBaseStats(StatTier.Player);
            chicken.strengthStat = baseStats[0];
            chicken.dexterityStat = baseStats[1];
            chicken.intelligenceStat = baseStats[2];
            chicken.enduranceStat = baseStats[3];

            chickens.Add(chicken);
        }

        return chickens;
    }

    static public List<BaseChicken> genOpposingChicken(int noOfChickens, StatTier tier) {

        List<BaseChicken> chickens = new List<BaseChicken>();

        for (int i = 0; i < noOfChickens; i++)
        {
            BaseChicken chicken = new BaseChicken();

            chicken.chickenName = "Test Dummy";

            List<int> baseStats = new List<int> { 0, 0, 0, 0 };

            switch (tier)
            {
                case StatTier.Easy:
                    baseStats = genBaseStats(StatTier.Easy);
                    break;
                case StatTier.Medium:
                    baseStats = genBaseStats(StatTier.Medium);
                    break;
                case StatTier.Hard:
                    baseStats = genBaseStats(StatTier.Hard);
                    break;
            }
            
            chicken.strengthStat = baseStats[0];
            chicken.dexterityStat = baseStats[1];
            chicken.intelligenceStat = baseStats[2];
            chicken.enduranceStat = baseStats[3];

            chicken.ageInDays = Random.Range(7, 29);

            chickens.Add(chicken);
        }

        return chickens;
    }

    static private List<int> genBaseStats(StatTier statTier) {
        //This is just the method of generating the base stats for a chicken. If you have a more elaborate method
        //be my guest, all the numbers are just arbitrary since I have no idea how we are going to turn these numbers
        //into AI yet.

        List<int> stats = new List<int>();

        switch (statTier) {
            case StatTier.Player:
                for(int i = 0; i < 4; i++) {
                    int statValue = Random.Range(0, 12);
                    stats.Add(statValue);
                }
               
                break;

            case StatTier.Easy:
                for (int i = 0; i < 4; i++) {
                    int statValue = Random.Range(0, 16);
                    stats.Add(statValue);
                }
                break;

            case StatTier.Medium:
                for (int i = 0; i < 4; i++) {
                    int statValue = Random.Range(8, 26);
                    stats.Add(statValue);
                }
                break;

            case StatTier.Hard:
                for (int i = 0; i < 4; i++) {
                    int statValue = Random.Range(16, 36);
                    stats.Add(statValue);
                }
                break;
        }

        return stats;
    }
}
