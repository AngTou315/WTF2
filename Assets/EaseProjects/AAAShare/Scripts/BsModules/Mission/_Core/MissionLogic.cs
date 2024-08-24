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

        [ShowInInspector] public int runningMissionsCount => runningMissions.Count;

        //完成的列表
        public List<int> finishMissions = new List<int>();

        //实验状态发生了改变；
        public event Action<MissionManagerState> OnStateChange;

        //任何变化；
        public event Action OnChange;

        //播放声音
        public event Action<String> OnPlaySound;

        //高亮
        public event Action<GameObject> OnHightLightGameObject;

        //log
        public event Action<string> Log;

        public event Action<string> LogError;

        #region Properties

        public string currentMissionTip { get; set; }

        #endregion

        #region UNITY

        //任务数据
        //当前激活的任务列表。
        private void Awake()
        {
        }

        private void OnDestroy()
        {
            Log = null;
            LogError = null;
        }

        private void Start()
        {
            state = MissionManagerState.RUNNING;
            if (Log == null)
                Log += Debug.Log;
            if (LogError == null)
                LogError += Debug.LogError;
        }

        private void Update()
        {
            if (state == MissionManagerState.RUNNING)
            {
                var isChange = false;
                //1更新当前激活的任务列表。
                isChange = UpdateRunningMissions();
                //2增加新的激活任务列表。
                isChange = AddRunningMissions() || isChange;
                //3 更新状态
                UpdateState();
                //4 
                UpdateOther(isChange);
            }
        }

        #endregion

        #region Logic

        private bool UpdateRunningMissions()
        {
            //增加进完成列表
            foreach (var runningMission in runningMissions)
            {
                runningMission.OnUpdate();
                if (runningMission.State == MissionState.OVER)
                {
                    finishMissions.Add(runningMission.Data.id);
                    runningMission.OnDisable();
                }
            }

            //移除所有已经完成的任务。
            var num = runningMissions.RemoveAll(item => item.State != MissionState.RUNNING);
            return num > 0;
        }

        private bool AddRunningMissions()
        {
            var first = Datas.FirstOrDefault(x => !finishMissions.Contains(x.id));
            if (first != null && runningMissions.FirstOrDefault(x => x.Data == first) == null)
            {
                var newMission = new Mission(first);
                newMission.OnEnable();
                runningMissions.Add(newMission);
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
            if (runningMissions.Count > 0)
                currentMissionTip = runningMissions[0].Data.name;
            else
                currentMissionTip = "";
            if (isChange) OnChange?.Invoke();
        }

        #endregion
    }
}