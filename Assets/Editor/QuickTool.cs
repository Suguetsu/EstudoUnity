using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System;



// EStudo Gui Elements in unity Editor, by Rodrigo Sugue

public class QuickTool : EditorWindow
{
    [MenuItem("QuickTool/Open _%#T")]

    /* Note The characters _%#T at the end of the MenuItem lets us add a shortcut to open the window,
     * Which is CTRl + SHiFT + T 
     */
    public static void ShowWindow()
    {
        // Opens the window, otherwise focuses it if it's already open.
        var window = GetWindow<QuickTool>();

        // Adds a title to the window.
        window.titleContent = new GUIContent("QuickTool");

        // Sets a minimum size to the window.
        window.minSize = new Vector2(40, 40);
    }

    private void CreateGUI()
    {

        // Reference to the root of the window.
        var root = rootVisualElement;

        // Associates a stylesheet to our root. Thanks to inheritance, all root’s
        // children will have access to it.
        root.styleSheets.Add(Resources.Load<StyleSheet>("QuickTool_Style"));

        // Loads and clones our VisualTree (eg. our UXML structure) inside the root.
        var quickToolVisualTree = Resources.Load<VisualTreeAsset>("QuickTool_Main");
        quickToolVisualTree.CloneTree(root);

        // Queries all the buttons (via type) in our root and passes them
        // in the SetupButton method.
        var toolButtons = root.Query(className: "quicktool-button");
        toolButtons.ForEach(SetupButton);

       

    }

    private void SetupButton(VisualElement button)
    {
        // Reference to the VisualElement inside the button that serves
        // as the button's icon.
        var buttonIcon = button.Q(className: "quicktool-button-icon");

        // Icon's path in our project.
        var iconPath = "Icons/" + button.parent.name + "_icon";

        // Loads the actual asset from the above path.
        var iconAsset = Resources.Load<Texture2D>(iconPath);

        // Applies the above asset as a background image for the icon.
        buttonIcon.style.backgroundImage = iconAsset;

        // Instantiates our primitive object on a left click.
        button.RegisterCallback<PointerUpEvent, string>(CreateObject, button.parent.name);

        // Sets a basic tooltip to the button itself.
        button.tooltip = button.parent.name;
    }


    private void CreateObject(PointerUpEvent _, string primitiveTypeName)
    {
        var pt = (PrimitiveType)Enum.Parse
                     (typeof(PrimitiveType), primitiveTypeName, true);
        var go = ObjectFactory.CreatePrimitive(pt);
        go.transform.position = Vector3.zero;
    }

    public void OnEnable()
    {


        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;
        // VisualElements objects can contain other VisualElement following a tree hierarchy.
        VisualElement label = new Label("QuickTool");
        root.Add(label);

        CreateGUI();

   
    }
}
