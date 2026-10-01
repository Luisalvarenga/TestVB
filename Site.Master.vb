Imports System
Imports Microsoft.VisualBasic.ApplicationServices

Public Class SiteMaster
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(
        sender As Object,
        e As EventArgs
    ) Handles Me.Load

        If Not IsPostBack Then

            CargarUsuario()

        End If

    End Sub


    Private Sub CargarUsuario()

        If Not Context.User.Identity.IsAuthenticated Then
            phUsuario.Visible = False

            Return

        End If


        phUsuario.Visible = True


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


        lblUsuario.Text =
            "Hola, " &
            Server.HtmlEncode(nombreCompleto)

    End Sub

End Class