using System.Collections;
using System.Collections.Generic;
using System.Configuration.Assemblies;
using System.IO;
using System.Reflection;
using System.Text;
using UnityEditor;
using UnityEngine;
using System.Linq;
using System;
using MeshGenerator;
using UnityEditor.Compilation;

public class MeshGeneratorWindow : EditorWindow
{
    const string DATA_SCRIPT_PATH = "Assets/Scripts/Data/MeshGenerator";
    const string DATA_ASSET_PATH = "Assets/Data/MeshGenerator";
    const string GENERATOR_SCRIPT_PATH = "Assets/Scripts/Game/Services/MeshGenerators";
    const string GENERATOR_EDITOR_SCRIPT_PATH = "Assets/Scripts/Editor/MeshGenerators";
    const string GENERATOR_DATA_COLLECTION_PATH = "Assets/Scripts/Data/MeshGenerator/MeshGeneratorDataCollection.cs";

    [MenuItem("Assets/Create Mesh Generator")]
    static void Open()
    {
        MeshGeneratorWindow window = ScriptableObject.CreateInstance<MeshGeneratorWindow>();
        window.position = new Rect(Screen.width / 2, Screen.height / 2, 400, 18);
        window.ShowPopup();
    }

    [SerializeField]
    string _meshGeneratorName;
    [SerializeField]
    bool _addToCreateAssetMenu;

    StringBuilder _stringBuilder = new();

    private void OnGUI()
    {
        _meshGeneratorName = EditorGUILayout.TextField("Name", _meshGeneratorName);
        _addToCreateAssetMenu = EditorGUILayout.Toggle("Add to Create Asset Menu", _addToCreateAssetMenu);
        using (new GUILayout.HorizontalScope())
        {
            if (GUILayout.Button("Create"))
            {
                CreateGenerator();
                Close();
            }
            if (GUILayout.Button("Cancel"))
            {
                Close();
            }
        }
    }

    void CreateGenerator()
    {
        CreateDataScript();

        AssetDatabase.Refresh();
        CompilationPipeline.compilationFinished += OnRecompiled;
        CompilationPipeline.RequestScriptCompilation();
    }

    void OnRecompiled(object _)
    {
        CompilationPipeline.compilationFinished -= OnRecompiled;

        AssetDatabase.Refresh();

        RegenerateMeshGeneratorDataCollectionScript();
        CreateGeneratorScript();
        CreateEditorScript();

        //CreateDataAsset();
    }

    void CreateDataScript()
    {
        var name = $"{_meshGeneratorName}MeshGeneratorData";
        CreateScript($@"using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName=""Data/Mesh Generators/{_meshGeneratorName}"")]
public class {name} : ScriptableObject
{{

}}",
            name,
            DATA_SCRIPT_PATH);
    }

    void CreateDataAsset()
    {
        var t = Type.GetType($"{_meshGeneratorName}MeshGeneratorData");
        //Debug.Log(t.Name);
        //Debug.Log(typeof(ScriptableObject).IsAssignableFrom(t));
        var data = CreateInstance(t);
        var path = Path.Combine(DATA_ASSET_PATH, $"{_meshGeneratorName}MeshGeneratorData.asset");
        AssetDatabase.CreateAsset(data, path);
        AssetDatabase.ImportAsset(path);
    }

    void CreateGeneratorScript()
    {
        var name = $"{_meshGeneratorName}MeshGenerator";
        CreateScript($@"using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MeshGenerator;
using System;
using MeshGenerator.Wireframe;

[MeshGenerator(""{_meshGeneratorName}"")]
public class {name} : MeshGeneratorWithData<{_meshGeneratorName}MeshGeneratorData>
{{
    public override MeshGeneratorResult Generate()
    {{
        return new();
    }}

    protected override {_meshGeneratorName}MeshGeneratorData LoadData() => DataService.GetData<MeshGeneratorDataCollection>().{_meshGeneratorName};
}}
",
            name,
            GENERATOR_SCRIPT_PATH);
    }

    void CreateEditorScript()
    {
        var name = $"{_meshGeneratorName}MeshGeneratorEditor";
        CreateScript($@"using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MeshGenerator.Editor;
using MeshGenerator;

[MeshGeneratorEditor(typeof({_meshGeneratorName}MeshGenerator))]
public class {name} : MeshGeneratorEditorWithWireFrame<{_meshGeneratorName}MeshGenerator, {_meshGeneratorName}MeshGeneratorData>
{{
        public override void BuildWireframe()
    {{

    }}
}}
",
            name,
            GENERATOR_EDITOR_SCRIPT_PATH);
    }

    void RegenerateMeshGeneratorDataCollectionScript()
    {
        _stringBuilder.Clear();
        _stringBuilder.Append($@"using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGeneratorDataCollection : ScriptableObject, IRegisteredData
{{
");

        foreach(var type in AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(a=>a.GetTypes())
            .Where(t => typeof(MeshGenerator.MeshGenerator).IsAssignableFrom(t))
            .Select(t => t.Name.Substring(0, t.Name.IndexOf("MeshGenerator")))
            .Where(t=>!string.IsNullOrEmpty(t)))
            {
            _stringBuilder.AppendLine($"    public {type}MeshGeneratorData {type};");
        }

        _stringBuilder.AppendLine($"    public {_meshGeneratorName}MeshGeneratorData {_meshGeneratorName};");

        _stringBuilder.Append("}");

        File.Delete(GENERATOR_DATA_COLLECTION_PATH);
        File.AppendAllText(GENERATOR_DATA_COLLECTION_PATH, _stringBuilder.ToString());
    }


    void CreateScript(string script, string name, string filePath)
    {
        _stringBuilder.Clear();
        _stringBuilder.Append(script);

        var path = Path.Combine(filePath, $"{name}.cs");
        File.WriteAllText(path, _stringBuilder.ToString());
    } 

}