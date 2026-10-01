Imports System.Data
Imports System.Data.SqlClient

Public Class BitacoraRepository

    Public Sub Registrar(
        bitacora As Bitacora,
        connection As SqlConnection,
        transaction As SqlTransaction
    )

        Const sql As String = "
            INSERT INTO dbo.Bitacora
            (
                Accion,
                IdCliente,
                IdUsuario,
                NombreUsuario,
                FechaHora,
                Detalle
            )
            VALUES
            (
                @Accion,
                @IdCliente,
                @IdUsuario,
                @NombreUsuario,
                SYSUTCDATETIME(),
                @Detalle
            );
        "

        Using command As New SqlCommand(
            sql,
            connection,
            transaction
        )

            command.Parameters.Add(
                "@Accion",
                SqlDbType.NVarChar,
                20
            ).Value = bitacora.Accion

            command.Parameters.Add(
                "@IdCliente",
                SqlDbType.Int
            ).Value = bitacora.IdCliente

            command.Parameters.Add(
                "@IdUsuario",
                SqlDbType.Int
            ).Value = bitacora.IdUsuario

            command.Parameters.Add(
                "@NombreUsuario",
                SqlDbType.NVarChar,
                50
            ).Value = bitacora.NombreUsuario

            command.Parameters.Add(
                "@Detalle",
                SqlDbType.NVarChar,
                1000
            ).Value =
                If(
                    String.IsNullOrWhiteSpace(bitacora.Detalle),
                    CType(DBNull.Value, Object),
                    bitacora.Detalle
                )

            command.ExecuteNonQuery()

        End Using

    End Sub

End Class