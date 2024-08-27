using System;
using AAAShare.Adapter;
using AAAShare.BsModules;
using Ease.Config;
using UnityEngine;

namespace AAAShare.BsPublic.Project
{
    public class ProjectManager : MonoBehaviour, IProjectManager
    {
        [SerializeField] private ProjectData _projectData = new ProjectData();

        public Difficulty difficulty
        {
            get => _projectData.difficulty;
            set => _projectData.difficulty = value;
        }
    }
}