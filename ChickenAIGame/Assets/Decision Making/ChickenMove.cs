using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenMove : MonoBehaviour
{

    public GameObject chicken_0;
    public GameObject chicken_1;
    public GameObject chicken_2;
    public GameObject chicken_3;

    float chicken_0_speed;
    float chicken_1_speed;
    float chicken_2_speed;
    float chicken_3_speed;

    float chicken_0_mod;
    float chicken_1_mod;
    float chicken_2_mod;
    float chicken_3_mod;

    int i = 0;
    int chickencount = 4;
    float walkspeed = 0.5f;
    float runspeed = 1f;
    float chickenmovespeed = 0f;
    ID3Action action;

    // Use this for initialization
    void Start()
    {




    }

    // Update is called once per frame
    void Update()
    {

        for (i = 0; i < chickencount; i++)
        {
            switch (action)
            {
                default:
                case ID3Action.STOP:
                    Stop();
                    break;
                case ID3Action.WALK:
                    Walk();
                    break;
                case ID3Action.RUN:
                    Run();
                    break;
                case ID3Action.JUMP:
                    Jump();
                    break;
            }

        }

    }

    public void SetAction(ID3Action newAction)
    {
        action = newAction;
    }

    private void Stop()
    {

        if (chickenmovespeed != 0f)
        {
            chickenmovespeed = 0f;
        }

        Move();
    }


    private void Walk()
    {

        if (chickenmovespeed != 0.5f)
        {
            chickenmovespeed = 0.5f;
        }

        Move();

    }

    private void Run()
    {

        if (chickenmovespeed != 0.5f)
        {
            chickenmovespeed = 0.5f;
        }

        Move();

    }

    private void Move()
    {
        switch (i)
        {
            case 0:
                chicken_0_speed = chickenmovespeed * chicken_0_mod;
                break;
            case 1:
                chicken_1_speed = chickenmovespeed * chicken_1_mod;
                break;
            case 2:
                chicken_2_speed = chickenmovespeed * chicken_2_mod;
                break;
            case 3:
                chicken_3_speed = chickenmovespeed * chicken_3_mod;
                break;
        }

    }

    private void Jump()
    {



    }


}
