using System;
using System.Linq;
using System.Threading.Tasks;
using BCrypt.Net;
using Gestion.Api.Models.Entities;
using Gestion.Api.Models.Request;
using Gestion.Api.Models.Response;
using Gestion.Api.Repository;
using Gestion.Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using AppContext = Gestion.Api.Repository.AppContext;

namespace Gestion.Api.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly AppContext _context;

        public UsuarioService(AppContext context)
        {
            _context = context;
        }

        public async Task<UsuarioResponse> CrearUsuarioAsync(UsuarioCreateRequest request)
        {
            // Verificar si el Username ya existe
            var usuarioExistente = await _context.Usuarios
                .AnyAsync(u => u.Username.ToLower() == request.Username.ToLower());

            if (usuarioExistente)
                throw new InvalidOperationException("El nombre de usuario ya está en uso.");

            // Generar hash de contraseña con BCrypt
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var usuario = new Usuario
            {
                IdGuid = Guid.NewGuid(),
                Username = request.Username,
                Password = hashedPassword,
                Nombre = request.Nombre,
                Apellido = request.Apellido,
                IdTipoUsuario = request.IdTipoUsuario,
                IdEntidad = request.IdEntidad,
                FechaAlta = DateTime.UtcNow
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            // Cargar datos relacionados (TipoUsuario y Entidad)
            await _context.Entry(usuario).Reference(u => u.TipoUsuario).LoadAsync();
            await _context.Entry(usuario).Reference(u => u.Entidad).LoadAsync();

            return new UsuarioResponse
            {
                Id = usuario.Id,
                IdGuid = usuario.IdGuid,
                Username = usuario.Username,
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                TipoUsuario = usuario.TipoUsuario?.Descripcion,
                Entidad = usuario.Entidad?.Nombre,
                FechaAlta = usuario.FechaAlta
            };
        }
    }
}
