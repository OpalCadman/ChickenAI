using UnityEngine;

public class StateController : MonoBehaviour {
    //I had to create this class since in order to actually make use of
    //the StateMachine class we need an instance of it. Not much is going on
    //here except creating the first state (TitleScreenState) and then 
    //calling the HandleEvents(), Update() and Render() functions of the
    //stateMachine.
    public StateMachine stateMachine;

	void Start() {
        stateMachine.PushState(TitleScreenState.Instance());
	}
	void Update() {
		if (stateMachine.Running()) {
            stateMachine.HandleEvents();
            stateMachine.Update();
            stateMachine.Render();
        }
        else {
            stateMachine.CleanUp();
        }
	}
}
