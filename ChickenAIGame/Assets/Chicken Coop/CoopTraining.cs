
public class CoopTraining
{

    public enum TrainingType
    {
        Str = 0,
        Dex = 1,
        Int = 2,
        End = 3,
        All = 4,
    }

    public void AssignChicken(PlayerChicken chicken, TrainingType type)
    {
        //We would check if the chicken is available before calling this function.
        chicken.currentStatus = PlayerChicken.Status.Training;
        chicken.daysLeftUntilActive = 2;
        //At the end of each day we will need to loop through all chickens decrementing this value by one.

        switch (type)
        {
            case TrainingType.Str:
                chicken.strengthStat += 5;
                chicken.intelligenceStat -= 2;
                break;

            case TrainingType.Dex:
                chicken.dexterityStat += 5;
                chicken.strengthStat -= 2;
                break;

            case TrainingType.Int:
                chicken.intelligenceStat += 5;
                chicken.dexterityStat -= 2;
                break;

            case TrainingType.End:
                chicken.enduranceStat += 3;
                break;

            case TrainingType.All:
                chicken.strengthStat++;
                chicken.dexterityStat++;
                chicken.intelligenceStat++;
                break;
        }
    }

    static public CoopTraining Instance() { return training; }
    static private CoopTraining training = new CoopTraining();
    protected CoopTraining() { }
}
