using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface  IDmageAble 
{
    bool getFlagLive
    {
        get;
    }
    void setDmg(int dmg, GameObject prefabEffect);
}
