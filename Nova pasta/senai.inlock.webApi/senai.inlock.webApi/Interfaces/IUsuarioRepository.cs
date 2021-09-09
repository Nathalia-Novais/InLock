using senai.inlock.webApi_.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai.inlock.webApi_.Interfaces
{
    interface IUsuarioRepository
    {
        List<UsuarioDomain> ListarTodos();

        UsuarioDomain BuscarPorId(int idUsuario);

        void Cadastrar(UsuarioDomain novoUsuario);

        void AtualizarIdUrl(int idUsuario, UsuarioDomain usuarioAtualizado);

        void Deletar(int idUsuario);

        UsuarioDomain BuscarPorEmailSenha(string email, string senha);
    }
}
