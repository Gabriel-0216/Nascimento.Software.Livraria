using Nascimento.Software.Livraria.Dominio.Dominios;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Dapper;
using System.Threading.Tasks;

namespace Nascimento.Software.Livraria.Infraestrutura.Repositorio
{
    public class FotoRepositorio : IRepositorio<Foto>, IConnection
    {
        private SqlConnection _dbConnection;
        public FotoRepositorio() => _dbConnection = new SqlConnection(GetConnectionString());
        public DbConnection GetConnection() => _dbConnection;

        public string GetConnectionString() => Settings.ConnectionString;
        public async Task<bool> Add(Foto entidade)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("FotoId", entidade.FotoId);
                param.Add("ImagemUrl", entidade.ImagemURL);
                var query = $@"INSERT INTO FOTO(FotoId, IMAGEMURL) VALUES (@FotoId, @ImagemUrl)";
                return await _dbConnection.ExecuteAsync(query, param: param, commandType: System.Data.CommandType.Text).ConfigureAwait(false) > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> Delete(Foto entidade)
        {
            var param = new DynamicParameters();
            param.Add("FotoId", entidade.FotoId);
            var query = $@"DELETE FROM FOTO WHERE FotoId = @FotoId";
            return await _dbConnection.ExecuteAsync(query, param: param, commandType: System.Data.CommandType.Text).ConfigureAwait(false) > 0;
        }

        public async Task<Foto> Get(int id)
        {
            var param = new DynamicParameters();
            param.Add("FotoId", id);
            var query = $@"SELECT FotoId, IMAGEMURL FROM FOTO WHERE FotoId = @FotoId";
            return await _dbConnection.QueryFirstOrDefaultAsync<Foto>(query, param: param, commandType: System.Data.CommandType.Text).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Foto>> GetAll()
        {
            var query = $@"SELECT FotoId, IMAGEMURL FROM FOTO";
            return await _dbConnection.QueryAsync<Foto>(query, commandType: System.Data.CommandType.Text).ConfigureAwait(false);
        }

        public async Task<bool> Update(Foto entidade)
        {
            var param = new DynamicParameters();
            param.Add("FotoId", entidade.FotoId);
            param.Add("ImagemUrl", entidade.ImagemURL);
            var query = $@"UPDATE FOTO SET ImagemUrl = @ImagemUrl WHERE FotoId = @FotoId";
            return await _dbConnection.ExecuteAsync(query, param: param, commandType: System.Data.CommandType.Text).ConfigureAwait(false) > 0;
        }
    }
}
