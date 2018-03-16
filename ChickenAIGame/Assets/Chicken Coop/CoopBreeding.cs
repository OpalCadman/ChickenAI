
public class CoopBreeding {

    private int baseStr = 1;
    private int baseDex = 1;
    private int baseInt = 1;
    private int baseEnd = 1;
    //These are the base chances of passing down each stat. Since they are all equal and there are 4 of them there would be a 25% chance of landing on any one.

    public void AssignChickens(PlayerChicken chickenA, PlayerChicken chickenB) {
        chickenA.currentStatus = PlayerChicken.Status.Breeding;
        chickenB.currentStatus = PlayerChicken.Status.Breeding;
        chickenA.daysLeftUntilActive = 3;
        chickenB.daysLeftUntilActive = 3;

        bool strPassed = false;
        bool dexPassed = false;
        bool intPassed = false;
        bool endPassed = false;

        //More temp variables, these track which stats have been passed down already to prevent both chickens trying to pass down the same stat.

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

        int statsPassedFromA = 2;
        int statsPassedFromB = 2;

        //Then we need to check which chicken has the highest breeding proficiency, this determines who we will select the first stat from. If they are equal then we just 
        //use chickenA.

        if (chickenA.breedingPotency < chickenB.breedingPotency)
        {
            //We then take it in turns for each chicken to pass down a stat until we have a value for all 4 stats.
            while(statsPassedFromB != 0) {
                statsPassedFromB -= 1;

                int total = strChanceB + dexChanceB + intChanceB + endChanceB;
                //To find which stat we pass down we need to add up all the chanceB's to get a value such as 4. Then you generate a random number between 0 and 4, say we get 3. Then
                //I forgot the next part but I have done it before in previous work so I can just check the rest of the algorithm there. However the difference here is that we need to 
                //remove the chance of getting a stat that has already been gotten, so after a stat has been selected we will need to minus that chancevalue from the total.

                while(statsPassedFromA != 0) {
                    statsPassedFromA -= 1;
                }
            }
        }
    }

    static public CoopBreeding Instance() { return breeding; }
    static private CoopBreeding breeding = new CoopBreeding();
    protected CoopBreeding() { }
}
