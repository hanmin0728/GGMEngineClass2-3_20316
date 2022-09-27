using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAtkAble
{
    AtkBehaviour nowAtkBehaviour
    {
        get;
    }
    void OnExecuteAttack(int atkIdx);
}
