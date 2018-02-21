
public class TitleScreenState : GameState {
    //This is the title screen state to be used by the state machine. Any additional 
    //states that you want to create will need the functions/variables seen in this 
    //class.

    override public void Initialise() {
        //This function is called when you initially push the state onto the stack.
        //So anything that you want to happen when the states first becomes active
        //should be placed here. Mostly assigning variables/calling functions etc.
        UnityEngine.Debug.Log("[Title Screen] Active");
    }
    override public void CleanUp() {
        //This function is called when when we pop this state off from the stack.
        //It can also be called if we call the Quit() function from the StateMachine.
        //Here it is just intended that you make any changes that you only need when
        //the state is inactive. Mostly used for disabling stuff.
        UnityEngine.Debug.Log("[Title Screen] Cleaning Up");
    }
    override public void Pause() {
        //If this state is active and we push another state on top of it, this state
        //will become paused. You would probably use this similarly to CleanUp()
        //except you do not want to delete everything since we will resume this state
        //later, and we want it to either pick up where it left off or reset back to
        //default values.
        UnityEngine.Debug.Log("[Title Screen] Paused");
    }
    override public void Resume() {
        //If this state is in the stack but not currently active, once we pop off enough
        //states to the point where this state becomes active we need to resume it. 
        //Depending on the state you would either want to reset some things, maybe
        //it is a menu and you want to reset the button they last had highlighted,
        //or maybe you want to keep the last tab they had open in the menu etc.
        UnityEngine.Debug.Log("[Title Screen] Resumed");
    }
    override public void HandleEvents(StateMachine stateMachine) {
        //Here is where you would place most keyboard/mouse events etc. This is called 
        //every frame so we keep checking for different events such as 'W' key being held
        //down, then you would write what to do if W is pressed. I don't know much about
        //getting events from Unity show tell me if you know how to do it properly.
        if (UnityEngine.Input.anyKeyDown) {
            UnityEngine.Debug.Log("[Title Screen] Entering Main Menu");
            stateMachine.PushState(MainMenuState.Instance());
        }
        //This is an example of creating a new state from an already existing state.
        //This is also seen in the StateController, however this just shows that you
        //can use PushState() and PopState() from the states themselves rather than 
        //using only the StateController. The StateController was just acting like a 
        //main.cpp file for me. For an example of the PopState() function check
        //MainMenuState.cs and it's HandleEvents() function.
    }
    override public void Update(StateMachine stateMachine) {
        //Don't know if the Update() and Render() functions are neccessary, this is mostly code
        //I wrote in C++ with SFML where I had to create my own game loop and keep track of the 
        //render window etc. But I left it in here in case it is useful. Called every frame after
        //HandleEvents(), you could just place everything into HandleEvents() but it might be
        //easier to read in here.
    }
    override public void Render(StateMachine stateMachine) {
        //Again, similar to Update(), called every frame after Update(). Don't know if this 
        //will be used at all with Unity.
    }

    //When you push a state onto the stack you need to pass in a GameState, instead
    //of creating an instance of a state and then passing that into the function you
    //would just write:
    //stateMachine.PushState(TitleScreenState.Instance());
    //This creates an instance to be pushed onto the stack from the function itself, 
    //and we let that instance keep track of itself since we are constantly adding
    //and removing states from the stack.

    static public TitleScreenState Instance() { return titleState; }

    static private TitleScreenState titleState = new TitleScreenState();

    protected TitleScreenState() { }
}
