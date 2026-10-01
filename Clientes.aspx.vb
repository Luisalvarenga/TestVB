Imports System

Public Class Clientes
    Inherits BasePage

    Private ReadOnly _clienteService As ClienteService

    Public Sub New()

        _clienteService = New ClienteService()

    End Sub


    Protected Sub Page_Load(
        sender As Object,
        e As EventArgs
    ) Handles Me.Load

        If Not User.Identity.IsAuthenticated Then

            Response.Redirect("~/Login.aspx")

            Return

        End If


        If Not IsPostBack Then

            CargarInformacionUsuario()

            CargarClientes()

        End If

    End Sub


    Private Sub CargarInformacionUsuario()

        Dim nombreCompleto As String =
            Convert.ToString(
                Session("NombreCompleto")
            )

        If String.IsNullOrWhiteSpace(nombreCompleto) Then

            nombreCompleto =
                Convert.ToString(
                    Session("NombreUsuario")
                )

        End If


        lblBienvenida.Text =
            "Bienvenido, " &
            Server.HtmlEncode(nombreCompleto)

    End Sub


    Private Sub CargarClientes()

        Try

            Dim clientes As List(Of Cliente) =
                _clienteService.ObtenerClientes()

            gvClientes.DataSource = clientes

            gvClientes.DataBind()

        Catch ex As Exception

            MostrarMensaje(
                "No fue posible cargar los clientes."
            )

        End Try

    End Sub


    Protected Sub btnConfirmarEliminar_Click(
    sender As Object,
    e As EventArgs
) Handles btnConfirmarEliminar.Click

        lblMensaje.Visible = False

        Try

            Dim idCliente As Integer

            If Not Integer.TryParse(
            hfClienteEliminar.Value,
            idCliente
        ) Then

                MostrarMensaje(
                "El cliente seleccionado no es válido."
            )

                Return

            End If


            Dim idUsuario As Integer =
            Convert.ToInt32(
                Session("IdUsuario")
            )

            Dim nombreUsuario As String =
            Convert.ToString(
                Session("NombreUsuario")
            )


            _clienteService.EliminarCliente(
            idCliente,
            idUsuario,
            nombreUsuario
        )


            MostrarMensaje(
            "Cliente eliminado correctamente."
        )

            CargarClientes()

        Catch ex As ArgumentException

            MostrarMensaje(
            ex.Message
        )

        Catch ex As InvalidOperationException

            MostrarMensaje(
            ex.Message
        )

        Catch ex As Exception

            MostrarMensaje(
            "No fue posible eliminar el cliente."
        )

        End Try

    End Sub

    Private Sub MostrarMensaje(
        mensaje As String
    )

        lblMensaje.Text =
            Server.HtmlEncode(mensaje)

        lblMensaje.Visible = True

    End Sub

End Class