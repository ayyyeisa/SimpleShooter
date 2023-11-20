using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerController player;

    [SerializeField] private int lives = 3;

    public void PlayerDied()
    {
        lives--;

    }

    private void Respawn()
    {

    }
}
