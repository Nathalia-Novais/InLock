using senai.inlock.webApi_.Domains;
using senai.inlock.webApi_.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai.inlock.webApi_.Repositories
{
    public class JogoRepository : IJogoRepository
    {
        public void AtualizarIdUrl(int idJogo, JogoDomain jogoAtualizado)
        {
            throw new NotImplementedException();
        }

        public JogoDomain BuscarPorId(int idJogo)
        {
            throw new NotImplementedException();
        }

        public void Cadastrar(JogoDomain novoJogo)
        {
            throw new NotImplementedException();
        }

        public void Deletar(int idJogo)
        {
            throw new NotImplementedException();
        }

        public List<JogoDomain> ListarTodos()
        {
            throw new NotImplementedException();
        }
    }
}
