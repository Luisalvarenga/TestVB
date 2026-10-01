Imports System
Imports System.Web.UI

Public Class BasePage
    Inherits Page

    Protected Overrides Sub OnInit(e As EventArgs)
        MyBase.OnInit(e)

        If Context IsNot Nothing AndAlso
           Context.Session IsNot Nothing Then

            ViewStateUserKey = Context.Session.SessionID
        End If
    End Sub
End Class