using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//条件转移比较复杂时可以将状态转移抽象为一个类实现解耦合。

public abstract class FSMTransition
{
    private State goalState;
    public abstract bool CheckTransition();
}
