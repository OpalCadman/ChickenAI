
public class DayManager {
    
    public enum Day {
        Monday = 0,
        Tuesday = 1,
        Wednesday = 2,
        Thursday = 3,
        Friday = 4,
        Saturday = 5,
        Sunday = 6
    }

    public Day currentDay;
    public int noOfDaysPast;

	public void AdvanceDay() {
        if((int)currentDay != 6) {
            currentDay++;
        } else {
            currentDay = 0;
        }
    }

    static public DayManager Instance() { return dayManager; }
    static private DayManager dayManager = new DayManager();
    protected DayManager() { }
}
