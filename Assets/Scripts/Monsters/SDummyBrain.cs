using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SDummyBrain : SMonsterBrain {

    public override void Think(Monster monster)
    {
        if(monster.health <= 0)
        {
            Destroy(monster.gameObject);
        }
    }
}
