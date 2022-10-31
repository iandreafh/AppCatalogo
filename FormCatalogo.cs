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
        /// Evento KeyPress para controlar que no se puedan escribir caracteres en el tel�fono.
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
        /// Evento del bot�n a�adir.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnA�adir_Click(object sender, EventArgs e)
        {
            modoEdicion = ModoEdicion.crear;
            ModoPantallaEdicion();

        }

        /// <summary>
        /// Evento del bot�n eliminar.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            bool correcto = false;
            DialogResult respuesta = MessageBox.Show("�Est� seguro de que desea eliminar esta pel�cula?", "Confirmaci�n", MessageBoxButtons.YesNo);

            if (respuesta == DialogResult.Yes)
            {
                Pelicula p = ObtenerInformacion();
                correcto = Repositorio.EliminarPelicula(p);

                // Si todo ha ido bien, mostramos un mensaje
                if (correcto)
                {
                    MessageBox.Show("La pel�cula " + p.titulo + " se elimin� correctamente.");
                    // Cambiamos el modo a lectura
                    ModoPantallaLectura();
                    // Una vez hemos hecho la acci�n, recargamos el grid:
                    CargarYConfigurarGrid();
                }

            }

        }

        /// <summary>
        /// Evento del bot�n Modificar.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnModificar_Click(object sender, EventArgs e)
        {
            modoEdicion = ModoEdicion.modificar;
            ModoPantallaEdicion();

        }

        /// <summary>
        /// Evento click del bot�n guardar.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGuardar_Click(object sender, EventArgs e)
        {

            bool correcto = false;

            if (InformacionObligatoriaCumplimentada())
            {
                // Rellenamos la entidad con la informaci�n
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
                    MessageBox.Show("La pel�cula " + p.titulo + " se registr� correctamente.");
                    modoEdicion = ModoEdicion.lectura;
                    // Cambiamos el modo a lectura
                    ModoPantallaLectura();
                    // Una vez hemos hecho la acci�n, recargamos el grid:
                    CargarYConfigurarGrid();
                }

            }
            else
            {
                MessageBox.Show("Los campos T�tulo y A�o son obligatorios.");
            }

        }

        /// <summary>
        /// Evento click del bot�n Cancelar.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult respuesta = MessageBox.Show("�Est� seguro de que desea salir de la edici�n?", "Confirmaci�n", MessageBoxButtons.YesNo);

            if (respuesta == DialogResult.Yes)
            {
                modoEdicion = ModoEdicion.lectura;
                ModoPantallaLectura();

                // Si tenemos una fila seleccionada en el grid:
                if (dgvCatalogo.SelectedRows.Count == 1)
                {
                    // Recargamos su informaci�n en el formulario
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
            // Para cualquier operaci�n que no sea que ha cambiado la selecci�n de la fila, nos salimos
            if (e.StateChanged != DataGridViewElementStates.Selected) return;

            // Obtenemos la fila seleccionada
            DataGridViewRow filaSeleccionada = e.Row;
            CargarInfoFilaSeleccionadaFormulario(filaSeleccionada);

        }

        #endregion

        #region M�todos

        /// <summary>
        /// M�todo para establecer el modo de la pantalla a edici�n.
        /// </summary>
        public void ModoPantallaEdicion()
        {
            // Limpiamos s�lo si es modo creaci�n, si es modo edici�n dejamos los valores:
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

            btnA�adir.Enabled = false;
            btnEliminar.Enabled = false;
            btnModificar.Enabled = false;

            dgvCatalogo.Enabled = false;
        }

        /// <summary>
        /// M�todo para establecer el modo de la pantalla a edici�n.
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
            btnA�adir.Enabled = true;
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
        /// M�todo para obtener la informaci�n del formulario.
        /// </summary>
        /// <returns>Pelicula con la informaci�n del formulario.</returns>
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
        /// M�todo que carga y configura el grid.
        /// </summary>
        /// <param name="Peliculas">DataTable con la info de los peliculas</param>
        public void CargarYConfigurarGrid()
        {
            DataSet ds = Repositorio.ObtenerPeliculas();
            dgvCatalogo.DataSource = ds.Tables[0];
            

            // Tama�os columnas
            dgvCatalogo.Columns["Id"].Width = 40;
            dgvCatalogo.Columns["Titulo"].Width = 410;
            dgvCatalogo.Columns["Estreno"].Width = 150;
            dgvCatalogo.Columns["Genero"].Width = 150;
            dgvCatalogo.Columns["Valoracion"].Width = 100;

            // Renombrado columnas
            dgvCatalogo.Columns["Estreno"].HeaderText = "Fecha Estreno";

            // Formato fecha en espa�ol
            dgvCatalogo.Columns["Estreno"].DefaultCellStyle.Format = "MM/yyyy";

            string[] generos = { "Comedia", "Romantica", "Infantil", "Acci�n", "Terror" };
            comboGenero.Items.Clear();
            comboGenero.Items.AddRange(generos);

            // Seleccionamos la primera fila del grid si existe
            SeleccionarPrimeraFilaGrid();

        }

        /// <summary>
        /// M�todo que selecciona la primera fila del grid si existe.
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
        /// M�todo que carga la informaci�n de la fila seleccionada en los controles.
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
        /// Funci�n que nos indica si la informaci�n obligatoria est� cumplimentada o no.
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