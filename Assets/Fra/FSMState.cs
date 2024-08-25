using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;

namespace HaoFsm
{
    public class FSMManager : MonoBehaviour
    {
        public Node node;
        public Node currentState;
        public List<Data> datas = new List<Data>();
        void Start()
        {
            AddNode(new Test());
            AddNode(new NewBehaviourScript());
            currentState = node;
            currentState.OnCreate(this);
            currentState.OnEnter();
        }

        // Update is called once per frame
        void Update()
        {
            currentState.OnUpdate();
        }

        public void AddNode<T>(T node) where T : IFSM
        {
            Node current = this.node;
            if (this.node == null)
            {
                this.node = new StateNode<T>(node);
                return;
            }

            while (current.nextNode != null)
            {
                current = current.nextNode;
            }
            current.nextNode = new StateNode<T>(node);
        }
        public object GetData(string key)
        {
            foreach (var item in datas)
            {
                if (item.key == key)
                {
                    return item.value;
                }
            }
            return null;
        }
        public void SetData(string key, UnityEngine.Object data)
        {
            datas.Add(new Data()
            {
                key = key,
                value = data
            });
        }
        public void Next()
        {
            currentState = currentState.Next();
        }
    }
    [Serializable]
    public class Data
    {
        public string key;
        public UnityEngine.Object value;
    }

}
