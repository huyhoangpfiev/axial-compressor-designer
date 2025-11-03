# Contributing to Axial Compressor Designer

Thank you for your interest in contributing to the Axial Compressor Designer project! This document provides guidelines and information for contributors.

## Table of Contents
1. [Code of Conduct](#code-of-conduct)
2. [Getting Started](#getting-started)
3. [How to Contribute](#how-to-contribute)
4. [Development Process](#development-process)
5. [Coding Standards](#coding-standards)
6. [Testing Guidelines](#testing-guidelines)
7. [Documentation](#documentation)
8. [Pull Request Process](#pull-request-process)

## Code of Conduct

### Our Pledge
We are committed to providing a welcoming and inspiring community for everyone. Please be respectful and constructive in all interactions.

### Expected Behavior
- Use welcoming and inclusive language
- Be respectful of differing viewpoints
- Accept constructive criticism gracefully
- Focus on what is best for the community
- Show empathy towards other community members

## Getting Started

### Prerequisites
1. Windows 10 or later
2. Visual Studio 2022 or later
3. .NET 8.0 SDK
4. Git for version control
5. GitHub account

### Setup Development Environment
1. Fork the repository on GitHub
2. Clone your fork locally:
   ```bash
   git clone https://github.com/YOUR-USERNAME/axial-compressor-designer.git
   cd axial-compressor-designer
   ```
3. Add upstream remote:
   ```bash
   git remote add upstream https://github.com/huyhoangpfiev/axial-compressor-designer.git
   ```
4. Open `AxialCompressorDesigner.sln` in Visual Studio
5. Restore NuGet packages
6. Build the solution to ensure everything works

## How to Contribute

### Types of Contributions

We welcome various types of contributions:

1. **Bug Reports**
   - Use GitHub Issues
   - Provide clear description
   - Include steps to reproduce
   - Add screenshots if applicable

2. **Feature Requests**
   - Describe the feature clearly
   - Explain the use case
   - Discuss potential implementation

3. **Code Contributions**
   - Bug fixes
   - New features
   - Performance improvements
   - Code refactoring

4. **Documentation**
   - Fix typos or errors
   - Improve clarity
   - Add examples
   - Create tutorials

5. **Testing**
   - Write unit tests
   - Perform manual testing
   - Report test results

## Development Process

### Branch Strategy

- `main`: Stable, production-ready code
- `develop`: Integration branch for features
- `feature/*`: New features
- `bugfix/*`: Bug fixes
- `hotfix/*`: Urgent fixes for production

### Workflow

1. **Update your local repository**:
   ```bash
   git checkout main
   git pull upstream main
   ```

2. **Create a feature branch**:
   ```bash
   git checkout -b feature/your-feature-name
   ```

3. **Make your changes**:
   - Write code
   - Add tests
   - Update documentation

4. **Commit your changes**:
   ```bash
   git add .
   git commit -m "Add feature: description"
   ```

5. **Push to your fork**:
   ```bash
   git push origin feature/your-feature-name
   ```

6. **Create a Pull Request** on GitHub

## Coding Standards

### C# Style Guide

Follow Microsoft's C# coding conventions:

#### Naming Conventions
```csharp
// PascalCase for classes, methods, properties
public class BladeProfile { }
public void CalculateProfile() { }
public int ChordLength { get; set; }

// camelCase for local variables and parameters
int bladeCount = 10;
void SetAngle(double staggerAngle) { }

// _camelCase for private fields (optional)
private int _maxIterations;

// UPPER_CASE for constants
const double PI = 3.14159;
```

#### Code Organization
```csharp
// Order: fields, constructors, properties, methods
public class MyClass
{
    // Private fields
    private int _value;
    
    // Constructors
    public MyClass() { }
    
    // Properties
    public int Value { get; set; }
    
    // Public methods
    public void DoSomething() { }
    
    // Private methods
    private void HelperMethod() { }
}
```

#### Comments
```csharp
/// <summary>
/// XML documentation for public APIs
/// </summary>
/// <param name="angle">Description of parameter</param>
/// <returns>Description of return value</returns>
public double Calculate(double angle)
{
    // Inline comments for complex logic
    double result = Math.Sin(angle);
    return result;
}
```

### Best Practices

1. **Keep methods small and focused**
   - Each method should do one thing
   - Maximum ~50 lines per method
   - Extract complex logic into separate methods

2. **Use meaningful names**
   - Variables: `bladeCount` not `bc`
   - Methods: `CalculateBladeProfile()` not `Calc()`
   - Classes: `BladeProfile2DPanel` not `Panel2`

3. **Error handling**
   ```csharp
   try
   {
       // Code that might throw
   }
   catch (SpecificException ex)
   {
       // Handle specific exception
       LogError(ex);
   }
   catch (Exception ex)
   {
       // Handle general exception
       LogError(ex);
       throw; // Re-throw if needed
   }
   ```

4. **Resource management**
   ```csharp
   // Use 'using' for IDisposable
   using (var stream = File.OpenRead(path))
   {
       // Use stream
   }
   
   // Or dispose explicitly
   protected override void Dispose(bool disposing)
   {
       if (disposing)
       {
           // Dispose managed resources
       }
       base.Dispose(disposing);
   }
   ```

5. **Avoid magic numbers**
   ```csharp
   // Bad
   if (angle > 45) { }
   
   // Good
   const double MaxAngle = 45.0;
   if (angle > MaxAngle) { }
   ```

## Testing Guidelines

### Unit Tests
When adding unit tests (future):
```csharp
[TestClass]
public class BladeProfileTests
{
    [TestMethod]
    public void CalculateProfile_ValidParameters_ReturnsPoints()
    {
        // Arrange
        var calculator = new BladeProfileCalculator();
        
        // Act
        var points = calculator.CalculateProfile(100, 30, 10, 20);
        
        // Assert
        Assert.IsNotNull(points);
        Assert.IsTrue(points.Count > 0);
    }
}
```

### Manual Testing
For UI changes:
1. Test all affected features
2. Test edge cases
3. Test on different screen resolutions
4. Document test results in PR

## Documentation

### Code Documentation
- Add XML comments to all public APIs
- Document complex algorithms
- Explain non-obvious code

### User Documentation
When adding features:
1. Update README.md if it affects main features
2. Update USER_GUIDE.md with usage instructions
3. Add examples where appropriate
4. Update ARCHITECTURE.md if architecture changes

### Commit Messages
Follow conventional commits:
```
<type>(<scope>): <subject>

<body>

<footer>
```

Types:
- `feat`: New feature
- `fix`: Bug fix
- `docs`: Documentation only
- `style`: Code style changes
- `refactor`: Code refactoring
- `test`: Adding tests
- `chore`: Maintenance tasks

Examples:
```
feat(2d): Add support for custom airfoil import

Added ability to import custom airfoil coordinates from CSV files.
Users can now use their own airfoil data instead of generated NACA profiles.

Closes #123
```

```
fix(3d): Correct normal calculation for blade surfaces

Fixed inverted normals causing incorrect lighting in 3D view.
```

## Pull Request Process

### Before Submitting
1. ✅ Code compiles without errors
2. ✅ All tests pass (when available)
3. ✅ Code follows style guidelines
4. ✅ Documentation is updated
5. ✅ Commit messages are clear
6. ✅ Branch is up to date with main

### PR Description Template
```markdown
## Description
Brief description of changes

## Type of Change
- [ ] Bug fix
- [ ] New feature
- [ ] Breaking change
- [ ] Documentation update

## Testing
Describe testing performed

## Screenshots (if applicable)
Add screenshots for UI changes

## Checklist
- [ ] Code follows style guidelines
- [ ] Self-review completed
- [ ] Documentation updated
- [ ] No new warnings
```

### Review Process
1. Automated checks run (if configured)
2. Maintainers review code
3. Feedback is provided
4. You make requested changes
5. PR is approved and merged

### After Merge
1. Delete your feature branch
2. Update your local repository
3. Celebrate your contribution! 🎉

## Issue Guidelines

### Bug Reports
Use this template:
```markdown
## Bug Description
Clear description of the bug

## Steps to Reproduce
1. Step 1
2. Step 2
3. ...

## Expected Behavior
What should happen

## Actual Behavior
What actually happens

## Environment
- Windows Version:
- .NET Version:
- Application Version:

## Screenshots
Add if applicable

## Additional Context
Any other relevant information
```

### Feature Requests
Use this template:
```markdown
## Feature Description
Clear description of proposed feature

## Use Case
Why is this feature needed?

## Proposed Solution
How might this be implemented?

## Alternatives Considered
Other approaches you've thought about

## Additional Context
Any other relevant information
```

## Getting Help

### Communication Channels
- GitHub Issues: Bug reports and feature requests
- GitHub Discussions: General questions and ideas
- Pull Request comments: Code-specific discussions

### Questions?
- Check existing issues and PRs
- Review documentation
- Ask in GitHub Discussions

## Recognition

Contributors will be:
- Listed in CONTRIBUTORS.md
- Mentioned in release notes
- Credited in commits (Co-authored-by)

## License

By contributing, you agree that your contributions will be licensed under the MIT License.

---

Thank you for contributing to Axial Compressor Designer! Your efforts help make this tool better for everyone.
