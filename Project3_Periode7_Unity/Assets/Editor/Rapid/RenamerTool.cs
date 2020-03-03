using UnityEngine;
using UnityEditor;
using System;

public class RenamerTool : EditorWindow {

    static RenamerTool window;

    Color colorToAdd;
    MonoScript objectToAdd;
    string nameForItems;
    string tagForItems;
    bool giveIndex;
    bool coloIsChanged;
    int index;
    Renderer rnd;
    Material mat;

    GameObject _currentSelected;

    [MenuItem( "Tools/Renamer" , false , 1 )]

    public static void ShowWindow() {
        window = (RenamerTool)GetWindow( typeof( RenamerTool ) );
    }

    private void OnEnable() {
        tagForItems = Selection.activeGameObject.tag;
        _currentSelected = Selection.activeGameObject;
    }

    //private void OnInspectorUpdate() {
    //    if(Selection.activeGameObject != null) {
    //        if(_currentSelected.GetInstanceID() != Selection.activeGameObject.GetInstanceID()) {
    //            tagForItems = Selection.activeGameObject.tag;
    //            _currentSelected = Selection.activeGameObject;
    //        }
    //    }
    //}

    void OnGUI() {
        GUILayout.Label( "Enter the new Name for the Items! " , EditorStyles.boldLabel );
        nameForItems = EditorGUILayout.TextField( "Name" , nameForItems );

        if(nameForItems != "") {
            giveIndex = EditorGUILayout.Toggle( "Give Index" , giveIndex );
        }

        EditorGUILayout.LabelField( "\n" );
        EditorGUILayout.LabelField( "Enter the new Tag for the Items!" , EditorStyles.boldLabel );

        tagForItems = EditorGUILayout.TagField( tagForItems );

        EditorGUILayout.LabelField( "\n" );
        EditorGUILayout.LabelField( "Enter the new Object for the Items!" , EditorStyles.boldLabel );

        objectToAdd = EditorGUILayout.ObjectField( objectToAdd , typeof( MonoScript ) , false ) as MonoScript;

        EditorGUILayout.LabelField( "\n" );
        EditorGUILayout.LabelField( "Enter the new Color for the Items!" , EditorStyles.boldLabel );

        EditorGUI.BeginChangeCheck();
        colorToAdd = EditorGUILayout.ColorField( "Color" , colorToAdd );
        if(!coloIsChanged) {
            coloIsChanged = EditorGUI.EndChangeCheck();
        } else {
            EditorGUI.EndChangeCheck();
        }
        if(GUILayout.Button( "Add!" )) {
            AddItems();
        }
        if(GUILayout.Button( "Make Prefab" )) {
            rnd = Selection.activeGameObject.GetComponent<Renderer>();
            mat = rnd.material;
            CreatePrefab();
        }
    }

    private void AddItems() {
        Renderer rend;
        foreach(GameObject obj in Selection.gameObjects) {

            rend = obj.GetComponent<Renderer>();

            if(!giveIndex && nameForItems != "" && tagForItems != null)
                obj.name = nameForItems;
            else if(giveIndex && nameForItems != "" && tagForItems != null) {
                obj.name = nameForItems + " (" + index + ")";
                obj.transform.SetSiblingIndex( index );
                index++;
            }

            if(coloIsChanged) {
                rend.sharedMaterial.color = colorToAdd;
            }

            obj.tag = tagForItems;
            if(objectToAdd != null) {
                Type _type = objectToAdd.GetClass();
                obj.AddComponent( _type );
            }
        }
        index = 0;

        //window.Close();
    }

    private void CreatePrefab() {
        GameObject obj = Selection.activeGameObject;
        string name = obj.name;
        rnd.material = mat;
        UnityEngine.Object prefab = EditorUtility.CreateEmptyPrefab( "Assets/" + name + ".prefab" );
        EditorUtility.ReplacePrefab( obj , prefab);
        AssetDatabase.Refresh();
    }
}


//CR: Thijmen Hermans 2GD2
