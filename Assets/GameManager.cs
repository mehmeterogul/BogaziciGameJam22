using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum STATE { WALKING, ROBBING }
    public STATE gameState = STATE.WALKING;

    PlayerMovement playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    public void ChangeGameStateToWalking()
    {
        gameState = STATE.WALKING;
        playerMovement.PlayerCanWalk();
    }

    public void ChangeGameStateToRobbing()
    {
        gameState = STATE.ROBBING;
        playerMovement.PlayerCantWalk();
    }
}
