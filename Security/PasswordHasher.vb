Imports System
Imports System.Security.Cryptography

Public NotInheritable Class PasswordHasher

    Private Const Iterations As Integer = 100000
    Private Const SaltSize As Integer = 16
    Private Const HashSize As Integer = 32

    Private Sub New()
    End Sub

    Public Shared Function HashPassword(
        password As String
    ) As String

        If String.IsNullOrEmpty(password) Then
            Throw New ArgumentException(
                "La contraseña no puede estar vacía.",
                NameOf(password)
            )
        End If

        Dim salt(SaltSize - 1) As Byte

        Using random As RandomNumberGenerator =
            RandomNumberGenerator.Create()

            random.GetBytes(salt)

        End Using

        Dim hash As Byte()

        Using pbkdf2 As New Rfc2898DeriveBytes(
            password,
            salt,
            Iterations,
            HashAlgorithmName.SHA256
        )

            hash = pbkdf2.GetBytes(HashSize)

        End Using

        Return String.Join(
            ".",
            Iterations.ToString(),
            Convert.ToBase64String(salt),
            Convert.ToBase64String(hash)
        )

    End Function


    Public Shared Function VerifyPassword(
        password As String,
        storedHash As String
    ) As Boolean

        If String.IsNullOrEmpty(password) Then
            Return False
        End If

        If String.IsNullOrWhiteSpace(storedHash) Then
            Return False
        End If

        Dim parts As String() =
            storedHash.Split("."c)

        If parts.Length <> 3 Then
            Return False
        End If

        Dim iterations As Integer

        If Not Integer.TryParse(
            parts(0),
            iterations
        ) Then
            Return False
        End If

        If iterations <= 0 Then
            Return False
        End If

        Dim salt As Byte()
        Dim expectedHash As Byte()

        Try

            salt = Convert.FromBase64String(parts(1))
            expectedHash = Convert.FromBase64String(parts(2))

        Catch ex As FormatException

            Return False

        End Try

        Dim actualHash As Byte()

        Using pbkdf2 As New Rfc2898DeriveBytes(
            password,
            salt,
            iterations,
            HashAlgorithmName.SHA256
        )

            actualHash = pbkdf2.GetBytes(
                expectedHash.Length
            )

        End Using

        Return CompararEnTiempoConstante(
    actualHash,
    expectedHash
)

    End Function

    Private Shared Function CompararEnTiempoConstante(
    a As Byte(),
    b As Byte()
) As Boolean

        If a Is Nothing OrElse b Is Nothing Then
            Return False
        End If

        If a.Length <> b.Length Then
            Return False
        End If

        Dim diferencia As Integer = 0

        For i As Integer = 0 To a.Length - 1
            diferencia = diferencia Or (a(i) Xor b(i))
        Next

        Return diferencia = 0

    End Function

End Class