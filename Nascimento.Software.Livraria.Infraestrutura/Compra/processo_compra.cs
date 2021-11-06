using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nascimento.Software.Livraria.Dominio.Dominios.Compra;
using Dapper;
using System.Data.SqlClient;
using System.Data.Common;

namespace Nascimento.Software.Livraria.Infraestrutura.Compra
{
    public class processo_compra : IConnection
    {
        private readonly SqlConnection _sql;
        public processo_compra()
        {
            _sql = new SqlConnection(GetConnectionString());
        }

        public DbConnection GetConnection()
        {
            throw new NotImplementedException();
        }

        public string GetConnectionString() => Settings.ConnectionString;

        public async Task<bool> Start(Dominio.Dominios.Compra.Compra compra)
        {
            try
            {
                if (await InserirTabela(compra))
                {
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
            return false;
        }
        public async Task<IEnumerable<Dominio.Dominios.Compra.Compra>> GetCompras()
        {
            try
            {
                var query = $@"SELECT ID, LIVROID, LIVRONOME, USERID, USEREMAIL, DATACOMPRA FROM COMPRA";

                return await _sql.QueryAsync<Dominio.Dominios.Compra.Compra>(query).ConfigureAwait(false);

            }
            catch (Exception)
            {
                return null;
            }
        }

        private int GeradorId()
        {
            try
            {
                var query = $@"SELECT MAX(Id) FROM COMPRA";
                var retorno = _sql.ExecuteScalar(query, commandType: System.Data.CommandType.Text);
                if (!Convert.IsDBNull(retorno))
                {
                    return Convert.ToInt32(retorno);
                }
            }
            catch (Exception)
            {
                return 0;
            }
            return 0;
        }
        private async Task<bool> InserirTabela(Dominio.Dominios.Compra.Compra compra)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("Id", GeradorId());
                param.Add("LivroId", compra.Livro.Id);
                param.Add("LivroNome", compra.Livro.Nome);
                param.Add("UserId", compra.Usuario.Id);
                param.Add("UserEmail", compra.Usuario.Email);
                param.Add("DataCompra", compra.DataCompra);

                var query = $@"INSERT INTO Compra(ID,LIVROID,LIVRONOME,USERID,
                            USEREMAIL,DATACOMPRA) VALUES (@Id, @LivroId, @LivroNome,
                            @UserId, @UserEmail, @DataCompra)";

                return await _sql.ExecuteAsync(query, param: param, commandType: System.Data.CommandType.Text).ConfigureAwait(false) > 0;

            }
            catch (Exception)
            {
                return false;
            }
        }

       
    }
}
