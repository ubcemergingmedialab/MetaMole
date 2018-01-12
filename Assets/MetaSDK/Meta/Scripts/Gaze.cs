using UnityEngine;
using UnityEngine.EventSystems;

namespace Meta
{
    /// <summary>
    /// The Gaze class allows MetaBodies to receive OnGazeStart and OnGazeEnd events
    /// </summary>
    internal class Gaze : IEventReceiver
    {

        /// <summary>
        /// The GameObject that is currently being gazed at
        /// </summary>
        private GameObject _currentlyGazedObject;
        /// <summary>
        /// The GameObject that is currently being gazed at
        /// </summary>
        public GameObject currentlyGazedObject
        {
            get { return _currentlyGazedObject; }
        }
        /// <summary>
        /// Whether the currently gazed object implements an interface
        /// </summary>
        /// <remarks>
        /// These objects have received the OnGazeStart event and therefore should also receive the OnGazeEnd event.
        /// This saves us from having to call the ObjectImplemenetsGazeInterface() method twice.
        /// </remarks>
        private bool _objectImplementsInterface;

        /// <summary>
        /// Adds the IEventReceiver functions to the delegates in order to be called from MetaManager
        /// </summary>
        public void Init(IEventHandlers eventHandlers)
        {
            eventHandlers.SubscribeOnUpdate(Update);
        }

        /// <summary>
        /// Runs the update loop
        /// </summary>
        private void Update()
        {
            UpdateGazeCast(Camera.main.transform.position, Camera.main.transform.forward);
        }

        /// <summary>
        /// Updates the gazed GameObject and sends OnGazeStart event to newly gazed GameObject
        /// </summary>
        private void UpdateGazeCast(Vector3 origin, Vector3 direction)
        {
            GameObject gazedObject = null;
            RaycastHit hit;
            if (UnityEngine.Physics.Raycast(origin, direction, out hit, Mathf.Infinity))
            {
                gazedObject = hit.collider.gameObject;
                if (gazedObject != _currentlyGazedObject)
                {
                    EndGaze();
                }
                StartGaze(gazedObject);
                _currentlyGazedObject = gazedObject;
            }
            else
            {
                EndGaze();
            }
            _currentlyGazedObject = gazedObject;
        }

        /// <summary>
        /// Sends the OnGazeStart event to an object that is just being gazed at
        /// </summary>
        private void StartGaze(GameObject gazedObject)
        {
            
            if (gazedObject != _currentlyGazedObject && ObjectImplemenetsGazeInterface(gazedObject))
            {
                ExecuteEvents.Execute<IGazeStartEvent>(gazedObject, null, (x, y) => x.OnGazeStart());
                _objectImplementsInterface = true;
            }
        }

        /// <summary>
        /// Sends the OnGazeEnd event to an object that is no longer being gazed at
        /// </summary>
        private void EndGaze()
        {
            
            if (_currentlyGazedObject != null && _objectImplementsInterface)
            {
                ExecuteEvents.Execute<IGazeEndEvent>(_currentlyGazedObject, null, (x, y) => x.OnGazeEnd());
                _objectImplementsInterface = false;
            }
        }

        /// <summary>
        /// Checks if a gameobject's monobehaviours implement the IGazeStart or IGazeEnd interfaces
        /// </summary>
        private bool ObjectImplemenetsGazeInterface(GameObject objectToSearch)
        {
            MonoBehaviour[] list = objectToSearch.GetComponents<MonoBehaviour>();
            foreach (MonoBehaviour mb in list)
            {
                if (mb is IGazeStartEvent || mb is IGazeEndEvent)
                {
                    return true;
                }
            }
            return false;
        }

    }
}