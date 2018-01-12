
using UnityEngine;

namespace Meta
{
    //todo: make this inacceesbible to external devs before release
    /// <summary>   A hand kernel settings. </summary>
    ///
    /// <seealso cref="T:Meta.MetaBehaviour"/>
    public class HandKernelSettings : MetaBehaviour
    {
        public enum HandKernelType
        {
            NONE,
            META1,
            META1LEGACY,
            META1LEGACY_NOHANDPROCESSOR,
            META1LEGACY_NODEPTHPOINTCLOUD,
            META1LEGACY_NODEPTHVISUALIZER,
            META1CLUSTEREDPOINTCLOUD
        }

        [SerializeField]
        public HandKernelType handKernelType = HandKernelType.META1LEGACY_NOHANDPROCESSOR;

        /// <summary>   The minimum confidence. </summary>
        [Range(1, 255)]
        public int minimumConfidence = 20;

        /// <summary>   The maximum noise. </summary>
        [Range(0.001f, 1.0f)]
        public float maximumNoise = 0.035f;

        /// <summary>   The minimum depth to perform hand physics interactions. </summary>
        [Range(1, 500)]
        public int minimumDepth = 50;

        /// <summary>   The maximum depth to perform hand physics interactions. </summary>
        [Range(500, 1200)]
        public int maximumDepth = 1200;

        /// <summary>   Size of the median filter. </summary>
        [Range(3, 5)]
        public int medianFilterSize = 3;

        /// <summary>   Size of the morphological filter. </summary>
        [Range(0, 3)]
        public int morphologicalFilterSize = 3;

        /// <summary>   The morpholical iteration. </summary>
        [Range(1, 3)]
        public int morpholicalIteration = 1;

        /// <summary>   true to debug display. </summary>
        public bool debugDisplay = true;

        /// <summary>   The subsample factor. </summary>
        [Range(0.1f, 1)]
        public float clusterRadius= 1;

        /// <summary>   The grab threshold. </summary>
        [Range(-20, 20)]
        [HideInInspector]
        public int grabThresh = -10;

        /// <summary>   true to enable, false to disable the kalman. </summary>
        [HideInInspector]
        public bool enableKalman = false;

        /// <summary>   true to use default values. </summary>
        public bool UseDefaultValues = true;

        void Awake()
        {
            if (UseDefaultValues)
            {
                minimumConfidence = 10;
                maximumNoise = 0.07f;
                minimumDepth = 50;
                maximumDepth = 1000;
                medianFilterSize = 3;
                morphologicalFilterSize = 3;
                morpholicalIteration = 1;
                debugDisplay = true;
                clusterRadius = 0.5f;
                grabThresh = -10;
                enableKalman = true;
            }
        }

        internal void UpdateOptions(ref DepthDataCleanerOptions depthDataCleanerOptions, ref CloudGeneratorOptions cloudGeneratorOptions, IPointCloudSource<PointXYZConfidence> pointCloudSource)
        {
            // -- Set Generation Options
            cloudGeneratorOptions.clusterRadius = clusterRadius;
            cloudGeneratorOptions.debugDisplay = debugDisplay;

            // -- Set DepthData Cleaner Options
            depthDataCleanerOptions.maximumNoise = maximumNoise;
            depthDataCleanerOptions.minimumDepth = minimumDepth;
            depthDataCleanerOptions.maximumDepth = maximumDepth;
            depthDataCleanerOptions.minimumConfidence = minimumConfidence;
            depthDataCleanerOptions.medianFilterSize = medianFilterSize;
            depthDataCleanerOptions.morphologicalFilterSize = morphologicalFilterSize;
            depthDataCleanerOptions.morpholicalIteration = morpholicalIteration;
            depthDataCleanerOptions.debugDisplay = debugDisplay;
            depthDataCleanerOptions.cameraRotated180 = debugDisplay;



            // -- Set DepthData Cleaner Options
            HandKernelInterop.SetDepthDataCleanerOptions(ref depthDataCleanerOptions);

            // -- Set PointCloudSource Generation Options
            pointCloudSource.SetPointCloudGeneratorOptions(cloudGeneratorOptions);
        }

    }

}
