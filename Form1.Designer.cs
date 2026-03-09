namespace CarreraBarras
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.ProgressBar[] progressBars;
        private System.Windows.Forms.Label[] labelsPorcentaje;
        private System.Windows.Forms.Label[] labelsPosicion;
        private System.Windows.Forms.Button btnIniciar;
        private System.Windows.Forms.Label lblGanador;
        private System.Windows.Forms.ListBox listBoxResultados;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            
            // Inicializar arreglos
            progressBars = new System.Windows.Forms.ProgressBar[3];
            labelsPorcentaje = new System.Windows.Forms.Label[3];
            labelsPosicion = new System.Windows.Forms.Label[3];

            // Configuración del formulario
            this.Text = "Carrera de Barras de Progreso";
            this.Size = new System.Drawing.Size(600, 500);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

            // Crear controles para cada barra
            for (int i = 0; i < 3; i++)
            {
                // Label para número de barra
                System.Windows.Forms.Label lblBarra = new System.Windows.Forms.Label();
                lblBarra.Text = $"Barra {i + 1}:";
                lblBarra.Location = new System.Drawing.Point(20, 30 + i * 70);
                lblBarra.Size = new System.Drawing.Size(60, 20);

                // ProgressBar
                progressBars[i] = new System.Windows.Forms.ProgressBar();
                progressBars[i].Location = new System.Drawing.Point(90, 30 + i * 70);
                progressBars[i].Size = new System.Drawing.Size(400, 30);
                progressBars[i].Minimum = 0;
                progressBars[i].Maximum = 1000;

                // Label para porcentaje
                labelsPorcentaje[i] = new System.Windows.Forms.Label();
                labelsPorcentaje[i].Location = new System.Drawing.Point(500, 30 + i * 70);
                labelsPorcentaje[i].Size = new System.Drawing.Size(60, 20);
                labelsPorcentaje[i].Text = "0%";

                // Label para posición
                labelsPosicion[i] = new System.Windows.Forms.Label();
                labelsPosicion[i].Location = new System.Drawing.Point(20, 55 + i * 70);
                labelsPosicion[i].Size = new System.Drawing.Size(400, 20);
                labelsPosicion[i].Text = "Esperando inicio...";
                labelsPosicion[i].ForeColor = System.Drawing.Color.Gray;

                // Agregar controles al formulario
                this.Controls.Add(lblBarra);
                this.Controls.Add(progressBars[i]);
                this.Controls.Add(labelsPorcentaje[i]);
                this.Controls.Add(labelsPosicion[i]);
            }

            // Botón Iniciar
            this.btnIniciar = new System.Windows.Forms.Button();
            this.btnIniciar.Text = "Iniciar Carrera";
            this.btnIniciar.Location = new System.Drawing.Point(220, 250);
            this.btnIniciar.Size = new System.Drawing.Size(150, 40);
            this.btnIniciar.BackColor = System.Drawing.Color.LightGreen;
            this.btnIniciar.Click += new System.EventHandler(this.BtnIniciar_Click);
            this.Controls.Add(this.btnIniciar);

            // Label para ganador
            this.lblGanador = new System.Windows.Forms.Label();
            this.lblGanador.Location = new System.Drawing.Point(20, 310);
            this.lblGanador.Size = new System.Drawing.Size(540, 30);
            this.lblGanador.Font = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold);
            this.lblGanador.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Controls.Add(this.lblGanador);

            // ListBox para resultados
            this.listBoxResultados = new System.Windows.Forms.ListBox();
            this.listBoxResultados.Location = new System.Drawing.Point(20, 350);
            this.listBoxResultados.Size = new System.Drawing.Size(540, 100);
            this.Controls.Add(this.listBoxResultados);
        }
    }
}