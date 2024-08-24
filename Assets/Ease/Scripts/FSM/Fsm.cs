using System;
using System.Collections.Generic;
using Ease.Core;
using UnityEngine;

namespace Ease.FSM
{
    public class Fsm<T> : ILife
    {
        private IFsmState<T> currentState;

        private T owner;

        private Dictionary<string, object> dicCache;
        private Dictionary<Type, IFsmState<T>> dicStates;

        public Fsm(T owner, params IFsmState<T>[] states)
        {
            this.owner = owner;
            dicCache = new Dictionary<string, object>();
            dicStates = new Dictionary<Type, IFsmState<T>>();
            foreach (var item in states)
            {
                dicStates.Add(item.GetType(), item);
            }
        }

        #region 数据存储

        public void SetData(string key, object value)
        {
            if (dicCache.ContainsKey(key))
                dicCache[key] = value;
            else
                dicCache.Add(key, value);
        }

        public object GetData(string key)
        {
            if (dicCache.TryGetValue(key, out var value))
                return value;
            return null;
        }

        #endregion

        #region Get/Set

        public IFsmState<T> CurrentState => currentState;

        #endregion

        #region 逻辑

        public void ChangeState<Tstate>() where Tstate : IFsmState<T>
        {
            if (dicStates.TryGetValue(typeof(Tstate), out var newState))
            {
                if (currentState != null)
                    currentState.OnQuit(this);
                currentState = newState;
                currentState.OnEnter(this);
            }
            else
            {
                throw new Exception($"{this.GetType().Name}:ChangeState Error: {typeof(Tstate).FullName} ");
            }
        }

        public void Start<Tstate>() where Tstate : IFsmState<T>
        {
            ChangeState<Tstate>();
        }

        #endregion

        public void OnUpdate(float time, float realtime)
        {
            if (currentState != null)
                currentState.OnUpdate(this, time, realtime);
        }

        public void OnClose()
        {
            currentState = null;
            dicStates.Clear();
            dicCache.Clear();
        }
    }
}