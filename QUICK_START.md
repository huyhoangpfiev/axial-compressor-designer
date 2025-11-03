# Quick Start Guide

Get up and running with Axial Compressor Designer in 5 minutes!

## Prerequisites Check

Before starting, ensure you have:
- ✅ Windows 10 or later
- ✅ .NET 8.0 SDK installed
- ✅ Visual Studio 2022 (recommended) OR Visual Studio Code with C# extension

## Installation Steps

### Option 1: Visual Studio (Recommended)

1. **Clone the repository**
   ```bash
   git clone https://github.com/huyhoangpfiev/axial-compressor-designer.git
   cd axial-compressor-designer
   ```

2. **Open in Visual Studio**
   - Double-click `AxialCompressorDesigner.sln`
   - Visual Studio will open and restore NuGet packages automatically

3. **Build the project**
   - Press `F6` or click Build → Build Solution
   - Wait for build to complete (should take < 30 seconds)

4. **Run the application**
   - Press `F5` or click Debug → Start Debugging
   - The application window will open

### Option 2: Command Line

1. **Clone and build**
   ```bash
   git clone https://github.com/huyhoangpfiev/axial-compressor-designer.git
   cd axial-compressor-designer
   dotnet restore
   dotnet build
   ```

2. **Run**
   ```bash
   dotnet run --project AxialCompressorDesigner
   ```

## First Steps Tutorial

### Tutorial 1: Design Your First Blade (2 minutes)

1. **Launch the application** (it opens to the "2D Blade Profile & Flowpath" tab)

2. **Use default parameters** or adjust:
   - Chord Length: 100 mm
   - Stagger Angle: 30°
   - Max Thickness: 10%
   - Camber Angle: 20°

3. **Click "Calculate Profile"**
   - A red blade profile appears in the drawing area

4. **Try tandem configuration**:
   - Check "Enable Tandem Blade"
   - Click "Calculate Profile" again
   - Now you see two blades!

**Congratulations!** You've designed your first blade profile.

### Tutorial 2: Visualize in 3D (1 minute)

1. **Click the "3D Visualization" tab**

2. **Set parameters**:
   - Number of Blades: 20
   - Stage Height: 200 mm

3. **Click "Generate 3D Model"**
   - A 3D model appears with blades arranged in a circle

4. **Interact with the view**:
   - Drag the rotation sliders to rotate
   - Adjust zoom slider to zoom in/out
   - Toggle "Show Grid" and "Show Axes"

**Amazing!** You're now viewing your compressor stage in 3D.

### Tutorial 3: Export for Analysis (1 minute)

1. **Click the "OpenVOGEL Solver" tab**

2. **Review default flight conditions** (they're already set)

3. **Click "Export Geometry to OpenVOGEL"**
   - A file is created in the Export directory
   - Status shows "Export completed"

4. **View results panel**
   - Read the getting started information
   - Note the export location

**Done!** You've exported your design for aerodynamic analysis.

## Common Issues & Quick Fixes

### Issue: "Build failed - NETSDK1100"
**Fix**: Make sure you're on Windows. This application requires Windows to run.

### Issue: "NuGet packages not restored"
**Fix**: 
```bash
dotnet restore
```
Or in Visual Studio: Right-click solution → Restore NuGet Packages

### Issue: "Black screen in 3D view"
**Fix**: Update your graphics drivers. The application requires OpenGL 3.3+.

### Issue: Application won't start
**Fix**: Verify .NET 8.0 is installed:
```bash
dotnet --version
```
Should show 8.0.x or later.

## Next Steps

Now that you have the basics:

1. **Experiment with parameters**
   - Try different chord lengths
   - Adjust camber angles
   - Play with tandem blade ratios

2. **Read detailed documentation**
   - [USER_GUIDE.md](USER_GUIDE.md) - Complete feature guide
   - [INSTALLATION.md](INSTALLATION.md) - Detailed setup
   - [ARCHITECTURE.md](ARCHITECTURE.md) - Technical details

3. **Optional: Install OpenVOGEL**
   - Download from: https://github.com/npapnet/OpenVOGEL
   - Set path in application
   - Run full aerodynamic analysis

4. **Contribute**
   - Found a bug? Open an issue
   - Have an idea? Share it
   - Want to code? Check [CONTRIBUTING.md](CONTRIBUTING.md)

## Keyboard Shortcuts

While the application is running:
- `F5` in Visual Studio: Run with debugging
- `Ctrl+F5` in Visual Studio: Run without debugging

Within the application:
- Mouse controls for 3D view rotation
- Mouse wheel for zooming (in 2D view, if enabled)

## Project Structure Quick Reference

```
axial-compressor-designer/
├── AxialCompressorDesigner/          # Main project
│   ├── Program.cs                    # Entry point
│   ├── MainForm.cs                   # Main window
│   ├── BladeProfile2DPanel.cs        # 2D design
│   ├── Blade3DViewerPanel.cs         # 3D visualization
│   └── OpenVOGELIntegrationPanel.cs  # Solver integration
├── README.md                         # Overview
├── QUICK_START.md                    # This file
├── USER_GUIDE.md                     # Detailed usage
├── INSTALLATION.md                   # Setup guide
├── ARCHITECTURE.md                   # Technical docs
└── CONTRIBUTING.md                   # How to contribute
```

## Tips for Success

1. **Start simple**: Use default values first, then experiment
2. **One change at a time**: Adjust one parameter, see the effect
3. **2D before 3D**: Design in 2D first, then visualize in 3D
4. **Save your work**: Export designs regularly (feature coming soon)
5. **Ask for help**: Open an issue if stuck

## Getting Help

**Quick help**: Check the tooltips and labels in the application

**Documentation**: 
- Start with [USER_GUIDE.md](USER_GUIDE.md)
- Technical questions: [ARCHITECTURE.md](ARCHITECTURE.md)
- Installation problems: [INSTALLATION.md](INSTALLATION.md)

**Community support**:
- GitHub Issues: Bug reports and questions
- GitHub Discussions: General discussions

## Video Tutorial (Coming Soon)

We're working on video tutorials! Check the repository for updates.

## What's Next?

After mastering the basics:
- Try designing realistic compressor stages
- Experiment with tandem blade configurations
- Learn about compressor design theory
- Contribute improvements to the project

## Feedback

We'd love to hear from you!
- Did this guide help?
- What was confusing?
- What should we add?

Open an issue with the tag "documentation" to provide feedback.

---

**Welcome to the Axial Compressor Designer community!** 🚀

Happy designing! ✈️
