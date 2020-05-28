using System;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Unity.AnimeToolbox.Editor {

//TODO-sin: 2020-5-28 Move to AnimeToolbox
internal static class UIElementsExtensions  {
    internal static ObjectField AddObjectField<T>(this VisualElement container, string label, UnityEngine.Object obj,
        Action<T> onChangedCallback) 
        where T: UnityEngine.Object 
    {
        ObjectField newField = new ObjectField(label) {
            objectType = typeof(T),
            value = obj
        };
        container.Add(newField);
        newField.RegisterCallback((ChangeEvent<UnityEngine.Object> evt) => {
            onChangedCallback(evt.newValue as T);
        });
       
        
        return newField;
    }
}

}