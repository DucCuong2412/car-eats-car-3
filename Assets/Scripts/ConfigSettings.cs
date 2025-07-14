using System;
using System.Globalization;
using System.Net;
using System.Text.RegularExpressions;
using UnityEngine;

public class ConfigSettings : ScriptableObject
{
	public class FTPListDetail
	{
		private const string regex = "^(?<dir>[\\-ld])(?<permission>[\\-rwx]{9})\\s+(?<filecode>\\d+)\\s+(?<owner>\\w+)\\s+(?<group>\\w+)\\s+(?<size>\\d+)\\s+(?<month>\\w{3})\\s+(?<day>\\d{1,2})\\s+(?<timeyear>[\\d:]{4,5})\\s+(?<filename>(.*))$";

		public bool IsDirectory => !string.IsNullOrEmpty(Dir) && Dir.Equals("D", StringComparison.OrdinalIgnoreCase);

		internal string Dir
		{
			get;
			set;
		}

		public string Permission
		{
			get;
			set;
		}

		public string Filecode
		{
			get;
			set;
		}

		public string Owner
		{
			get;
			set;
		}

		public string Group
		{
			get;
			set;
		}

		public int Size
		{
			get;
			set;
		}

		public string Name
		{
			get;
			set;
		}

		public string FullPath
		{
			get;
			set;
		}

		internal string Month
		{
			get;
			set;
		}

		internal string Day
		{
			get;
			set;
		}

		internal string YearTime
		{
			get;
			set;
		}

		public DateTime Date
		{
			get
			{
				int month = DateTime.ParseExact(Month, "MMM", CultureInfo.CurrentCulture).Month;
				if (!YearTime.Contains(":"))
				{
					int result = 0;
					int result2 = 0;
					int.TryParse(YearTime, out result);
					int.TryParse(Day, out result2);
					return new DateTime(result, month, result2);
				}
				string[] array = YearTime.Split(new string[1]
				{
					":"
				}, StringSplitOptions.RemoveEmptyEntries);
				if (array.Length == 2)
				{
					int result3 = 0;
					int.TryParse(Day, out result3);
					int result4 = 0;
					int.TryParse(array[0], out result4);
					int result5 = 0;
					int.TryParse(array[1], out result5);
					return new DateTime(DateTime.Now.Year, month, result3, result4, result5, 0);
				}
				int result6 = 0;
				int.TryParse(Day, out result6);
				return new DateTime(DateTime.Now.Year, month, result6);
			}
		}

		public FTPListDetail()
		{
		}

		public FTPListDetail(string details)
		{
			Match match = new Regex("^(?<dir>[\\-ld])(?<permission>[\\-rwx]{9})\\s+(?<filecode>\\d+)\\s+(?<owner>\\w+)\\s+(?<group>\\w+)\\s+(?<size>\\d+)\\s+(?<month>\\w{3})\\s+(?<day>\\d{1,2})\\s+(?<timeyear>[\\d:]{4,5})\\s+(?<filename>(.*))$").Match(details);
			Dir = match.Groups["dir"].ToString();
			Permission = match.Groups["permission"].ToString();
			Filecode = match.Groups["filecode"].ToString();
			Owner = match.Groups["owner"].ToString();
			Group = match.Groups["group"].ToString();
			int result = 0;
			int.TryParse(match.Groups["size"].ToString(), out result);
			Size = result;
			Month = match.Groups["month"].ToString();
			YearTime = match.Groups["timeyear"].ToString();
			Day = match.Groups["day"].ToString();
			Name = match.Groups["filename"].ToString();
		}

		public override string ToString()
		{
			return $"{Dir} {Permission} {Filecode} {Owner} {Group} {Size} {Date.ToShortDateString()} {Name}";
		}
	}

	public string httpAddress = "ftp://smokoko.ftp.ukraine.com.ua//smokoko.com//unity//cec//";

	public string username = "smokoko_ftp";

	public string password = "c569kds4";

	public bool useCache;

	private const string ISNSettingsAssetName = "ConfigSettings";

	private const string ISNSettingsPath = "Extensions/Config/Resources";

	private const string ISNSettingsAssetExtension = ".asset";

	private static ConfigSettings instance;

	public static ConfigSettings Instance
	{
		get
		{
			if (instance == null)
			{
				instance = (Resources.Load("ConfigSettings") as ConfigSettings);
				if (instance == null)
				{
					instance = ScriptableObject.CreateInstance<ConfigSettings>();
				}
			}
			return instance;
		}
	}

	public static FtpWebRequest CreateRequest(string webRequestMethod = "NLST", string fileName = "")
	{
		UriBuilder uriBuilder = new UriBuilder(Instance.httpAddress);
		uriBuilder.Scheme = Uri.UriSchemeFtp;
		uriBuilder.Port = -1;
		UriBuilder uriBuilder2 = uriBuilder;
		FtpWebRequest ftpWebRequest = (FtpWebRequest)WebRequest.Create(uriBuilder2.Uri + fileName);
		ftpWebRequest.Method = webRequestMethod;
		ftpWebRequest.Credentials = new NetworkCredential(Instance.username, Instance.password);
		ftpWebRequest.UsePassive = true;
		ftpWebRequest.UseBinary = true;
		ftpWebRequest.KeepAlive = false;
		return ftpWebRequest;
	}
}
