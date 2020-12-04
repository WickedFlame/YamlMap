# Yaml
[![Build Status](https://travis-ci.org/WickedFlame/Yaml.svg?branch=master)](https://travis-ci.org/WickedFlame/Yaml)
[![Build status](https://ci.appveyor.com/api/projects/status/u0vhwefngralstax?svg=true)](https://ci.appveyor.com/project/chriswalpen/yaml)

A .NET Yaml Parser

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

