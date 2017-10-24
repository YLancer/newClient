//#define DEBUG_LEVEL_LOG
//#define DEBUG_LEVEL_AILOG
//#define DEBUG_LEVEL_WARN
//#define DEBUG_LEVEL_ERROR

using UnityEngine;
using System.Collections;
using System.Text;
using System.Collections.Generic;

namespace ImangiUtilities
{
	public static class ExtensionMethods_String
	{
		public static void Clear(this StringBuilder instance)
		{
			// HACK this shall reset the stringbuilder without releasing the memory
			// .net 4.0 adds a clear method which is not available here
			// at least it can be made compatible by removing this method 
			// source: http://www.mindfiresolutions.com/A-better-way-to-Clear-a-StringBuilder-951.php
			instance.Length = 0;
		}
	}
	
#region ImangiLog
	public partial class ImangiLog
	{
		public enum EnabledState
		{
			Disabled,
			Enabled,
			EnabledStack
		}

		public static string LogChannel = "ImangiLog";

		//This gives the relative path from the Assets directory for the log channel configuration file.
		public static string LogChannelConfigResourcesFilePath = "Game/Resources";

		//This gives the relative ath from the Resources directory to the configuration file. "LogChannels" is the file name in this path exluding the extension
		public static string LogChannelConfigFilePath = "GameConfig/LogChannels";

		//** track which channels are enabled/disabled
		private static Dictionary<string, EnabledState> EnabledChannels = new Dictionary<string, EnabledState>();
		
		//** string builder to construct the final out text
		private static StringBuilder _StringBuilder = new System.Text.StringBuilder();

		public static void LoadLogChannelConfig()
		{
			EnabledChannels.Clear();

			// Enable Log Channels based on config file
			TextAsset logChannelConfig = Resources.Load(LogChannelConfigFilePath, typeof(TextAsset)) as TextAsset;

			if (logChannelConfig != null && (string.IsNullOrEmpty(logChannelConfig.text) == false))
			{
				ImangiLog.Log(LogChannel, "Loaded Config: " + logChannelConfig.text);

				Dictionary<string, object> data = ImangiUtilities.MiniJson.Json.Deserialize(logChannelConfig.text) as Dictionary<string, object>;
				
				foreach(KeyValuePair<string, object> kvp in data)
				{
					ImangiLog.EnabledState logEnabledState = JSONTools.ReadEnum(kvp.Value, ImangiLog.EnabledState.Disabled);

					EnabledChannels[kvp.Key] = logEnabledState;
				}
			}
		}

		public static List<string> GetLogChannelKeys()
		{
			return new List<string>(EnabledChannels.Keys);
		}

		public static void EnabledAllChannels(EnabledState inState)
		{
			List<string> channelKeys = new List<string>(EnabledChannels.Keys);
			foreach(string channelKey in channelKeys)
			{
				EnabledChannels[channelKey] = inState;
			}
		}
		
		public static void EnabledChannel(string channel, EnabledState state)
		{
			if (channel == null)
			{
				return;
			}
			
			if (!EnabledChannels.ContainsKey(channel))
			{
				EnabledChannels.Add(channel, state);
			}
			else
			{
				EnabledChannels[channel] = state;
			}
		}
		
		public static bool IsChannelEnabled(string channel)
		{
			if (channel == null)
			{
				return false;
			}
			
			if (!EnabledChannels.ContainsKey(channel))
			{
				EnabledChannels.Add(channel, EnabledState.Enabled);
			}
			
			switch(EnabledChannels[channel])
			{
			case EnabledState.Disabled:
				break;
			case EnabledState.Enabled:
			case EnabledState.EnabledStack:
				return true;
			}
			
			return false;
		}
		
		public static EnabledState ChannelEnabledState(string channel)
		{
			if (channel != null)
			{
				if (EnabledChannels.ContainsKey(channel) == true)
				{
					return EnabledChannels[channel];
				}
			}
			
			return EnabledState.Disabled;
		}
		
		public static bool IsChannelEnabledStack(string channel)
		{
			if (channel == null)
			{
				return false;
			}
			
			if (!EnabledChannels.ContainsKey(channel))
			{
				EnabledChannels.Add(channel, EnabledState.Enabled);
			}
			
			switch(EnabledChannels[channel])
			{
			case EnabledState.Disabled:
			case EnabledState.Enabled:
				break;
			case EnabledState.EnabledStack:
				return true;
			}
			
			return false;
		}
		
