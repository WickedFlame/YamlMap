# Change Log
All notable changes to this project will be documented in this file.
 
The format is based on [Keep a Changelog](http://keepachangelog.com/)
and this project adheres to [Semantic Versioning](http://semver.org/).
 
## v1.4.0
### Added
- Multiline string literals

### Changed
- Performance improvements

## v1.3.0
### Added
- Use List<> to map to IEnumerable<> properties
- Extended Serializer to serialize to file and deserialize from file
  
### Changed
- Breaking - Throw YamlSerializationException instead of InvalidConfigurationException when creating objects
- Exceptions deliver better messages to show what configuration caused the problem
 
### Fixed
 
## v1.2.4
### Added
- Create instances of types with parameterless constructors
- Added Benchmarks to ensure performance
   
## v1.2.3
### Changed  
- Downgrade to netstandard2.0 instead of netstandard2.1
- Update building to use [https://nuke.build/](https://nuke.build/)
