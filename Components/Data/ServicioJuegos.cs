using Act.Components.Data;


namespace Act.Components.Data
{
    public class ServicioJuegos
    {
        private List<Juego> juegos = new List<Juego>
    {
    new Juego{Identificador=1, Nombre="Minecraft" , Jugado=false},
    new Juego{Identificador= 2, Nombre= "RDR2", Jugado= true},
     new Juego{Identificador=3, Nombre="GTAVI" , Jugado=false},
    new Juego{Identificador= 4, Nombre= "Bloodborne", Jugado= true}
};
        public Task<List<Juego>> ObtenerJuegos() => Task.FromResult(juegos);
        public Task AgregarJuego(Juego juego)
        {
            juegos.Add(juego);
            return Task.CompletedTask;
        }

        public Task ActualizarJuego(Juego juego)
        {
            var juegoExistente = juegos.FirstOrDefault(j => j.Identificador == juego.Identificador);
            if (juegoExistente != null)
            {
                juegoExistente.Nombre = juego.Nombre;
                juegoExistente.Jugado = juego.Jugado;
            }
            return Task.CompletedTask;
        }
        public Task EliminarJuego(Juego juego)
        {
            juegos.RemoveAll(j => j.Identificador == juego.Identificador);
            return Task.CompletedTask;
        }
    }


}