using System.Data;
using System.Runtime.InteropServices;

using NAudio.CoreAudioApi;

namespace SetSoundVolume {
  /// <summary>
  /// Provides functionality to control system audio volume through command line interface.
  /// </summary>
  internal class Program {
    /// <summary>
    /// Displays the usage instructions for the application.
    /// </summary>
    /// <remarks>
    /// Shows supported commands and arguments, using the actual executable name in the examples.
    /// </remarks>
    static void ShowUsage() {
      string exeName = Path.GetFileName(Environment.ProcessPath ?? "volume.exe");
      Console.WriteLine($"Usage: {exeName} [volume_percentage (0-100)] [-q|--quiet|/quiet] [-h|--help|/help]");
      Console.WriteLine("If no arguments are provided, displays current volume");
      Console.WriteLine("Options:");
      Console.WriteLine("  -q, --quiet, /quiet    Suppress output");
      Console.WriteLine("  -h, --help, /help      Show this help message");
    }

    /// <summary>
    /// Main entry point for the application.
    /// </summary>
    /// <param name="args">Command line arguments:
    /// <list type="bullet">
    /// <item><description>No arguments: Display current volume</description></item>
    /// <item><description>Number (0-100): Set volume to specified percentage</description></item>
    /// <item><description>-q, --quiet, /quiet: Suppress output</description></item>
    /// <item><description>-h, --help, /help: Show usage instructions</description></item>
    /// </list>
    /// </param>
    /// <remarks>
    /// The application can be used to either view or set the system's master volume.
    /// When setting the volume, values must be between 0 and 100 inclusive.
    /// </remarks>
    static void Main(string[] args) {
      bool quietMode = false;
      float? volumeToSet = null;

      // Process arguments
      foreach (string arg in args) {
        // Check for help argument
        if (arg.Equals("-h", StringComparison.OrdinalIgnoreCase) ||
            arg.Equals("--help", StringComparison.OrdinalIgnoreCase) ||
            arg.Equals("/help", StringComparison.OrdinalIgnoreCase)) {
          ShowUsage();
          return;
        }
        // Check for quiet mode argument
        else if (arg.Equals("-q", StringComparison.OrdinalIgnoreCase) ||
                 arg.Equals("--quiet", StringComparison.OrdinalIgnoreCase) ||
                 arg.Equals("/quiet", StringComparison.OrdinalIgnoreCase)) {
          quietMode = true;
        }
        // Try to parse as volume value
        else if (float.TryParse(arg, out float volume)) {
          if (volume < 0 || volume > 100) {
            if (!quietMode) {
              Console.WriteLine("Error: Volume must be between 0 and 100");
            }
            return;
          }
          volumeToSet = volume;
        }
        // Invalid argument
        else {
          if (!quietMode) {
            Console.WriteLine($"Error: Invalid argument '{arg}'");
            ShowUsage();
          }
          return;
        }
      }

      try {
        // Initialize audio device
        var deviceEnumerator = new MMDeviceEnumerator();
        var device = deviceEnumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);

        // Get current volume
        float currentVolume = device.AudioEndpointVolume.MasterVolumeLevelScalar * 100;

        // If no volume specified, just show current volume
        if (volumeToSet == null) {
          if (!quietMode) {
            Console.WriteLine($"Current Master Volume: {currentVolume:F1}%");
          }
          return;
        }

        // Set new volume
        device.AudioEndpointVolume.MasterVolumeLevelScalar = volumeToSet.Value / 100.0f;

        if (!quietMode) {
          Console.WriteLine($"Volume set to: {volumeToSet:F1}%");
        }
      }
      catch (Exception ex) {
        if (!quietMode) {
          Console.WriteLine($"Error: Failed to access audio device: {ex.Message}");
        }
      }
    }
  }
}


//  Original hand-written version

//using System.Data;
//using System.Runtime.InteropServices;

//using NAudio.CoreAudioApi;

//namespace SetSoundVolume {
//  internal class Program {

//    static void Main(string[] args) {
//      // Ensure a command-line argument is provided and is a valid volume
//      if (args.Length != 1 || !float.TryParse(args[0], out float newVolume) || newVolume < 0 || newVolume > 100) {
//        Console.WriteLine("Usage: VolumeControl <volume_percentage (0-100)>");
//        return;
//      }

//      var deviceEnumerator = new MMDeviceEnumerator();
//      var device = deviceEnumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);

//      //// Read the current volume
//      //float currentVolume = device.AudioEndpointVolume.MasterVolumeLevelScalar * 100;
//      //Console.WriteLine($"Current Master Volume: {currentVolume}%");

//      // Set the new volume
//      device.AudioEndpointVolume.MasterVolumeLevelScalar = newVolume / 100.0f;
//      Console.WriteLine($"Volume set to: {newVolume}%");
//    }
//  }
//}
