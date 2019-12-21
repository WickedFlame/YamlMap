# Yaml
[![Build Status](https://travis-ci.org/WickedFlame/Yaml.svg?branch=master)](https://travis-ci.org/WickedFlame/Yaml)
[![Build status](https://ci.appveyor.com/api/projects/status/u0vhwefngralstax?svg=true)](https://ci.appveyor.com/project/chriswalpen/yaml)

A .NET Yaml Parser

# Currently implemented 
- Reading YAML strings to Tokens
- Deserializing YAML string to a POCO

# Not yet implemented 
- Serializing POCO to YAML

# Currently implemented YAML Features
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


# Not yet implemented YAML Features

Blocks
```
--- 
```

Inline Objects
```
User: {name: John Smith, age: 33}
```

Inline Lists
```
Movies: [Casablanca, Spellbound, Notorious]
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

Quotations
```
Movies: "Casablanca, Spellbound, Notorious"
Drive: "c: is a drive"
```

