using UnityEngine;

namespace Meta.Examples
{
    /// <summary>
    /// An example Monobehaviour which uses the gaze events to change the color of the game object it is assigned to.
    /// </summary>
    public class GazeExampleScript : MetaBehaviour, IGazeStartEvent, IGazeEndEvent
    {
        /// <summary>
        /// The material applied to the object for rendering.
        /// </summary>
        private Material _renderMaterial;

        /// <summary>
        /// The color of the object when it is intended to appear dull.
        /// </summary>
        private Color _colorDull = new Color(0.05f, 0.05f, 0.05f);

        /// <summary>
        /// The color of the object when it is intended to appear highlighted.
        /// </summary>
        private Color _colorHighlighted;

        /// <summary>
        /// The interpolation variable for defining a color between dull and highlighted.
        /// </summary>
        private float _lambda = 0f;

        /// <summary>
        /// The amount by which to change _lambda every update frame.
        /// </summary>
        private float _delta = 0.05f;

        /// <summary>
        /// Whether the user is gazing on this object.
        /// </summary>
        private bool _bIsGazing;

        private void Start()
        {
            //Obtain a reference to the material assigned to this game object. 
            Renderer renderer = GetComponent<Renderer>();
            _renderMaterial = renderer.material;

            //Calculate a random highlighted color for this game object
            _colorHighlighted = Color.HSVToRGB(Random.Range(0f, 1f), 1f, 1f);
        }

        private void Update()
        {
            ChangeColor();
        }

        /// <summary>
        /// From the IGazeStartEvent interface. This occurs when the gaze gesture begins with this object as the subject.
        /// </summary>
        public void OnGazeStart()
        {
            _bIsGazing = true;
        }

        /// <summary>
        /// From the IGazeEndEvent interface. This occurs when the gaze gesture ends and this object is no longer the subject.
        /// </summary>
        public void OnGazeEnd()
        {
            _bIsGazing = false;
        }

        /// <summary>
        /// Update the color of the game object that this Monobehaviour is attached to. 
        /// The resulting color is between the color member variables _colorDull and _colorHighlighted
        /// </summary>
        private void ChangeColor()
        {
            //A bias for the interpolation. Positive values make highlighting occur quicker and dulling occur slower.
            float lerpBias = 0.75f;

            //Whether to incrementally increase/decrease the vibrance of the object in this frame.
            float sign = _bIsGazing ? 1 : -1;

            //Modify lambda to incrementally increase/decrease the vibrance of the game object. 
            _lambda += (sign + lerpBias) * _delta;
            _lambda = Mathf.Clamp(_lambda, 0f, 2f); //Allow lambda beyond 1 so that the game object may glow for a little longer. 

            //Calculate the color of the object. Lambda greater than 1 are clamped to 1.
            Color color = Color.Lerp(_colorDull, _colorHighlighted, Mathf.Clamp(_lambda, 0f, 1f));
            _renderMaterial.color = color;
        }
    }
}
