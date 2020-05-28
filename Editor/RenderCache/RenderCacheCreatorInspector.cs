using System;
using System.Collections;
using System.IO;
using Unity.AnimeToolbox.Editor;
using Unity.EditorCoroutines.Editor;
using UnityEditor.Timeline;
using UnityEditor.UIElements;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.StreamingImageSequence;
using UnityEngine.Timeline;
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

//----------------------------------------------------------------------------------------------------------------------
    public override VisualElement CreateInspectorGUI() {
        
        
        string path = Path.Combine(StreamingImageSequenceEditorConstants.UIELEMENTS_PATH, "RenderCacheCreatorInspector");
        VisualTreeAsset visualTree = UIElementsEditorUtility.LoadVisualTreeAsset(path);
        VisualElement inspector = visualTree.CloneTree();
        
        string ussPath = Path.Combine(StreamingImageSequenceEditorConstants.UIELEMENTS_PATH, "InspectorStyles");
        UIElementsEditorUtility.LoadAndAddStyle(inspector.styleSheets,ussPath);


        //Fields
        VisualElement fieldsContainer = inspector.Query<VisualElement>("FieldsContainer").First();
        fieldsContainer.AddObjectField<Camera>("Camera", m_asset.GetCamera(), (Camera cam) => {
            m_asset.SetCamera(cam);           
        });
        fieldsContainer.AddObjectField<PlayableDirector>("Director", m_asset.GetDirector(), (PlayableDirector dir) => {
            m_asset.SetDirector(dir);           
        });
        
        
        //Update
        Button button = inspector.Query<Button>("UpdateButton").First();
        button.clickable.clicked += OnUpdateButtonClicked;
        
        return inspector;
    }    
    
//----------------------------------------------------------------------------------------------------------------------


    void OnUpdateButtonClicked() {
        //[TODO-sin: 2020-5-27] Check the MD5 hash of the folder before overwriting
        if (!m_asset.IsInitialized()) {
            EditorUtility.DisplayDialog("StreamingImageSequence", "Please initialize RenderCacheCreator", "Ok");
            return;
        }
        
        PlayableDirector director = m_asset.GetDirector();
        TimelineAsset timelineAsset = director.playableAsset as TimelineAsset;
        if (null == timelineAsset) {
            EditorUtility.DisplayDialog("StreamingImageSequence", "The assigned playableDirector is invalid", "Ok");
            return;
            
        }

        
        
        //Loop time 
        m_timePerFrame = 1.0f / timelineAsset.editorSettings.fps;
        m_updateDirectorTime = director.initialTime;
        EditorCoroutineUtility.StartCoroutine(UpdateRenderCacheCoroutine(), this);


    }

    private double m_updateDirectorTime = 0;
    private double m_timePerFrame = 0;
    
//----------------------------------------------------------------------------------------------------------------------    
    
    IEnumerator UpdateRenderCacheCoroutine() {

        //Assign render texture
        Camera cam = m_asset.GetCamera();
        RenderTexture prevTargetTexture = cam.targetTexture;
        RenderTexture rt = new RenderTexture(cam.pixelWidth, cam.pixelHeight, 24);
        rt.Create();
        cam.targetTexture = rt;
        
        
        int    fileCounter = 0;
        PlayableDirector director = m_asset.GetDirector();
        while (m_updateDirectorTime <= director.initialTime + director.duration) {
            SetDirectorTime(director,m_updateDirectorTime);
            m_updateDirectorTime += m_timePerFrame;

            Capture(cam, fileCounter.ToString("000"));
            Debug.Log("Time: " + m_updateDirectorTime + " " + (m_updateDirectorTime < director.initialTime + director.duration).ToString());
            yield return null;
            ++fileCounter;
        }
        
        cam.targetTexture = prevTargetTexture;
        
    }
    
    private static void SetDirectorTime(PlayableDirector director, double time) {
        director.time = time;
        TimelineEditor.Refresh(RefreshReason.ContentsModified); 
    }    

    private void Capture(Camera cam, string fileCounter) {
        
        
        RenderTexture prevRenderTexture = RenderTexture.active;
  
        RenderTexture camRT = cam.targetTexture;
        RenderTexture.active = camRT;
 
        cam.Render();

        
        Texture2D image = new Texture2D(camRT.width, camRT.height);
        image.ReadPixels(new Rect(0, 0, camRT.width, camRT.height), 0, 0);
        image.Apply();
 
        byte[] bytes = image.EncodeToPNG();
        ObjectUtility.Destroy(image);
 
        File.WriteAllBytes(Application.dataPath + "/StreamingAssets/" + fileCounter + ".png", bytes);
        
        //Set back
        RenderTexture.active = prevRenderTexture;
        
    }

    void Something(Camera cam) {
        RenderTexture rt = new RenderTexture(cam.pixelWidth, cam.pixelHeight, 24);
        rt.Create();
        cam.targetTexture = rt;
    }

//----------------------------------------------------------------------------------------------------------------------
    private RenderCacheCreator m_asset = null;

}

}