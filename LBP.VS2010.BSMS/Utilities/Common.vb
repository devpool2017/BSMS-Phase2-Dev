Imports System.Net
Imports System.Web
Imports System.Configuration.ConfigurationManager
Imports System.IO
Imports LBP.VS2010.BSMS.Utilities.Messages.ErrorMessages
Imports System.Configuration


Public Class Common
    Shared UploadedFiles2 As List(Of FileAttachmentClassLocal)

    Public Function GetIPAddress(Optional ByVal getIPV4 As Boolean = False) As String
        Dim sIPAddress As String = HttpContext.Current.Request.ServerVariables("HTTP_X_FORWARDED_FOR")
        Dim ipAddress As String = String.Empty

        If String.IsNullOrEmpty(sIPAddress) Then
            ipAddress = HttpContext.Current.Request.ServerVariables("REMOTE_ADDR")
        End If

        If getIPV4 Then
            ipAddress = Dns.GetHostEntry(Dns.GetHostName()).AddressList _
        .Where(Function(a As IPAddress) Not a.IsIPv6LinkLocal AndAlso Not a.IsIPv6Multicast AndAlso Not a.IsIPv6SiteLocal) _
        .First() _
        .ToString()
        End If


        Return ipAddress
    End Function

    Public Function GetComuterName() As String
        Dim computerName As String = "UNKNOWN HOST"
        Try
            computerName = Net.Dns.GetHostEntry(HttpContext.Current.Request.ServerVariables("remote_addr")).HostName.ToString().ToUpper()
        Catch ex As Exception

        End Try

        Return computerName
    End Function

    Shared Function GetFileStoragePath2(ByVal moduleID As String, ByVal formID As String) As List(Of String)
        Dim reponseData As New List(Of String)
        Dim logonUser = HttpContext.Current.Session("LogonUser")
        Dim rootPathTemp As String = ""
        Dim rootPath As String = ""

        Try
            Dim fileStorageLocation As String = String.Empty

            If CBool(GetSection("Commons")("isInDevelopmentPhase")) Then
                rootPathTemp = GetSection("Commons")("FileAttachmentPath").ToString + "\\TempFiles\\" + logonUser
                rootPath = GetSection("Commons")("FileAttachmentPath").ToString + "\\" + GetModuleShortName(moduleID) + "\\" + formID

            Else
                rootPathTemp = HttpContext.Current.Server.MapPath("~/" & "FileAttachments\TempFiles\" + logonUser)
                rootPath = HttpContext.Current.Server.MapPath("~/" & "FileAttachments\" + GetModuleShortName(moduleID) + "\" + formID)
            End If

            reponseData.Add(rootPathTemp)
            reponseData.Add(rootPath)

            If Not Directory.Exists(reponseData(0)) Then
                Directory.CreateDirectory(reponseData(0))
            End If
            If Not Directory.Exists(reponseData(1)) Then
                Directory.CreateDirectory(reponseData(1))
            End If

        Catch ex As Exception
            Logger.writeLog("Creating Directories", "", "", ex.Message)
        End Try

        Return reponseData
    End Function

    Public Function GetSubmenuTabs() As List(Of NavbarItem)
        Dim navbarItems As New List(Of NavbarItem)()
        Dim subMenuTabs = HttpContext.Current.Session("systemMenusTab")
        Dim submenuID = HttpContext.Current.Session("SubMenuId")
        Dim mainSubmenuID As String = submenuID
        Dim mainSubmenuIDSplit As String() = submenuID.Split(".")
        If mainSubmenuIDSplit.Length > 0 Then
            mainSubmenuID = mainSubmenuIDSplit(0)
        End If

        If Not IsNothing(subMenuTabs) Then
            navbarItems = New List(Of NavbarItem)()
            For Each row As DataRow In subMenuTabs.Rows
                If (row.Item("SubMenuID").ToString = mainSubmenuID.ToString) Then
                    navbarItems.Add(New NavbarItem() With {.Title = row.Item("TabName").ToString, .Url = row.Item("Url").ToString, .TabID = row.Item("SubMenuID").ToString + "." + row.Item("TabID").ToString})
                End If
            Next
        End If

        Return navbarItems
    End Function

    Shared Function ValidateFileAttachment(ByVal filename As String, ByVal fileSize As String, ByVal fileData As Object) As List(Of String)
        Dim reponseData As New List(Of String)

        Try
            Dim extensionString As String = GetSection("Commons")("FileTypes").ToString()
            Dim extensionArray As String() = extensionString.Split("|"c)
            Dim validExtensions As String() = extensionArray.Select(Function(ext) ext.Trim()).ToArray()
            Dim fileExtension As String = Path.GetExtension(filename)

            If filename.Length < 100 Then
                If validExtensions.Contains(fileExtension.ToLower()) Then
                    Dim binaryData As Byte() = Convert.FromBase64String(fileData.ToString())
                    Dim fileSizeInBytes As Integer = binaryData.Length
                    Dim configMaxLength As Integer = GetSection("Commons")("MaxFileSizeInMB")
                    Dim maxAllowedSize As Integer = configMaxLength * 1024 * 1024

                    If fileSizeInBytes > 0 And fileSize > 0 Then
                        If fileSizeInBytes <= maxAllowedSize And fileSize <= maxAllowedSize Then
                            reponseData.Add(True)
                            reponseData.Add("")
                        Else
                            reponseData.Add(False)
                            Dim tempMessage As String = configMaxLength.ToString + "MB"
                            reponseData.Add(Validation.getErrorMessage(Validation.ErrorType.VALUE_TOO_LARGE, "File attachment", {tempMessage}))
                        End If
                    Else
                        reponseData.Add(False)
                        reponseData.Add(Validation.getErrorMessage(Validation.ErrorType.EMPTY, "File attachment", {}))
                    End If

                Else
                    reponseData.Add(False)
                    reponseData.Add(Validation.getErrorMessage(Validation.ErrorType.NOTHING_SELECTED, "attchment with valid file type", {}))
                End If
            Else
                reponseData.Add(False)
                reponseData.Add(Validation.getErrorMessage(Validation.ErrorType.TOO_LONG, "File name", {"100"}))
            End If

        Catch ex As Exception
            reponseData.Add(False)
            reponseData.Add(Validation.getErrorMessage(Validation.ErrorType.INVALID_DATA, "File attachment", {}))
            Logger.writeLog("Validate file attachement", "", "", ex.Message)
        End Try

        Return reponseData
    End Function

    Shared Function GetModuleShortName(ByVal moduleID As String) As String
        Dim response As String = "NoModule"
        Select Case moduleID
            Case "9"
                response = "GenerateChangeID"
            Case "10"
                response = "WIF"
            Case "11"
                response = "ACRF"
            Case "12"
                response = "SMR"
            Case "13"
                response = "PMR"
        End Select

        Return response
    End Function

    Shared Function SaveFileAttachmentToTemp(ByVal fileID As String, ByVal fileData As Object, ByVal moduleID As String, ByVal formID As String) As List(Of String)
        Dim reponseData As New List(Of String)
        Dim logonUser = HttpContext.Current.Session("LogonUser")
        Dim fileDataBytes As Byte() = Convert.FromBase64String(fileData.ToString())
        Dim fileStoragePaths As List(Of String) = GetFileStoragePath2(moduleID, formID)

        Try
            File.WriteAllBytes(Path.Combine(fileStoragePaths(0), fileID.ToString()), fileDataBytes)
        Catch ex As Exception
            reponseData.Add(False)
            reponseData.Add(Validation.getErrorMessage(Validation.ErrorType.INVALID_DATA, "File attachment", {}))
            Logger.writeLog("Validate file attachement", "", "", ex.Message)
        End Try

        Return reponseData
    End Function

    Shared Function DownloadFileAttachment(ByVal fileID As String, ByVal fileName As String, ByVal fileStatus As Integer, ByVal moduleID As String, ByVal formNo As String) As List(Of String)
        Dim reponseData As New List(Of String)
        Dim logonUser = HttpContext.Current.Session("LogonUser")
        Dim fileStoragePaths As List(Of String) = GetFileStoragePath2(moduleID, formNo)

        Try
            Dim rootPath As String = String.Empty

            If fileStatus = 0 Then
                rootPath = fileStoragePaths(0)
            Else
                rootPath = fileStoragePaths(1)
            End If

            Dim filePath As String = Path.Combine(rootPath, fileID)

            If File.Exists(filePath) Then
                Dim fileBytes As Byte() = File.ReadAllBytes(filePath)

                reponseData.Add("")
                reponseData.Add(Convert.ToBase64String(fileBytes) + "|||" + fileName)
            Else
                reponseData.Add(ConfigurationManager.GetSection("Messages")("NoFileFound").ToString().Replace("[file name]", fileName))
                reponseData.Add("")
            End If

        Catch ex As Exception
            reponseData.Add(Messages.ErrorMessages.Validation.getErrorMessage(Messages.ErrorMessages.Validation.ErrorType.SAVE_ERROR, "during file download", {}))
            reponseData.Add("")
            Logger.writeLog("Download file attachement", "", "", ex.Message)
        End Try

        Return reponseData
    End Function

    Shared Function TransferAttachment(ByVal formID As String, ByVal moduleID As String) As Boolean
        Try
            Dim filePath As String = String.Empty
            Dim fileTempPath As String = String.Empty
            Dim logonUser = HttpContext.Current.Session("LogonUser")
            Dim fileStoragePaths As List(Of String) = GetFileStoragePath2(moduleID, formID)

            filePath = fileStoragePaths(1)
            fileTempPath = fileStoragePaths(0)

            Dim di As New DirectoryInfo(filePath)
            Dim diTemp As New DirectoryInfo(fileTempPath)

            If Not di.Exists() Then
                di.Create()
            Else
                Dim fiArr As FileInfo() = di.GetFiles()
                Dim fri As FileInfo

                For Each fri In fiArr
                    Dim attachment As FileAttachmentClassLocal = UploadedFiles2.FirstOrDefault(Function(a) a.FileID = fri.Name)

                    If attachment Is Nothing Then
                        File.Delete(fri.FullName)
                    End If
                Next fri
            End If

            If diTemp.Exists Then
                Dim fiArrTemp As FileInfo() = diTemp.GetFiles()

                For Each friTemp In fiArrTemp
                    Dim fileName As String = friTemp.Name
                    Dim destinationFilePath As String = Path.Combine(di.ToString, fileName)

                    File.Move(friTemp.FullName, destinationFilePath)
                Next friTemp
            End If

            Return True
        Catch ex As Exception
            Logger.writeLog(GetModuleShortName(moduleID), "TransferAttachment", "TransferAttachment", ex.Message)

            Return False
        End Try
    End Function

    ''THIS MUST BE REMOVE ONCE ALL WHO REFERENCED TO THIS FUNCTION CHANGED THE FILE HANDLING PROCESS TO NEW
    Shared Sub CreateStoragePath(fileStorageLocation As String)
        Try
            If CBool(GetSection("Commons")("isInDevelopmentPhase")) Then
                If (Not Directory.Exists(fileStorageLocation)) Then
                    Directory.CreateDirectory(fileStorageLocation)
                End If
            Else
                Dim virtualDirectoryName1 As String = "Files"
                fileStorageLocation = HttpContext.Current.Server.MapPath("~/" & "FileAttachments\" & virtualDirectoryName1)
                If Directory.Exists(fileStorageLocation) Then
                    Directory.CreateDirectory(fileStorageLocation)
                End If
            End If
        Catch ex As Exception
            Logger.writeLog("Creating Directories", "", "", ex.Message)
        End Try
    End Sub

End Class


Public Class NavbarItem
    Public Property Title As String
    Public Property Url As String
    Public Property TabID As String
End Class

Public Class FileAttachmentClassLocal
    Property ModuleID As String
    Property FormID As String
    Property TabID As String
    Property OtherDeterminant As String
    Property FileID As String
    Property FileName As String
    Property FileSize As String
    Property FileStatus As Integer = 0
End Class