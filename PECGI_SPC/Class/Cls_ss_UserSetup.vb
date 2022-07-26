Public Class Cls_ss_UserSetup
    Public Property AppID As String
    Public Property UserID As String
    Public Property CCCode As String
    Public Property Company As String
    Public Property Name As String
    Public Property Password As String
    Public Property Description As String
    Public Property LockedCls As String
    Public Property InvalidLogin As String
    Public Property StatusAdminCls As String
    Public Property LastLogin As String
    Public Property RegisterDate As String
    Public Property LastUpdate As String
    Public Property JAI As String
    Public Property PASIAW As String
    Public Property PASIWH As String
    Public Property PEMI As String
    Public Property SAI As String
    Public Property SAMIJF As String
    Public Property SAMITUGU As String
    Public Property SUAI As String
    Public Property userLock As String
    Public Property UserName As String
    Public Property UserType As String
    Public Property Locked As String
    Public Property AdminStatus As String
    Public Property SecurityQuestion As String
    Public Property SecurityAnswer As String
    Public Property FirstLoginFlag As String
    Public Property FailedLogin As String
    Public Property PasswordHint As String
    Public Property CreateDate As String
    Public Property CreateUser As String
    Public Property UpdateDate As String
    Public Property UpdateUser As String
    Public ReadOnly Property Privileges As String
        Get
            Return "Privileges"
        End Get
    End Property
End Class
