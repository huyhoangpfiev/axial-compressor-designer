# Project Architecture

## Overview

The Axial Compressor Designer is built using a modular architecture with clear separation of concerns. Each major feature is encapsulated in its own panel component.

## Architecture Diagram

```
┌─────────────────────────────────────────────────────────┐
│                     MainForm.cs                          │
│                  (Main Application Window)               │
│                                                           │
│  ┌────────────────────────────────────────────────────┐  │
│  │            TabControl                               │  │
│  │                                                      │  │
│  │  ┌──────────────────────────────────────────────┐  │  │
│  │  │   Tab 1: BladeProfile2DPanel.cs              │  │  │
│  │  │   - 2D Drawing Environment                   │  │  │
│  │  │   - Blade Profile Calculation (NACA)         │  │  │
│  │  │   - Flowpath Calculation                     │  │  │
│  │  │   - Tandem Blade Support                     │  │  │
│  │  └──────────────────────────────────────────────┘  │  │
│  │                                                      │  │
│  │  ┌──────────────────────────────────────────────┐  │  │
│  │  │   Tab 2: Blade3DViewerPanel.cs               │  │  │
│  │  │   - OpenGL/OpenTK 3D Rendering               │  │  │
│  │  │   - Interactive Rotation & Zoom              │  │  │
│  │  │   - Stage Configuration                      │  │  │
│  │  │   - Blade Geometry Generation                │  │  │
│  │  └──────────────────────────────────────────────┘  │  │
│  │                                                      │  │
│  │  ┌──────────────────────────────────────────────┐  │  │
│  │  │   Tab 3: OpenVOGELIntegrationPanel.cs        │  │  │
│  │  │   - Flight Conditions Setup                  │  │  │
│  │  │   - Solver Configuration                     │  │  │
│  │  │   - Geometry Export (XML)                    │  │  │
│  │  │   - Results Import & Visualization           │  │  │
│  │  └──────────────────────────────────────────────┘  │  │
│  └────────────────────────────────────────────────────┘  │
└─────────────────────────────────────────────────────────┘
```

## Component Details

### 1. Program.cs
**Responsibility**: Application entry point

**Key Functions**:
- Initialize Windows Forms application
- Set up application configuration
- Create and run MainForm

**Code Structure**:
```csharp
static void Main()
{
    ApplicationConfiguration.Initialize();
    Application.Run(new MainForm());
}
```

### 2. MainForm.cs
**Responsibility**: Main application window and tab management

**Key Features**:
- Tab-based interface
- Panel initialization and lifecycle management
- Cross-panel communication (future enhancement)

**Properties**:
- `mainTabControl`: TabControl containing all feature tabs
- `bladeProfile2DPanel`: Reference to 2D design panel
- `blade3DViewerPanel`: Reference to 3D visualization panel
- `openVOGELPanel`: Reference to solver integration panel

### 3. BladeProfile2DPanel.cs
**Responsibility**: 2D blade profile design and flowpath calculation

**Key Features**:
- **Blade Parameter Controls**:
  - Chord length (NumericUpDown)
  - Stagger angle (NumericUpDown)
  - Max thickness (NumericUpDown)
  - Camber angle (NumericUpDown)

- **Tandem Blade Configuration**:
  - Enable/disable checkbox
  - Gap ratio control
  - Second chord ratio control

- **Flowpath Configuration**:
  - Inlet radius
  - Outlet radius
  - Axial length

- **Calculation Engine**:
  - NACA 4-digit airfoil algorithm
  - Thickness distribution calculation
  - Camber line generation
  - Coordinate transformation

- **Visualization**:
  - GDI+ based 2D drawing
  - Real-time rendering
  - Coordinate transformation for display

**Key Methods**:
```csharp
void CalculateBladeProfile()     // Generate blade coordinates
void CalculateFlowpath()         // Generate flowpath points
void DrawingPanel_Paint()        // Render 2D graphics
```

**Data Structures**:
```csharp
List<PointF> bladeProfile1Points  // First blade coordinates
List<PointF> bladeProfile2Points  // Second blade (tandem)
List<PointF> flowpathInnerPoints  // Inner flowpath boundary
List<PointF> flowpathOuterPoints  // Outer flowpath boundary
```

