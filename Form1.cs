using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace EvaluadorLenguajes
{
    public partial class Form1 : Form
    {
        // Controles de la Interfaz
        private TextBox txtAlfabeto;
        private Label lblAlfabetoActual;
        private RadioButton rbReglaA;
        private RadioButton rbReglaB;
        private RadioButton rbReglaC;
        private TextBox txtCadena;
        private Label lblResultado;
        private NumericUpDown numLongitud;
        private NumericUpDown numCantidad;
        private TextBox txtGeneradas;

        // Estructura del Alfabeto
        private HashSet<char> alfabeto = new HashSet<char> { 'a', 'b' };

        public Form1()
        {
            // Inicializar componentes
            InitializeComponentesPersonalizados();
        }

        private void InitializeComponentesPersonalizados()
        {
            // --- PALETA DE COLORES ---
            Color colorFondo = Color.FromArgb(245, 247, 250);
            Color colorTarjetas = Color.White;
            Color colorTexto = Color.FromArgb(33, 37, 41);
            Color colorPrimario = Color.FromArgb(13, 110, 253);
            Color colorTextoBoton = Color.White;

            // --- CONFIGURACIÓN DE LA VENTANA ---
            this.Text = "Evaluador y Generador de Cadenas - Lenguajes Formales";
            this.Size = new Size(620, 670);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = colorFondo;
            this.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);

            // --- 1. SECCIÓN ALFABETO ---
            GroupBox gbAlfabeto = new GroupBox
            {
                Text = "1. Configuración del Alfabeto (Σ)",
                Location = new Point(20, 15),
                Size = new Size(560, 95),
                ForeColor = colorTexto,
                BackColor = colorTarjetas
            };
            Label lblAlfHint = new Label { Text = "Símbolos separados por comas (ej. a,b o 0,1):", Location = new Point(15, 25), AutoSize = true };
            txtAlfabeto = new TextBox { Text = "a,b", Location = new Point(15, 50), Width = 230, Font = new Font("Segoe UI", 10F) };

            Button btnDefinirAlf = new Button
            {
                Text = "Definir Alfabeto",
                Location = new Point(255, 48),
                Width = 130,
                Height = 30,
                BackColor = colorPrimario,
                ForeColor = colorTextoBoton,
                FlatStyle = FlatStyle.Flat
            };
            btnDefinirAlf.FlatAppearance.BorderSize = 0;
            btnDefinirAlf.Click += BtnDefinirAlf_Click;

            lblAlfabetoActual = new Label { Text = "Alfabeto actual: { a, b }", Location = new Point(395, 54), AutoSize = true, Font = new Font("Segoe UI", 9F, FontStyle.Italic) };

            gbAlfabeto.Controls.AddRange(new Control[] { lblAlfHint, txtAlfabeto, btnDefinirAlf, lblAlfabetoActual });

            // --- 2. SECCIÓN REGLAS ---
            GroupBox gbReglas = new GroupBox
            {
                Text = "2. Selección de Regla Formal",
                Location = new Point(20, 120),
                Size = new Size(560, 115),
                ForeColor = colorTexto,
                BackColor = colorTarjetas
            };
            rbReglaA = new RadioButton { Text = "Regla A: Inicia con 'a' y termina con 'b'", Location = new Point(15, 25), AutoSize = true, Checked = true };
            rbReglaB = new RadioButton { Text = "Regla B: Cantidad PAR del primer símbolo del alfabeto", Location = new Point(15, 52), AutoSize = true };
            rbReglaC = new RadioButton { Text = "Regla C: No contiene dos símbolos iguales consecutivos (ej. no 'aa' ni 'bb')", Location = new Point(15, 79), AutoSize = true };

            gbReglas.Controls.AddRange(new Control[] { rbReglaA, rbReglaB, rbReglaC });

            // --- 3. SECCIÓN EVALUAR ---
            GroupBox gbEvaluar = new GroupBox
            {
                Text = "3. Evaluar Cadena",
                Location = new Point(20, 245),
                Size = new Size(560, 105),
                ForeColor = colorTexto,
                BackColor = colorTarjetas
            };
            Label lblCadHint = new Label { Text = "Cadena a evaluar (deje vacío o use 'λ' para cadena vacía):", Location = new Point(15, 25), AutoSize = true };
            txtCadena = new TextBox { Location = new Point(15, 50), Width = 230, Font = new Font("Segoe UI", 10F) };

            Button btnEvaluar = new Button
            {
                Text = "Evaluar Cadena",
                Location = new Point(255, 48),
                Width = 130,
                Height = 30,
                BackColor = colorPrimario,
                ForeColor = colorTextoBoton,
                FlatStyle = FlatStyle.Flat
            };
            btnEvaluar.FlatAppearance.BorderSize = 0;
            btnEvaluar.Click += BtnEvaluar_Click;

            lblResultado = new Label { Text = "Resultado: -", Location = new Point(395, 54), AutoSize = true, Font = new Font("Segoe UI", 10F, FontStyle.Bold) };

            gbEvaluar.Controls.AddRange(new Control[] { lblCadHint, txtCadena, btnEvaluar, lblResultado });

            // --- 4. SECCIÓN GENERADOR ---
            GroupBox gbGenerador = new GroupBox
            {
                Text = "4. Generador de Cadenas Válidas (Bonus)",
                Location = new Point(20, 360),
                Size = new Size(560, 240),
                ForeColor = colorTexto,
                BackColor = colorTarjetas
            };
            Label lblK = new Label { Text = "Longitud (k):", Location = new Point(15, 28), AutoSize = true };
            numLongitud = new NumericUpDown { Location = new Point(100, 25), Width = 55, Value = 3, Minimum = 0, Maximum = 10, Font = new Font("Segoe UI", 10F) };

            Label lblN = new Label { Text = "Cantidad (N):", Location = new Point(175, 28), AutoSize = true };
            numCantidad = new NumericUpDown { Location = new Point(260, 25), Width = 60, Value = 5, Minimum = 1, Maximum = 100, Font = new Font("Segoe UI", 10F) };

            Button btnGenerar = new Button
            {
                Text = "Generar Cadenas",
                Location = new Point(335, 23),
                Width = 140,
                Height = 30,
                BackColor = Color.FromArgb(25, 135, 84),
                ForeColor = colorTextoBoton,
                FlatStyle = FlatStyle.Flat
            };
            btnGenerar.FlatAppearance.BorderSize = 0;
            btnGenerar.Click += BtnGenerar_Click;

            txtGeneradas = new TextBox
            {
                Location = new Point(15, 65),
                Size = new Size(530, 160),
                Multiline = true,
                ScrollBars = ScrollBars.Vertical,
                ReadOnly = true,
                BackColor = Color.FromArgb(248, 249, 250),
                Font = new Font("Consolas", 10F)
            };

            gbGenerador.Controls.AddRange(new Control[] { lblK, numLongitud, lblN, numCantidad, btnGenerar, txtGeneradas });

            // Agregar contenedores al formulario
            this.Controls.AddRange(new Control[] { gbAlfabeto, gbReglas, gbEvaluar, gbGenerador });
        }

        private void BtnDefinirAlf_Click(object sender, EventArgs e)
        {
            string entrada = txtAlfabeto.Text.Trim();
            if (string.IsNullOrEmpty(entrada))
            {
                MessageBox.Show("El alfabeto no puede estar vacío.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string[] simbolos = entrada.Split(',').Select(s => s.Trim()).Where(s => s != "").ToArray();

            if (simbolos.Length != simbolos.Distinct().Count())
            {
                MessageBox.Show("El alfabeto contiene símbolos duplicados.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            alfabeto = new HashSet<char>(simbolos.Select(s => s[0]));
            lblAlfabetoActual.Text = $"Alfabeto actual: {{ {string.Join(", ", alfabeto.OrderBy(c => c))} }}";
            MessageBox.Show($"Alfabeto definido con {alfabeto.Count} símbolos.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private (bool esValido, char invalido) ValidarAlfabeto(string cadena)
        {
            if (string.IsNullOrEmpty(cadena) || cadena == "λ" || cadena == "ε")
                return (true, ' ');

            foreach (char c in cadena)
            {
                if (!alfabeto.Contains(c))
                    return (false, c);
            }
            return (true, ' ');
        }

        private bool ValidarRegla(string cadena)
        {
            string cadenaReal = (cadena == "λ" || cadena == "ε") ? "" : cadena;
            var simbolosOrd = alfabeto.OrderBy(c => c).ToList();

            if (rbReglaA.Checked)
            {
                if (cadenaReal.Length == 0) return false;
                char sInicio = simbolosOrd[0];
                char sFin = simbolosOrd.Count > 1 ? simbolosOrd[1] : simbolosOrd[0];
                return cadenaReal.StartsWith(sInicio.ToString()) && cadenaReal.EndsWith(sFin.ToString());
            }
            else if (rbReglaB.Checked)
            {
                char sObjetivo = simbolosOrd[0];
                int conteo = cadenaReal.Count(c => c == sObjetivo);
                return conteo % 2 == 0;
            }
            else if (rbReglaC.Checked)
            {
                for (int i = 0; i < cadenaReal.Length - 1; i++)
                {
                    if (cadenaReal[i] == cadenaReal[i + 1])
                        return false;
                }
                return true;
            }

            return false;
        }

        private void BtnEvaluar_Click(object sender, EventArgs e)
        {
            string cadena = txtCadena.Text.Trim();

            var resAlfabeto = ValidarAlfabeto(cadena);
            if (!resAlfabeto.esValido)
            {
                lblResultado.Text = $"RECHAZADA: El símbolo '{resAlfabeto.invalido}' no pertenece al alfabeto.";
                lblResultado.ForeColor = Color.Red;
                return;
            }

            if (ValidarRegla(cadena))
            {
                lblResultado.Text = "Resultado: ACEPTADA ✓";
                lblResultado.ForeColor = Color.Green;
            }
            else
            {
                lblResultado.Text = "Resultado: RECHAZADA ✗";
                lblResultado.ForeColor = Color.Red;
            }
        }

        private void BtnGenerar_Click(object sender, EventArgs e)
        {
            int k = (int)numLongitud.Value;
            int n = (int)numCantidad.Value;

            List<string> combinaciones = GenerarCombinaciones(alfabeto.OrderBy(c => c).Select(c => c.ToString()).ToList(), k);
            List<string> validas = combinaciones.Where(c => ValidarRegla(c)).Take(n).ToList();

            if (validas.Count > 0)
            {
                txtGeneradas.Text = string.Join(Environment.NewLine, validas);
            }
            else
            {
                txtGeneradas.Text = "No se encontraron cadenas válidas con estos parámetros.";
            }
        }

        private List<string> GenerarCombinaciones(List<string> elementos, int longitud)
        {
            if (longitud == 0) return new List<string> { "" };

            var resultado = new List<string>();
            var subCombinaciones = GenerarCombinaciones(elementos, longitud - 1);

            foreach (var sub in subCombinaciones)
            {
                foreach (var el in elementos)
                {
                    resultado.Add(sub + el);
                }
            }
            return resultado;
        }
    }
}