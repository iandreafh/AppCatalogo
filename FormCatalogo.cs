using System.Data;
using System.Diagnostics.Contracts;

namespace AppCatalogoBBDD
{
    public partial class FormCatalogo : Form
    {
        public FormCatalogo()
        {
            InitializeComponent();
        }



        #region Enumerado

        public enum ModoEdicion
        {
            lectura,
            crear,
            modificar
        }

        public ModoEdicion modoEdicion;

        #endregion

        #region Eventos

        /// <summary>
        /// Evento Load del formulario.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {

            modoEdicion = ModoEdicion.lectura;
            ModoPantallaLectura();

            // Cargamos los items de bbdd.
            CargarYConfigurarGrid();

        }

        /*
        /// <summary>
        /// Evento KeyPress para controlar que no se puedan escribir caracteres en el teléfono.
        /// </summary>
        /// <param name="sender">Objeto llamante.</param>
        /// <param name="e">Argumentos del evento.</param>
        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        */

        /// <summary>
        /// Evento del botón añadir.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAñadir_Click(object sender, EventArgs e)
        {
            modoEdicion = ModoEdicion.crear;
            ModoPantallaEdicion();

        }

        /// <summary>
        /// Evento del botón eliminar.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            bool correcto = false;
            DialogResult respuesta = MessageBox.Show("¿Está seguro de que desea eliminar esta película?", "Confirmación", MessageBoxButtons.YesNo);

