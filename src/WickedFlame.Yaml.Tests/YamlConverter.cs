using System;
using System.Collections.Generic;
using System.Text;

namespace WickedFlame.Yaml.Tests
{
	public static class YamlConverter
	{
		public static T Read<T>(string file) where T : class, new()
		{
			var reader = new YamlReader();
			return reader.Read<T>(file);
		}
	}
}
