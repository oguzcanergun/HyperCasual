using System;
using UnityEngine;
using UnityEditor;
using Backbones;

public class BallCreatorWindow : EditorWindow
{
    private string[] colorOptions = Enum.GetNames(typeof(BallColour));
    private int colourIndex = 0;

    [MenuItem("Tools/Ball Creator Editor")]

    public static void Open()
    {
        GetWindow<BallCreatorWindow>();
    }

    [SerializeField] Transform ballsParent;
    [CustomNameAttribute] [SerializeField] Material[] ballsMaterials;
    [SerializeField] GameObject ballsIndicatorPrefab;

    private void OnGUI()
    {
        SerializedObject sObject = new SerializedObject(this);

        GUILayout.Space(20);
        GUILayout.Label("Colour tags and their Material equivalents", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(sObject.FindProperty("ballsMaterials"));

        GUILayout.Space(20);
        GUILayout.Label("Indicator prefab for balls", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(sObject.FindProperty("ballsIndicatorPrefab"));

        GUILayout.Space(20);
        GUILayout.Label("Root of all ball objects", EditorStyles.boldLabel);

        EditorGUILayout.PropertyField(sObject.FindProperty("ballsParent"));
        

        if (ballsParent!= null)
        {
            if (ballsParent.GetComponent<BallMaster>() != null)
            {
                GUILayout.Space(20);
                GUILayout.Label("Create a new ball with in-box settings", EditorStyles.boldLabel);
                EditorGUILayout.BeginVertical("box");
                DrawMenuForBall();
                EditorGUILayout.EndVertical();
            }
            else
            {
                EditorGUILayout.HelpBox("Parent transform must contain BallMaster script. Please assign a parent with BallMaster script.", MessageType.Warning);
            }
        }
        else
        {
            EditorGUILayout.HelpBox("Parent transform must be selected for ball creator editor. Please assign a parent transform.", MessageType.Warning);
        }

        GUILayout.Space(20);
        GUILayout.Label("Create a new ball spitter", EditorStyles.boldLabel);
        EditorGUILayout.BeginVertical("box");
        DrawMenuForBallSpitter();
        EditorGUILayout.EndVertical();

        sObject.ApplyModifiedProperties();
    }

    private void DrawMenuForBall()
    {
        if ((Selection.activeGameObject != null) && (ReferenceEquals(Selection.activeGameObject, ballsParent.gameObject)))
        {
            colourIndex = EditorGUILayout.Popup(colourIndex, colorOptions);
            if (GUILayout.Button("Create a Ball"))
            {
                CreateABall();
            }
        }
    }

    private void DrawMenuForBallSpitter()
    {
        if (GUILayout.Button("Create a Ball Spitter"))
        {
            CreateABallSpitter();
        }
    }

    private void CreateABall()
    {
        GameObject newBall = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        newBall.name = "Ball: " + ballsParent.childCount;
        newBall.transform.SetParent(ballsParent, false);

        newBall.layer = 8;

        newBall.AddComponent<Ball>();
        newBall.GetComponent<Ball>().SetBallSpeedFactor(1.0f);
        newBall.GetComponent<Ball>().SetBallColour((BallColour)colourIndex);
        newBall.GetComponent<Ball>().SetBallScoreValue(100.0f);

        newBall.AddComponent<Rigidbody>();
        newBall.GetComponent<Rigidbody>().useGravity = false;

        Instantiate(ballsIndicatorPrefab, newBall.transform);

        if (ballsMaterials[colourIndex] != null)
        {
            newBall.GetComponent<Renderer>().material = ballsMaterials[colourIndex];
        }
    }

    private void CreateABallSpitter()
    {
        GameObject newBallSpitter = new GameObject();
        newBallSpitter.name = "Ball Spitter";

        newBallSpitter.layer = 2;

        newBallSpitter.AddComponent<BallSpitter>();

        newBallSpitter.AddComponent<BoxCollider>();
        newBallSpitter.GetComponent<BoxCollider>().isTrigger = true;
    }

    private void OnValidate()
    {
        if (ballsMaterials.Length != Enum.GetNames(typeof(BallColour)).Length)
        {
            Debug.LogWarning("Balls Material array size should equal to size of BallColour enum, which is " + Enum.GetNames(typeof(BallColour)).Length + ".");
            Array.Resize(ref ballsMaterials, Enum.GetNames(typeof(BallColour)).Length);
        }
    }
}
