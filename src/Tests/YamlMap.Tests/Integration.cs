using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using NUnit.Framework;

namespace YamlMap.Tests
{
	[TestFixture]
	public class Integration
	{
		[Test]
		public void LoadDataFromFile()
		{
            var path = Assembly.GetExecutingAssembly().Location;
            path = Path.Combine(Path.GetDirectoryName(path), "TestData", "users.yml");

            var reader = new YamlFileReader();
			var users = reader.Read<List<User>>(path);

			Assert.IsTrue(users.Count == 3);

		}

		public class User
		{
			public enum UserType
			{
				User,
				Client
			}

			public User()
			{
				Type = UserType.User;
			}

			public User(string id, string name)
				: this(id, name, UserType.User)
			{
			}

			public User(string id, string name, UserType type)
			{
				Id = id;
				Name = name;
				Type = type;
			}

			public string Id { get; set; }

			public UserType Type { get; set; }

			public string Name { get; set; }

			public string Password { get; set; }

			public Scope[] Scopes { get; set; }
		}

		public class Scope
		{
			public string Name { get; set; }
		}
	}
}
