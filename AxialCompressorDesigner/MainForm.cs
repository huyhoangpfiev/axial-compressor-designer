using System;
using System.Windows.Forms;

namespace AxialCompressorDesigner
{
    public partial class MainForm : Form
    {
        private TabControl mainTabControl;
        private TabPage tabPage2D;
        private TabPage tabPage3D;
        private TabPage tabPageSolver;

        private BladeProfile2DPanel bladeProfile2DPanel;
        private Blade3DViewerPanel blade3DViewerPanel;
        private OpenVOGELIntegrationPanel openVOGELPanel;

        public MainForm()
        {
            InitializeComponent();
            SetupUI();
        }

        private void InitializeComponent()
        {
            this.Text = "Axial Compressor Designer - Tandem Blade";
            this.Size = new System.Drawing.Size(1200, 800);
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void SetupUI()
        {
            // Create main tab control
            mainTabControl = new TabControl
            {
                Dock = DockStyle.Fill
            };

            // Create 2D design tab
            tabPage2D = new TabPage("2D Blade Profile & Flowpath");
            bladeProfile2DPanel = new BladeProfile2DPanel
            {
                Dock = DockStyle.Fill
            };
            tabPage2D.Controls.Add(bladeProfile2DPanel);

            // Create 3D visualization tab
            tabPage3D = new TabPage("3D Visualization");
            blade3DViewerPanel = new Blade3DViewerPanel
            {
                Dock = DockStyle.Fill
            };
            tabPage3D.Controls.Add(blade3DViewerPanel);

            // Create OpenVOGEL integration tab
            tabPageSolver = new TabPage("OpenVOGEL Solver");
            openVOGELPanel = new OpenVOGELIntegrationPanel
            {
                Dock = DockStyle.Fill
            };
            tabPageSolver.Controls.Add(openVOGELPanel);

            // Add tabs to main control
            mainTabControl.TabPages.Add(tabPage2D);
            mainTabControl.TabPages.Add(tabPage3D);
            mainTabControl.TabPages.Add(tabPageSolver);

            // Add main control to form
            this.Controls.Add(mainTabControl);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                bladeProfile2DPanel?.Dispose();
                blade3DViewerPanel?.Dispose();
                openVOGELPanel?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
