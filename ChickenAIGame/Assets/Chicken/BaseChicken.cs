
public class BaseChicken {

    public int uniqueID;
    //Can't use their name's as an ID since you could easliy end up with 2 chickens with the same name
    //so we will just assign them an ID on creation, we can easily just track the last ID that was given
    //to a chicken and then add 1 to it. That would be the new ID for the next chicken.
    public int ageInDays;
    //Simple age that increments by 1 after each day. Should be taken into account when calculating
    //breeding and racing potency. 
    public int strengthStat, dexterityStat, intelligenceStat, enduranceStat;
    //These stats would be randomised for enemy chickens, and randomised for the player's starting 
    //chickens. They need to be able to be incremented and decremented through in-game training.
    //Determines how the chicken approaches obstacles in the races.
    public int racesCompleted;
    //Just tracks the number of times a chicken has competed in the races, the more they have completed
    //the more familiar they should be with the course and should perform better.
    private float racingPotency;
    //Not sure on exact calculations yet but this should combine other statistics such as age, stats,
    //races completed etc. to give an overall potency that could be used when determining how a chicken 
    //performs in the race.

    public int timesPlaced5th, timesPlaced4th, timesPlaced3rd, timesPlayed2nd, timesPlaced1st;
    //Another simple tracker, we just increment these as they place in the races.
    public string chickenName;
    //Could do with a long list of these to give randomly to the chickens as they are created, we want
    //the player to be able to edit the names of their own chicken.
    public int percentile;
    //To calculate percentile we need a list of all the chickens, chances are we will have 2 lists, one
    //for the players chicken and another for the opposing chickens. We just need to loop through each
    //chicken adding its stats together (Str, Dex, Int, End) and sorting all these totals from smallest
    //to largest. Once we have this list we just select any chicken we want and take its position in the
    //list and divide it by the total number of chickens, multiply that value by 100 and then we have
    //the percentile.
}
