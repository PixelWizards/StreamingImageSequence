using System;
using System.IO;
using Unity.AnimeToolbox.Editor;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.StreamingImageSequence;
using UnityEngine.UIElements;

namespace UnityEditor.StreamingImageSequence {

/// <summary>
/// The inspector of RenderCacheCreator
/// </summary>
[CustomEditor(typeof(RenderCacheCreator))]
internal class RenderCacheCreatorInspector : Editor {

//----------------------------------------------------------------------------------------------------------------------
    void OnEnable() {
        m_asset = target as RenderCacheCreator;
    }

    public override VisualElement CreateInspectorGUI() {
        
        
        string path = Path.Combine(StreamingImageSequenceEditorConstants.UIELEMENTS_PATH, "RenderCacheCreatorInspector");
        VisualTreeAsset visualTree = UIElementsEditorUtility.LoadVisualTreeAsset(path);
        VisualElement inspector = visualTree.CloneTree();
        
        string ussPath = Path.Combine(StreamingImageSequenceEditorConstants.UIELEMENTS_PATH, "InspectorStyles");
        UIElementsEditorUtility.LoadAndAddStyle(inspector.styleSheets,ussPath);


        Button button = inspector.Query<Button>("UpdateButton").First();
        button.clickable.clicked += OnUpdateButtonClicked;
        
        return inspector;
    }    
    
//----------------------------------------------------------------------------------------------------------------------

    void OnUpdateButtonClicked() {
        //[TODO-sin: 2020-5-27] Check the MD5 hash of the folder before overwriting
        Debug.Log("Update Button Clicked");
    }


//----------------------------------------------------------------------------------------------------------------------
    private RenderCacheCreator m_asset = null;

}

}