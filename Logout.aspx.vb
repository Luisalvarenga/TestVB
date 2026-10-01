Imports System
Imports System.Web
Imports System.Web.Security

Public Class Logout
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(
        sender As Object,
        e As EventArgs
    ) Handles Me.Load

        CerrarSesion()

    End Sub


    Private Sub CerrarSesion()

        FormsAuthentication.SignOut()

        Session.Clear()
        Session.Abandon()

        Response.Cache.SetCacheability(
            HttpCacheability.NoCache
        )

        Response.Cache.SetNoStore()

        Response.Cache.SetExpires(
            DateTime.UtcNow.AddYears(-1)
        )

        Response.Redirect(
            "~/Login.aspx",
            False
        )

        Context.ApplicationInstance.CompleteRequest()

    End Sub

End Class