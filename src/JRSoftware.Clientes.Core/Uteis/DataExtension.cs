using JRSoftware.Clientes.Core.Abstracao;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace JRSoftware.Clientes.Core.Uteis
{
	public static class DataExtension
	{
		public static IDbCommand CreateCommandText(this IConnectionManager connectionManager, string cmdSQL, IDbTransaction iDbTransaction = null, int commandTimeout = 30)
		{
			return connectionManager.Connection.CreateCommandText(cmdSQL, iDbTransaction, commandTimeout);
		}

		public static IDbCommand CreateCommandText(this IDbConnection iDbConnection, string cmdSQL, IDbTransaction iDbTransaction = null, int commandTimeout = 30)
		{
			var iDbCommand = iDbConnection.CreateCommand();
			iDbCommand.CommandText = cmdSQL;
			iDbCommand.CommandType = CommandType.Text;
			iDbCommand.CommandTimeout = commandTimeout;

			if (iDbTransaction != null)
				iDbCommand.Transaction = iDbTransaction;

			return iDbCommand;
		}

		public static void AddParameters(this IDbCommand iDbCommand, IDictionary<string, object> parametros)
		{
			foreach (var parametro in parametros)
			{
				var parameter = iDbCommand.CreateParameter();
				parameter.Direction = ParameterDirection.Input;
				parameter.ParameterName = "@" + parametro.Key;
				parameter.Value = parametro.Value;
				iDbCommand.Parameters.Add(parameter);
			}
		}

		public static IEnumerable<TResult> ExecuteReader<TResult>(this IDbCommand iDbCommand, Func<IDataRecord, TResult> func)
		{
			var iDataReader = iDbCommand.ExecuteReader();
			try
			{
				return ExecuteReader(iDataReader, func).ToArray();
			}
			finally
			{
				iDataReader.Close();
				iDataReader.Dispose();
				iDbCommand.Dispose();
			}
		}

		public static IEnumerable<TResult> ExecuteReader<TResult>(this IDataReader iDataReader, Func<IDataRecord, TResult> func)
		{
			while (iDataReader.Read())
				yield return func(iDataReader);
		}
	}
}
