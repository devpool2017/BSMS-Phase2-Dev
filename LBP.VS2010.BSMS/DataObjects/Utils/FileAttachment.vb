Public Class FileAttachment
    Inherits MasterDO

    Property ModuleID As String
    Property FormID As String
    Property TabID As String
    Property OtherDeterminant As String
    Property FileID As String
    Property FileName As String
    Property FileSize As String
    Property FileStatus As Integer = 0

    Partial Class DownloadResult
        Property Data As String = String.Empty
        Property Message As String
    End Class
End Class
