using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using SimpleFileBrowser;

namespace Unity3DViewer
{
    public static class FileLoader 
    {
        // Warning: paths returned by FileBrowser dialogs do not contain a trailing '\' character
        // Warning: FileBrowser can only show 1 dialog at a time

        private static bool isInit = false;

        private static void Init()
        {
            if(isInit) return;

            // Set filters (optional)
            // It is sufficient to set the filters just once (instead of each time before showing the file browser dialog), 
            // if all the dialogs will be using the same filters
            // FileBrowser.SetFilters(true, 
            //     new FileBrowser.Filter("Images", ".jpg", ".png"), 
            //     new FileBrowser.Filter("Text Files", ".txt", ".pdf"),
            //     new FileBrowser.Filter("Model Files", ".gltf", ".ply", ".txt", ".pcd"));

            FileBrowser.SetFilters(true, new FileBrowser.Filter("Model Files", ".gltf"));

            // Set default filter that is selected when the dialog is shown (optional)
            // Returns true if the default filter is set successfully
            // In this case, set Images filter as the default filter
            FileBrowser.SetDefaultFilter(".gltf");

            // Set excluded file extensions (optional) (by default, .lnk and .tmp extensions are excluded)
            // Note that when you use this function, .lnk and .tmp extensions will no longer be
            // excluded unless you explicitly add them as parameters to the function
            FileBrowser.SetExcludedExtensions(".lnk", ".tmp", ".zip", ".rar", ".exe");

            // Add a new quick link to the browser (optional) (returns true if quick link is added successfully)
            // It is sufficient to add a quick link just once
            // Name: Users
            // Path: C:\Users
            // Icon: default (folder icon)
            FileBrowser.AddQuickLink("Users", "C:\\Users", null);

            // Coroutine example
            // StartCoroutine(ShowLoadDialogCoroutine());
        }

        public static IEnumerator LoadCoroutine(UnityAction<string> onSuccess)
        {
            Init();

            // Show a load file dialog and wait for a response from user
            // Load file/folder: file, Allow multiple selection: true
            // Initial path: default (Documents), Title: "Load File", submit button text: "Load"
            yield return FileBrowser.WaitForLoadDialog(false, true, null, "Load File", "Load");

            // Dialog is closed
            // Print whether the user has selected some files/folders or cancelled the operation (FileBrowser.Success)
            Debug.Log(FileBrowser.Success);

            if (FileBrowser.Success)
            {
                // Print paths of the selected files (FileBrowser.Result) (null, if FileBrowser.Success is false)
                for (int i = 0; i < FileBrowser.Result.Length; i++)
                {
                    var path = FileBrowser.Result[i];
                    Debug.Log("FileLoader >> path: " + path);
                    onSuccess(path);
                }

                // // Read the bytes of the first file via FileBrowserHelpers
                // // Contrary to File.ReadAllBytes, this function works on Android 10+, as well
                // byte[] bytes = FileBrowserHelpers.ReadBytesFromFile(FileBrowser.Result[0]);
            }
        }
    }
}