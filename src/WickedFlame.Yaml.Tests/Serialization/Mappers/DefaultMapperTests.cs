using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using WickedFlame.Yaml.Serialization.Mappers;

namespace WickedFlame.Yaml.Tests.Serialization.Mappers
{
	public class DefaultMapperTests
	{

		public T Map<T>(IToken token)
		{
			var item = new MapperValue<T>();
			var mapper = new DefaultMapper(typeof(MapperValue<T>));
			Assert.IsTrue(mapper.Map(token, item));

			return item.Value;
		}

		[Test]
		public void DefaultMapper_String()
		{
			Assert.AreEqual(Map<string>(new ValueToken("Value", "String", 0)), "String");
		}

		[Test]
		public void DefaultMapper_Bool()
		{
			Assert.AreEqual(Map<bool>(new ValueToken("Value", "0", 0)), false);
			Assert.AreEqual(Map<bool>(new ValueToken("Value", "1", 0)), true);
			Assert.AreEqual(Map<bool>(new ValueToken("Value", "false", 0)), false);
			Assert.AreEqual(Map<bool>(new ValueToken("Value", "true", 0)), true);
		}

		[Test]
		public void DefaultMapper_Long()
		{
			Assert.AreEqual(Map<long>(new ValueToken("Value", "123456789", 0)), 123456789);
		}

		[Test]
		public void DefaultMapper_Int()
		{
			Assert.AreEqual(Map<int>(new ValueToken("Value", "2", 0)), 2);
		}

		[Test]
		public void DefaultMapper_Decimal()
		{
			Assert.AreEqual(Map<decimal>(new ValueToken("Value", "2.2", 0)), 2.2M);
		}

		[Test]
		public void DefaultMapper_Double()
		{
			Assert.AreEqual(Map<double>(new ValueToken("Value", "2.2", 0)), 2.2);
		}

		[Test]
		public void DefaultMapper_DateTime()
		{
			Assert.AreEqual(Map<DateTime>(new ValueToken("Value", "2020.01.01 12:12:12", 0)), DateTime.Parse("2020.01.01 12:12:12"));
		}

		[Test]
		public void DefaultMapper_Guid()
		{
			Assert.AreEqual(Map<Guid>(new ValueToken("Value", "1FB53019-AD8C-4FBD-B528-BCD07F6EB6BA", 0)), Guid.Parse("1FB53019-AD8C-4FBD-B528-BCD07F6EB6BA"));
		}

		[Test]
		public void DefaultMapper_Type()
		{
			Assert.AreEqual(
				Map<Type>(new ValueToken("Value", "WickedFlame.Yaml.Tests.Serialization.Mappers.DefaultMapperTests, WickedFlame.Yaml.Tests", 0)),
				this.GetType());
		}

		[Test]
		public void DefaultMapper_GenericType()
		{
			Assert.AreEqual(
				Map<Type>(new ValueToken("Value", "WickedFlame.Yaml.Tests.Serialization.Mappers.MapperValue`1[[WickedFlame.Yaml.Tests.Serialization.Mappers.DefaultMapperTests, WickedFlame.Yaml.Tests]], WickedFlame.Yaml.Tests", 0)), 
				typeof(MapperValue<DefaultMapperTests>));
		}

	}

	public class MapperValue<T>
	{
		public T Value { get; set; }
	}
}
