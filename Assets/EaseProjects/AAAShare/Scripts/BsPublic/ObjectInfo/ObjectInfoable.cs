using System;
using AAAShare.BsPublic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

namespace AAAShare.BsPublic.ObjectInfo
{
    public class ObjectInfoable : MonoBehaviour
    {
        private void OnMouseEnter()
        {
            Entry.GetModule<IObjectInfoManager>().Show(this.gameObject);
        }
        private void OnMouseExit()
        {
            Entry.GetModule<IObjectInfoManager>().Hide(this.gameObject);
        }
    }
}