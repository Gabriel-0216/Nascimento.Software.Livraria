using Nascimento.Software.Livraria.Dominio.Dominios.Emprestimos;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Dapper;
using System.Threading.Tasks;
using Nascimento.Software.Livraria.Dominio.Dominios.Identity;

namespace Nascimento.Software.Livraria.Infraestrutura.Emprestimos
{
    public class Processo_Emprestimo : IConnection
    {
        public SqlConnection sqlConnection;
        public DbConnection GetConnection() => sqlConnection;
        public string GetConnectionString() => Settings.ConnectionString;

        public Processo_Emprestimo()
        {
            sqlConnection = new SqlConnection(GetConnectionString());
        }

        private async Task<int> GeradorId()
        {
            var query = $@"SELECT MAX(ID) FROM Aluguel_Livro";

            var retorno = await GetConnection().ExecuteScalarAsync(query, commandType: System.Data.CommandType.Text).ConfigureAwait(false);
            if(!Convert.IsDBNull(retorno) || !(retorno != null))
            {
                return Convert.ToInt32(retorno);
            }
            return 0;

        }
        public async Task<bool> InicioEmprestimo(Aluguel aluguel)
        {
            try
            {
                var param = new DynamicParameters();

                param.Add("Id", await GeradorId()); // ID único (Chave primária da tabela)

                param.Add("UserId", aluguel.Usuario.Id);
                param.Add("UserName", aluguel.Usuario.UserName);
                param.Add("UserEmail", aluguel.Usuario.Email);

                param.Add("LivroId", aluguel.Livro.Id);
                param.Add("NomeLivro", aluguel.Livro.Nome);
                param.Add("DataEmprestimo", aluguel.DataEmprestimo);
                param.Add("DataDevolucao", aluguel.DataDevolucao);
                param.Add("Devolvido", 'N');


                var query = $@"INSERT INTO Aluguel_Livro (Id, UserId, UserName, UserEmail, LivroId, NomeLivro,
                    DataEmprestimo,DataDevolucao,Devolvido) VALUES(@Id, @UserId, @UserName, @UserEmail,
                                                                @LivroId, @NomeLivro, @DataEmprestimo,
                                                                @DataDevolucao, @Devolvido)";

                return await GetConnection().ExecuteAsync(query, param: param, commandType: System.Data.CommandType.Text).ConfigureAwait(false) > 0;

            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<AluguelModel>> GetAlugueis(Usuario usuario)
        {
            var param = new DynamicParameters();
            param.Add("UserId", usuario.Id);
            var query = $@"SELECT Id, UserId, UserName, UserEmail, LivroId, NomeLivro,
                    DataEmprestimo, DataDevolucao, Devolvido FROM ALUGUEL_LIVRO WHERE UserId = @UserId";

            return await GetConnection().QueryAsync<AluguelModel>(query, param: param, commandType: System.Data.CommandType.Text).ConfigureAwait(false);
        }

        public async Task<AluguelModel> GetAluguelSelecionado(Usuario usuario, int id)
        {
            var param = new DynamicParameters();
            param.Add("UserId", usuario.Id);
            param.Add("EmprestimoId", id);

            var query = @"SELECT Id, UserId, UserName, UserEmail, LivroId, NomeLivro,
                    DataEmprestimo, DataDevolucao, Devolvido FROM ALUGUEL_LIVRO WHERE UserId = @UserId AND Id = @EmprestimoId";
            return await GetConnection().QueryFirstOrDefaultAsync<AluguelModel>(query, param: param, commandType: System.Data.CommandType.Text).ConfigureAwait(false);
        }

    }
}