### 4. Blade3DViewerPanel.cs
**Responsibility**: 3D visualization using OpenGL

**Key Features**:
- **OpenGL Context Management**:
  - GLControl initialization
  - OpenGL state setup
  - Lighting configuration

- **View Controls**:
  - Rotation X, Y, Z (TrackBars)
  - Zoom control
  - Reset view functionality

- **Stage Configuration**:
  - Number of blades
  - Stage height
  - 3D model generation

- **Rendering Pipeline**:
  - Vertex generation
  - Triangle mesh creation
  - Normal calculation
  - Lighting and shading

- **Display Options**:
  - Grid display
  - Axes display
  - Wireframe overlay

**Key Methods**:
```csharp
void GlControl_Load()           // Initialize OpenGL
void GlControl_Paint()          // Render frame
void GenerateSimpleBlade()      // Create blade geometry
void DrawBlade()                // Render blade mesh
Vector3 CalculateNormal()       // Calculate surface normals
```

**Data Structures**:
```csharp
List<Vector3> bladeVertices     // 3D vertex positions
List<int> bladeIndices          // Triangle indices
struct Vector3                  // 3D coordinate structure
```

**OpenGL Features Used**:
- Depth testing
- Lighting (Light0)
- Color material
- Matrix transformations
- Perspective projection

### 5. OpenVOGELIntegrationPanel.cs
**Responsibility**: Integration with OpenVOGEL aerodynamic solver

**Key Features**:
- **Flight Conditions**:
  - Velocity (m/s)
  - Density (kg/m³)
  - Viscosity (Pa·s)
  - Mach number

- **Solver Settings**:
  - Max iterations
  - Convergence criteria
  - Unsteady flow toggle
  - Viscous flow toggle

- **File Operations**:
  - Geometry export (XML format)
  - Results import
  - Results export

- **Status Management**:
  - Real-time status updates
  - Color-coded feedback
  - Result logging

**Key Methods**:
```csharp
void BtnExportGeometry_Click()   // Export to OpenVOGEL format
void BtnRunSolver_Click()        // Execute solver
void BtnImportResults_Click()    // Load results
void UpdateStatus()              // Update status display
void AppendResultText()          // Log results
```

**File Formats**:
- Export: XML (OpenVOGEL compatible)
- Import: XML/TXT (results)
- Export: TXT (analysis results)

## Data Flow

### 2D to 3D Flow
```
User Input (2D Parameters)
    ↓
CalculateBladeProfile()
    ↓
bladeProfile1Points, bladeProfile2Points
    ↓
[User switches to 3D tab]
    ↓
GenerateSimpleBlade() (uses similar parameters)
    ↓
bladeVertices, bladeIndices
    ↓
DrawBlade() → OpenGL Rendering
```

### Export to OpenVOGEL Flow
```
2D Design + Parameters
    ↓
User clicks "Export Geometry"
    ↓
Create XML file with:
  - Geometry data
  - Flight conditions
  - Solver settings
    ↓
Save to Export directory
    ↓
[User runs OpenVOGEL externally]
    ↓
Import results back to application
    ↓
Display in results panel
```

## Technology Stack

### Core Framework
- **.NET 8.0**: Modern .NET platform
- **Windows Forms**: UI framework
- **C# 12**: Programming language

### Graphics Libraries
- **System.Drawing**: 2D graphics (GDI+)
- **OpenTK 4.8.2**: OpenGL bindings for 3D
- **OpenTK.GLControl 3.3.3**: WinForms OpenGL control

### File I/O
- **System.IO**: File operations
- **System.Xml**: XML parsing (future enhancement)

## Design Patterns Used

### 1. Component Pattern
Each panel is a self-contained UserControl:
- Encapsulates its own UI
- Manages its own state
- Independent lifecycle

### 2. Event-Driven Architecture
UI events trigger calculations and updates:
- Button clicks → Calculations
- Value changes → Parameter updates
- Paint events → Rendering

