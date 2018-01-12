using UnityEngine;
using System.Collections;
using UnityEditor;

namespace Meta
{
    public class UnityMenu : Editor
    {
        [MenuItem("Help/Meta SDK2 Guide Documentation")]
        [MenuItem("Meta 2/Meta SDK2 Guide Documentation")]
        static void MetaDocumentationMenuItem()
        {
            MetaUtils.OpenURL(MetaUtils.metaDocsURL);
        }
    }
}
