using System;
using UnityEngine;

namespace EaseProjects.AAAShare.BsPublic.Component
{
    public class ClickableObject : MonoBehaviour
    {
        public event Action OnClick;

        private void OnMouseDown()
        {
            OnClick?.Invoke();
        }

        private void OnMouseEnter()
        {
        }

        private void OnMouseExit()
        {
        }
    }
}