### 3. Separation of Concerns
Clear boundaries between:
- UI layer (Controls and panels)
- Business logic (Calculations)
- Rendering (Drawing methods)

## Extension Points

### Adding New Features

1. **New Blade Profiles**:
   - Extend `CalculateBladeProfile()` in BladeProfile2DPanel
   - Add new profile type selector
   - Implement profile-specific mathematics

2. **Additional 3D Views**:
   - Create new rendering methods in Blade3DViewerPanel
   - Add view mode selector
   - Implement camera presets

3. **Advanced Solver Integration**:
   - Extend OpenVOGELIntegrationPanel
   - Add direct API calls to OpenVOGEL
   - Implement result parsing

4. **Data Persistence**:
   - Add serialization to each panel
   - Implement save/load functionality
   - Create project file format

## Performance Considerations

### 2D Rendering
- Calculated points cached in Lists
- Redrawn only on Invalidate()
- Efficient coordinate transformation

### 3D Rendering
- Vertex buffer objects (future enhancement)
- Efficient triangle indexing
- Minimized OpenGL state changes

### Memory Management
- Proper disposal of resources
- Event handler cleanup
- Graphics object disposal

## Testing Strategy

### Unit Testing (Future)
- Blade calculation algorithms
- Coordinate transformations
- File I/O operations

### Integration Testing (Future)
- Panel initialization
- Cross-panel data flow
- OpenVOGEL integration

### UI Testing
- Manual testing required
- Visual inspection of:
  - 2D profiles
  - 3D models
  - User interactions

## Security Considerations

### File Operations
- Validate file paths
- Handle I/O exceptions
- Sandbox export directory

### External Process Execution
- Validate OpenVOGEL path
- Safe process launching
- Error handling

### Input Validation
- NumericUpDown controls provide range validation
- Additional validation in calculation methods
- Exception handling for edge cases

## Future Architecture Enhancements

### Suggested Improvements

1. **Model-View-ViewModel (MVVM)**:
   - Separate data models from UI
   - Enable better testability
   - Facilitate data binding

2. **Plugin Architecture**:
   - Load profile generators dynamically
   - Support custom solvers
   - Extensible visualization

3. **Multi-threading**:
   - Async solver execution
   - Background calculation
   - Responsive UI

4. **Database Integration**:
   - Store design history
   - Manage design library
   - Performance tracking

5. **Cloud Integration**:
   - Remote solver execution
   - Collaborative design
   - Version control

## Build Configuration

### Debug Build
- Full symbols
- No optimization
- Debug assertions enabled

### Release Build
- Optimizations enabled
- No debug symbols
- Smaller binary size

## Dependencies Management

All dependencies managed via NuGet:
```xml
<PackageReference Include="OpenTK" Version="4.8.2" />
<PackageReference Include="OpenTK.GLControl" Version="3.3.3" />
```

## Deployment

### Standalone Deployment
- Self-contained: Include .NET runtime
- Framework-dependent: Require .NET 8.0 installed

### Installation
- No installer required for development
- Can create installer with WiX or NSIS

## Documentation

### Code Documentation
- XML comments on public methods
- Clear variable names
- Inline comments for complex algorithms

### User Documentation
- README.md: Overview and features
- INSTALLATION.md: Setup instructions
- USER_GUIDE.md: Detailed usage guide
- ARCHITECTURE.md: This file

## Version Control

### Git Strategy
- Main branch for stable releases
- Feature branches for development
- Clear commit messages

### File Organization
```
/
├── AxialCompressorDesigner/      # Source code
├── .gitignore                     # Git ignore rules
├── *.md                           # Documentation
└── AxialCompressorDesigner.sln   # Solution file
```

## Maintenance

### Code Quality
- Follow C# coding conventions
- Keep methods focused and small
- Regular refactoring

### Performance Monitoring
- Profile 3D rendering
- Monitor memory usage
- Optimize hot paths

### Bug Tracking
- Use GitHub Issues
- Reproduce before fixing
- Test fixes thoroughly

---

**Document Version**: 1.0
**Last Updated**: 2024
**Author**: Axial Compressor Designer Development Team
