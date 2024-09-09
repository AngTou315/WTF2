using System;
using System.Collections.Generic;
using Ease.Core;
using UnityEngine;

namespace Ease.FSM
{
    public class Fsm<T> : ILife
    {
        private IFsmState<T> currentState;// 当前状态

        private T owner;// 拥有状态机的对象

        private Dictionary<string, object> dicCache;// 状态机内部数据缓存
        private Dictionary<Type, IFsmState<T>> dicStates;// 状态表，状态类型与状态实例的映射

        //接受状态机的所有者owner和多个状态实例states作为参数。
        //初始化数据缓存dicCache和状态字典dicStates。
        //将每个传入的状态添加到状态字典中，键为状态的类型，值为状态的实例。
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
        //用于向状态机的数据缓存中添加或更新数据。
        //可以存储任何东西，我们可以将即将切换的场景存储进来
        public void SetData(string key, object value)
        {
            if (dicCache.ContainsKey(key))
                dicCache[key] = value;
            else
                dicCache.Add(key, value);
        }
        //用于从状态机的数据缓存中获取数据。
        public object GetData(string key)
        {
            if (dicCache.TryGetValue(key, out var value))
                return value;
            return null;
        }

        #endregion

        #region Get/Set
        //公开封装后的当前状态
        public IFsmState<T> CurrentState => currentState;

        #endregion

        #region 逻辑
        //用于切换状态。它首先从状态字典dicStates中查找目标状态（Tstate），
        //如果找到，则调用当前状态的OnQuit方法，退出当前状态。
        //接着，更新currentState为新状态并调用其OnEnter方法，进入新的状态。
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
                throw new Exception("ChangeState Error ");
            }
        }
        //启动状态机，调用ChangeState<Tstate>切换到初始状态。
        public void Start<Tstate>() where Tstate : IFsmState<T>
        {
            ChangeState<Tstate>();
        }

        #endregion
        //每帧更新状态机逻辑，调用当前状态的OnUpdate方法。
        public void OnUpdate(float time, float realtime)
        {
            if (currentState != null)
                currentState.OnUpdate(this, time, realtime);
        }
        //关闭状态机，清空当前状态和缓存的所有状态及数据。
        public void OnClose()
        {
            currentState = null;
            dicStates.Clear();
            dicCache.Clear();
        }
    }
}