
public class MainMenuState : GameState {

    override public void Initialise() {
        UnityEngine.Debug.Log("[Main Menu] Active");
    }
    override public void CleanUp() {
        UnityEngine.Debug.Log("[Main Menu] Cleaning Up");
    }
    override public void Pause() {
        UnityEngine.Debug.Log("[Main Menu] Paused");
    }
    override public void Resume() {
        UnityEngine.Debug.Log("[Main Menu] Resumed");
    }
    override public void HandleEvents(StateMachine stateMachine) {
        if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.Escape)) {
            UnityEngine.Debug.Log("[Main Menu] Exiting Main Menu");
            stateMachine.PopState();
            //This is an example of using PopState() to remove the currently 
            //active state from the stack. Doing this will automatically 
            //call the CleanUp() function and will Resume() the state next
            //in the stack.
        }
    }
    override public void Update(StateMachine stateMachine) {
 
    }
    override public void Render(StateMachine stateMachine) {
      
    }

    static public MainMenuState Instance() { return mainMenuState; }

    static private MainMenuState mainMenuState = new MainMenuState();

    protected MainMenuState() { }
}
