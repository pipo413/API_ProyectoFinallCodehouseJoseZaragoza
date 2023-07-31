using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

public class DB_Helper
{
    private List<SqlParameter> parametros;
    private readonly string connectionString = //Coloca aquí tu string de conexión;

    public DB_Helper()
    {
        //this.connectionString = connectionString;
        parametros = new List<SqlParameter>();
    }

    public enum TipoDato
    {
        Int,
        String,
        Money
    }

    // Método privado para obtener el SqlDbType basado en el tipo de dato especificado.
    private SqlDbType GetSqlDbType(TipoDato tipoDato)
    {
        switch (tipoDato)
        {
            case TipoDato.Int:
                return SqlDbType.Int;
            case TipoDato.String:
                return SqlDbType.VarChar;
            case TipoDato.Money: 
                return SqlDbType.Money;

            default:
                throw new ArgumentException("Tipo de dato no válido.");
        }
    }

    // Método para agregar parámetros al Helper.
    public void AddParametro(string nombre, object valor, TipoDato tipoDato)
    {
        parametros.Add(new SqlParameter(nombre, GetSqlDbType(tipoDato)) { Value = valor });
    }

    // Método para obtener un DataTable a partir de una consulta SQL.
    public DataTable GetDataTable(string script)
    {
        DataTable dataTable = new DataTable();

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            using (SqlCommand command = new SqlCommand(script, connection))
            {
                // Agregar los parámetros al comando SQL.
                command.Parameters.AddRange(parametros.ToArray());

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                // Llenar el DataTable con los resultados de la consulta.
                adapter.Fill(dataTable);

                // Limpiar la lista de parámetros para la próxima ejecución.
                parametros.Clear();
            }
        }

        // Devolver el DataTable que contiene los datos de la consulta.
        return dataTable;
    }

    // Método para ejecutar una consulta que no devuelve datos (INSERT, UPDATE, DELETE, etc.).
    public int ExecuteNonQuery(string script)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            using (SqlCommand command = new SqlCommand(script, connection))
            {
                // Agregar los parámetros al comando SQL.
                command.Parameters.AddRange(parametros.ToArray());

                connection.Open();
                // Ejecutar la consulta y obtener el número de filas afectadas.
                int rowsAffected = command.ExecuteNonQuery();

                // Limpiar la lista de parámetros para la próxima ejecución.
                parametros.Clear();

                // Devolver el número de filas afectadas.
                return rowsAffected;
            }
        }
    }
}
