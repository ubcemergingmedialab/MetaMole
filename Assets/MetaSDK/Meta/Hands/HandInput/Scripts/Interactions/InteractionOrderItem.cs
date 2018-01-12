using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Meta
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class InteractionOrderItem
    {
        /// <summary>
        /// 
        /// </summary>
        [SerializeField]
        [FormerlySerializedAs("_manipulationList")]
        private List<Interaction> _interactionList = new List<Interaction>();

        /// <summary>
        /// 
        /// </summary>
        public List<Interaction> InteractionList
        {
            get { return _interactionList; }
        }

        /// <summary>
        /// 
        /// </summary>
        public InteractionOrderItem()
        {
            //add one reference on start so that user has place to assign something
            _interactionList.Add(null);
        }

        /// <summary>
        /// 
        /// </summary>
        public void RemoveEmptyInteractions()
        {
            _interactionList.RemoveAll(m => m == null);
            _interactionList.Add(null);
        }
    }
}