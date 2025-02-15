﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPartyManager : PartyManager
{
    void Start()
    {
        FindPartyMembers();
        SortPartyPositions();
        HideAllButFirst();
        
        //GameManager.OnEnterBattle += RevealAllParty;
    }
    void OnCollisionEnter(Collision col)
    {
        //Debug.Log("Collided with "+col.gameObject.name);
        if (col.gameObject.layer == LayerMask.NameToLayer("WildEnemy"))
        {
            StartCoroutine(DelayPositioning(col));
        }
    }
    IEnumerator DelayPositioning(Collision col)
    {
        yield return new WaitForSeconds(BattleManager.instance.delayTimeBeforePartyReveals);

        //RevealAllParty();

        transform.Translate(-transform.forward * 6);
        col.transform.Translate(transform.forward * 6);

        BattleManager.BattleStart(this, col.gameObject.GetComponent<PartyManager>());
    }
    public void GainExp(int expGained)
    {
        foreach(Player player in party)
        {
            if(player.IsAlive)
                player.GainExp(expGained);
        }
    }
    
}
