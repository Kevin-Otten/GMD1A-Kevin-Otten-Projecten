﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    // events die ik zou maken had ik liever in een instantsiations van scripts stoppen maar aangezien veel van unitys dingen niet bruikbaar zonder de kenis om die aan te roepen deed ik het voornu maar op deze manier(daarom zoveel bools).
    public GameObject eventCamara;
    public UIManager ui;
    public ObjectiveManager obj;
    public PlayerManager pm;
    public NpcTrigger currentTrigger;
    public bool firstContact = true;
    public bool firstGoodby = true;
    public GameObject target;
    public bool camaraMovement;
    public bool eventNPV2ExitDone;
    public GameObject invisibleStopWall;

    // movement van een camara in 1  van de events
    void Update()
    {
        if(camaraMovement == true)
        {
            eventCamara.transform.position = Vector3.MoveTowards(eventCamara.transform.position, target.transform.position, 10 * Time.deltaTime);
        }
    }

    // insert info van Trigger script op npc
    public void TriggerInsert(NpcTrigger trigger)
    {
        currentTrigger = trigger;
    }

    // event voor de enter van een trigger
    public void EventEnter()
    {
        if(currentTrigger.enter == 1)
        {
            if(firstContact == true)
            {
                StartCoroutine(popupTextNPC1Enter(2));
                firstContact = false;
            }
        }

        if(currentTrigger.enter == 2)
        {
            pm.SetConversationSettings(currentTrigger.gameObject);
        }
    }

    // event voor de exit van een trigger
    public void EventExit()
    {
        if(currentTrigger.exit == 1)
        {
            if(firstGoodby == true)
            {
                StartCoroutine(popupTextNPC1Exit(2));
                firstGoodby = false;
            }
        }

        if(currentTrigger.exit == 2 && obj.objectives[1] == true && eventNPV2ExitDone != true)
        {
            StartCoroutine(EventNPC2Exit(20));
        }
    }

    //coroutines voor NPC1 Trigger
    IEnumerator popupTextNPC1Enter(float i)
    {
            ui.PopUpTextInsert("Hey Over Here!");
            yield return new WaitForSeconds(i);
            ui.PopUpTextInsert("");
    }
    IEnumerator popupTextNPC1Exit(float i)
    {
            ui.PopUpTextInsert("ByBy");
            yield return new WaitForSeconds(i);
            ui.PopUpTextInsert("");
    }

    //coroutine voor NPC2 Trigger
    IEnumerator EventNPC2Exit(float i)
    {
        pm.MovementOnOf();
        pm.CamaraOnOf();
        camaraMovement = true;
        yield return new WaitForSeconds(i);
        camaraMovement = false;
        pm.CamaraOnOf();
        pm.MovementOnOf();
        eventNPV2ExitDone = true;
        invisibleStopWall.SetActive(false);
    }
}

