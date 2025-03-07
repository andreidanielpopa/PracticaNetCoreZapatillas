using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PracticaNetCoreZapatillas.Data;
using PracticaNetCoreZapatillas.Models;

#region stored procedures

/*
 
alter procedure sp_ZapatillasImagenes (@posicion int,@idproducto int, @registros int out)
as
	select @registros = count(idimagen) from imageneszapaspractica where idproducto = @idproducto
	select idimagen,idproducto,imagen from
	(select cast( ROW_NUMBER() over (order by idimagen) as int)as posicion, idimagen, idproducto, imagen 
	from imageneszapaspractica where idproducto = @idproducto)
	query where posicion = @posicion
go
 
 */

#endregion

namespace PracticaNetCoreZapatillas.Repositories
{
    public class RepositoryZapatillas
    {
		private ZapasContext context;

		public RepositoryZapatillas(ZapasContext context)
		{
			this.context = context;
		}

		public async Task<List<Zapatilla>> GetAllZapatillasAsync()
		{
            var zapatillas = await this.context.Zapatillas.ToListAsync();
            return zapatillas;
        }

		public async Task<Zapatilla> FindZapatillaByIdAsync(int id)
		{
            var zapatilla = await this.context.Zapatillas.Where(x => x.IdProducto == id).FirstOrDefaultAsync();
            return zapatilla;
        }

        public async Task<ModelImagenes> GetImagenesZapatillaAsync(int posicion, int idproducto)
        {
            string sql = "sp_ZapatillasImagenes @posicion, @idproducto, @registros out";

            SqlParameter paramPosicion = new SqlParameter("@posicion", posicion);
            SqlParameter paramIdproducto = new SqlParameter("@idproducto", idproducto);
            SqlParameter paramRegistros = new SqlParameter("@registros", 0);
            paramRegistros.Direction = System.Data.ParameterDirection.Output;

            var consulta = await this.context.ImagenesZapatillas.FromSqlRaw(sql, paramPosicion, paramIdproducto, paramRegistros).ToListAsync();

            ImagenesZapatilla imagen =  consulta.FirstOrDefault();
            int registros = int.Parse(paramRegistros.Value.ToString());

            return new ModelImagenes
            {
                Imagen = imagen,
                Registros = registros
            };
        }
    }
}
