# Installation Guide

## Prerequisites

### System Requirements
- Windows 10 or later (64-bit recommended)
- .NET 8.0 SDK or later
- Visual Studio 2022 (Community, Professional, or Enterprise)
- Minimum 4 GB RAM
- OpenGL 3.3 compatible graphics card

### Software Installation

#### 1. Install .NET 8.0 SDK
Download and install from: https://dotnet.microsoft.com/download/dotnet/8.0

Verify installation:
```bash
dotnet --version
```

#### 2. Install Visual Studio 2022
Download from: https://visualstudio.microsoft.com/

During installation, ensure you select:
- .NET desktop development workload
- Windows Forms components

#### 3. Install OpenVOGEL (Optional)
For full aerodynamic analysis capabilities:

1. Visit: https://github.com/npapnet/OpenVOGEL
2. Download the latest release
3. Follow OpenVOGEL installation instructions
4. Note the installation path for later use in the application

## Building from Source

### Option 1: Using Visual Studio

1. Clone the repository:
   ```bash
   git clone https://github.com/huyhoangpfiev/axial-compressor-designer.git
   cd axial-compressor-designer
   ```

2. Open `AxialCompressorDesigner.sln` in Visual Studio 2022

3. Restore NuGet packages:
   - Right-click on Solution in Solution Explorer
   - Select "Restore NuGet Packages"
   - Or: Tools → NuGet Package Manager → Package Manager Console
   - Run: `dotnet restore`

4. Build the solution:
   - Press F6 or select Build → Build Solution
   - Wait for build to complete

5. Run the application:
   - Press F5 or select Debug → Start Debugging
   - Or press Ctrl+F5 for running without debugging

### Option 2: Using .NET CLI

1. Clone the repository:
   ```bash
   git clone https://github.com/huyhoangpfiev/axial-compressor-designer.git
   cd axial-compressor-designer
   ```

2. Restore dependencies:
   ```bash
   dotnet restore
   ```

3. Build the project:
   ```bash
   dotnet build --configuration Release
   ```

4. Run the application:
   ```bash
   dotnet run --project AxialCompressorDesigner --configuration Release
   ```

## Troubleshooting

### Build Errors

#### "NETSDK1100: To build a project targeting Windows..."
**Problem:** Building on non-Windows OS
**Solution:** This project requires Windows to build and run. Use a Windows machine or Windows VM.

#### "Package restore failed"
**Problem:** NuGet packages not downloading
**Solution:**
1. Check internet connection
2. Clear NuGet cache: `dotnet nuget locals all --clear`
3. Restore again: `dotnet restore`

#### "OpenTK not found"
**Problem:** OpenTK packages not installed
**Solution:**
1. Open Package Manager Console in Visual Studio
2. Run: `Install-Package OpenTK -Version 4.8.2`
3. Run: `Install-Package OpenTK.GLControl -Version 3.3.3`

### Runtime Errors

#### "OpenGL context creation failed"
**Problem:** Graphics drivers not up to date
**Solution:**
1. Update graphics card drivers to latest version
2. Ensure OpenGL 3.3 or higher is supported
3. Check: Run `glxinfo | grep "OpenGL version"` (Linux) or use GPU-Z (Windows)

#### "System.TypeLoadException"
**Problem:** .NET runtime version mismatch
**Solution:**
1. Verify .NET 8.0 runtime is installed
2. Reinstall .NET 8.0 SDK if necessary

#### Application crashes on startup
**Problem:** Missing dependencies or corrupted installation
**Solution:**
1. Clean and rebuild: `dotnet clean` then `dotnet build`
2. Delete `bin` and `obj` folders
3. Restore and rebuild

## Running the Application

After successful build:

### From Visual Studio
- Press F5 (Debug mode) or Ctrl+F5 (Release mode)

### From Command Line
```bash
cd AxialCompressorDesigner\bin\Release\net8.0-windows
.\AxialCompressorDesigner.exe
```

### From File Explorer
Navigate to:
```
axial-compressor-designer\AxialCompressorDesigner\bin\Release\net8.0-windows\
```
Double-click `AxialCompressorDesigner.exe`

## Configuration

### Setting OpenVOGEL Path
1. Launch the application
2. Go to "OpenVOGEL Solver" tab
3. Click "Browse..." next to OpenVOGEL Path
4. Navigate to your OpenVOGEL installation directory
5. Click OK

### First Time Setup
1. Start with the "2D Blade Profile & Flowpath" tab
2. Use default parameters to familiarize yourself
3. Click "Calculate Profile" to see results
4. Move to "3D Visualization" tab
5. Click "Generate 3D Model"
6. Explore the interface and controls

## Getting Help

If you encounter issues:

1. Check this installation guide thoroughly
2. Review the main README.md for usage instructions
3. Open an issue on GitHub with:
   - Your Windows version
   - .NET SDK version (`dotnet --version`)
   - Visual Studio version
   - Complete error message
   - Steps to reproduce

## Updates

To update to the latest version:

```bash
git pull origin main
dotnet restore
dotnet build
```

Or download the latest release from the GitHub repository.
