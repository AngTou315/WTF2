using Ease.UI;
using UnityEngine;

namespace AAAShare.Adapter
{
    public abstract class BaseUILogic : MonoBehaviour
    {
        public BaseWindowConfig config { get; set; }
        public BaseWindowParam param { get; set; }

        public void Open()
        {
            OnOpen();
        }

        public void Close()
        {
            OnClose();
        }
        
        protected abstract void OnOpen();
        protected abstract void OnClose();
    }
}