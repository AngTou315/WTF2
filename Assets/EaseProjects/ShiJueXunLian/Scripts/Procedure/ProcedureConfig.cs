using System;
using AAAShare.BsPublic;
using Ease.Core;
using Ease.Procedure;
using UnityEngine;

namespace EaseProjects.Template.Procedure
{
    public class ProcedureConfig : MonoBehaviour
    {
        public string currentProcedure = "";

        private void Update()
        {
            currentProcedure = Entry.GetModule<IProcedureManager>().CrrentProcedure.ToString();
        }
    }
}