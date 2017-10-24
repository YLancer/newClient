using System;
using System.Collections.Generic;
using System.IO;

namespace DldUtil
{

public static class BigFileReader
{
	public static bool FileHasText(string path, params string[] seekText)
	{
		FileStream fs = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
		BufferedStream bs = new BufferedStream(fs);
		StreamReader sr = new StreamReader(bs);

		string line = "";

		long currentLine = 0;
		while (true)
		{
			++currentLine;
			line = sr.ReadLine();

			if (line == null)
			{
				break;
			}

			for (var seekTextIdx = 0; seekTextIdx < seekText.Length; ++seekTextIdx)
			{
				if (line.IndexOf(seekText[seekTextIdx], StringComparison.Ordinal) >= 0)
				{
					sr.Close();
					bs.Close();
					fs.Close();

					return true;
				}
			}
		}

		sr.Close();
		bs.Close();
		fs.Close();

		return false;
	}


	public static IEnumerable<string> ReadFile(string path, params string[] seekText)
	{
		return ReadFile(path, true, seekText);
	}

	public static IEnumerable<string> ReadFile(string path, bool startAfterSeekedText, params string[] seekText)
	{
		FileStream fs = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
		BufferedStream bs = new BufferedStream(fs);
		StreamReader sr = new StreamReader(bs);

		string line = "";
		
		bool seekTextRequested = (seekText != null) && (seekText.Length > 0) && !string.IsNullOrEmpty(seekText[0]);

		
		long seekTextFoundAtLine = -1;
		
		
		if (seekTextRequested)
		{
			long currentLine = 0;
			while (true)
			{
				++currentLine;
				line = sr.ReadLine();
				
				if (line == null)
				{
					break;
				}

				var atLeastOneSeekTextFound = false;
				for (var seekTextIdx = 0; seekTextIdx < seekText.Length; ++seekTextIdx)
				{
					if (line.IndexOf(seekText[seekTextIdx], StringComparison.Ordinal) >= 0)
					{
						atLeastOneSeekTextFound = true;
						break;
					}
				}

				// if seekText not found yet, skip
				if (!atLeastOneSeekTextFound)
				{
					continue;
				}

				seekTextFoundAtLine = currentLine;
					
				//Debug.Log("seeking: " + line);
				//Debug.Log("seekText found at: " + currentLine);
			}
			//Debug.Log("done seeking");
		
			if (seekTextFoundAtLine != -1)
			{	
				fs.Seek(0, SeekOrigin.Begin);
				
				currentLine = 0;
				while (true)
				{
					++currentLine;
					line = sr.ReadLine();
					
					if (line == null)
					{
						break;
					}
					if (startAfterSeekedText && currentLine <= seekTextFoundAtLine)
					{
						continue;
					}
					if (!startAfterSeekedText && currentLine < seekTextFoundAtLine)
					{
						continue;
					}
					
					//Debug.Log("seeked: " + line);
					
					yield return line;
				}
			}
		}
		else
		{
			while (true)
			{
				line = sr.ReadLine();
				
				if (line == null)
				{
					break;
				}
				
				yield return line;
			}
		}
		
		line = "";
		
		sr.Close();
		bs.Close();
		fs.Close();
	}


	public static IEnumerable<string> ReadFile(string path)
	{
		FileStream fs = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
		BufferedStream bs = new BufferedStream(fs);
		StreamReader sr = new StreamReader(bs);

		string line = "";

		while (true)
		{
			line = sr.ReadLine();

			if (line == null)
			{
				break;
			}

			yield return line;
		}

		line = "";

		sr.Close();
		bs.Close();
		fs.Close();
	}
}

}
