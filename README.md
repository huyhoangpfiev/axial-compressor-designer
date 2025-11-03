# Axial Compressor Designer - Tandem Blade

A Visual Studio C# Windows Forms application for designing axial compressors with tandem blade configurations.

## Features

### 1. 2D Blade Profile Design & Flowpath Calculation
- **Blade Parameters Configuration:**
  - Chord length adjustment
  - Stagger angle control
  - Maximum thickness ratio
  - Camber angle settings
- **NACA-style Airfoil Generation:**
  - Automatic profile calculation based on parameters
  - Real-time 2D visualization
- **Tandem Blade Support:**
  - Enable/disable tandem configuration
  - Gap ratio control between blades
  - Second chord ratio adjustment
- **Flowpath Calculation:**
  - Inlet and outlet radius definition
  - Axial length specification
  - Automatic flowpath generation

### 2. 3D Visualization
- **OpenGL-based 3D Rendering:**
  - Real-time 3D blade visualization using OpenTK
  - Interactive rotation controls (X, Y, Z axes)
  - Zoom control
  - Grid and axes display options
- **Stage Configuration:**
  - Adjustable number of blades
  - Stage height control
  - Automatic 3D model generation
- **Visualization Features:**
  - Solid rendering with wireframe overlay
  - Lighting and shading effects
  - Multiple blade arrangement in circular pattern

### 3. OpenVOGEL Solver Integration
- **Flight Conditions Setup:**
  - Velocity configuration
  - Density settings
  - Viscosity parameters
  - Mach number specification
- **Solver Settings:**
  - Maximum iterations control
  - Convergence criteria
  - Unsteady flow analysis option
  - Viscous flow effects
- **OpenVOGEL Integration:**
  - Geometry export to OpenVOGEL XML format
  - Solver execution interface
  - Results import and visualization
  - Results export to file
- **Analysis Results:**
  - Lift, drag, and moment coefficients
  - Pressure distribution
  - Velocity field
  - Vorticity distribution

## Project Structure

```
AxialCompressorDesigner/
├── AxialCompressorDesigner.sln          # Visual Studio solution file
├── AxialCompressorDesigner/             # Main project directory
│   ├── AxialCompressorDesigner.csproj   # Project file
│   ├── Program.cs                       # Application entry point
│   ├── MainForm.cs                      # Main form with tab interface
│   ├── BladeProfile2DPanel.cs           # 2D design and calculation panel
│   ├── Blade3DViewerPanel.cs            # 3D visualization panel
│   └── OpenVOGELIntegrationPanel.cs     # OpenVOGEL integration panel
└── README.md                            # This file
```

## Requirements

### System Requirements
- **Operating System:** Windows 10 or later
- **.NET Version:** .NET 8.0 or later
- **Graphics:** OpenGL 3.3 compatible graphics card

### Dependencies
The project uses the following NuGet packages:
- **OpenTK** (v4.8.2) - OpenGL bindings for 3D rendering
- **OpenTK.GLControl** (v3.3.3) - Windows Forms OpenGL control

### External Software
- **OpenVOGEL** (Optional): For aerodynamic analysis
  - GitHub: https://github.com/npapnet/OpenVOGEL
  - Download and install separately for full solver functionality

## Quick Download

**Download the complete project as a zip file:** [AxialCompressorDesigner_v1.0.zip](AxialCompressorDesigner_v1.0.zip)

This zip file contains all source code, documentation, and project files ready to be opened in Visual Studio.

## Building the Project

### Using Visual Studio
1. Open `AxialCompressorDesigner.sln` in Visual Studio 2022 or later
2. Restore NuGet packages (should happen automatically)
3. Build the solution (F6 or Build → Build Solution)
4. Run the application (F5 or Debug → Start Debugging)

### Using .NET CLI (on Windows)
```bash
cd axial-compressor-designer
dotnet restore
dotnet build
dotnet run --project AxialCompressorDesigner
```

**Note:** This project requires Windows to build and run due to Windows Forms dependencies.

## Usage Guide

### Getting Started

1. **Launch the Application**
   - Run the executable from Visual Studio or the built application
   - The main window will open with three tabs

2. **Design Blade Profile (2D Tab)**
   - Adjust blade parameters in the control panel:
     - Chord length
     - Stagger angle
     - Max thickness
     - Camber angle
   - Click "Calculate Profile" to generate the blade profile
   - For tandem configuration:
     - Enable "Enable Tandem Blade" checkbox
     - Adjust gap ratio and second chord ratio
     - Recalculate to see both blades
   - Configure flowpath parameters and click "Calculate Flowpath"

3. **View 3D Model (3D Tab)**
   - Set number of blades and stage height
   - Click "Generate 3D Model" to create the 3D visualization
   - Use rotation sliders to view from different angles
   - Adjust zoom for better visualization
   - Toggle grid and axes display as needed

4. **Run Aerodynamic Analysis (OpenVOGEL Tab)**
   - Set flight conditions (velocity, density, etc.)
   - Configure solver settings
   - Set the OpenVOGEL installation path
   - Click "Export Geometry" to save the design
   - Click "Run OpenVOGEL Solver" to start analysis
   - Import and view results

### Tips
- Start with default parameters and adjust incrementally
- Use the 2D view to verify blade geometry before 3D generation
- Export results regularly to save your work
- The tandem blade configuration allows for more aggressive turning angles

## Technical Details

### 2D Profile Generation
The blade profiles are generated using NACA 4-digit airfoil equations with:
- Thickness distribution: 5-term polynomial equation
- Camber line: Parabolic distribution
- Coordinate transformation: Applied stagger angle rotation

### 3D Model Generation
- Blades are arranged in a circular pattern around the axis
- Profile is extruded along the radial direction
- Slight taper can be applied for realistic geometry
- Triangulation for OpenGL rendering

### OpenVOGEL Integration
- Exports geometry in XML format compatible with OpenVOGEL
- Includes flight conditions and solver settings
- Results can be imported and analyzed within the application

## OpenVOGEL Information

OpenVOGEL is an open-source software for aerodynamic analysis using the Vortex Lattice Method (VLM). It's particularly suitable for:
- Lifting surfaces analysis
- Multiple lifting surfaces configurations
- Steady and unsteady flow analysis
- Viscous corrections

To use the full OpenVOGEL integration:
1. Download from: https://github.com/npapnet/OpenVOGEL
2. Install following the OpenVOGEL documentation
3. Set the installation path in the application
4. Export geometry and run analysis

## Future Enhancements

Potential improvements for future versions:
- Advanced blade profile shapes (custom airfoils)
- Non-linear flowpath configurations
- Performance map generation
- CFD mesh export
- More detailed OpenVOGEL integration
- Results visualization improvements
- Multiple stage analysis
- Off-design condition analysis

## License

This project is provided as-is for educational and research purposes.

## Acknowledgments

- OpenVOGEL team for the aerodynamic solver
- OpenTK contributors for OpenGL bindings
- The compressor design community for reference materials

## Contact & Support

For questions or issues, please open an issue on the GitHub repository.