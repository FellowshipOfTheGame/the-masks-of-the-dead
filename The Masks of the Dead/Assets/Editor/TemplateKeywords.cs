using UnityEngine;
using UnityEditor;

public class TemplateKeywords : UnityEditor.AssetModificationProcessor {

    public static void OnWillCreateAsset(string path) {
        path = path.Replace(".meta", "");
        int index = path.LastIndexOf(".");
        if (index < 0)
            return;

        string file = path.Substring(index);
        if (file != ".cs" && file != ".js" && file != ".boo")
            return;

        index = Application.dataPath.LastIndexOf("Assets");
        path = Application.dataPath.Substring(0, index) + path;
        if (!System.IO.File.Exists(path))
            return;

        string content = System.IO.File.ReadAllText(path);

        content = content.Replace("#CREATIONDATE#", System.DateTime.Now + "");
        content = content.Replace("#PROJECTNAME#", PlayerSettings.productName);
        content = content.Replace("#COMPANY#", PlayerSettings.companyName);

        System.IO.File.WriteAllText(path, content);
        AssetDatabase.Refresh();
    }
}