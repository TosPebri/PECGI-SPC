Public Class Sconn
    Public Shared Function Stringkoneksi() As String
        Return ConfigurationManager.ConnectionStrings("ApplicationServices").ConnectionString
    End Function   
End Class
