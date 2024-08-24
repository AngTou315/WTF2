using System.Collections.Generic;
using cakeslice;
using UnityEngine;
using UnityEngine.UIElements;

namespace AAAShare.BsModules
{
    /// <summary>
    /// OutlineEffect
    /// </summary>
    public class HightLightManager : IHightLightManager
    {
        private HashSet<GameObject> hightingGoes = new HashSet<GameObject>();

        public void OnUpdate(float time, float realtime)
        {
        }

        public void OnClose()
        {
            hightingGoes.Clear();
        }

        public void Show(GameObject go)
        {
            if (hightingGoes.Add(go))
            {
                InnerShow(go, true);
            }
        }

        public void Hide(GameObject go)
        {
            if (hightingGoes.Contains(go))
            {
                InnerShow(go, false);
            }

            hightingGoes.Remove(go);
        }

        public void HideAll()
        {
            foreach (var hightingGo in hightingGoes)
            {
                InnerShow(hightingGo, false);
            }
        }

        private void InnerShow(GameObject go, bool isShow)
        {
            var outlines = go.GetComponentsInChildren<Outline>();
            var length = outlines.Length;
            for (int i = 0; i < length; i++)
            {
                var item = outlines[i];
                item.enabled = isShow;
            }
        }
    }
}