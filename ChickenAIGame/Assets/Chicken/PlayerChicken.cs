﻿using System.Collections.Generic;

public class PlayerChicken : BaseChicken {

    public struct BreedingBonus {
        //Whenever a chicken trains it gains a breeding bonus of 0.1 in that stat. Training another stat will remove the bonus
        //and give a bonus in the newly trained stat instead. If the chicken trains the same stat multiple times the value will keep
        //increasing by 0.1. This value is used when calculating the stats passed down whilst breeding. The value will increase
        //the chance of passing down the corresponding stat.
        public CoopTraining.TrainingType stat;
        public int value;
        public BreedingBonus(CoopTraining.TrainingType statParameter, int valueParameter) { stat = statParameter; value = valueParameter; } //Initialisation lists don't exist in C#.
    }

    public enum Perk {
        //Here is where we will list all the perks the chickens can get, think of this as more 
        //a declaration of the perk names, the definitions will be wherever the perk is relevant.
        //For example if we have a perk that makes their next training session twice as effective.
        //We would add that perk to the list. In the code that handles training the chickens we will
        //check if that chicken has anything in the perkList, if it does we will check for any relevant
        //perks such as the one mentioned earlier. That is where we would then define what the perk actual
        //does.
    }

    public struct ChickenPerk {
        //Aside from the actual perk effect itself we would also need a duration it doesn't last forever.
        //When checking a chicken for it's perks list, if it has a perk we would then check the duration,
        //if the duration is 0 then we would remove it from the list and move onto the next perk in the list 
        //if there is one.
        int duration;
        Perk perk;
    }

    public enum Status {
        //Just a list of states a chicken can be in, we will need to keep track of this when the player is
        //selecting chickens to train/breed/race etc. We would want to grey out the chickens that are busy.
        Idle = 0,
        Training = 1,
        Breeding = 2,
        Racing = 3,
        Egg = 4 // Chickens are created at the beginning of the breeding phase but only become available to use at the end.
    };

    public int enduranceCurrent;
    //As the chicken does training/rests their endurance will decrease/increase, we need to track this value 
    //so we know when a chicken is able to train or not. This value could also be used during the races, the 
    //lower their endurance is the worse they will perform.
    public Status currentStatus = Status.Idle;
    public int daysLeftUntilActive;
    private List<ChickenPerk> perkList = new List<ChickenPerk>();

    public float breedingPotency = 10;
    //Similar to racingPotency from BaseChicken.cs, not sure on calculations yet but it would help determine
    //the result of the chicken breeding.
    public BreedingBonus breedingBonus = new BreedingBonus(CoopTraining.TrainingType.All, 0);
    //Well this is a lot messier than I had hoped. The breeding bonus here would just be a propety of all player chickens so it
    //it easily accessible from the CoopBreeding.cs where it is required to calclate the offspring.
}
