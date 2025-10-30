using Act.Components.Data;
using Microsoft.Data.Sqlite;

namespace Act.Components.Servicios
{
    public class ServicioJuegos
    {
        private List<Juego> juegos = new List<Juego>();

        public async Task<List<Juego>> ObtenerJuegos()
        {
            juegos.Clear();
            string ruta = "mibase.db";
            using var conexion = new SqliteConnection($"DataSource={ruta}");
            await conexion.OpenAsync();
            var comando = conexion.CreateCommand();
            comando.CommandText = @"SELECT Identificador, Nombre, Jugado FROM Juegos";

            using var lector = await comando.ExecuteReaderAsync();
            while (await lector.ReadAsync())
            {
                juegos.Add(new Juego
                {
                    Identificador = lector.GetInt32(0),
                    Nombre = lector.GetString(1),
                    Jugado = lector.GetInt32(2) == 1
                });
            }
            return juegos;
        }

        public async Task AgregarJuego(Juego juego)
        {
            string ruta = "mibase.db";
            using var conexion = new SqliteConnection($"DataSource={ruta}");
            await conexion.OpenAsync();
            var comando = conexion.CreateCommand();
            comando.CommandText = @"
                INSERT INTO Juegos (Identificador, Nombre, Jugado) 
                VALUES ($IDENTIFICADOR, $NOMBRE, $JUGADO)";

            comando.Parameters.AddWithValue("$IDENTIFICADOR", juego.Identificador);
            comando.Parameters.AddWithValue("$NOMBRE", juego.Nombre);
            comando.Parameters.AddWithValue("$JUGADO", juego.Jugado ? 1 : 0);

            await comando.ExecuteNonQueryAsync();
            juegos.Add(juego);
        }

        public async Task ActualizarJuego(Juego juego)
        {
            string ruta = "mibase.db";
            using var conexion = new SqliteConnection($"DataSource={ruta}");
            await conexion.OpenAsync();
            var comando = conexion.CreateCommand();
            comando.CommandText = @"
                UPDATE Juegos 
                SET Nombre = $NOMBRE, Jugado = $JUGADO 
                WHERE Identificador = $IDENTIFICADOR";

            comando.Parameters.AddWithValue("$IDENTIFICADOR", juego.Identificador);
            comando.Parameters.AddWithValue("$NOMBRE", juego.Nombre);
            comando.Parameters.AddWithValue("$JUGADO", juego.Jugado ? 1 : 0);

            await comando.ExecuteNonQueryAsync();
        }

        public async Task EliminarJuego(int identificador)
        {
            string ruta = "mibase.db";
            using var conexion = new SqliteConnection($"DataSource={ruta}");
            await conexion.OpenAsync();
            var comando = conexion.CreateCommand();
            comando.CommandText = @"DELETE FROM Juegos WHERE Identificador = $IDENTIFICADOR";

            comando.Parameters.AddWithValue("$IDENTIFICADOR", identificador);

            await comando.ExecuteNonQueryAsync();
        }
    }
}
