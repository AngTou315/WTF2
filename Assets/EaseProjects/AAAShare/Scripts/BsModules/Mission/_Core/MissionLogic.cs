using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using UnityEngine;

namespace AAAShare.BsModules
{
    /// <summary>
    /// 任务逻辑：
    /// </summary>
    public class MissionLogic : MonoBehaviour, IMissionLogic
    {
        [ListDrawerSettings(ShowIndexLabels = true, ListElementLabelName = "name")]
        public List<MissionData> Datas = new List<MissionData>();

        [SerializeField] private MissionManagerState _state = MissionManagerState.INACTIVATED;

        public MissionManagerState state
        {
            get => _state;
            set
            {
                var oldState = _state;
                _state = value;
                if (oldState != _state)
                {
                    OnStateChange?.Invoke(_state);
                }
            }
        }

        //正在进行的任务；
        public List<Mission> runningMissions = new List<Mission>();

        //完成的列表
        public List<int> finishMissions = new List<int>();

        //实验状态变化后的事件
        public event Action<MissionManagerState> OnStateChange;

        //其他变化后的事件
        public event Action OnChange;

        //高亮
        //public event Action<GameObject> OnHightLightGameObject;

        public string currentMissionTip { get; set; }


        private void Start()
        {
            state = MissionManagerState.RUNNING;
        }

        private void Update()
        {
            if (state == MissionManagerState.RUNNING)
            {
                var isChange = false;
                //1更新当前激活的任务列表。
                isChange = UpdateRunningMissions();
                // 尝试添加新的正在运行的任务，返回是否有变化
                isChange = AddRunningMissions() || isChange;
                // 判断任务数量，如果为0改变管理器状态
                UpdateState();
                // 根据是否有变化更新其他信息
                UpdateOther(isChange);
            }
        }

        #region Logic
        //负责更新正在运行的任务。遍历 要执行任务的列表runningMissions ，调用每个任务的 OnUpdate() 方法。
        //如果任务状态变为 OVER，将其添加到 finishMissions 列表中，并移除不再运行的任务。
        private bool UpdateRunningMissions()
        {
            // 遍历当前正在运行的任务列表
            foreach (var runningMission in runningMissions)
            {
                runningMission.OnUpdate();//调用任务的 OnUpdate() 方法，更新任务。
                //如果任务的状态变为 OVER（完成），则将该任务标记为完成，并从运行列表中移除。
                if (runningMission.State == MissionState.OVER)
                {
                    //// 将任务ID添加到已完成任务列表
                    finishMissions.Add(runningMission.Data.id);
                    // 执行任务的 OnDisable 方法
                    runningMission.OnDisable();
                }
            }
            //移除所有已经完成的任务。
            var num = runningMissions.RemoveAll(item => item.State != MissionState.RUNNING);
            return num > 0;
        }
        //检查 Datas 中未完成的任务，并将新的任务添加到 runningMissions 列表中，确保始终有任务在执行。
        private bool AddRunningMissions()
        {
            // 从 Datas 列表中找到一个未完成的任务
            var first = Datas.FirstOrDefault(x => !finishMissions.Contains(x.id));
            // 如果找到一个未完成的任务，并且该任务没有在运行列表中
            if (first != null && runningMissions.FirstOrDefault(x => x.Data == first) == null)
            {
                // 创建一个新的 Mission 实例
                var newMission = new Mission(first);
                //// 执行新任务的 OnEnable 方法
                newMission.OnEnable();
                // 将新任务添加到运行任务列表中
                runningMissions.Add(newMission);
                // 返回 true，表示有新任务添加
                return true;
            }
            return false;
        }

        private void UpdateState()
        {
            if (runningMissions.Count == 0)
            {
                state = MissionManagerState.OVER;
            }
        }

        private void UpdateOther(bool isChange)
        {
            //// 如果还有正在运行的任务
            if (runningMissions.Count > 0)
                currentMissionTip = runningMissions[0].Data.name;
            else
                //// 如果都执行完了，清空任务提示
                currentMissionTip = "";
            //// 如果任务发生了变化，触发 OnChange 事件
            if (isChange) 
                OnChange?.Invoke();
        }

        #endregion
    }
}