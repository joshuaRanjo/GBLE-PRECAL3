using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationScript : MonoBehaviour
{
    public void EnterConversation()
    {
        EventManager.TriggerEvent("EnterConversation");
    }

    public void ExitConversation()
    {
        EventManager.TriggerEvent("ExitConversation");
    }
}
