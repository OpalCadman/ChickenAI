using System.Collections.Generic;
using UnityEngine;

public class CoopTraining {

    public enum TrainingType {
        Str = 0,
        Dex = 1,
        Int = 2,
        End = 3,
        All = 4,
    }

    public void AssignChicken(PlayerChicken chicken, TrainingType type) {
        //We would check if the chicken is available before calling this function. Would also need to check for training slots remaining.
        chicken.currentStatus = PlayerChicken.Status.Training;
        chicken.daysLeftUntilActive = 2;
        //At the end of each day we loop through all chickens decrementing this value by one.

        chicken.breedingPotency -= 0.2f;

        switch (type) {
            case TrainingType.Str:
                chicken.strengthStat += 3;
                chicken.intelligenceStat -= 1;
                break;

            case TrainingType.Dex:
                chicken.dexterityStat += 3;
                chicken.strengthStat -= 1;
                break;

            case TrainingType.Int:
                chicken.intelligenceStat += 3;
                chicken.dexterityStat -= 1;
                break;

            case TrainingType.End:
                chicken.enduranceStat += 2;
                break;

            case TrainingType.All:
                chicken.strengthStat+=1;
                chicken.dexterityStat+=1;
                chicken.intelligenceStat+=1;
                break;
        }
    }

    public void TrainChickens(List<PlayerChicken> allChickens)
    {
        foreach (var chicken in allChickens)
        {
            chicken.currentStatus = PlayerChicken.Status.Training;
            chicken.daysLeftUntilActive = 3;
            chicken.breedingPotency -= 0.2f;

            int rnd = Random.Range(0, 4);

            switch (rnd)
            {
                case 0:
                    chicken.strengthStat += 3;
                    chicken.intelligenceStat -= 1;
                    break;
                case 1:
                    chicken.dexterityStat += 3;
                    chicken.strengthStat -= 1;
                    break;
                case 2:
                    chicken.intelligenceStat += 3;
                    chicken.dexterityStat -= 1;
                    break;
                case 3:
                    chicken.enduranceStat += 2;
                    break;
                case 4:
                    chicken.strengthStat += 1;
                    chicken.dexterityStat += 1;
                    chicken.intelligenceStat += 1;
                    break;
            }
        }
    }

    static public CoopTraining Instance() { return training; }
    static private CoopTraining training = new CoopTraining();
    protected CoopTraining() { }
}