		private static string GetOSString(string channel, string type, string format, params object[] paramList)
		{
			_StringBuilder.Clear();
			
#if !UNITY_IPHONE && !UNITY_ANDROID
			_StringBuilder.Append(' ').Append(System.DateTime.Now).Append(' ');
#endif
			
			_StringBuilder.Append('[').Append(channel).Append(']');
			
			_StringBuilder.Append('-').Append(type).Append('-');
			
			if (paramList.Length > 0)
			{
				_StringBuilder.AppendFormat(format, paramList);
			}
			else
			{
				_StringBuilder.Append(format);
			}
			
			if(IsChannelEnabledStack(channel))
			{
				// Unity based stack trace collector
				// StackTraceUtility.ExtractStackTrace();
				
				string stackTrace = System.Environment.StackTrace;
				_StringBuilder.Append("\n\n").Append(stackTrace);
			}
			
			return _StringBuilder.ToString();
		}
		
		public static string BuildStringWithFormat(string format, params object[] paramList)
		{
			_StringBuilder.Clear();
			
			if (paramList.Length > 0)
			{
				_StringBuilder.AppendFormat(format, paramList);
			}
			else
			{
				_StringBuilder.Append(format);
			}
			
			return _StringBuilder.ToString();
		}
		
		// Do not log in production build
		[System.Diagnostics.Conditional("IMANGI_LOGGING")]
		public static void Log(string channel, string format, params object[] paramList)
		{
			if (IsChannelEnabled(channel) == true)
			{
				ImangiOSLog_Log(GetOSString(channel, "    ", format, paramList));
			}
		}
		
		// Do not log in production build
		[System.Diagnostics.Conditional("IMANGI_LOGGING")]
		public static void LogIf(bool condition, string channel, string format, params object[] paramList)
		{
			if (IsChannelEnabled(channel) == true && condition)
			{
				ImangiOSLog_Log(GetOSString(channel, "    ", format, paramList));
			}
		}
		
		/** Log the given message for the given channel - enabled or not */
		public static void ForceLog(string channel, string format, params object[] paramList)
		{
			ImangiOSLog_Log(GetOSString(channel, "    ", format, paramList));
		}
		
		// Do not warn in production build
		[System.Diagnostics.Conditional("IMANGI_LOGGING")]
		public static void Warn(string channel, string format, params object[] paramList)
		{
			if (IsChannelEnabled(channel) == true)
			{
				ImangiOSLog_Warn(GetOSString(channel, "WARN", format, paramList));
			}
		}
		
		// Do not warn in production build
		[System.Diagnostics.Conditional("IMANGI_LOGGING")]
		public static void WarnIf(bool condition, string channel, string format, params object[] paramList)
		{
			if (IsChannelEnabled(channel) == true && condition)
			{
				ImangiOSLog_Warn(GetOSString(channel, "WARN", format, paramList));
			}
		}
		
		/** Show the given warning for the given channel - enabled or not */
		public static void ForceWarn(string channel, string format, params object[] paramList)
		{
			ImangiOSLog_Warn(GetOSString(channel, "WARN", format, paramList));
		}

		public static void Error(string channel, string format, params object[] paramList)
		{
			ImangiOSLog_Error(GetOSString(channel, "ERR ", format, paramList));
		}
		
		// Do not error in production build
		[System.Diagnostics.Conditional("IMANGI_LOGGING")]
		public static void ErrorIf(bool condition, string channel, string format, params object[] paramList)
		{
			if (condition)
			{
				ImangiOSLog_Error(GetOSString(channel, "ERR ", format, paramList));
			}
		}

        // INTERNAL FUNCTIONS
//#if UNITY_EDITOR || UNITY_ANDROID
        protected const int MaxLogLineLength = 1024;
		
		protected static void ImangiOSLog_LongLog(string inMessage, System.Action<string> inLogFunction)
		{
			if (inMessage.Length < MaxLogLineLength)
			{
				inLogFunction(inMessage);
			}
			else
			{
				string tempString = inMessage;
				string tempOutputString;
				
				while (tempString.Length > MaxLogLineLength)
				{
					tempOutputString = tempString.Substring(0, MaxLogLineLength);
					inLogFunction(tempOutputString);
					tempString = tempString.Substring(MaxLogLineLength);
				}
				inLogFunction(tempString);
			}
		}
		
		protected static void ImangiOSLog_Log(string inMessage)
		{
			ImangiOSLog_LongLog(inMessage, Debug.Log);
		}
		
		protected static void ImangiOSLog_Warn(string inMessage)
		{
			ImangiOSLog_LongLog(inMessage, Debug.LogWarning);
		}
		
		protected static void ImangiOSLog_Error(string inMessage)
		{
			ImangiOSLog_LongLog(inMessage, Debug.LogError);
		}
//#endif
	};
#endregion	//ImangiLog
}	//namespace ImangiUtilities
