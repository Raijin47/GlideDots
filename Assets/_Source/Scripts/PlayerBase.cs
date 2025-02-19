using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{

    public void AddBall(BallType type)
    {
        switch (type)
        {
            case BallType.Speed: break;
            case BallType.Heal: break;
            case BallType.Damage: break;
        }
    }
}