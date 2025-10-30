﻿using Act.Components.Data;

namespace Act.Components.Servicios
{
    public class ServicioControlador
    {
        private readonly ServicioJuegos _servicioJuegos;

        public ServicioControlador(ServicioJuegos servicioJuegos)
        {
            _servicioJuegos = servicioJuegos;
        }

        public async Task<List<Juego>> ObtenerJuegos()
        {
            return await _servicioJuegos.ObtenerJuegos();
        }

        public async Task AgregarJuego(Juego juego)
        {
            juego.Identificador = await GenerarNuevoID();
            await _servicioJuegos.AgregarJuego(juego);
        }

        private async Task<int> GenerarNuevoID()
        {
            var juegos = await _servicioJuegos.ObtenerJuegos();
            return juegos.Any() ? juegos.Max(j => j.Identificador) + 1 : 1;
        }

        public async Task ActualizarJuego(Juego juego)
        {
            await _servicioJuegos.ActualizarJuego(juego);
        }

        public async Task EliminarJuego(int identificador)
        {
            await _servicioJuegos.EliminarJuego(identificador);
        }
    }
}
