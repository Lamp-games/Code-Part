using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryState : MonoBehaviour
{
    /// <summary>
    /// This script manages the Storyline
    /// 
    /// </summary>
    public static StoryState instance; // a tasty singleton

    //gameplay programming is a lot like a fat bitch. everybody wants the story, but nobody wants to write an enum with 150 different things in it.
    //this is the enum, pls fill it up till the end. comment a bit in the extended switch of Update()
    public enum misancene { DJing , WalkAround , WalkToFridge , GetCheeseOutOfFridge }
    public misancene PlotPoint = misancene.DJing;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("We have a situation here " + StoryState.instance.gameObject.name + " and "+ gameObject.name); 
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        // Microsoft Visual Studio Code helps auto-fill a switch
        switch (PlotPoint)
        {
            case misancene.DJing:
                //would be at the dj table, and play soem music on loop
                break;
            case misancene.WalkAround:
                // pick 5 locations in hallway, bedroom , restroom.
                break;
            case misancene.WalkToFridge:
                // get to the chopper
                break;
            case misancene.GetCheeseOutOfFridge:
                //call an event to make cheese appear on the table
                break;
            default:
                break;
        }


    }
}
