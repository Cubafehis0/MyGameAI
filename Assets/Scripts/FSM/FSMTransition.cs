using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//����ת�ƱȽϸ���ʱ���Խ�״̬ת�Ƴ���Ϊһ����ʵ�ֽ���ϡ�

public abstract class FSMTransition
{
    private State goalState;
    public abstract bool CheckTransition();
}
