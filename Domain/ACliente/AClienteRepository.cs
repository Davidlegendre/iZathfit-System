using Configuration;
using Dapper;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ACliente
{
    public class AClienteRepository : IAClienteRepository
    {
        IGeneralConfiguration _config;
        public AClienteRepository(IGeneralConfiguration config)
        {
            _config = config;
        }

        public async Task<bool> InsertACliente(PersonaModel persona, SaldoXPersonaModel pago,
            List<PadecimientoEnfermedades> padecimientos, ContratoModel contrato)
        {
            var user = _config.getuserSistema();
            if (user == null) return false;

            var dt = new DataTable();
            dt.Columns.Add("padecimiento", typeof(string));
            padecimientos.Select(x => x.Padecimiento).AsList().ForEach(x => dt.Rows.Add(x));


            using (var con = new SqlConnection(_config.GetConnection()))
            {
                var result = await con.ExecuteAsync("InsertarClienteTodoConContratoYPago",
                    new
                    {
                        @Nombres = persona.Nombres,
                        @Apellidos = persona.Apellidos,
                        @Fech_Nacimiento = persona.Fech_Nacimiento,
                        @Direccion = persona.Direccion,
                        @Telefono = persona.Telefono,
                        @Email = persona.Email,
                        @NumeroEmergencia1 = persona.NumeroEmergencia1,
                        @NumeroEmergencia2 = persona.NumeroEmergencia2,
                        @idGenero = persona.IdGenero,
                        @Identificacion = persona.Identificacion,
                        @idtipoIdentificacion = persona.IdTipoIdentity,
                        @idRol = persona.IdRol,
                        @idOcupacion = persona.IdOcupacion,

                        @IdPlan = contrato.IdPlan,
                        @IdUsuario = user.idUsuario,
                        @ValorTotal = contrato.ValorTotal,
                        @Descuento = contrato.Descuento,
                        @ValorOriginal = contrato.ValorOriginal,
                        @FechaFinal = contrato.FechaFinal,
                        @FechaFinalReal = contrato.FechaFinalReal,
                        @NumeroContrato = contrato.NumeroContrato,
                        @IdTipoPago = contrato.IdTipoPago,

                        @TotalPagadoActual = pago.TotalPagadoActual,

                        @Padecimientos = dt.AsTableValuedParameter("dbo.PadecimientoType")
                    }, commandType: CommandType.StoredProcedure);
                await con.CloseAsync();
                return result != 0;
            }
        }
    }
}
