using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using YamlMap.Serialization.Mappers;

namespace YamlMap.Tests.Serialization.Mappers
{
	public class DefaultMapperTests
	{

		public T Map<T>(IToken token)
		{
			var item = new MapperValue<T>();
			var mapper = new DefaultMapper(typeof(MapperValue<T>));
			Assert.That(mapper.Map(token, item));

			return item.Value;
		}

		[Test]
		public void DefaultMapper_String()
		{
			Assert.That(Map<string>(new ValueToken("Value", "String", 0)), Is.EqualTo("String"));
		}

		[Test]
		public void DefaultMapper_Bool()
		{
			Assert.That(Map<bool>(new ValueToken("Value", "0", 0)), Is.EqualTo(false));
			Assert.That(Map<bool>(new ValueToken("Value", "1", 0)), Is.EqualTo(true));
			Assert.That(Map<bool>(new ValueToken("Value", "false", 0)), Is.EqualTo(false));
			Assert.That(Map<bool>(new ValueToken("Value", "true", 0)), Is.EqualTo(true));
		}

		[Test]
		public void DefaultMapper_Long()
		{
			Assert.That(Map<long>(new ValueToken("Value", "123456789", 0)), Is.EqualTo(123456789));
		}

		[Test]
		public void DefaultMapper_Int()
		{
			Assert.That(Map<int>(new ValueToken("Value", "2", 0)), Is.EqualTo(2));
		}

		[Test]
		public void DefaultMapper_Decimal()
		{
			Assert.That(Map<decimal>(new ValueToken("Value", "2.2", 0)), Is.EqualTo(2.2M));
		}

		[Test]
		public void DefaultMapper_Double()
		{
			Assert.That(Map<double>(new ValueToken("Value", "2.2", 0)), Is.EqualTo(2.2));
		}

		[Test]
		public void DefaultMapper_DateTime()
		{
			Assert.That(Map<DateTime>(new ValueToken("Value", "2020.01.01 12:12:12", 0)), Is.EqualTo(DateTime.Parse("2020.01.01 12:12:12")));
		}

		[Test]
		public void DefaultMapper_Guid()
		{
			Assert.That(Map<Guid>(new ValueToken("Value", "1FB53019-AD8C-4FBD-B528-BCD07F6EB6BA", 0)), Is.EqualTo(Guid.Parse("1FB53019-AD8C-4FBD-B528-BCD07F6EB6BA")));
		}

		[Test]
		public void DefaultMapper_Type()
		{
			Assert.That(
				Map<Type>(new ValueToken("Value", "YamlMap.Tests.Serialization.Mappers.DefaultMapperTests, YamlMap.Tests", 0)),
                Is.EqualTo(this.GetType()));
		}

		[Test]
		public void DefaultMapper_GenericType()
		{
			Assert.That(
				Map<Type>(new ValueToken("Value", "YamlMap.Tests.Serialization.Mappers.MapperValue`1[[YamlMap.Tests.Serialization.Mappers.DefaultMapperTests, YamlMap.Tests]], YamlMap.Tests", 0)),
                Is.EqualTo(typeof(MapperValue<DefaultMapperTests>)));
		}

	}

	public class MapperValue<T>
	{
		public T Value { get; set; }
	}
}
