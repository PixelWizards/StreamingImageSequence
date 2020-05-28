using UnityEngine.Playables;

namespace UnityEngine.StreamingImageSequence {

internal class RenderCacheCreator : MonoBehaviour {

    internal bool IsInitialized() {
        return null != m_camera && null != m_director;
    }
    
//----------------------------------------------------------------------------------------------------------------------
    internal PlayableDirector GetDirector() { return m_director;}
    internal Camera           GetCamera()   { return m_camera;}
    internal void SetDirector(PlayableDirector director) { m_director = director;}
    internal void SetCamera(Camera cam) { m_camera = cam;}

//----------------------------------------------------------------------------------------------------------------------
    
    [SerializeField] private PlayableDirector m_director;
    [SerializeField] private Camera           m_camera;



}

} //end namespace

