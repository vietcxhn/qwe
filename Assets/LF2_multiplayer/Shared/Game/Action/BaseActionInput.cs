using System;
using UnityEngine;

namespace LF2
{
    public abstract class BaseActionInput : MonoBehaviour
    {
        protected NetworkCharacterState m_PlayerOwner;
        protected StateType m_ActionType;
        protected Action<StateRequestData> m_SendInput;
        Action m_OnFinished;

        public void Initiate(NetworkCharacterState playerOwner, StateType actionType, Action<StateRequestData> onSendInput, Action onFinished)
        {
            m_PlayerOwner = playerOwner;
            m_ActionType = actionType;
            m_SendInput = onSendInput;
            m_OnFinished = onFinished;
        }

        public void OnDestroy()
        {
            m_OnFinished();
        }

        public virtual void OnReleaseKey() {}
    }
}
