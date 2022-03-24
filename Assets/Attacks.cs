using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacks : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

/* Attack
 *   WedgeAttack
 *     Wedge
 *   ProjectileAttack
 *     ProjectileBurst
 *   BeamAttack
 *   ChargeAttack
 *   PlumeAttack
 */
    

public abstract class Attack
{
    public Boss boss;
    float warningDuration;
    int damage;
}

public class WedgeAttack : Attack
{
    List<Wedge> wedges;
}

public class Wedge // NOT ATTACK
{
    float startAngle;
    float endAngle;
    float range;
}
public class ProjectileAttack : Attack
{
    List<ProjectileBurst> bursts;
    float burstDelay;
}
public class ProjectileBurst // NOT ATTACK
{
    List<float> angles;
    float bulletSpeed;
    float bulletSize;
}

public class BeamAttack : Attack
{
    float startAngle;
    float endAngle;
    float duration;
    float range;
}

public class ChargeAttack : Attack
{
    float speed;
    float range;
    bool isHoming;
}

public class PlumeAttack : Attack
{
    List<Vector2> plumePositions;
}
