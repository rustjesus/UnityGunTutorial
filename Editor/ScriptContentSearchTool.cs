using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
public class ScriptContentSearchTool : EditorWindow
{
    private string folderPath = "Assets"; // The folder path to search.
    private string searchString = ""; // The string to check for.
    private bool caseSensitive = false; // Toggle for case sensitivity
    private bool recursiveSearch = true; // Toggle for recursive searching
    private Vector2 scrollPosition;

    [MenuItem("CustomTools/Script Content Search Tool")]
    public static void ShowWindow()
    {
        GetWindow<ScriptContentSearchTool>("Script Search Tool");
    }

    private void OnGUI()
    {
        GUILayout.Label("Script Content Search Tool", EditorStyles.boldLabel);

        folderPath = EditorGUILayout.TextField("Folder Path", folderPath);
        searchString = EditorGUILayout.TextField("Search String", searchString);
        caseSensitive = EditorGUILayout.Toggle("Case Sensitive", caseSensitive);
        recursiveSearch = EditorGUILayout.Toggle("Recursive Search", recursiveSearch);

        if (GUILayout.Button("Search"))
        {
            SearchInFolder(folderPath);
        }

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Search Results:");

        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

        // Display search results
        EditorGUI.BeginDisabledGroup(true); // Make the text uneditable
        foreach (string result in searchResults)
        {
            EditorGUILayout.TextField(result);
        }
        EditorGUI.EndDisabledGroup();

        EditorGUILayout.EndScrollView();
    }

    private List<string> searchResults = new List<string>();

    private void SearchInFolder(string currentFolder)
    {
        searchResults.Clear(); // Clear previous search results

        StringComparison comparisonType = caseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;
        SearchOption searchOption = recursiveSearch ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;

        if (Directory.Exists(currentFolder))
        {
            string[] files = Directory.GetFiles(currentFolder, "*.cs", searchOption);

            foreach (string file in files)
            {
                string fileContent = File.ReadAllText(file);
                if (fileContent.IndexOf(searchString, comparisonType) >= 0)
                {
                    string result = "Found matching content in file: " + file;
                    searchResults.Add(result);
                    Debug.Log(result);
                }
            }
        }
        else
        {
            string result = "Folder does not exist: " + currentFolder;
            searchResults.Add(result);
            Debug.Log(result);
        }
    }
}
