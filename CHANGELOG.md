# Changelog

All notable changes to the Axial Compressor Designer project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [1.0.0] - 2024-11-03

### Added - Initial Release

#### 2D Blade Profile Design
- NACA 4-digit airfoil generation algorithm
- Configurable blade parameters:
  - Chord length (10-1000 mm)
  - Stagger angle (-90° to 90°)
  - Maximum thickness (1-20%)
  - Camber angle (0-60°)
- Real-time 2D visualization using GDI+
- Coordinate transformation and rendering
- Clear drawing functionality

#### Tandem Blade Support
- Enable/disable tandem blade configuration
- Gap ratio control (1-50%)
- Second chord ratio adjustment (10-100%)
- Dual blade profile calculation and display
- Independent tandem blade visualization

#### Flowpath Calculation
- Inlet radius configuration (50-5000 mm)
- Outlet radius configuration (50-5000 mm)
- Axial length specification (10-1000 mm)
- Automatic flowpath generation
- Inner and outer flowpath boundaries
- Real-time flowpath visualization

#### 3D Visualization
- OpenGL-based 3D rendering using OpenTK 4.8.2
- Interactive rotation controls:
  - X-axis rotation (-180° to 180°)
  - Y-axis rotation (-180° to 180°)
  - Z-axis rotation (-180° to 180°)
- Zoom control (1.0x to 20.0x)
- Reset view functionality
- Stage configuration:
  - Number of blades (3-100)
  - Stage height (10-1000 mm)
- Display options:
  - Show/hide grid
  - Show/hide coordinate axes
- Circular blade arrangement
- Solid rendering with wireframe overlay
- Lighting and shading effects
- Surface normal calculation

#### OpenVOGEL Integration
- Flight conditions configuration:
  - Velocity (0-500 m/s)
  - Density (0.1-10 kg/m³)
  - Viscosity (0.00001-0.001 Pa·s)
  - Mach number (0-2)
- Solver settings:
  - Maximum iterations (10-10000)
  - Convergence criteria (0.0000001-0.1)
  - Unsteady flow analysis option
  - Viscous flow effects option
- Geometry export to OpenVOGEL XML format
- OpenVOGEL installation path configuration
- Solver execution interface
- Results import from XML/TXT files
- Results display in rich text format
- Results export to text files
- Status tracking with color-coded feedback
- Simulated analysis results display

#### User Interface
- Tab-based interface with three main sections
- Responsive layout with docked panels
- Control panels with grouped parameters
- Real-time parameter adjustment
- NumericUpDown controls with validation
- Button-triggered calculations
- Progress feedback and status messages

#### Documentation
- Comprehensive README.md with features overview
- Detailed INSTALLATION.md guide
- Complete USER_GUIDE.md with:
  - Interface overview
  - Parameter descriptions
  - Workflow examples
  - Tips and best practices
  - Troubleshooting section
- ARCHITECTURE.md with technical details
- CONTRIBUTING.md with contribution guidelines
- LICENSE file (MIT License)
- This CHANGELOG

#### Project Structure
- Visual Studio 2022 solution
- .NET 8.0 Windows Forms application
- Modular panel-based architecture
- Proper resource management and disposal
- .gitignore for Visual Studio projects

### Technical Details

#### Dependencies
- .NET 8.0 SDK
- OpenTK 4.8.2 (OpenGL bindings)
- OpenTK.GLControl 3.3.3 (Windows Forms integration)

#### System Requirements
- Windows 10 or later
- .NET 8.0 Runtime
- OpenGL 3.3 compatible graphics card
- Minimum 4 GB RAM

#### File Formats
- Project files: .sln, .csproj (MSBuild)
- Source files: .cs (C#)
- Export format: .xml (OpenVOGEL compatible)
- Results format: .txt, .xml

### Known Limitations

#### Current Version
- 2D and 3D designs are independent (no automatic transfer)
- OpenVOGEL integration requires manual installation
- No data persistence (save/load projects)
- Limited to NACA 4-digit profiles
- Single stage analysis only
- No CFD mesh export
- Manual result interpretation required

#### Platform
- Windows only (due to Windows Forms dependency)
- Cannot build on Linux/macOS without modifications

### Future Enhancements

Planned for future releases:
- Project save/load functionality
- Custom airfoil import (CSV, DAT files)
- Advanced blade profiles (NACA 5-digit, custom)
- Multi-stage analysis
- Non-linear flowpath shapes
- Direct OpenVOGEL API integration
- CFD mesh export (ANSYS, OpenFOAM)
- Performance map generation
- Off-design analysis
- Optimization algorithms
- Database for design library
- Cloud-based solver execution

---

## Version History Summary

- **1.0.0** (2024-11-03) - Initial release with core features

---

## Notes

### Versioning
- Major version: Breaking changes
- Minor version: New features (backwards compatible)
- Patch version: Bug fixes

### Contributing
See CONTRIBUTING.md for guidelines on how to contribute to this project.

### Support
For issues and questions, please use GitHub Issues.
