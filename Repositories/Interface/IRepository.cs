using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Mao_Na_Massa_blog.Repositories.Interface
{
    public interface IRepository<T>
        where T : class
    {
        protected IEnumerable<T> Buscar();

        protected T Busca(short id);

        protected bool Inserir(T model);

        public bool Atualizar(T model);

        public bool Deletar(short id);
    }
}