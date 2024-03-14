using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;       // �̺�Ʈ ���

[CreateAssetMenu(fileName = "NewEventChannel",menuName = "Events/Event Channel")]
public class EventChannel : ScriptableObject
{
    public UnityEvent OnEventRaised;


    public void RaiseEvent()
    {
        if(OnEventRaised != null)
        {
            OnEventRaised.Invoke();
        }
    }
}
