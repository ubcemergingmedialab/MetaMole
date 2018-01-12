using UnityEngine;


namespace Meta.Internal.HandPhysics
{
    /// <summary>
    ///     Renders a point cloud of the hand.
    /// </summary>
    internal class HandPointCloudFromRealPoints : MetaBehaviour
    {
        /// <summary>
        ///     particle colour before display confidence was turned on.
        /// </summary>
        protected Color _originalParticleColor; // for debugging

        /// <summary>
        ///     Color to render particles as.
        /// </summary>
        [SerializeField]
        private Color _particleColor = Color.red;

        /// <summary>
        ///     Size of particles.
        /// </summary>
        [SerializeField]

        //[Range(0.1f, 4)]
        private float _particleSize = 1;

        /// <summary>
        ///     Particle system used to render the cloud.
        /// </summary>
        internal ParticleSystem.Particle[] m_cloud;

        private ParticleSystem m_particleSystem;

        private const int MAX_POINTS = 80000;

        public Color particleColor
        {
            get { return _particleColor; }
            set { _particleColor = value; }
        }

        public float particleSize
        {
            get { return _particleSize; }
            set { _particleSize = value; }
        }

        PointCloudMetaData pointCloudMetaData = new PointCloudMetaData();

        /// <summary>
        ///     Initialise the point cloud.
        /// </summary>
        private void Start()
        {
            _originalParticleColor = _particleColor;
        }

        private PointCloudData<PointXYZConfidence> pointCloudData;

        private int prevFrameID = -1;
        /// <summary>
        ///     Update the point cloud.
        /// </summary>
        private void Update()
        {
            if (pointCloudData == null)
            {

                if (metaContext.Get<InteractionEngine>().GetCloudMetaData(ref pointCloudMetaData))
                {
                    pointCloudData = new PointCloudData<PointXYZConfidence>(pointCloudMetaData.maxSize);
                    if (pointCloudMetaData == null)
                    {
                        m_cloud = new ParticleSystem.Particle[MAX_POINTS];
                    }
                    else
                    {
                        m_cloud = new ParticleSystem.Particle[pointCloudMetaData.maxSize];
                    }
                    m_particleSystem = GetComponent<ParticleSystem>();

                }
                else
                {
                    return;
                }
            }
            metaContext.Get<InteractionEngine>().GetCloudData(ref pointCloudData);
                        
            if (prevFrameID != pointCloudData.frameId)
            {
                DisplayRealBodies();
                prevFrameID = pointCloudData.frameId;
            }

            if (Input.GetKey(KeyCode.Keypad9))
            {
                if (Input.GetKeyDown(KeyCode.KeypadPlus))
                {
                    particleColor += new Color(0.1f, 0.1f, 0.1f, 0);
                }
                else if (Input.GetKeyDown(KeyCode.KeypadMinus))
                {
                    particleColor -= new Color(0.1f, 0.1f, 0.1f, 0);
                }
            }

            if (Input.GetKey(KeyCode.Keypad8))
            {
                if (Input.GetKeyDown(KeyCode.KeypadPlus))
                {
                    _particleSize += 0.0005f;
                }
                else if (Input.GetKeyDown(KeyCode.KeypadMinus))
                {
                    _particleSize -= 0.0005f;
                }
            }
        }

        private void DisplayRealBodies()
        {
            for (int i = 0; i < pointCloudData.size; i++)
            {
                m_cloud[i].position = pointCloudData.points[i].vertex;
                m_cloud[i].position = new Vector3(m_cloud[i].position.x, -m_cloud[i].position.y, m_cloud[i].position.z);
                m_cloud[i].startSize = particleSize;
                m_cloud[i].startColor = particleColor;
            }
            m_particleSystem.SetParticles(m_cloud, pointCloudData.size);
        }

        /// <summary>
        ///     Enable point cloud.
        /// </summary>
        public void Enable()
        {
            enabled = true;
        }

        /// <summary>
        ///     Disable point cloud.
        /// </summary>
        public void Disable()
        {
            enabled = false;
        }
    }
}
