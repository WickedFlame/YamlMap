﻿using System;
using FluentAssertions;
using NUnit.Framework;
using Polaroider;
using YamlMap.Serialization;

namespace YamlMap.Tests.Serialization
{
    public class ConstructorResolverTests
    {
        [Test]
        public void ConstructorResolver_NoCtor()
        {
            var resolver = new ContractResolver();
            var contract = resolver.GetConstructor(typeof(NoCtor));

            contract.Constructor.GetParameters().Should().HaveCount(0);
        }

        [Test]
        public void ConstructorResolver_WithCtor()
        {
            var resolver = new ContractResolver();
            var contract = resolver.GetConstructor(typeof(WithCtor));

            contract.Constructor.GetParameters().Should().HaveCount(2);
        }

        [Test]
        public void ConstructorResolver_CreateConstructoParameters()
        {
            var tokens = new[]
            {
                new ValueToken("test", "fail", 0),
                new ValueToken("Id", "1", 0),
                new ValueToken("Value", "passed", 0),
                new ValueToken("values", "fail", 0)
            };
            var resolver = new ContractResolver();
            var contract = resolver.GetConstructor(typeof(WithCtor));

            resolver.CreateConstructoParameters(contract.Constructor, tokens).MatchSnapshot();
        }

        [Test]
        public void ConstructorResolver_CreateConstructoParameters_InvalidType()
        {
            var tokens = new[]
            {
                new ValueToken("Id", "fail", 0),
                new ValueToken("Value", "passed", 0)
            };
            var resolver = new ContractResolver();
            var contract = resolver.GetConstructor(typeof(WithCtor));

            resolver.CreateConstructoParameters(contract.Constructor, tokens).Should().HaveCount(1);
        }

        [Test]
        public void ConstructorResolver_CreateConstructoParameters_Primitive()
        {
            var tokens = new[]
            {
                new ValueToken("Value", "5", 0)
            };
            var resolver = new ContractResolver();
            var contract = resolver.GetConstructor(typeof(int));

            resolver.CreateConstructoParameters(contract.Constructor, tokens).Should().HaveCount(0);
        }
    }
    
    public class NoCtor
    {
        public int Id { get; set; }

        public string Value { get; set; }
    }

    public class WithCtor
    {
        public WithCtor(int id)
        {
            Id = id;
        }

        public WithCtor(string value, int id)
        {
            Value = value;
            Id = id;
        }

        public int Id { get; set; }

        public string Value { get; set; }
    }
}
