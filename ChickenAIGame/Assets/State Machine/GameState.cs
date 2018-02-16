
public abstract class GameState {
    //This is the generic game state class that all game states will inherit from.
    //Each game state will be quite different but the following public functions
    //should be enough to cover all functionality of all game states.

    //For more information and an example on creating states check TitleScreenState.cs

    public abstract void Initialise();
    public abstract void CleanUp();
    public abstract void Pause();
    public abstract void Resume();
    public abstract void HandleEvents(StateMachine stateMachine);
    public abstract void Update(StateMachine stateMachine);
    public abstract void Render(StateMachine stateMachine);

    //The constructor is protected to prevent anyone from trying to create an instance
    //of this class.
    protected GameState() { }
}
