using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCatalogoBBDD
{
    internal class Repositorio
    {
        /// <summary>
        /// Método para obtener todas las peliculas del catalogo
        /// </summary>
        /// <returns></returns>
        public static DataSet ObtenerPeliculas()
        {
            Conexion conexion = new Conexion();
            SqlCommand comando = new SqlCommand();
            DataSet ds = new DataSet();
            SqlDataAdapter adaptador = new SqlDataAdapter();

            try
            {
                comando.CommandText = "SELECT * FROM PELICULAS";
                comando.Connection = conexion.cnx;
                adaptador.SelectCommand = comando;
                conexion.cnx.Open();
                adaptador.Fill(ds);
                conexion.cnx.Close();

                return ds;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
            return ds;
        }

        /// <summary>
        /// Método para crear una pelicula
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static  bool CrearPelicula(Pelicula p)
        {
            bool todoCorrecto = false;
            Conexion conexion = new Conexion();
            SqlCommand comando = new SqlCommand();

            try
            {
                comando.CommandText = "set dateformat dmy; INSERT INTO PELICULAS VALUES ('" + p.titulo + "', '" + p.estreno + "', '" + p.genero + "', '" + p.valoracion + "')";
                
                comando.Connection = conexion.cnx;
                conexion.cnx.Open();
                comando.ExecuteNonQuery();
                conexion.cnx.Close();

                todoCorrecto = true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                todoCorrecto = false;
            }

            return todoCorrecto;
        }

        /// <summary>
        /// Método para eliminar una pelicula
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static bool EliminarPelicula(Pelicula p)
        {
            bool todoCorrecto = false;
            Conexion conexion = new Conexion();
            SqlCommand comando = new SqlCommand();

            try
            {
                comando.CommandText = "DELETE FROM PELICULAS WHERE ID = '" + p.Id + "'";

                comando.Connection = conexion.cnx;
                conexion.cnx.Open();
                comando.ExecuteNonQuery();
                conexion.cnx.Close();

                todoCorrecto = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                todoCorrecto = false;
            }

            return todoCorrecto;
        }

        /// <summary>
        /// Método para modificar una pelicula
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static bool ModificarPelicula(Pelicula p)
        {
            bool todoCorrecto = false;
            Conexion conexion = new Conexion();
            SqlCommand comando = new SqlCommand();

            try
            {
                comando.CommandText = "set dateformat dmy; UPDATE PELICULAS SET TITULO= '" + p.titulo + "', ESTRENO= '" + p.estreno + "', " + 
                    "GENERO = '" + p.genero + "', VALORACION = '" + p.valoracion + "' " +
                    " WHERE ID = '" + p.Id + "'";

                comando.Connection = conexion.cnx;
                conexion.cnx.Open();
                comando.ExecuteNonQuery();
                conexion.cnx.Close();

                todoCorrecto = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                todoCorrecto = false;
            }

            return todoCorrecto;
        }
    }
}
