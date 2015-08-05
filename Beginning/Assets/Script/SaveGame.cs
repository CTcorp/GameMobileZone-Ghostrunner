using UnityEngine;
using System.Collections;
using System.IO;

public class SaveGame : MonoBehaviour
{
	public TextMesh tm;
	string message;
	string data;
	FileInfo f; 
	
	void Start()
	{
		f = new FileInfo(Application.dataPath + "\\" + "myFile.txt");
		if(!f.Exists)
		{
			//Save();
			Load();
		}
		else
		{
			//Save();
			Load();
		}
	}
	
	void Save()
	{
		StreamWriter w;
		if(!f.Exists)
		{
			w = f.CreateText();    
		}
		else
		{
			f.Delete();
			w = f.CreateText();
		}
		w.WriteLine("loadMessage1");
		w.WriteLine("loadMessage2");
		w.Close();
	}
	
	void Load()
	{
		StreamReader r = File.OpenText(Application.dataPath + "\\" + "myFile.txt");
		r.ReadLine ();
		string info = r.ReadLine ();
		r.Close();
		data = info;
		tm.text = info;
	}
}

