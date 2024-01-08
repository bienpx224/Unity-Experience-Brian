using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class FileHelper
{
	#region Path
	public static string GetProjectPath()
	{
#if UNITY_EDITOR
		return Application.dataPath.Replace("Assets", "");
#else
        return Application.persistentDataPath + Path.DirectorySeparatorChar;
#endif
	}

	public static string GetAssetsPath()
	{
		return GetProjectPath() + "Assets\\";
	}

	public static string GetResourcesPath()
	{
		return GetAssetsPath() + "Resources\\";
	}

	public static void CheckAndCreateDirectory(string path)
	{
		if (Directory.Exists(path))
			return;
		else
			Directory.CreateDirectory(path);
	}
	#endregion

	#region CSV File
	public static List<string> ReadCSV(string filepath, bool removeFirstLine = true)
	{
		if (File.Exists(filepath))
		{
			using (var reader = new StreamReader(filepath))
			{
				List<string> lines = new List<string>();

				if (removeFirstLine)
					reader.ReadLine();

				while (!reader.EndOfStream)
				{
					string line = reader.ReadLine();

					if (!string.IsNullOrEmpty(line))
						lines.Add(line);
				}

				return lines;
			}
		}
		else
		{
			return null;
		}
	}

	public static List<string> ReadCSVFromResources(string filepath, bool removeFirstLine = true)
	{
		var textFile = Resources.Load<TextAsset>(filepath);
		return ReadCSVFromText(textFile.text);
	}

	public static List<string> ReadCSVFromText(string text, bool removeFirstLine = true)
	{
		if (!string.IsNullOrEmpty(text))
		{
			string[] lines_raw = text.Split('\n');
			List<string> lines = new List<string>();

			for (int i = removeFirstLine ? 1 : 0; i < lines_raw.Length; i++)
			{
				if (!string.IsNullOrEmpty(lines_raw[i]))
					lines.Add(lines_raw[i]);
			}

			return lines;
		}
		else
		{
			Debug.LogError("(ReadCSVFromText) Text is null!");
			return null;
		}
	}
	#endregion

	#region Save/Load/Delete File
	/// <summary>
	/// Read a text file from local storage and decrypt it as needed
	/// </summary>
	/// <param name="filePath">Where the file is saved</param>
	/// <param name="password">If not null, will be used to decrypt the file</param>
	/// <param name="isAbsolutePath">Is the file path an absolute one?</param>
	/// <returns></returns>
	public static string LoadFileWithPassword(string filePath, string password = null, bool isAbsolutePath = false)
	{
		var bytes = LoadFile(filePath, isAbsolutePath);
		if (bytes != null)
		{
			string text = System.Text.Encoding.UTF8.GetString(bytes, 0, bytes.Length);

			return text;

		}
		else
		{
			return null;
		}
	}

	/// <summary>
	/// Read a file at specified path
	/// </summary>
	/// <param name="filePath">Path to the file</param>
	/// <param name="isAbsolutePath">Is this path an absolute one?</param>
	/// <returns>Data of the file, in byte[] format</returns>
	public static byte[] LoadFile(string filePath, bool isAbsolutePath = false)
	{
		if (filePath == null || filePath.Length == 0)
		{
			return null;
		}

		string absolutePath = filePath;
		if (!isAbsolutePath) { absolutePath = GetWritablePath(filePath); }

		if (File.Exists(absolutePath))
		{
			return File.ReadAllBytes(absolutePath);
		}
		else
		{
			return null;
		}
	}

	public static string SaveFileWithPassword(string content, string filePath, string password = null, bool isAbsolutePath = false)
	{
		byte[] bytes;
		bytes = System.Text.Encoding.UTF8.GetBytes(content);
		return SaveFile(bytes, filePath, isAbsolutePath);
	}

	/// <summary>
	/// Save a byte array to storage at specified path and return the absolute path of the saved file
	/// </summary>
	/// <param name="bytes">Data to write</param>
	/// <param name="filePath">Where to save file</param>
	/// <param name="isAbsolutePath">Is this path an absolute one or relative</param>
	/// <returns>Absolute path of the file</returns>
	public static string SaveFile(byte[] bytes, string filePath, bool isAbsolutePath = false)
	{
		if (filePath == null || filePath.Length == 0)
		{
			return null;
		}

		string path = filePath;
		if (!isAbsolutePath)
		{
			path = GetWritablePath(filePath);
		}

		string folderName = Path.GetDirectoryName(path);

		if (!Directory.Exists(folderName))
		{
			Directory.CreateDirectory(folderName);
		}

		File.WriteAllBytes(path, bytes);
		return path;
	}

	/// <summary>
	/// Return a path to a writable folder on a supported platform
	/// </summary>
	/// <param name="relativeFilePath">A relative path to the file, from the out most writable folder</param>
	/// <returns></returns>
	public static string GetWritablePath(string relativeFilePath)
	{
		string result = "";
		//folder += (folder.Trim().Equals("")) ? "" : "/";
		//extension = (fileName.Trim().Equals("")) ? "" : "." + extension;

#if UNITY_EDITOR
		result = Application.dataPath.Replace("Assets", "obj/UserData") + Path.DirectorySeparatorChar + relativeFilePath;
#elif UNITY_ANDROID
		result = Application.persistentDataPath + Path.DirectorySeparatorChar + relativeFilePath;
#elif UNITY_IPHONE
		result = Application.persistentDataPath + Path.DirectorySeparatorChar + relativeFilePath;
#elif UNITY_WP8 || NETFX_CORE || UNITY_WSA
		result = Application.persistentDataPath + "/" + relativeFilePath;
#endif
		Debug.Log("FILE PATH ASSET PATH: " + result);
		return result;
	}

	/// <summary>
	/// Return a path to a Resources folder on a supported platform
	/// </summary>
	/// <param name="relativeFilePath"></param>
	/// <returns></returns>
	public static string GetResourcesPath(string relativeFilePath)
	{
		string result = "";

#if UNITY_EDITOR
		result = Application.dataPath + "/_Game/Resources/Config" + Path.DirectorySeparatorChar + relativeFilePath;
#elif UNITY_ANDROID
		result = Application.persistentDataPath + "/_Game/Resources/Config" + Path.DirectorySeparatorChar + relativeFilePath;
#elif UNITY_IPHONE
		result = Application.persistentDataPath + "/_Game/Resources/Config" + Path.DirectorySeparatorChar + relativeFilePath;
#elif UNITY_WP8 || NETFX_CORE || UNITY_WSA
		result = Application.persistentDataPath + "/_Game/Resources/Config" + "/" + relativeFilePath;
#endif

		return result;
	}

	/// <summary>
	/// Delete a file from storage using default setting
	/// </summary>
	/// <param name="filePath">The path to the file</param>
	/// <param name="isAbsolutePath">Is this file path an absolute path or relative one?</param>
	public static void DeleteFile(string filePath, bool isAbsolutePath = false)
	{
		if (filePath == null || filePath.Length == 0)
			return;

		if (isAbsolutePath)
		{
			if (System.IO.File.Exists(filePath))
			{
				//Debug.Log("Delete file : " + absoluteFilePath);
				System.IO.File.Delete(filePath);
			}
		}
		else
		{
			string file = GetWritablePath(filePath);
			DeleteFile(file);
		}
	}
	#endregion
}
