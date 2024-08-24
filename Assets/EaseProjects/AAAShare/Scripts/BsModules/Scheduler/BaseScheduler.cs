using System;
using System.Collections.Generic;
using Ease.Core;
using UnityEngine;
using UnityEngine.Assertions;

namespace EaseProjects.AAAShare.BsModules.Scheduler
{
    public abstract class BaseDelayInfo
    {
        public Action DelayAction;
        public Action OverAction;
        public abstract bool IsOver();
        public abstract Action Process();
    }

    /// <summary>
    /// 按时间延迟，
    /// </summary>
    public class TimeDelayInfo : BaseDelayInfo
    {
        public float DelayInterval; //时间间隔
        public int RepeatTime; //重复次数
        public int CurrentTime; //当前次数
        public float LastTime;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="delayTime">时间间隔</param>
        /// <param name="repeatTime">重复次数：</param>
        /// <param name="action">回调</param>
        public TimeDelayInfo(float delayTime, int repeatTime, Action action, Action overAction)
        {
            DelayInterval = delayTime;
            RepeatTime = repeatTime;
            DelayAction = action;
            OverAction = overAction;
            CurrentTime = 0;
            LastTime = Time.realtimeSinceStartup;
        }

        public override bool IsOver()
        {
            return CurrentTime >= RepeatTime;
        }

        public override Action Process()
        {
            if (IsOver()) return null;

            bool doAction = false;
            if (Time.realtimeSinceStartup - LastTime >= DelayInterval)
            {
                LastTime = Time.realtimeSinceStartup;
                CurrentTime++;
                doAction = true;
            }

            if (doAction) return DelayAction;
            return null;
        }
    }

    /// <summary>
    /// 按帧数延迟
    /// </summary>
    public class FrameDelayInfo : BaseDelayInfo
    {
        public int DelayInterval; //次数间隔
        public int RepeatTime; //重复次数
        public int CurrentTime; //当前次数
        public int LastFrameIndex = 0;

        public override bool IsOver()
        {
            return CurrentTime >= RepeatTime;
        }

        public FrameDelayInfo(int delayInterval, int repeatTime, Action delayAction, Action overAction)
        {
            this.DelayInterval = delayInterval;
            this.RepeatTime = repeatTime;
            this.DelayAction = delayAction;
            OverAction = overAction;
            CurrentTime = 0;
            LastFrameIndex = 0;
        }

        public override Action Process()
        {
            if (IsOver()) return null;
            bool doAction = false;
            LastFrameIndex++;
            if (LastFrameIndex >= this.DelayInterval)
            {
                LastFrameIndex = 0;
                CurrentTime++;
                doAction = true;
            }

            if (doAction) return DelayAction;
            return null;
        }
    }

    public abstract class BaseScheduler : IScheduler
    {
        private List<Action> AllActions = new List<Action>();

        private List<Action> PostTaskActions = new List<Action>();

        private List<Action> DelayActions = new List<Action>();

        private List<BaseDelayInfo> DelayInfos = new List<BaseDelayInfo>();

        public event Action<string> Logger;
        public event Action<string> LoggerError;

        public void PostTask(Action task)
        {
            List<Action> temp = PostTaskActions;
            lock (temp)
            {
                PostTaskActions.Add(task);
            }
        }

        /// <summary>
        /// 下一帧运行
        /// </summary>
        /// <param name="task"></param>
        public void Delay(Action task)
        {
            DelayActions.Add(task);
        }

        /// <summary>
        /// 延迟一段时间,
        /// </summary>
        /// <param name="task"></param>
        /// <param name="second"></param>
        /// <param name="repeatTime"></param>
        public void Delay(Action task, float second, int repeatTime = 1, Action over = null)
        {
            Assert.IsTrue(task != null);
            Assert.IsTrue(second > 0);
            Assert.IsTrue(repeatTime >= 1);
            BaseDelayInfo item = new TimeDelayInfo(second, repeatTime, task, over);
            DelayInfos.Add(item);
        }

        public void Delay(Action task, int frame, int repeatTime = 1, Action over = null)
        {
            Assert.IsTrue(task != null);
            Assert.IsTrue(frame >= 1);
            Assert.IsTrue(repeatTime >= 1);
            BaseDelayInfo item = new FrameDelayInfo(frame, repeatTime, task, over);
            DelayInfos.Add(item);
        }

        public virtual void OnUpdate(float time, float realtime)
        {
            List<Action> postTaskActions = PostTaskActions;
            lock (postTaskActions)
            {
                if (PostTaskActions.Count > 0)
                {
                    for (int i = 0; i < PostTaskActions.Count; i++)
                    {
                        AllActions.Add(PostTaskActions[i]);
                    }

                    PostTaskActions.Clear();
                }
            }

            if (DelayActions.Count > 0)
            {
                for (int j = 0; j < DelayActions.Count; j++)
                {
                    AllActions.Add(DelayActions[j]);
                }

                DelayActions.Clear();
            }

            if (DelayInfos.Count > 0)
            {
                Predicate<BaseDelayInfo> temp = (info) =>
                {
                    var action = info.Process();
                    if (action != null)
                    {
                        AllActions.Add(action);
                    }

                    if (info.IsOver())
                    {
                        info.OverAction?.Invoke();
                        return true;
                    }

                    return false;
                };
                DelayInfos.RemoveAll(temp);
            }

            RunAction();
        }

        private void RunAction()
        {
            for (int i = 0; i < AllActions.Count; i++)
            {
                Action action = AllActions[i];
                try
                {
                    action();
                }
                catch (Exception ex)
                {
                    LoggerError?.Invoke(ex.ToString());
                }
            }

            AllActions.Clear();
        }


        public virtual void OnClose()
        {
            AllActions.Clear();
            PostTaskActions.Clear();
            DelayActions.Clear();
            DelayInfos.Clear();
        }
    }
}