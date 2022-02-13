using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum BarrierType { normal, bouncier, flat }

public class Barrier : MonoBehaviour
{

    public BarrierType type;

    void BallImpact()
    {
        print("Wall was impacted");

    }

}