            if (respuesta == DialogResult.Yes)
            {
                Pelicula p = ObtenerInformacion();
                correcto = Repositorio.EliminarPelicula(p);

                // Si todo ha ido bien, mostramos un mensaje
                if (correcto)
                {
                    MessageBox.Show("La película " + p.titulo + " se eliminó correctamente.");
                    // Cambiamos el modo a lectura
                    ModoPantallaLectura();
                    // Una vez hemos hecho la acción, recargamos el grid:
                    CargarYConfigurarGrid();
                }

            }

        }

        /// <summary>
        /// Evento del botón Modificar.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnModificar_Click(object sender, EventArgs e)
        {
            modoEdicion = ModoEdicion.modificar;
            ModoPantallaEdicion();

        }

        /// <summary>
        /// Evento click del botón guardar.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGuardar_Click(object sender, EventArgs e)
        {

            bool correcto = false;

            if (InformacionObligatoriaCumplimentada())
            {
                // Rellenamos la entidad con la información
                Pelicula p = ObtenerInformacion();

                switch (modoEdicion)
                {
                    case ModoEdicion.crear:
                        correcto = Repositorio.CrearPelicula(p);
                        break;
                    case ModoEdicion.modificar:
                        correcto = Repositorio.ModificarPelicula(p);
                        break;
                }

                // Si todo ha ido bien, mostramos un mensaje
                if (correcto)
                {
                    MessageBox.Show("La película " + p.titulo + " se registró correctamente.");
                    modoEdicion = ModoEdicion.lectura;
                    // Cambiamos el modo a lectura
                    ModoPantallaLectura();
                    // Una vez hemos hecho la acción, recargamos el grid:
                    CargarYConfigurarGrid();
                }

            }
            else
            {
                MessageBox.Show("Los campos Título y Año son obligatorios.");
            }

        }

        /// <summary>
        /// Evento click del botón Cancelar.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult respuesta = MessageBox.Show("¿Está seguro de que desea salir de la edición?", "Confirmación", MessageBoxButtons.YesNo);

            if (respuesta == DialogResult.Yes)
            {
                modoEdicion = ModoEdicion.lectura;
                ModoPantallaLectura();

                // Si tenemos una fila seleccionada en el grid:
                if (dgvCatalogo.SelectedRows.Count == 1)
                {
                    // Recargamos su información en el formulario
                    CargarInfoFilaSeleccionadaFormulario(dgvCatalogo.SelectedRows[0]);
                }
            }
        }

        /// <summary>
        /// Evento click de la celda.
        /// Cuando se hace click en una celda se selecciona la fila entera.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvCatalogo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvCatalogo.Rows[e.RowIndex].Selected = true;
        }

        /// <summary>
        /// Evento para cuando una fila cambia de estado en el grid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvCatalogo_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            // Para cualquier operación que no sea que ha cambiado la selección de la fila, nos salimos
            if (e.StateChanged != DataGridViewElementStates.Selected) return;

            // Obtenemos la fila seleccionada
            DataGridViewRow filaSeleccionada = e.Row;
            CargarInfoFilaSeleccionadaFormulario(filaSeleccionada);

        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método para establecer el modo de la pantalla a edición.
        /// </summary>
        public void ModoPantallaEdicion()
        {
            // Limpiamos sólo si es modo creación, si es modo edición dejamos los valores:
            if (modoEdicion == ModoEdicion.crear)
            {
                txtTitulo.Text = "";
                dtpEstreno.Value = DateTime.Now;
                comboGenero.SelectedIndex = 0;
                trackBarValoracion.Value = 1;
                txtId.Text = "";

            }

            btnGuardar.Enabled = true;
            btnCancelar.Enabled = true;
            txtTitulo.Enabled = true;
            dtpEstreno.Enabled = true;
            comboGenero.Enabled = true;
            trackBarValoracion.Enabled = true;
            txtId.Enabled = false;

            btnAñadir.Enabled = false;
            btnEliminar.Enabled = false;
            btnModificar.Enabled = false;

            dgvCatalogo.Enabled = false;
        }

        /// <summary>
        /// Método para establecer el modo de la pantalla a edición.
        /// </summary>
        public void ModoPantallaLectura()
        {

            txtTitulo.Text = "";
            dtpEstreno.Value = DateTime.Now;
            comboGenero.SelectedIndex = -1;
            trackBarValoracion.Value = 1;
            txtId.Text = "";

            btnGuardar.Enabled = false;
            btnCancelar.Enabled = false;
            btnAñadir.Enabled = true;
            btnEliminar.Enabled = true;
            btnModificar.Enabled = true;

            txtTitulo.Enabled = false;
            dtpEstreno.Enabled = false;
            comboGenero.Enabled = false;
            trackBarValoracion.Enabled = false;
            txtId.Enabled = false;

            dgvCatalogo.Enabled = true;
        }

        /// <summary>
        /// Método para obtener la información del formulario.
        /// </summary>
        /// <returns>Pelicula con la información del formulario.</returns>
        public Pelicula ObtenerInformacion()
        {
            Pelicula pelicula = new Pelicula();

            pelicula.titulo = txtTitulo.Text.Trim();
            pelicula.genero = comboGenero.SelectedItem.ToString();
            pelicula.valoracion = trackBarValoracion.Value;
            pelicula.estreno = dtpEstreno.Value;

            if (!String.IsNullOrEmpty(txtId.Text))
            {
                pelicula.Id = Convert.ToInt32(txtId.Text);
            }

            return pelicula;
        }

        /// <summary>
        /// Método que carga y configura el grid.
        /// </summary>
        /// <param name="Peliculas">DataTable con la info de los peliculas</param>
        public void CargarYConfigurarGrid()
        {
            DataSet ds = Repositorio.ObtenerPeliculas();
            dgvCatalogo.DataSource = ds.Tables[0];
            

            // Tamaños columnas
            dgvCatalogo.Columns["Id"].Width = 40;
            dgvCatalogo.Columns["Titulo"].Width = 410;
            dgvCatalogo.Columns["Estreno"].Width = 150;
            dgvCatalogo.Columns["Genero"].Width = 150;
            dgvCatalogo.Columns["Valoracion"].Width = 100;

            // Renombrado columnas
            dgvCatalogo.Columns["Estreno"].HeaderText = "Fecha Estreno";

            // Formato fecha en español
            dgvCatalogo.Columns["Estreno"].DefaultCellStyle.Format = "MM/yyyy";

            string[] generos = { "Comedia", "Romantica", "Infantil", "Acción", "Terror" };
            comboGenero.Items.Clear();
            comboGenero.Items.AddRange(generos);

            // Seleccionamos la primera fila del grid si existe
            SeleccionarPrimeraFilaGrid();

        }

        /// <summary>
        /// Método que selecciona la primera fila del grid si existe.
        /// </summary>
        public void SeleccionarPrimeraFilaGrid()
        {
            // Si hay alguna fila, seleccionamos la primera
            if (dgvCatalogo.Rows.Count > 0)
            {
                dgvCatalogo.Rows[0].Selected = true;
            }
        }

        /// <summary>
        /// Método que carga la información de la fila seleccionada en los controles.
        /// </summary>
        /// <param name="filaSeleccionada"></param>
        public void CargarInfoFilaSeleccionadaFormulario(DataGridViewRow filaSeleccionada)
        {
            if (!string.IsNullOrEmpty(filaSeleccionada.Cells["Titulo"].Value.ToString()))
            {
                txtId.Text = filaSeleccionada.Cells["Id"].Value.ToString();
                txtTitulo.Text = filaSeleccionada.Cells["Titulo"].Value.ToString();
                dtpEstreno.Value = (DateTime)filaSeleccionada.Cells["Estreno"].Value;
                comboGenero.Text = filaSeleccionada.Cells["Genero"].Value.ToString();
                trackBarValoracion.Value = (int)filaSeleccionada.Cells["Valoracion"].Value;
            }
        }

        /// <summary>
        /// Función que nos indica si la información obligatoria está cumplimentada o no.
        /// </summary>
        /// <returns></returns>
        public bool InformacionObligatoriaCumplimentada()
        {
            if (String.IsNullOrEmpty(txtTitulo.Text) || String.IsNullOrEmpty(dtpEstreno.Text))
            {
                return false;
            }
            else
            {
                return true;
            }
        }



        #endregion

    }
}