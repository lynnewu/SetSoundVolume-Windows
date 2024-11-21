# Volume Control CLI

A simple command-line utility for controlling the Windows system volume.

## Features

* Display current system volume
* Set system volume (0-100%)
* Quiet mode for script integration
* Help system with usage instructions

## Installation

1. Ensure you have .NET 6.0 or later installed
2. Download the latest release from the Releases page
3. Place the executable wherever you like (no installation required)

## Usage

````powershell
# Show current volume
volume

# Set volume to 50%
volume 50

# Silently set volume to 75%
volume 75 -q

# Show help
volume -h
````

### Command Line Arguments

* No arguments: Displays current volume
* `0-100`: Sets volume to specified percentage
* `-q, --quiet, /quiet`: Suppress output
* `-h, --help, /help`: Show usage instructions

## Requirements

* Windows 10 or later
* .NET 6.0 or later
* NAudio NuGet package

## Building from Source

1. Clone the repository:
````bash
git clone https://github\.com/yourusername/VolumeControl\.git
````

2. Build the solution:
````bash
dotnet build
````

## Dependencies

* NAudio - For audio device control
* System.Runtime.InteropServices - For Windows API integration

## Contributing

Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

## License

MIThttps://choosealicense 
c
˙
 om/licenses/mit/

## Sponsor
* Silver Star Brands
  * <a href="https://www.silverstarbrands.com " target="_blank">https://www.silverstarbrands.com </a>

## Acknowledgments

* NAudio library contributors
* Windows Core Audio API

## Version History

* 1.0.0
* Initial release
* Basic volume control functionality
* Command-line interface