﻿using Configuration;
using Configuration.GlobalHelpers;
using Dapper;
using Domain.Persona;
using Domain.TipoPago;
using Models;
using Models.DTOS;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contratos
{
    public class ContratosRepository : IContratoRepository
    {
        IGeneralConfiguration _config;
        IGlobalHelpers _helpers;
        public ContratosRepository(IGeneralConfiguration config, IGlobalHelpers helpers)
        {
            _config = config;
            _helpers = helpers;
        }

        public async Task<List<ContratoModel>?> GetContratos()
        {
            using (var con = new SqlConnection(_config.GetConnection()))
            {
                var result = await con.QueryAsync<ContratoModel>("SelectAllContratos", commandType: System.Data.CommandType.StoredProcedure);
                result.AsList().ForEach(x => x.IsDueñoOrDesarrollador = _helpers.PolicyReturnBool(TypeRol.Desarrollador, TypeRol.Dueño));
                await con.CloseAsync();
                return result.Count() == 0? null : result.AsList();
            }           
          
        }

        public async Task<List<ContratoModel>?> SelectOneContratoPerDNIPerson(string identificacion) { 
            using(var con = new SqlConnection(_config.GetConnection()))
            {
                var result = await con.QueryAsync<ContratoModel>("SelectOneContratoPerDNIPerson",
                    new { @Identificacion = identificacion }, commandType: System.Data.CommandType.StoredProcedure);
                await con.CloseAsync();
                return result.Count() == 0? null : result.AsList();
            }
        }

        public async Task<bool> InsertContrato(ContratoModel contrato)
        {
            var user = _config.getuserSistema();
            if(user == null) return false;
            using(var con = new SqlConnection(_config.GetConnection()))
            {
                var result = await con.ExecuteAsync("InsertContratos",
                    new
                    {
                        @IdPlan = contrato.IdPlan,
                        @IdPersona = contrato.IdPersona,
                        @IdUsuario = user.idUsuario,
                        @ValorTotal = contrato.ValorTotal,
                        @Descuento = contrato.Descuento,
                        @ValorOriginal = contrato.ValorOriginal,
                        @FechaFinal = contrato.FechaFinal,
                        @NumeroContrato = contrato.NumeroContrato,
                        @IdTipoPago = contrato.IdTipoPago
                    }, commandType: System.Data.CommandType.StoredProcedure);
                await con.CloseAsync();
                return result != 0;
            }
        }

        public async Task<bool> SetContratoNotValid(Guid idcontrato, bool idnotvalid, string description)
        { 
            using(var con = new SqlConnection(_config.GetConnection()))
            {
                var result = await con.ExecuteAsync("SetContratoNotValid",
                    new
                    {
                        @idcontrato = idcontrato,
                        @descripcionIsnotvalid = description,
                        @isnotvalid = idnotvalid
                    }, commandType: System.Data.CommandType.StoredProcedure);
                await con.CloseAsync();
                return result != 0;
            }
        }

        public async Task<bool> EliminarContrato(Guid idcontrato) {
            _helpers.Policy(TypeRol.Desarrollador, TypeRol.Dueño);
            using (var con = new SqlConnection(_config.GetConnection()))
            {
                var result = await con.ExecuteAsync("DeleteContratos",
                    new { @idcontratos = idcontrato }, commandType: System.Data.CommandType.StoredProcedure);
                await con.CloseAsync(); 
                return result != 0;
            }
        }

    }
}