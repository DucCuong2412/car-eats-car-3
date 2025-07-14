using System.Collections.Generic;

namespace SMKK
{
	public class AudioSerialization
	{
		public List<AudioFile> files = new List<AudioFile>();

		public void AddAudio(string fileName, string filePath)
		{
			files.Add(new AudioFile(fileName, filePath));
		}
	}
}
