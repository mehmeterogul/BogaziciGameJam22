using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum STATE { WALKING, ROBBING }
    public STATE gameState = STATE.WALKING;

    PlayerMovement playerMovement;
    NPCSpawner npcSpawner;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        npcSpawner = FindObjectOfType<NPCSpawner>();
    }

    public void ChangeGameStateToWalking()
    {
        gameState = STATE.WALKING;
        playerMovement.PlayerCanWalk();
        npcSpawner.ResumeSpawning();
        ResumeNPCsWalk();
    }

    public void ChangeGameStateToRobbing()
    {
        gameState = STATE.ROBBING;
        playerMovement.PlayerCantWalk();
        npcSpawner.PauseSpawning();
        StopNPCsWalk();
    }

    void StopNPCsWalk()
    {
        NPCMovement[] nPCMovement = FindObjectsOfType<NPCMovement>();

        if(nPCMovement.Length > 0)
        {
            foreach(NPCMovement npc in nPCMovement)
            {
                npc.StopWalk();
            }
        }
    }
    
    void ResumeNPCsWalk()
    {
        NPCMovement[] nPCMovement = FindObjectsOfType<NPCMovement>();

        if(nPCMovement.Length > 0)
        {
            foreach(NPCMovement npc in nPCMovement)
            {
                npc.ResumeWalk();
            }
        }
    }
}
