using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace CarreraBarras
{
    public partial class Form1 : Form
    {
        // Variables para la carrera
        private Thread[] hilos;
        private int[] valoresActuales;
        private int[] valoresMeta;
        private bool[] carreraTerminada;
        private int posicionesCompletadas;
        private readonly Random random = new Random();
        private readonly object lockObject = new object();

        public Form1()
        {
            InitializeComponent();
            InicializarCarrera();
        }

        private void InicializarCarrera()
        {
            hilos = new Thread[3];
            valoresActuales = new int[3];
            valoresMeta = new int[3];
            carreraTerminada = new bool[3];

            for (int i = 0; i < 3; i++)
            {
                valoresActuales[i] = 0;
                valoresMeta[i] = 1000;
                carreraTerminada[i] = false;
                progressBars[i].Value = 0;
                labelsPorcentaje[i].Text = "0%";
                labelsPosicion[i].Text = "Esperando inicio...";
                labelsPosicion[i].ForeColor = Color.Gray;
            }
            posicionesCompletadas = 0;
            lblGanador.Text = "";
            listBoxResultados.Items.Clear();
        }

        private void BtnIniciar_Click(object sender, EventArgs e)
        {
            btnIniciar.Enabled = false;
            InicializarCarrera();

            for (int i = 0; i < 3; i++)
            {
                int barraIndex = i; // Importante: capturar el valor para el closure
                hilos[i] = new Thread(() => EjecutarBarra(barraIndex));
                hilos[i].Start();
            }
        }

        private void EjecutarBarra(int index)
        {
            try
            {
                while (valoresActuales[index] < valoresMeta[index])
                {
                    Thread.Sleep(100); // Esperar 100ms

                    lock (lockObject)
                    {
                        if (!carreraTerminada[index])
                        {
                            int avance = random.Next(10, 26); // Valor aleatorio entre 10 y 25
                            valoresActuales[index] = Math.Min(valoresActuales[index] + avance, valoresMeta[index]);
                            
                            // Actualizar UI
                            ActualizarBarra(index);

                            // Verificar si llegó a la meta
                            if (valoresActuales[index] >= valoresMeta[index] && !carreraTerminada[index])
                            {
                                carreraTerminada[index] = true;
                                posicionesCompletadas++;
                                ActualizarPosicion(index);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error en la barra {index + 1}: {ex.Message}");
            }
        }

        private void ActualizarBarra(int index)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<int>(ActualizarBarra), index);
                return;
            }

            progressBars[index].Value = valoresActuales[index];
            int porcentaje = (valoresActuales[index] * 100) / 1000;
            labelsPorcentaje[index].Text = $"{porcentaje}%";

            if (carreraTerminada[index])
            {
                labelsPosicion[index].ForeColor = GetColorPorPosicion(posicionesCompletadas);
            }
        }

        private void ActualizarPosicion(int index)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<int>(ActualizarPosicion), index);
                return;
            }

            string posicionTexto = "";
            Color colorTexto = Color.Black;

            switch (posicionesCompletadas)
            {
                case 1:
                    posicionTexto = "¡1er Lugar!";
                    colorTexto = Color.Gold;
                    break;
                case 2:
                    posicionTexto = "2do Lugar";
                    colorTexto = Color.Silver;
                    break;
                case 3:
                    posicionTexto = "3er Lugar";
                    colorTexto = Color.Brown;
                    break;
            }

            labelsPosicion[index].Text = posicionTexto;
            labelsPosicion[index].ForeColor = colorTexto;

            // Agregar al listBox de resultados
            string resultado = $"Barra {index + 1}: {posicionTexto} - {DateTime.Now:HH:mm:ss.fff}";
            listBoxResultados.Items.Add(resultado);

            // Si es el primer lugar, mostrar en el label principal
            if (posicionesCompletadas == 1)
            {
                lblGanador.Text = $"¡La Barra {index + 1} es la GANADORA!";
                lblGanador.ForeColor = Color.Green;
            }
            else if (posicionesCompletadas == 3)
            {
                lblGanador.Text += " - ¡Carrera Finalizada!";
                btnIniciar.Enabled = true;
            }
        }

        private Color GetColorPorPosicion(int posicion)
        {
            switch (posicion)
            {
                case 1: return Color.Gold;
                case 2: return Color.Silver;
                case 3: return Color.Brown;
                default: return Color.Gray;
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // Asegurar que todos los hilos se detengan al cerrar
            for (int i = 0; i < 3; i++)
            {
                if (hilos != null && hilos[i] != null && hilos[i].IsAlive)
                {
                    try
                    {
                        hilos[i].Abort();
                    }
                    catch { }
                }
            }
            base.OnFormClosing(e);
        }
    }
}