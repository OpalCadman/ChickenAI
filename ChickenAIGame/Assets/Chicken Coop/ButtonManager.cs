﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour {

    public List<GameObject> chickenStats;
    public GameObject trainingButton;
    public GameObject breedingButton;
    public GameObject dayCounter;
    public GameObject advanceDay;
    public GameObject raceButton;
    public GameObject race;

    private ChickenCoop chickenCoopRef;
    private CoopTraining training = CoopTraining.Instance();
    private CoopBreeding breeding = CoopBreeding.Instance();
    private DayManager dayManager = DayManager.Instance();

    bool trainingActive = false;

    public bool coopAlive = false;

    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {

        switch (dayManager.currentDay)
        {
            case DayManager.Day.Monday:
                trainingButton.GetComponent<Button>().interactable = true;
                breedingButton.GetComponent<Button>().interactable = false;
                advanceDay.GetComponent<Button>().interactable = true;
                raceButton.GetComponent<Button>().interactable = false;
                break;
            case DayManager.Day.Tuesday:
                trainingButton.GetComponent<Button>().interactable = true;
                breedingButton.GetComponent<Button>().interactable = false;
                advanceDay.GetComponent<Button>().interactable = true;
                break;
            case DayManager.Day.Wednesday:
                trainingButton.GetComponent<Button>().interactable = true;
                breedingButton.GetComponent<Button>().interactable = false;
                advanceDay.GetComponent<Button>().interactable = true;
                break;
            case DayManager.Day.Thursday:
                trainingButton.GetComponent<Button>().interactable = true;
                breedingButton.GetComponent<Button>().interactable = false;
                advanceDay.GetComponent<Button>().interactable = true;
                break;
            case DayManager.Day.Friday:
                trainingButton.GetComponent<Button>().interactable = true;
                breedingButton.GetComponent<Button>().interactable = false;
                advanceDay.GetComponent<Button>().interactable = true;
                break;
            case DayManager.Day.Saturday:
                trainingButton.GetComponent<Button>().interactable = false;
                breedingButton.GetComponent<Button>().interactable = true;
                advanceDay.GetComponent<Button>().interactable = true;
                break;
            case DayManager.Day.Sunday:
                trainingButton.GetComponent<Button>().interactable = false;
                breedingButton.GetComponent<Button>().interactable = false;
                advanceDay.GetComponent<Button>().interactable = false;
                raceButton.GetComponent<Button>().interactable = true;
                break;
        }


        if (coopAlive)
        {
            if (chickenCoopRef.playerChickens.Count != 0)
            {
                if (chickenCoopRef.playerChickens[0].daysLeftUntilActive == 0) { trainingActive = false; }
            }
        }
        
        if (trainingActive)
        {
            trainingButton.GetComponent<Button>().interactable = false;
        }
	}

    public void ActivateButtons()
    {
        foreach (var chicken in chickenStats)
        {
            chicken.SetActive(true);
        }

        raceButton.SetActive(true);
        advanceDay.SetActive(true);
        dayCounter.SetActive(true);
        breedingButton.SetActive(true);
        trainingButton.SetActive(true);
    }

    public void DeactivateButtons()
    {
        foreach (var chicken in chickenStats)
        {
            chicken.SetActive(false);
        }

        raceButton.SetActive(false);
        advanceDay.SetActive(false);
        dayCounter.SetActive(false);
        breedingButton.SetActive(false);
        trainingButton.SetActive(false);
    }

    public void Initialise()
    {
        chickenCoopRef = GameObject.Find("Chicken Coop").GetComponent<ChickenCoop>();
        dayCounter.GetComponent<Text>().text = "Day: " + dayManager.currentDay
                                               + "\nDays Past: " + dayManager.noOfDaysPast;
        UpdateValues();
    }

    private void LoadChickenData()
    {
        foreach (var chicken in chickenCoopRef.playerChickens)
        {
            
        }
    }

    public void TrainingButton()
    {
        training.TrainChickens(chickenCoopRef.playerChickens);
        chickenCoopRef.ChickenDecrement();
        dayCounter.GetComponent<Text>().text = "Day: " + dayManager.currentDay
                                               + "\nDays Past: " + dayManager.noOfDaysPast;
        UpdateValues();
        trainingActive = true;
    }

    public void BreedingButton()
    {
        chickenCoopRef.BreedChickens();
        chickenCoopRef.ChickenDecrement();
        dayCounter.GetComponent<Text>().text = "Day: " + dayManager.currentDay
                                               + "\nDays Past: " + dayManager.noOfDaysPast;
        UpdateValues();
    }

    public void RaceButton()
    {
        Instantiate(race);
        List<PlayerChicken> selectedChickens = chickenCoopRef.SelectChickens();
        GameObject[] actualChickens = GameObject.FindGameObjectsWithTag("Chicken");

        for(int i = 0; i < 3; i++)
        {
            actualChickens[i].AddComponent<ActualPlayerChicken>();
            actualChickens[i].GetComponent<ActualPlayerChicken>().chickenName = selectedChickens[i].chickenName;
            actualChickens[i].GetComponent<ActualPlayerChicken>().strength = selectedChickens[i].strengthStat;
            actualChickens[i].GetComponent<ActualPlayerChicken>().dexterity = selectedChickens[i].dexterityStat;
            actualChickens[i].GetComponent<ActualPlayerChicken>().intelligence = selectedChickens[i].intelligenceStat;
            actualChickens[i].GetComponent<ActualPlayerChicken>().endurance = selectedChickens[i].enduranceStat;

        }

        chickenCoopRef.ChickenDecrement();
        dayCounter.GetComponent<Text>().text = "Day: " + dayManager.currentDay
                                               + "\nDays Past: " + dayManager.noOfDaysPast;
        UpdateValues();
    }

    public void AdvanceDayButton()
    {
        chickenCoopRef.ChickenDecrement();
        dayCounter.GetComponent<Text>().text = "Day: " + dayManager.currentDay
                                               + "\nDays Past: " + dayManager.noOfDaysPast;
        UpdateValues();
    }

    private void UpdateValues()
    {

        int displaySlots = 0;

        foreach (var chicken in chickenCoopRef.playerChickens)
        {
            if (displaySlots >= 18) { break; }

            chickenStats[displaySlots].GetComponent<Text>().text = "Name: " + chicken.chickenName
                                                    + "\nID: " + chicken.uniqueID
                                                    + "\nStatus: " + chicken.currentStatus
                                                    + "\n\tStrength: " + chicken.strengthStat
                                                    + "\n\tDexterity: " + chicken.dexterityStat
                                                    + "\n\tIntelligence: " + chicken.intelligenceStat
                                                    + "\n\tEndurance: " + chicken.enduranceStat;

            displaySlots += 1;
        }
    }
}
