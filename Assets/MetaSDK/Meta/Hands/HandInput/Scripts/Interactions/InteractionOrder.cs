using UnityEngine;
using System.Collections.Generic;

namespace Meta
{
    /// <summary>
    /// Controls order of Interaction execution based on Interaction priority
    /// </summary>
    public class InteractionOrder : MonoBehaviour
    {
        /// <summary>
        /// Interactions to be managed
        /// </summary>
        [SerializeField]
        private List<InteractionOrderItem> _itemList = new List<InteractionOrderItem>();

        private void Update()
        {
            bool engaged = false;
            for (int i = 0; i < _itemList.Count; ++i)
            {
                for (int j = 0; j < _itemList[i].InteractionList.Count; ++j)
                {
                    if (_itemList[i].InteractionList[j] != null)
                    {
                        _itemList[i].InteractionList[j].HigherPriorityRunning = engaged;
                    }
                }
                for (int j = 0; j < _itemList[i].InteractionList.Count; ++j)
                {
                    if (_itemList[i].InteractionList[j] != null && _itemList[i].InteractionList[j].State == InteractionState.On)
                    {
                        engaged = true;
                    }
                }
            }
        }

        /// <summary>
        /// Automatically called when the Reset command is given by the editor
        /// </summary>
        private void Reset()
        {
            //Add two at start for ease of use
            _itemList.Add(null);
            _itemList.Add(null);
        }

        /// <summary>
        /// Removes empty Interactions
        /// </summary>
        public void RemoveEmptyManipulations()
        {
            for (int i = 0; i < _itemList.Count; ++i)
            {
                _itemList[i].RemoveEmptyInteractions();
            }
        }
    }
}
