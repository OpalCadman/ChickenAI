using UnityEngine;

public class CoopBreeding {

    private int baseStr = 10;
    private int baseDex = 10;
    private int baseInt = 10;
    private int baseEnd = 10;
    //These are the base chances of passing down each stat. Since they are all equal and there are 4 of them there would be a 25% chance of landing on any one.

    public int[] AssignChickens(PlayerChicken chickenA, PlayerChicken chickenB) {
        chickenA.currentStatus = PlayerChicken.Status.Breeding;
        chickenB.currentStatus = PlayerChicken.Status.Breeding;
        chickenA.daysLeftUntilActive = 3;
        chickenB.daysLeftUntilActive = 3;

        
        int strChanceA = baseStr;
        int dexChanceA = baseDex;
        int intChanceA = baseInt;
        int endChanceA = baseEnd;
        

        int strChanceB = baseStr;
        int dexChanceB = baseDex;
        int intChanceB = baseInt;
        int endChanceB = baseEnd;
        

        //Creating temporary variables since we want to apply modifiers to the value, but we don't want to alter the base values.

        switch (chickenA.breedingBonus.stat) {
            case CoopTraining.TrainingType.All:

                break;
            case CoopTraining.TrainingType.Str:
                strChanceA += chickenA.breedingBonus.value;
                break;
            case CoopTraining.TrainingType.Dex:
                dexChanceA += chickenA.breedingBonus.value;
                break;
            case CoopTraining.TrainingType.Int:
                intChanceA += chickenA.breedingBonus.value;
                break;
            case CoopTraining.TrainingType.End:
                endChanceA += chickenA.breedingBonus.value;
                break;
        }

        switch (chickenB.breedingBonus.stat) {
            case CoopTraining.TrainingType.All:

                break;
            case CoopTraining.TrainingType.Str:
                strChanceB += chickenB.breedingBonus.value;
                break;
            case CoopTraining.TrainingType.Dex:
                dexChanceB += chickenB.breedingBonus.value;
                break;
            case CoopTraining.TrainingType.Int:
                intChanceB += chickenB.breedingBonus.value;
                break;
            case CoopTraining.TrainingType.End:
                endChanceB += chickenB.breedingBonus.value;
                break;
        }

        //Changing the chance variables based on the breedingBonus of the chicken. Tell me if there is a better way of doing this it is pretty bad.

        //First thing to do is check how many of the stats are passed down from each parent. This will mostly just be 2 from each however we could add a modifer that
        //causes 3 to be passed down from one chicken. To do that we would just check both chickens for a modifer that alters this, if one is found then we make the 
        //suitable changes to these values.

        int statsPassedFromA = 2;   //For now just leave these as 2 as the stat inheriting algorithm doesn't work if the chicken with the higher breeding potentcy passes down less stats than the other.
        int statsPassedFromB = 2;

        //Arrays to store the chances of each stat for both chickens

        int[] chickenAProbability = new int[] { strChanceA, dexChanceA, intChanceA, endChanceA };
        int[] chickenBProbability = new int[] { strChanceB, dexChanceB, intChanceB, endChanceB };
        int[] chickenAStats = new int[] { chickenA.strengthStat, chickenA.dexterityStat, chickenA.intelligenceStat, chickenA.enduranceStat };
        int[] chickenBStats = new int[] { chickenB.strengthStat, chickenB.dexterityStat, chickenB.intelligenceStat, chickenB.enduranceStat };
        int[] newChickenStats = new int[4];

        //Then we need to check which chicken has the highest breeding proficiency, this determines who we will select the first stat from. If they are equal then we just 
        //use chickenA.

        if (chickenA.breedingPotency < chickenB.breedingPotency)
        {
            //We then take it in turns for each chicken to pass down a stat until we have a value for all 4 stats.
            while(statsPassedFromB != 0) {
                statsPassedFromB -= 1;

                int totalB = 0;
                foreach(int value in chickenBProbability) {
                    //Here we are adding the probability of landing on each stat together to reach a total, this value is used when determining which stat we have landed on.
                    totalB += value;
                }

                int randomNoB = Random.Range(0, totalB);
                int trackingNoB = 0;

                for (int i = 0; i < 4; i++)
                {
                    //To figure out which stat we have landed on we generate a random number between 0 and the total probability, we then go through all the values in the array adding them
                    //to the tracking number. After each value is added we check to see if the tracking number has exceeded the random number. If it has then that is the stat we have randomly selected.
                    //You can imagine it as a pie chart where an item with a probability of 10 has it's own chunk that consists of the numbers 0-9. Then another item with a probability 5 would have the numbers
                    //10-14 and so on. This algorithm just makes it so the probability of all the items doesn't need to add up to 100%.
                    trackingNoB += chickenBProbability[i];
                    if(randomNoB <= trackingNoB)
                    {
                        //Once we find the value we have landed on we want to copy that over into the new chicken, and set the values of the breeding chicken to 0 so that they won't be chosen again.
                        newChickenStats[i] = chickenBStats[i];
                        chickenAProbability[i] = 0;
                        chickenBProbability[i] = 0;

                        if(Random.Range(0,100) < 5) {
                            //This is a simple mutation, we have a 5% chance of the stat being mutated, if it is then we just select a random number between 0 and 25.
                            newChickenStats[i] = Random.Range(0, 25);
                        }
                        break;
                    }
                }
                //After copying one of the stats from one parent we now need to copy a stat from the other parent. However this time one of the stats will be removed from the pool since
                //we can't have the parents passing down the same stats as that would overwrite one stat whilst leaving another null.
                while(statsPassedFromA != 0) {
                    statsPassedFromA -= 1;

                    int totalA = 0;
                    foreach (int value in chickenAProbability) {
                        totalA += value;
                    }

                    int randomNoA = Random.Range(0, totalA);
                    int trackingNoA = 0;

                    for (int i = 0; i < 4; i++)
                    {
                        trackingNoA += chickenAProbability[i];
                        if (randomNoA <= trackingNoA)
                        {
                            newChickenStats[i] = chickenAStats[i];
                            chickenAProbability[i] = 0;
                            chickenBProbability[i] = 0;

                            if (Random.Range(0, 100) < 5) {
                                newChickenStats[i] = Random.Range(0, 25);
                            }
                            break;
                        }
                    }
                    break;
                }
            }
        }
        else {
            //This is just the same thing again except we inherit from chickenA first rather than chickenB. 
            while (statsPassedFromA != 0)
            {
                statsPassedFromA -= 1;

                int totalA = 0;
                foreach (int value in chickenAProbability)
                {
                    totalA += value;
                }

                int randomNoA = Random.Range(0, totalA);
                int trackingNoA = 0;

                for (int i = 0; i < 4; i++)
                {
                    trackingNoA += chickenAProbability[i];
                    if (randomNoA <= trackingNoA)
                    {
                        newChickenStats[i] = chickenAStats[i];
                        chickenAProbability[i] = 0;
                        chickenBProbability[i] = 0;

                        if (Random.Range(0, 100) < 5) {
                            newChickenStats[i] = Random.Range(0, 25);
                        }
                        break;
                    }
                }

                while (statsPassedFromB != 0)
                {
                    statsPassedFromB -= 1;

                    int totalB = 0;
                    foreach (int value in chickenBProbability)
                    {

                        totalB += value;
                    }

                    int randomNoB = Random.Range(0, totalB);
                    int trackingNoB = 0;

                    for (int i = 0; i < 4; i++)
                    {

                        trackingNoB += chickenBProbability[i];
                        if (randomNoB <= trackingNoB)
                        {
                            newChickenStats[i] = chickenBStats[i];
                            chickenAProbability[i] = 0;
                            chickenBProbability[i] = 0;

                            if (Random.Range(0, 100) < 5) {
                                newChickenStats[i] = Random.Range(0, 25);
                            }
                            break;
                        }
                    }
                    break;
                }
            }
        }
        Debug.Log("Str: " + newChickenStats[0]);
        Debug.Log("Dex: " + newChickenStats[1]);
        Debug.Log("Int: " + newChickenStats[2]);
        Debug.Log("End: " + newChickenStats[3]);

        chickenA.breedingPotency += 0.5f;
        chickenB.breedingPotency += 0.5f;

        return newChickenStats;
    }

    static public CoopBreeding Instance() { return breeding; }
    static private CoopBreeding breeding = new CoopBreeding();
    protected CoopBreeding() { }
}
