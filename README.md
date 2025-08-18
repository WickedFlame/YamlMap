# YamlMap
<!--[![GitHub Workflow Status](https://img.shields.io/github/workflow/status/WickedFlame/YamlMap/Build?label=Build&logo=Github&style=for-the-badge)](https://github.com/WickedFlame/YamlMap/actions/workflows/build.yml)-->
<!--[![GitHub Workflow Status (branch)](https://img.shields.io/github/workflow/status/WickedFlame/YamlMap/Build/dev?label=DEV&logo=github&style=for-the-badge)](https://github.com/WickedFlame/YamlMap/actions/workflows/build.yml)-->
<!--[![GitHub Workflow Status (branch)](https://img.shields.io/github/workflow/status/WickedFlame/YamlMap/linux%20build/dev?label=DEV%20LINUX&logo=github&style=for-the-badge)](https://github.com/WickedFlame/YamlMap/actions/workflows/linux.yml)-->
[![Build status](https://img.shields.io/appveyor/build/chriswalpen/yamlmap/master?label=Master&logo=appveyor&style=for-the-badge)](https://ci.appveyor.com/project/chriswalpen/yamlmap/branch/master)
[![Build status](https://img.shields.io/appveyor/build/chriswalpen/yamlmap/dev?label=Dev&logo=appveyor&style=for-the-badge)](https://ci.appveyor.com/project/chriswalpen/yamlmap/branch/dev)
  
[![NuGet Version](https://img.shields.io/nuget/v/yamlmap.svg?style=for-the-badge&label=Latest)](https://www.nuget.org/packages/yamlmap/)
[![NuGet Version](https://img.shields.io/nuget/vpre/yamlmap.svg?style=for-the-badge&label=RC)](https://www.nuget.org/packages/yamlmap/)
  
[![Codacy Badge](https://app.codacy.com/project/badge/Grade/ab8916dc1225487a8a19923e6c96d7fe)](https://www.codacy.com/gh/WickedFlame/YamlMap/dashboard?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=WickedFlame/YamlMap&amp;utm_campaign=Badge_Grade)
  
A .NET Yaml Parser.  
Map Yaml to .NET objects and vice versa.  

# Usage
## Deserialize a yaml string to an object
```
// Read a file using a YamlFileReader
var reader = new YamlFileReader();
var item = reader.Read<Item>(filepath);

// or

// Read a string using the YamlReader
var reader = new YamlReader();
var item = reader.Read<Item>(yaml);

// or

// Read a string using the static Serializer
var item = Serializer.Deserialize<Item>(yaml);
```

## Serialize an object to a yaml string
```
// Write a file using a YamFilelWriter
var writer = new YamlFileWriter();
writer.Write(filepath, item);

// or

// Serialize an object to a string using a YamlWriter
var writer = new YamlWriter();
writer.Write(item);

// or

// serialize an object to string using the static Serializer
var yml = Serializer.Serialize(item);
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

Block statements (multiline strings)
```
Text: |
  There was a young fellow of Warwick
  Who had reason for feeling euphoric
      For he could, by election
      Have triune erection
  Ionic, Corinthian, and Doric
```
```
WrappedText: >
  Wrapped text
  will be folded
  into a single
  paragraph

  Blank lines denote
  paragraph breaks
```
Pitfalls with Plain flow scalars:  
* Colons (:) can be contained in a string, but are not allowed before a space or newline

# Not yet implemented YAML Features

Blocks
```
--- 
```

Inline Objects
```
User: {name: John Smith, age: 33}
```

Block statements with strip or keep newlines at end
```
Text: |+
  There was a young fellow of Warwick
  
Text: |-
  There was a young fellow of Warwick
  
WrappedText: >+
  Wrapped text
  
WrappedText: >-
  Wrapped text
  
```

# Best practice
## Deserializng yaml to a Object
To deserialize a Yaml to a object, it is best to have a parameterless constructor.  
If a object needs to have a constructor with parameters, the parameter names have to be the same as the properties that they are mapped to.  
Else YamlMap will not be able to map the correct values to the parameters.  
