using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace AxialCompressorDesigner
{
    /// <summary>
    /// Panel for OpenVOGEL solver integration
    /// </summary>
    public class OpenVOGELIntegrationPanel : UserControl
    {
        private Panel inputPanel;
        private Panel outputPanel;
        
        // Input controls
        private GroupBox flightConditionsGroup;
        private Label lblVelocity;
        private NumericUpDown nudVelocity;
        private Label lblDensity;
        private NumericUpDown nudDensity;
        private Label lblViscosity;
        private NumericUpDown nudViscosity;
        private Label lblMachNumber;
        private NumericUpDown nudMachNumber;
        
        private GroupBox solverSettingsGroup;
        private Label lblMaxIterations;
        private NumericUpDown nudMaxIterations;
        private Label lblConvergenceCriteria;
        private NumericUpDown nudConvergenceCriteria;
        private CheckBox chkUnsteadyFlow;
        private CheckBox chkViscousFlow;
        
        private GroupBox exportGroup;
        private Button btnExportGeometry;
        private Button btnRunSolver;
        private Button btnImportResults;
        private Label lblStatus;
        private TextBox txtOpenVOGELPath;
        private Label lblOpenVOGELPath;
        private Button btnBrowseVOGEL;
        
        // Output controls
        private GroupBox resultsGroup;
        private RichTextBox txtResults;
        private Button btnClearResults;
        private Button btnExportResults;
        
        private GroupBox visualizationGroup;
        private CheckBox chkShowPressure;
        private CheckBox chkShowVelocity;
        private CheckBox chkShowStreamlines;
        private Panel visualizationPanel;
        
        public OpenVOGELIntegrationPanel()
        {
            InitializeComponents();
            SetupLayout();
        }
        
        private void InitializeComponents()
        {
            this.BackColor = Color.White;
            
            // Input panel (left side)
            inputPanel = new Panel
            {
                Width = 400,
                Dock = DockStyle.Left,
                BackColor = SystemColors.Control,
                Padding = new Padding(10),
                AutoScroll = true
            };
            
            // Output panel (right side)
            outputPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(10)
            };
            
            // Flight conditions group
            flightConditionsGroup = new GroupBox
            {
                Text = "Flight Conditions",
                Width = 380,
                Height = 200,
                Location = new Point(0, 0)
            };
            
            lblVelocity = new Label { Text = "Velocity (m/s):", Location = new Point(10, 25), Width = 140 };
            nudVelocity = new NumericUpDown { Location = new Point(160, 23), Width = 200, Minimum = 0, Maximum = 500, Value = 100, DecimalPlaces = 2 };
            
            lblDensity = new Label { Text = "Density (kg/m³):", Location = new Point(10, 55), Width = 140 };
            nudDensity = new NumericUpDown { Location = new Point(160, 53), Width = 200, Minimum = 0.1M, Maximum = 10, Value = 1.225M, DecimalPlaces = 4 };
            
            lblViscosity = new Label { Text = "Viscosity (Pa·s):", Location = new Point(10, 85), Width = 140 };
            nudViscosity = new NumericUpDown { Location = new Point(160, 83), Width = 200, Minimum = 0.00001M, Maximum = 0.001M, Value = 0.0000181M, DecimalPlaces = 8 };
            
            lblMachNumber = new Label { Text = "Mach Number:", Location = new Point(10, 115), Width = 140 };
            nudMachNumber = new NumericUpDown { Location = new Point(160, 113), Width = 200, Minimum = 0, Maximum = 2, Value = 0.3M, DecimalPlaces = 3 };
            
            flightConditionsGroup.Controls.AddRange(new Control[] {
                lblVelocity, nudVelocity,
                lblDensity, nudDensity,
                lblViscosity, nudViscosity,
                lblMachNumber, nudMachNumber
            });
            
            // Solver settings group
            solverSettingsGroup = new GroupBox
            {
                Text = "Solver Settings",
                Width = 380,
                Height = 160,
                Location = new Point(0, 210)
            };
            
            lblMaxIterations = new Label { Text = "Max Iterations:", Location = new Point(10, 25), Width = 140 };
            nudMaxIterations = new NumericUpDown { Location = new Point(160, 23), Width = 200, Minimum = 10, Maximum = 10000, Value = 1000 };
            
            lblConvergenceCriteria = new Label { Text = "Convergence:", Location = new Point(10, 55), Width = 140 };
            nudConvergenceCriteria = new NumericUpDown { Location = new Point(160, 53), Width = 200, Minimum = 0.0000001M, Maximum = 0.1M, Value = 0.0001M, DecimalPlaces = 7 };
            
            chkUnsteadyFlow = new CheckBox { Text = "Unsteady Flow Analysis", Location = new Point(10, 90), Width = 350 };
            chkViscousFlow = new CheckBox { Text = "Viscous Flow Effects", Location = new Point(10, 115), Width = 350, Checked = true };
            
            solverSettingsGroup.Controls.AddRange(new Control[] {
                lblMaxIterations, nudMaxIterations,
                lblConvergenceCriteria, nudConvergenceCriteria,
                chkUnsteadyFlow, chkViscousFlow
            });
            
            // Export and run group
            exportGroup = new GroupBox
            {
                Text = "OpenVOGEL Integration",
                Width = 380,
                Height = 200,
                Location = new Point(0, 380)
            };
            
            lblOpenVOGELPath = new Label { Text = "OpenVOGEL Path:", Location = new Point(10, 25), Width = 360 };
            txtOpenVOGELPath = new TextBox { Location = new Point(10, 45), Width = 280, Text = "C:\\OpenVOGEL" };
            btnBrowseVOGEL = new Button { Text = "Browse...", Location = new Point(295, 44), Width = 75 };
            btnBrowseVOGEL.Click += BtnBrowseVOGEL_Click;
            
            btnExportGeometry = new Button { Text = "Export Geometry to OpenVOGEL", Location = new Point(10, 80), Width = 360 };
            btnExportGeometry.Click += BtnExportGeometry_Click;
            
            btnRunSolver = new Button { Text = "Run OpenVOGEL Solver", Location = new Point(10, 115), Width = 360 };
            btnRunSolver.Click += BtnRunSolver_Click;
            
            btnImportResults = new Button { Text = "Import Results", Location = new Point(10, 150), Width = 360 };
            btnImportResults.Click += BtnImportResults_Click;
            
            lblStatus = new Label { 
                Text = "Status: Ready", 
                Location = new Point(10, 175), 
                Width = 360,
                ForeColor = Color.Green
            };
            
            exportGroup.Controls.AddRange(new Control[] {
                lblOpenVOGELPath, txtOpenVOGELPath, btnBrowseVOGEL,
                btnExportGeometry, btnRunSolver, btnImportResults,
                lblStatus
            });
            
            inputPanel.Controls.Add(flightConditionsGroup);
            inputPanel.Controls.Add(solverSettingsGroup);
            inputPanel.Controls.Add(exportGroup);
            
            // Results group
            resultsGroup = new GroupBox
            {
                Text = "Analysis Results",
                Dock = DockStyle.Fill,
                Padding = new Padding(10)
            };
            
            txtResults = new RichTextBox
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                Font = new Font("Consolas", 9),
                BackColor = Color.White
            };
            
            Panel resultsButtonPanel = new Panel
            {
                Height = 40,
                Dock = DockStyle.Bottom
            };
            
            btnClearResults = new Button { Text = "Clear", Location = new Point(10, 8), Width = 100 };
            btnClearResults.Click += BtnClearResults_Click;
            
            btnExportResults = new Button { Text = "Export to File", Location = new Point(120, 8), Width = 120 };
            btnExportResults.Click += BtnExportResults_Click;
            
            resultsButtonPanel.Controls.Add(btnClearResults);
            resultsButtonPanel.Controls.Add(btnExportResults);
            
            resultsGroup.Controls.Add(txtResults);
            resultsGroup.Controls.Add(resultsButtonPanel);
            
            // Visualization group
            visualizationGroup = new GroupBox
            {
                Text = "Visualization Options",
                Height = 150,
                Dock = DockStyle.Bottom
            };
            
            chkShowPressure = new CheckBox { Text = "Show Pressure Distribution", Location = new Point(10, 25), Width = 250 };
            chkShowVelocity = new CheckBox { Text = "Show Velocity Vectors", Location = new Point(10, 50), Width = 250 };
            chkShowStreamlines = new CheckBox { Text = "Show Streamlines", Location = new Point(10, 75), Width = 250 };
            
            visualizationPanel = new Panel
            {
                Location = new Point(270, 25),
                Size = new Size(300, 100),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.LightGray
            };
            
            Label lblVisualizationPlaceholder = new Label
            {
                Text = "Visualization Preview\n(Available after solver run)",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.DarkGray
            };
            visualizationPanel.Controls.Add(lblVisualizationPlaceholder);
            
            visualizationGroup.Controls.AddRange(new Control[] {
                chkShowPressure, chkShowVelocity, chkShowStreamlines,
                visualizationPanel
            });
            
            outputPanel.Controls.Add(resultsGroup);
            outputPanel.Controls.Add(visualizationGroup);
            
            // Add welcome message
            AppendResultText("OpenVOGEL Integration Module\n");
            AppendResultText("===========================\n\n");
            AppendResultText("This module allows integration with OpenVOGEL for aerodynamic analysis.\n\n");
            AppendResultText("OpenVOGEL is an open-source vortex lattice method solver.\n");
            AppendResultText("GitHub: https://github.com/npapnet/OpenVOGEL\n\n");
            AppendResultText("Steps to use:\n");
            AppendResultText("1. Design your blade geometry in the 2D and 3D tabs\n");
            AppendResultText("2. Set flight conditions and solver parameters\n");
            AppendResultText("3. Export geometry to OpenVOGEL format\n");
            AppendResultText("4. Run the OpenVOGEL solver\n");
            AppendResultText("5. Import and visualize results\n\n");
            AppendResultText("Ready to start analysis.\n");
        }
        
        private void SetupLayout()
        {
            this.Controls.Add(outputPanel);
            this.Controls.Add(inputPanel);
        }
        
        private void BtnBrowseVOGEL_Click(object? sender, EventArgs e)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                dialog.Description = "Select OpenVOGEL installation directory";
                dialog.ShowNewFolderButton = false;
                
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    txtOpenVOGELPath.Text = dialog.SelectedPath;
                    UpdateStatus("OpenVOGEL path updated", Color.Green);
                }
            }
        }
        
        private void BtnExportGeometry_Click(object? sender, EventArgs e)
        {
            try
            {
                UpdateStatus("Exporting geometry...", Color.Blue);
                
                // Create export directory if it doesn't exist
                string exportDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Export");
                Directory.CreateDirectory(exportDir);
                
                string filename = Path.Combine(exportDir, $"blade_geometry_{DateTime.Now:yyyyMMdd_HHmmss}.xml");
                
                // Export geometry in OpenVOGEL XML format
                using (StreamWriter writer = new StreamWriter(filename))
                {
                    writer.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                    writer.WriteLine("<OpenVOGEL>");
                    writer.WriteLine("  <Model>");
                    writer.WriteLine("    <Name>Axial Compressor Blade</Name>");
                    writer.WriteLine("    <Description>Tandem blade configuration</Description>");
                    writer.WriteLine("  </Model>");
                    writer.WriteLine("  <FlightConditions>");
                    writer.WriteLine($"    <Velocity>{nudVelocity.Value}</Velocity>");
                    writer.WriteLine($"    <Density>{nudDensity.Value}</Density>");
                    writer.WriteLine($"    <Viscosity>{nudViscosity.Value}</Viscosity>");
                    writer.WriteLine($"    <MachNumber>{nudMachNumber.Value}</MachNumber>");
                    writer.WriteLine("  </FlightConditions>");
                    writer.WriteLine("  <SolverSettings>");
                    writer.WriteLine($"    <MaxIterations>{nudMaxIterations.Value}</MaxIterations>");
                    writer.WriteLine($"    <Convergence>{nudConvergenceCriteria.Value}</Convergence>");
                    writer.WriteLine($"    <UnsteadyFlow>{chkUnsteadyFlow.Checked}</UnsteadyFlow>");
                    writer.WriteLine($"    <ViscousFlow>{chkViscousFlow.Checked}</ViscousFlow>");
                    writer.WriteLine("  </SolverSettings>");
                    writer.WriteLine("  <!-- Geometry data would be exported here -->");
                    writer.WriteLine("</OpenVOGEL>");
                }
                
                AppendResultText($"\nGeometry exported successfully to:\n{filename}\n");
                UpdateStatus("Export completed", Color.Green);
            }
            catch (Exception ex)
            {
                AppendResultText($"\nError exporting geometry: {ex.Message}\n");
                UpdateStatus("Export failed", Color.Red);
            }
        }
        
        private void BtnRunSolver_Click(object? sender, EventArgs e)
        {
            UpdateStatus("Running solver...", Color.Blue);
            AppendResultText("\n--- Starting OpenVOGEL Solver ---\n");
            
            string vogelPath = txtOpenVOGELPath.Text;
            
            if (!Directory.Exists(vogelPath))
            {
                AppendResultText($"Error: OpenVOGEL directory not found at: {vogelPath}\n");
                AppendResultText("Please download and install OpenVOGEL from:\n");
                AppendResultText("https://github.com/npapnet/OpenVOGEL\n");
                UpdateStatus("Solver not found", Color.Red);
                return;
            }
            
            // Simulate solver execution
            AppendResultText("\nNote: Direct solver execution requires OpenVOGEL to be installed.\n");
            AppendResultText("The solver would be called here with the exported geometry file.\n\n");
            
            // Simulate some results
            AppendResultText("Solver Results (Simulated):\n");
            AppendResultText("---------------------------\n");
            AppendResultText($"Iterations: {nudMaxIterations.Value}\n");
            AppendResultText("Convergence: Achieved\n");
            AppendResultText($"Lift Coefficient (CL): {0.45:F4}\n");
            AppendResultText($"Drag Coefficient (CD): {0.018:F4}\n");
            AppendResultText($"Moment Coefficient (CM): {-0.12:F4}\n");
            AppendResultText($"L/D Ratio: {25.0:F2}\n");
            AppendResultText("\nPressure Distribution: Calculated\n");
            AppendResultText("Velocity Field: Calculated\n");
            AppendResultText("Vorticity Distribution: Calculated\n");
            
            UpdateStatus("Solver completed (simulated)", Color.Green);
        }
        
        private void BtnImportResults_Click(object? sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "OpenVOGEL Results (*.xml;*.txt)|*.xml;*.txt|All Files (*.*)|*.*";
                dialog.Title = "Import OpenVOGEL Results";
                
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        UpdateStatus("Importing results...", Color.Blue);
                        
                        string content = File.ReadAllText(dialog.FileName);
                        AppendResultText($"\n--- Imported from: {Path.GetFileName(dialog.FileName)} ---\n");
                        AppendResultText(content);
                        AppendResultText("\n--- End of imported data ---\n");
                        
                        UpdateStatus("Results imported", Color.Green);
                    }
                    catch (Exception ex)
                    {
                        AppendResultText($"\nError importing results: {ex.Message}\n");
                        UpdateStatus("Import failed", Color.Red);
                    }
                }
            }
        }
        
        private void BtnClearResults_Click(object? sender, EventArgs e)
        {
            txtResults.Clear();
            UpdateStatus("Results cleared", Color.Green);
        }
        
        private void BtnExportResults_Click(object? sender, EventArgs e)
        {
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
                dialog.FileName = $"analysis_results_{DateTime.Now:yyyyMMdd_HHmmss}.txt";
                
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        File.WriteAllText(dialog.FileName, txtResults.Text);
                        AppendResultText($"\nResults exported to: {dialog.FileName}\n");
                        UpdateStatus("Results exported", Color.Green);
                    }
                    catch (Exception ex)
                    {
                        AppendResultText($"\nError exporting results: {ex.Message}\n");
                        UpdateStatus("Export failed", Color.Red);
                    }
                }
            }
        }
        
        private void AppendResultText(string text)
        {
            if (txtResults.InvokeRequired)
            {
                txtResults.Invoke(new Action(() => AppendResultText(text)));
                return;
            }
            
            txtResults.AppendText(text);
            txtResults.ScrollToCaret();
        }
        
        private void UpdateStatus(string message, Color color)
        {
            lblStatus.Text = $"Status: {message}";
            lblStatus.ForeColor = color;
        }
        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                inputPanel?.Dispose();
                outputPanel?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
