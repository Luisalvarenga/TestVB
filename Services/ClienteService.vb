Imports System
Imports System.Data.SqlClient

Public Class ClienteService

    Private ReadOnly _clienteRepository As ClienteRepository
    Private ReadOnly _bitacoraRepository As BitacoraRepository

    Public Sub New()

        _clienteRepository = New ClienteRepository()
        _bitacoraRepository = New BitacoraRepository()

    End Sub


    Public Function ObtenerClientes() As List(Of Cliente)

        Return _clienteRepository.ObtenerActivos()

    End Function

    Public Function BuscarClientes(
    busqueda As String
) As List(Of Cliente)

        If String.IsNullOrWhiteSpace(busqueda) Then

            Return ObtenerClientes()

        End If

        busqueda = busqueda.Trim()

        If busqueda.Length > 150 Then

            Throw New ArgumentException(
            "El texto de búsqueda es demasiado largo."
        )

        End If

        Return _clienteRepository.BuscarActivos(
        busqueda
    )

    End Function


    Public Function ObtenerCliente(idCliente As Integer) As Cliente

        If idCliente <= 0 Then
            Return Nothing
        End If

        Return _clienteRepository.ObtenerPorId(idCliente)

    End Function


    Public Function AgregarCliente(
        cliente As Cliente,
        idUsuario As Integer,
        nombreUsuario As String
    ) As Integer

        ValidarCliente(cliente)
        ValidarUsuario(idUsuario, nombreUsuario)

        If _clienteRepository.ExisteDocumento(
            cliente.Documento,
            0
        ) Then

            Throw New ArgumentException(
                "Ya existe un cliente con ese documento."
            )

        End If

        Using connection As SqlConnection =
            ConnectionFactory.CreateConnection()

            connection.Open()

            Using transaction As SqlTransaction =
                connection.BeginTransaction()

                Try

                    Dim idCliente As Integer =
                        _clienteRepository.Insertar(
                            cliente,
                            connection,
                            transaction
                        )

                    Dim bitacora As New Bitacora() With {
                        .Accion = "AGREGAR",
                        .IdCliente = idCliente,
                        .IdUsuario = idUsuario,
                        .NombreUsuario = nombreUsuario,
                        .Detalle =
                            "Cliente agregado: " &
                            cliente.Nombres.Trim() &
                            " " &
                            cliente.Apellidos.Trim()
                    }

                    _bitacoraRepository.Registrar(
                        bitacora,
                        connection,
                        transaction
                    )

                    transaction.Commit()

                    Return idCliente

                Catch

                    Try
                        transaction.Rollback()
                    Catch
                        ' No reemplazar la excepción original.
                    End Try

                    Throw

                End Try

            End Using
        End Using

    End Function


    Public Sub EditarCliente(
        cliente As Cliente,
        idUsuario As Integer,
        nombreUsuario As String
    )

        ValidarCliente(cliente)
        ValidarUsuario(idUsuario, nombreUsuario)


        If cliente.IdCliente <= 0 Then
            Throw New ArgumentException(
        "El cliente no es válido.")
        End If

        If _clienteRepository.ExisteDocumento(
            cliente.Documento,
            cliente.IdCliente
        ) Then

            Throw New ArgumentException(
                "Ya existe otro cliente con ese documento."
            )

        End If

        Using connection As SqlConnection =
            ConnectionFactory.CreateConnection()

            connection.Open()

            Using transaction As SqlTransaction =
                connection.BeginTransaction()

                Try

                    Dim actualizado As Boolean =
                        _clienteRepository.Actualizar(
                            cliente,
                            connection,
                            transaction
                        )

                    If Not actualizado Then
                        Throw New InvalidOperationException(
                            "El cliente no existe o ya fue eliminado."
                        )
                    End If

                    Dim bitacora As New Bitacora() With {
                        .Accion = "EDITAR",
                        .IdCliente = cliente.IdCliente,
                        .IdUsuario = idUsuario,
                        .NombreUsuario = nombreUsuario,
                        .Detalle =
                            "Cliente editado: " &
                            cliente.Nombres.Trim() &
                            " " &
                            cliente.Apellidos.Trim()
                    }

                    _bitacoraRepository.Registrar(
                        bitacora,
                        connection,
                        transaction
                    )

                    transaction.Commit()

                Catch

                    Try
                        transaction.Rollback()
                    Catch
                        ' No reemplazar la excepción original.
                    End Try

                    Throw

                End Try

            End Using
        End Using

    End Sub


    Public Sub EliminarCliente(
        idCliente As Integer,
        idUsuario As Integer,
        nombreUsuario As String
    )

        If idCliente <= 0 Then
            Throw New ArgumentException(
                "El cliente no es válido."
            )
        End If

        ValidarUsuario(idUsuario, nombreUsuario)

        Using connection As SqlConnection =
            ConnectionFactory.CreateConnection()

            connection.Open()

            Using transaction As SqlTransaction =
                connection.BeginTransaction()

                Try

                    Dim eliminado As Boolean =
                        _clienteRepository.EliminarLogicamente(
                            idCliente,
                            connection,
                            transaction
                        )

                    If Not eliminado Then
                        Throw New InvalidOperationException(
                            "El cliente no existe o ya fue eliminado."
                        )
                    End If

                    Dim bitacora As New Bitacora() With {
                        .Accion = "ELIMINAR",
                        .IdCliente = idCliente,
                        .IdUsuario = idUsuario,
                        .NombreUsuario = nombreUsuario,
                        .Detalle =
                            "Cliente eliminado lógicamente."
                    }

                    _bitacoraRepository.Registrar(
                        bitacora,
                        connection,
                        transaction
                    )

                    transaction.Commit()

                Catch

                    Try
                        transaction.Rollback()
                    Catch
                        ' No reemplazar la excepción original.
                    End Try

                    Throw

                End Try

            End Using
        End Using

    End Sub


    Private Sub ValidarCliente(cliente As Cliente)

        If cliente Is Nothing Then
            Throw New ArgumentNullException(
                NameOf(cliente)
            )
        End If

        cliente.Nombres = cliente.Nombres.Trim()
        cliente.Apellidos = cliente.Apellidos.Trim()
        cliente.Documento = cliente.Documento.Trim()

        If String.IsNullOrWhiteSpace(cliente.Nombres) Then
            Throw New ArgumentException(
                "Los nombres son obligatorios."
            )
        End If

        If String.IsNullOrWhiteSpace(cliente.Apellidos) Then
            Throw New ArgumentException(
                "Los apellidos son obligatorios."
            )
        End If

        If String.IsNullOrWhiteSpace(cliente.Documento) Then
            Throw New ArgumentException(
                "El documento es obligatorio."
            )
        End If

        If cliente.Nombres.Length > 100 Then
            Throw New ArgumentException(
                "Los nombres no pueden superar 100 caracteres."
            )
        End If

        If cliente.Apellidos.Length > 100 Then
            Throw New ArgumentException(
                "Los apellidos no pueden superar 100 caracteres."
            )
        End If

        If cliente.Documento.Length > 30 Then
            Throw New ArgumentException(
                "El documento no puede superar 30 caracteres."
            )
        End If

        If Not String.IsNullOrWhiteSpace(cliente.Telefono) AndAlso
           cliente.Telefono.Length > 30 Then

            Throw New ArgumentException(
                "El teléfono no puede superar 30 caracteres."
            )

        End If

        If Not String.IsNullOrWhiteSpace(cliente.Correo) AndAlso
           cliente.Correo.Length > 150 Then

            Throw New ArgumentException(
                "El correo no puede superar 150 caracteres."
            )

        End If

        If Not String.IsNullOrWhiteSpace(cliente.Correo) Then

            Try

                Dim correo As New System.Net.Mail.MailAddress(
            cliente.Correo.Trim()
        )

                If correo.Address <> cliente.Correo.Trim() Then
                    Throw New ArgumentException(
                "El correo electrónico no es válido."
            )
                End If

            Catch ex As FormatException

                Throw New ArgumentException(
            "El correo electrónico no es válido."
        )

            End Try

        End If

        If Not String.IsNullOrWhiteSpace(cliente.Direccion) AndAlso
           cliente.Direccion.Length > 250 Then

            Throw New ArgumentException(
                "La dirección no puede superar 250 caracteres."
            )

        End If

    End Sub


    Private Sub ValidarUsuario(
        idUsuario As Integer,
        nombreUsuario As String
    )

        If idUsuario <= 0 Then
            Throw New ArgumentException(
                "El usuario autenticado no es válido."
            )
        End If

        If String.IsNullOrWhiteSpace(nombreUsuario) Then
            Throw New ArgumentException(
                "El usuario autenticado no es válido."
            )
        End If

    End Sub

End Class