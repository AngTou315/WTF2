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
        public static Node currentState;
        void Start()
        {
            AddNode(new Test());
            AddNode(new NewBehaviourScript());
            currentState = node;
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
        public static void Next()
        {
            currentState = currentState.Next();
        }
    }

}
