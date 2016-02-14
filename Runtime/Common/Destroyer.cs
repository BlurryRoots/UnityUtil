using UnityEngine;
using System.Collections;

namespace BlurryRoots.Common {

    public static class Destroyer {

        public static void Destroy (Object go) {
            if (null != go) {
#if UNITY_EDITOR
                Object.DestroyImmediate (go);
#else
			    Object.Destroy (go);
#endif
            }
            else {
                Debug.LogWarning ("Trying to destroy object with null reference!");
            }
        }

    }

}
