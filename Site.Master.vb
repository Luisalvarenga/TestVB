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


        Dim nombreCompleto As String =
            Convert.ToString(
                Session("NombreCompleto")
            )


        Dim nombreUsuario As String =
            Convert.ToString(
                Session("NombreUsuario")
            )


        ' Si existe la cookie de autenticación pero
        ' la sesión se perdió, no mostrar información
        ' de un usuario que ya no tenemos identificado
        ' en la sesión.
        If String.IsNullOrWhiteSpace(nombreCompleto) AndAlso
           String.IsNullOrWhiteSpace(nombreUsuario) Then

            phUsuario.Visible = False

            Return

        End If


        phUsuario.Visible = True


        If String.IsNullOrWhiteSpace(nombreCompleto) Then

            nombreCompleto = nombreUsuario

        End If


        lblUsuario.Text =
            "Hola, " &
            Server.HtmlEncode(nombreCompleto)

    End Sub
End Class