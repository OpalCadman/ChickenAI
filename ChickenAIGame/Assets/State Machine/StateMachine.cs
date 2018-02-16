//URGENT: Please tell me there is an easier way to get the last item in a list. This code looks so much worse than it does in C++ with vectors.

using UnityEngine;
using System.Collections.Generic;

public class StateMachine : MonoBehaviour {
    //This is the state machine that controls which state is currently active
    //whilst the game is running. It uses a stack structure so whenever we move to
    //the next state we are just pushing it onto the stack, if we want to leave the
    //current state and return to the previous state we are popping it off the stack.
    
    public void Awake() {
        Initialise();
    }

    //These functions will correlate to the functions found within each game state.
    //This is because the state machine is mostly just calling the functions from the
    //appropriate state (The one currently active).

    public void Initialise() {
        Debug.Log("****\tRunning\t****");
        isRunning = true;
    }
    public void CleanUp() {
        //If the state machines CleanUp() function is called it will go through each 
        //state in the state machine and call it's CleanUp() function before removing
        //it from the stack.
        Debug.Log("****\tCleaning\t****");
        while (states.Count != 0) {
            states[states.Count - 1].CleanUp();
            states.Remove(states[states.Count - 1]);
        }
    }
    public void PushState(GameState state) {
        //Checking if there is a state already on the stack, if there is we want to
        //pause it. Either way we want to add the state being pushed onto the stack and
        //call it's Initialise() function.
        Debug.Log("****\tPushing\t****");
        if (states.Count != 0) {
            states[states.Count - 1].Pause();
        }

        states.Add(state);
        states[states.Count - 1].Initialise();
    }
    public void PopState() {
        //Starts by checking if there is a state on the stack to pop off, if there is
        //we call it's CleanUp() function and then remove it from the stack. After that
        //we do another check to see if there is anything else on the stack, if there
        //is then we call it's Resume() function to reactive it from where it left off.
        Debug.Log("****\tPopping\t****");
        if (states.Count != 0) {
            states[states.Count - 1].CleanUp();
            states.Remove(states[states.Count - 1]);
        }

        if (states.Count != 0) {
            states[states.Count - 1].Resume();
        }
    }

    public void HandleEvents() {
        //Just calls the HandleEvents() function of the currently active state.
        states[states.Count - 1].HandleEvents(this);
    }
    public void Update() {
        //Just calls the Update() function of the currently active state.
        states[states.Count - 1].Update(this);
    }
    public void Render() {
        //Just calls the Render() function of the currently active state.
        states[states.Count - 1].Render(this);
    }

    public bool Running() { return isRunning; }
    public void Quit() {
        //Just calls its own CleanUp() function to empty the stack, then sets
        //isRunning to false.
        Debug.Log("****\tShutting Down\t****");
        CleanUp();
        isRunning = false;
    }

    private bool isRunning;
    private List<GameState> states = new List<GameState>();
    
    //This list keeps track of all the game states on the stack. Each time you
    //push a state onto the state machine it is added to the list, everytime
    //you pop a state, the last state in the list is removed. The last state
    //in the list is the active state, all states before that are paused.
}
