using System;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.StreamingImageSequence;
using UnityEngine.UIElements;

namespace UnityEditor.StreamingImageSequence {

/// <summary>
/// The inspector of RenderCacheCreator
/// </summary>
[CustomEditor(typeof(RenderCacheCreator))]
internal class RenderCachePlayableAssetInspector : Editor {

//----------------------------------------------------------------------------------------------------------------------
    void OnEnable() {
        m_asset = target as RenderCacheCreator;
    }

    
//----------------------------------------------------------------------------------------------------------------------
    public override void OnInspectorGUI() {
        //[TODO-sin: 2020-5-27] Check the MD5 hash of the folder before overwriting
        if (GUILayout.Button("Refresh")) {
            Debug.Log("Clicked the image");
        }

    }


//----------------------------------------------------------------------------------------------------------------------
    private RenderCacheCreator m_asset = null;

}

}