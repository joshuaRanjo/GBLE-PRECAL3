using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class FlowchartController : MonoBehaviour
{
    [SerializeField] private Flowchart fc;

    public void Converse(string blockName)
    {
        fc.ExecuteBlock(blockName);
    } 

    public void NPCConversation(string npcName)
    {
        fc.SetStringVariable("NPC", npcName);
        fc.ExecuteBlock("Converse");
    }
}
