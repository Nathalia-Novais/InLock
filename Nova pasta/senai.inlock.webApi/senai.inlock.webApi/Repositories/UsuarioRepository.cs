using senai.inlock.webApi_.Domains;
using senai.inlock.webApi_.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai.inlock.webApi_.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        public void AtualizarIdUrl(int idUsuario, UsuarioDomain usuarioAtualizado)
        {
            throw new NotImplementedException();
        }

        public UsuarioDomain BuscarPorEmailSenha(string email, string senha)
        {
            throw new NotImplementedException();
        }

        public UsuarioDomain BuscarPorId(int idUsuario)
        {
            throw new NotImplementedException();
        }

        public void Cadastrar(UsuarioDomain novoUsuario)
        {
            throw new NotImplementedException();
        }

        public void Deletar(int idUsuario)
        {
            throw new NotImplementedException();
        }

        public List<UsuarioDomain> ListarTodos()
        {
            throw new NotImplementedException();
        }
    }
}
