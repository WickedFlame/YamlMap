# YamlMap
[![GitHub Workflow Status](https://img.shields.io/github/workflow/status/WickedFlame/YamlMap/Build?label=Build&logo=Github&style=for-the-badge)](https://github.com/WickedFlame/YamlMap/actions/workflows/build.yml)
[![Build status](https://img.shields.io/appveyor/build/chriswalpen/yamlmap/master?label=Master&logo=appveyor&style=for-the-badge)](https://ci.appveyor.com/project/chriswalpen/yamlmap/branch/master)
[![Build status](https://img.shields.io/appveyor/build/chriswalpen/yamlmap/dev?label=Dev&logo=appveyor&style=for-the-badge)](https://ci.appveyor.com/project/chriswalpen/yamlmap/branch/dev)
  
[![NuGet Version](https://img.shields.io/nuget/v/yamlmap.svg?style=for-the-badge&label=Latest)](https://www.nuget.org/packages/yamlmap/)
[![NuGet Version](https://img.shields.io/nuget/vpre/yamlmap.svg?style=for-the-badge&label=RC)](https://www.nuget.org/packages/yamlmap/)

A .NET Yaml Parser.  
Map Yaml to .NET objects and vice versa.  

# Usage
```
var items = YamlConverter.Read<Item>(filepath);

// or
var reader = new YamlReader();
items = reader.Read<Item>(filepath);

// serialize an object to string
var yml = Serializer.Serialize(items);

// deserialize a string to a object
items = Serializer.Deserialize<Item>(yml);
```

# Implemented serialization features
- Reading YAML strings to Tokens
- Deserializing YAML string to a POCO
- Writing Tokens to YAML
- Serializing POCO to YAML

# Implemented YAML features
Comments
```
# This is a comment
```

Properties
```
Property: Value
```

Objects
```
User:
  name: John Smith
  age: 33
```

Lists
```
Movies:
  - Casablanca
  - Spellbound
  - Notorious
```

Inline Lists
```
Movies: [Casablanca, Spellbound, Notorious]
```

Multiline Lists
```
Movies: [Casablanca, 
   Spellbound, Notorious]
```

Quotations
```
Movies: "Casablanca, Spellbound, Notorious"
Drive: 'c: is a drive'
```

# Not yet implemented YAML Features

Blocks
```
--- 
```

Inline Objects
```
User: {name: John Smith, age: 33}
```

Block statements
```
Text: |
  There was a young fellow of Warwick
  Who had reason for feeling euphoric
      For he could, by election
      Have triune erection
  Ionic, Corinthian, and Doric

WrappedText: >
  Wrapped text
  will be folded
  into a single
  paragraph

  Blank lines denote
  paragraph breaks

```

