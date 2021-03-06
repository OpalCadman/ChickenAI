﻿using UnityEngine;
public class CoopState : GameState {

    override public void Initialise(GameObject canvas) {
        UnityEngine.Debug.Log("[Coop] Active");

        var emptyGO = new UnityEngine.GameObject();
        emptyGO.name = "Chicken Coop";
        emptyGO.AddComponent<ChickenCoop>();
    }
    override public void CleanUp() {
        UnityEngine.Debug.Log("[Coop] Cleaning Up");

        GameObject.Find("Chicken Coop").GetComponent<ChickenCoop>().CleanUp();
    }
    override public void Pause() {
        UnityEngine.Debug.Log("[Coop] Paused");
    }
    override public void Resume() {
        UnityEngine.Debug.Log("[Coop] Resumed");
    }
    override public void HandleEvents(StateMachine stateMachine) {
        if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.Escape))
        {
            UnityEngine.Debug.Log("[Coop] Exiting to Title Screen");
            stateMachine.PopState();
        }
    }
    override public void Update(StateMachine stateMachine) {

    }
    override public void Render(StateMachine stateMachine) {

    }

    static public CoopState Instance() { return coopState; }

    static private CoopState coopState = new CoopState();

    protected CoopState() { }
}