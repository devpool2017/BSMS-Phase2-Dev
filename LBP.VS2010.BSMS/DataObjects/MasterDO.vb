Imports System.Reflection
Imports LBP.VS2010.BSMS.CustomValidators
Imports System.ComponentModel.DataAnnotations
Imports LBP.VS2010.BSMS.CustomValidators.MasterValidator
Imports LBP.VS2010.BSMS.Utilities.PageControlVariables
Imports LBP.VS2010.BSMS.CustomValidators.LengthAndValueValidator
Imports LBP.VS2010.BSMS.CustomValidators.DataType
Imports LBP.VS2010.BSMS.CustomValidators.DataType.TypeDate
Imports LBP.VS2010.BSMS.Utilities.Messages.ErrorMessages.Validation
Imports LBP.VS2010.BSMS.Utilities.Messages.ErrorMessages
Imports System.Text
Imports System.IO
Imports System.Runtime.Serialization.Formatters.Binary


<Serializable()> _
Public Class MasterDO

    'Make Class Cloneable
    Public Function Clone() As Object
        Dim mStream As New MemoryStream
        Dim formatter As New BinaryFormatter
        formatter.Serialize(mStream, Me)
        mStream.Seek(0, SeekOrigin.Begin)
        Return formatter.Deserialize(mStream)
    End Function

    '<DeclaredClass()>
    '<RequiredField(RequiredField.ControlType.DECLARED_CLASS)>
    Property Access As ProfileDO

    Property errMsg As String = String.Empty
    Const _NEWLINE As String = "<br />"

    Sub New()
        instantiateDeclaredClasses()
    End Sub

    Sub New(ByVal dr As DataRow, ByVal columnNames As DataColumnCollection)
        MapDataColumns(dr, columnNames)

        Access = ProfileDO.GetDefaultAccess(dr)
    End Sub
    Sub New(ByVal dr As DataRow, ByVal columnNames As DataColumnCollection, ByVal accessObj As ProfileDO)
        MapDataColumns(dr, columnNames)

        Access = accessObj
    End Sub
    Public Sub MapDataColumns(ByVal dr As DataRow, ByVal columnNames As DataColumnCollection)
        ' get all properties with Attribute DataColumnMapping
        Dim columnPropertyMapper As New Dictionary(Of String, PropertyInfo)
        Dim properties As IEnumerable(Of PropertyInfo) = Me.GetType.GetProperties().Where(Function(prop) prop.IsDefined(GetType(DataColumnMapping), False))

        Dim dcColumnPropertyMapper As Dictionary(Of String, PropertyInfo)
        Dim dcPropertyInfo As IEnumerable(Of PropertyInfo)
        Dim declaredClassProperties As IEnumerable(Of PropertyInfo) = Me.GetType.GetProperties().Where(Function(prop) prop.IsDefined(GetType(DeclaredClass), False))

        For Each prop In properties
            columnPropertyMapper.Add(CType(Attribute.GetCustomAttribute(prop, GetType(DataColumnMapping)), DataColumnMapping).columnField, prop)
        Next

        For Each dtColumn In columnNames
            Try
                columnPropertyMapper(dtColumn.ToString()).SetValue(Me, dr(dtColumn.ToString()).ToString, Nothing)
            Catch ex As Exception

            End Try
        Next

        For Each dc In declaredClassProperties
            Dim dcColumns As New List(Of String)

            For Each dtColumn In columnNames
                If dtColumn.ToString.Contains("." + dc.Name) Then
                    dcColumns.Add(dtColumn.ToString)
                End If
            Next

            If dcColumns.Count > 0 Then
                dcColumnPropertyMapper = New Dictionary(Of String, PropertyInfo)

                dcPropertyInfo = dc.PropertyType.GetProperties().Where(Function(prop) prop.IsDefined(GetType(DataColumnMapping), False))

                For Each dcProp In dcPropertyInfo
                    dcColumnPropertyMapper.Add(CType(Attribute.GetCustomAttribute(dcProp, GetType(DataColumnMapping)), DataColumnMapping).columnField + "." + dc.Name, dcProp)
                Next

                Try
                    Dim dummyClass As Object

                    dummyClass = Activator.CreateInstance(dc.PropertyType)

                    For Each dcCol In dcColumns
                        Try
                            dcColumnPropertyMapper(dcCol.ToString()).SetValue(dummyClass, dr(dcCol.ToString()), Nothing)
                        Catch ex As Exception

                        End Try
                    Next

                    dc.SetValue(Me, dummyClass, Nothing)
                Catch ex As Exception

                End Try
            End If


        Next
    End Sub

    Public Sub instantiateDeclaredClasses()
        Dim properties As IEnumerable(Of PropertyInfo) = Me.GetType.GetProperties().Where(Function(prop) prop.IsDefined(GetType(DeclaredClass), False))

        For Each dc In properties
            Dim dummyClass As Object

            dummyClass = Activator.CreateInstance(dc.PropertyType)

            dc.SetValue(Me, dummyClass, Nothing)
        Next

    End Sub

    Property isSuccess As Boolean = False
    Property objErrorMessage As String = ""
    Property objSuccessMessage As String = ""
    Property redirectPath As String = String.Empty
#Region "GENERIC VALIDATION"
    Public Overridable Function isValid() As Boolean
        Dim success As Boolean = True
        errMsg = String.Empty


        For Each prop In Me.GetType.GetProperties

            If Not validateProperty(prop.Name) Then
                success = False
            End If
        Next

        Return success
    End Function
#End Region
#Region "PROPERTY VALIDATION"
    Public Function validateProperty(ByVal propertyName As String) As Boolean
        Dim success As Boolean = True

        Dim prop As PropertyInfo = Me.GetType().GetProperty(propertyName)

        'get the displayLabel attribute
        Dim fieldLabelAttr As DisplayLabel = CType(Attribute.GetCustomAttribute(prop, GetType(DisplayLabel)), DisplayLabel)
        Dim fieldLabel As String = String.Empty

        If Not IsNothing(fieldLabelAttr) Then
            fieldLabel = fieldLabelAttr.fieldName
        End If

        'get the controlvariable
        Dim controlVariableAttr As ControlVariable = CType(Attribute.GetCustomAttribute(prop, GetType(ControlVariable)), ControlVariable)
        Dim controlVariable As FieldVariables = Nothing

        If Not IsNothing(controlVariableAttr) Then
            controlVariable = controlVariableAttr.fieldControlVariable
        End If

        '1. validate required field
        Dim isPropertyRequired As Boolean = False
        Dim isPropertyRequiredValid As Boolean = True

        Dim requiredFieldAttr As RequiredField = CType(Attribute.GetCustomAttribute(prop, GetType(RequiredField)), RequiredField)

        Dim requiredCondtionalAttr As RequiredConditional = CType(Attribute.GetCustomAttribute(prop, GetType(RequiredConditional)), RequiredConditional)

        Dim dependencyValidation As Boolean = False

        If Not IsNothing(requiredCondtionalAttr) Then
            If requiredCondtionalAttr.TypeId.name = "HasNoValue" Then

                If requiredCondtionalAttr.dependentPropertyNameList.Length > 0 Then

                    For Each dependentPropertyName As String In requiredCondtionalAttr.dependentPropertyNameList
                        dependencyValidation = RequiredConditional.HasNoValue.needToValidate(Me.GetType().GetProperty(dependentPropertyName).GetValue(Me, Nothing), requiredCondtionalAttr.compareValues)

                        If Not dependencyValidation Then
                            Exit For
                        End If
                    Next
                Else
                    dependencyValidation = RequiredConditional.HasNoValue.needToValidate(Me.GetType().GetProperty(requiredCondtionalAttr.dependentPropertyName).GetValue(Me, Nothing), requiredCondtionalAttr.compareValues)
                End If

            Else
                'Check if dependentPropertyName is a property of Me Class or a property of a class property of Me Class
                Dim dependentTypes As String() = requiredCondtionalAttr.dependentPropertyName.Split(".")
                Dim dependentProp As PropertyInfo = Me.GetType().GetProperty(dependentTypes(0))
                If dependentTypes.Length = 1 Then
                    'dependencyValidation = RequiredConditional.needToValidate(Me.GetType().GetProperty(requiredCondtionalAttr.dependentPropertyName).GetValue(Me, Nothing), requiredCondtionalAttr.compareValues)
                    dependencyValidation = RequiredConditional.needToValidate(dependentProp.GetValue(Me, Nothing), requiredCondtionalAttr.compareValues)
                Else
                    Dim dependentClass As Object = Activator.CreateInstance(dependentProp.PropertyType)
                    dependentClass = dependentProp.GetValue(Me, Nothing)
                    Dim dependentClassProp As PropertyInfo = dependentClass.GetType().GetProperty(dependentTypes(1))
                    dependencyValidation = RequiredConditional.needToValidate(dependentClassProp.GetValue(dependentClass, Nothing), requiredCondtionalAttr.compareValues)
                End If
            End If


            'if requiredFieldAttr is nothing then override it w/ the requried field inside requiredConditional attribute

            If dependencyValidation = True And IsNothing(requiredFieldAttr) Then
                requiredFieldAttr = requiredCondtionalAttr.reqField
            End If
        End If

        If Not IsNothing(requiredFieldAttr) Or dependencyValidation = True Then
            isPropertyRequired = True

            Dim reqValidated As Boolean = True
            Dim propIsDeclaredClass As Boolean = False
            Dim mirrorClass As New Object


            If requiredFieldAttr.fieldControlType = RequiredField.ControlType.DECLARED_CLASS Then
                propIsDeclaredClass = True
            End If

            If propIsDeclaredClass Then ' SINCE PROP IS A CLASS, CALL ITS OWN ISVALID FUNCTION
                mirrorClass = Activator.CreateInstance(prop.PropertyType)

                Try
                    Dim propType As Type = prop.PropertyType

                    mirrorClass = prop.GetValue(Me, Nothing)

                    reqValidated = CallByName(mirrorClass, "isValid", CallType.Method)
                Catch ex As Exception
                    Dim cute As String
                    cute = ex.Message
                End Try

            Else ' NORMAL FIELD VALIDATION
                If requiredFieldAttr.fieldControlType = RequiredField.ControlType.LIST Then
                    Try
                        reqValidated = RequiredField.validate(prop.GetValue(Me, Nothing), requiredFieldAttr.minCount)
                    Catch ex As Exception
                        Dim err As String
                        err = ex.Message
                    End Try

                Else
                    reqValidated = RequiredField.validate(prop.GetValue(Me, Nothing))
                End If
            End If

            If Not reqValidated Then
                isPropertyRequiredValid = False

                success = False

                If propIsDeclaredClass Then
                    errMsg = errMsg + mirrorClass.GetType.GetProperty("errMsg").GetValue(mirrorClass, Nothing)
                Else
                    errMsg = errMsg + RequiredField.getErrorMessage(fieldLabel, requiredFieldAttr.fieldControlType, requiredFieldAttr.minCount) + _NEWLINE
                End If

            End If
        End If



        'if property is required and it fails validation then do not validate further
        If isPropertyRequired And Not isPropertyRequiredValid Then
            Return False
        End If

        Dim propertyValidationSuccess As Boolean = True

        '2. validate length
        Dim maxLenAttr As MaxLengthCheck = CType(Attribute.GetCustomAttribute(prop, GetType(MaxLengthCheck)), MaxLengthCheck)

        If Not IsNothing(maxLenAttr) Then
            If Not MaxLengthCheck.validate(prop.GetValue(Me, Nothing), controlVariable, LengthSetting.MAX, ValidatorSetting.LENGTH) Then
                success = False
                propertyValidationSuccess = False

                errMsg = errMsg + MaxLengthCheck.getErrorMessage(fieldLabel, controlVariable, LengthSetting.MAX, ValidatorSetting.LENGTH) + _NEWLINE
            End If
        End If

        Dim minLenAttr As MinLengthCheck = CType(Attribute.GetCustomAttribute(prop, GetType(MinLengthCheck)), MinLengthCheck)

        If Not IsNothing(minLenAttr) Then
            If Not MinLengthCheck.validate(prop.GetValue(Me, Nothing), controlVariable, LengthSetting.MIN, ValidatorSetting.LENGTH) Then
                success = False
                propertyValidationSuccess = False

                errMsg = errMsg + MinLengthCheck.getErrorMessage(fieldLabel, controlVariable, LengthSetting.MIN, ValidatorSetting.LENGTH) + _NEWLINE
            End If
        End If

        '3. validate datatype

        ' numeric
        Dim validateValues As Boolean = False

        Dim numericAttr As TypeNumeric = CType(Attribute.GetCustomAttribute(prop, GetType(TypeNumeric)), TypeNumeric)

        If Not IsNothing(numericAttr) Then
            If TypeNumeric.validate(prop.GetValue(Me, Nothing)) Then
                validateValues = True
            Else
                success = False
                propertyValidationSuccess = False

                errMsg = errMsg + TypeNumeric.getErrorMessage(fieldLabel) + _NEWLINE
            End If
        End If

        ' decimal
        Dim decimalAttr As TypeDecimal = CType(Attribute.GetCustomAttribute(prop, GetType(TypeDecimal)), TypeDecimal)

        If Not IsNothing(decimalAttr) Then
            If TypeDecimal.validate(prop.GetValue(Me, Nothing)) Then
                validateValues = True
            Else
                success = False
                propertyValidationSuccess = False

                errMsg = errMsg + TypeDecimal.getErrorMessage(fieldLabel) + _NEWLINE
            End If
        End If

        ' check max
        If validateValues Then
            Dim minValAttr As MinValueCheck = CType(Attribute.GetCustomAttribute(prop, GetType(MinValueCheck)), MinValueCheck)

            If Not IsNothing(minValAttr) Then
                If Not MinValueCheck.validate(prop.GetValue(Me, Nothing), minValAttr.minValueControl, LengthSetting.MIN, ValidatorSetting.VALUE) Then
                    success = False
                    propertyValidationSuccess = False

                    errMsg = errMsg + MinValueCheck.getErrorMessage(fieldLabel, minValAttr.minValueControl, LengthSetting.MIN, ValidatorSetting.VALUE) + _NEWLINE
                End If
            End If


            Dim maxValAttr As MaxValueCheck = CType(Attribute.GetCustomAttribute(prop, GetType(MaxValueCheck)), MaxValueCheck)

            If Not IsNothing(maxValAttr) Then
                If Not MaxValueCheck.validate(prop.GetValue(Me, Nothing), maxValAttr.maxValueControl, LengthSetting.MAX, ValidatorSetting.VALUE) Then
                    success = False
                    propertyValidationSuccess = False

                    errMsg = errMsg + MaxValueCheck.getErrorMessage(fieldLabel, maxValAttr.maxValueControl, LengthSetting.MAX, ValidatorSetting.VALUE) + _NEWLINE
                End If
            End If
        End If
        ' email
        Dim emailAttr As TypeEmail = CType(Attribute.GetCustomAttribute(prop, GetType(TypeEmail)), TypeEmail)

        If Not IsNothing(emailAttr) Then
            If Not TypeEmail.validate(prop.GetValue(Me, Nothing)) Then
                success = False
                propertyValidationSuccess = False

                errMsg = errMsg + TypeEmail.getErrorMessage(fieldLabel) + _NEWLINE
            End If
        End If

        ' password
        Dim passwordAttr As TypePassword = CType(Attribute.GetCustomAttribute(prop, GetType(TypePassword)), TypePassword)

        If Not IsNothing(passwordAttr) Then
            If Not TypePassword.validate(prop.GetValue(Me, Nothing)) Then
                success = False
                propertyValidationSuccess = False

                errMsg = errMsg + TypePassword.getErrorMessage(fieldLabel) + _NEWLINE
            End If
        End If

        ' date
        Dim dateAttr As TypeDate = CType(Attribute.GetCustomAttribute(prop, GetType(TypeDate)), TypeDate)

        If Not IsNothing(dateAttr) Then
            If TypeDate.validate(prop.GetValue(Me, Nothing)) Then
                ' if valid date check if there are existing date types

                ' no Back Date
                Dim noBackDateAttr As NoBackDate = CType(Attribute.GetCustomAttribute(prop, GetType(NoBackDate)), NoBackDate)

                If Not IsNothing(noBackDateAttr) Then
                    If Not NoBackDate.validate(prop.GetValue(Me, Nothing)) Then
                        success = False
                        propertyValidationSuccess = False

                        errMsg = errMsg + NoBackDate.getErrorMessage(fieldLabel) + _NEWLINE
                    End If
                End If

                ' no Forward Date
                Dim noForwardDateAttr As NoForwardDate = CType(Attribute.GetCustomAttribute(prop, GetType(NoForwardDate)), NoForwardDate)

                If Not IsNothing(noForwardDateAttr) Then
                    If Not NoForwardDate.validate(prop.GetValue(Me, Nothing), noForwardDateAttr.allowCurrentDate) Then
                        success = False
                        propertyValidationSuccess = False

                        errMsg = errMsg + NoForwardDate.getErrorMessage(fieldLabel, noForwardDateAttr.allowCurrentDate) + _NEWLINE
                    End If
                End If

            Else
                success = False
                propertyValidationSuccess = False

                errMsg = errMsg + TypeDate.getErrorMessage(fieldLabel) + _NEWLINE
            End If
        End If

        '4. check if value is included in list of allowed values - perform only if previous validations are satisfied

        If propertyValidationSuccess Then
            Dim allowedValuesAttr As AllowedValues = CType(Attribute.GetCustomAttribute(prop, GetType(AllowedValues)), AllowedValues)

            If Not IsNothing(allowedValuesAttr) Then
                If Not AllowedValues.validate(prop.GetValue(Me, Nothing), allowedValuesAttr.allowedValuesList) Then
                    success = False
                    propertyValidationSuccess = False

                    errMsg = errMsg + Validation.getErrorMessage(Validation.ErrorType.INVALID_DATA, fieldLabel, Nothing) + _NEWLINE
                End If
            End If
        End If

        '5. call special valdiation routines - perform only if previous validations are satisfied

        If propertyValidationSuccess Then
            Dim customValidationAttr As CustomValidationMethod = CType(Attribute.GetCustomAttribute(prop, GetType(CustomValidationMethod)), CustomValidationMethod)

            If Not IsNothing(customValidationAttr) Then
                Try
                    If Not CallByName(Me, customValidationAttr.functionToCall, CallType.Method, fieldLabel) Then
                        success = False
                    End If
                Catch ex As Exception
                    Dim err As String
                    err = ex.Message
                End Try

            End If
        End If

        Return success
    End Function

    
#End Region


#Region "STRINGS VALIDATION"
    Friend Function IsTextEmpty(ByVal stringValue As String, ByVal fieldName As String) As Boolean
        If String.IsNullOrWhiteSpace(stringValue) Then
            objErrorMessage += Insertbreak() + Validation.getErrorMessage(ErrorType.EMPTY, fieldName, Nothing)
            Return True
        End If

        Return False
    End Function

    Friend Function IsSelectedEmpty(ByVal stringValue As String, ByVal fieldName As String) As Boolean
        If String.IsNullOrWhiteSpace(stringValue) Then
            objErrorMessage += Insertbreak() + Validation.getErrorMessage(ErrorType.NOTHING_SELECTED, fieldName, Nothing)
            Return True
        End If

        Return False
    End Function

    Friend Function IsTooShort(ByVal stringValue As String, ByVal fieldName As String, ByVal fieldVar As FieldVariables) As Boolean
        If Not String.IsNullOrWhiteSpace(getFieldLength(fieldVar, LengthSetting.MIN)) Then
            If stringValue.Length < getFieldLength(fieldVar, LengthSetting.MIN) Then
                objErrorMessage += Insertbreak() + Validation.getErrorMessage(ErrorType.TOO_SHORT, fieldName, {getFieldLength(fieldVar, LengthSetting.MIN)})
                Return True
            End If
        End If

        Return False
    End Function

    Friend Function IsTooLong(ByVal stringValue As String, ByVal fieldName As String, ByVal fieldVar As FieldVariables) As Boolean
        If Not String.IsNullOrWhiteSpace(getFieldLength(fieldVar, LengthSetting.MAX)) Then
            If stringValue.Length > getFieldLength(fieldVar, LengthSetting.MAX) Then
                objErrorMessage += Insertbreak() + Validation.getErrorMessage(ErrorType.TOO_LONG, fieldName, {getFieldLength(fieldVar, LengthSetting.MAX)})
                Return True
            End If
        End If

        Return False
    End Function
#End Region

#Region "LISTING"
    ' LIST OF GENERIC TO DATATABLE
    Shared Function listRecordsToTable(Of genericType)(ByVal objList As List(Of genericType), Optional ByVal sortFilter As String = "", Optional ByVal sortDirection As String = "ASC") As DataTable
        Dim dt As New DataTable
        Dim sortColumn As String = String.Empty
        'create the dt columns based on DataColumMappings of passed Class

        Dim properties As IEnumerable(Of PropertyInfo) = GetType(genericType).GetProperties() _
                                                                            .Where(Function(prop) prop.IsDefined(GetType(DataColumnMapping), False)) _
                                                                            .OrderBy(Function(prop) prop.MetadataToken)

        For Each prop In properties
            dt.Columns.Add(CType(Attribute.GetCustomAttribute(prop, GetType(DataColumnMapping)), DataColumnMapping).columnField).AllowDBNull = True

            If Not sortFilter = "" Then
                If prop.Name = sortFilter Then
                    sortColumn = CType(Attribute.GetCustomAttribute(prop, GetType(DataColumnMapping)), DataColumnMapping).columnField
                End If
            End If
        Next

        For Each obj In objList
            Dim dr As DataRow = dt.NewRow

            For Each prop In properties
                Try
                    Dim value As Object = Trim(prop.GetValue(obj, Nothing))
                    If IsNothing(value) Or String.IsNullOrWhiteSpace(value) Then
                        value = DBNull.Value
                    End If

                    dr(CType(Attribute.GetCustomAttribute(prop, GetType(DataColumnMapping)), DataColumnMapping).columnField) = value
                Catch ex As Exception

                End Try
            Next

            dt.Rows.Add(dr)
        Next

        If sortColumn = String.Empty Then
            Return dt
        Else
            Dim dv As DataView = dt.DefaultView

            dv.Sort = sortColumn + " " + sortDirection

            Return dv.ToTable

        End If
    End Function

    Shared Function listRecords(Of genericType)(ByVal dt As DataTable) As List(Of genericType)
        Dim list As New List(Of genericType)

        Dim columnNames As DataColumnCollection = dt.Columns

        For Each row As DataRow In dt.Rows
            list.Add(CType(Activator.CreateInstance(GetType(genericType), row, columnNames), genericType))
        Next

        Return list
    End Function

    ' DATATABLE TO LIST OF GENERIC (W/ ACCESS MATRIX)
    Shared Function listRecords(Of genericType)(ByVal dt As DataTable, ByVal access As ProfileDO) As List(Of genericType)
        Dim list As New List(Of genericType)

        Dim columnNames As DataColumnCollection = dt.Columns

        For Each row As DataRow In dt.Rows
            list.Add(CType(Activator.CreateInstance(GetType(genericType), row, columnNames, access), genericType))
        Next

        Return list
    End Function

    ' DATATABLE TO DICTIONARY
    Shared Function convertDTtoDictionary(ByVal dt As DataTable, ByVal keyColumn As String, ByVal valueColumn As String) As Dictionary(Of String, String)
        Dim dict As New Dictionary(Of String, String)

        For Each dr As DataRow In dt.Rows
            dict.Add(dr(keyColumn), dr(valueColumn))
        Next

        Return dict
    End Function

    ' LIST OF STRING TO SINGLE COLUMN DATATABLE
    Shared Function convertIDListToDT(ByVal lstID As List(Of String), ByVal IDColumnName As String) As DataTable
        Dim dt As New DataTable

        dt.Columns.Add(IDColumnName)

        If Not IsNothing(lstID) Then
            For Each ID In lstID
                Dim dr As DataRow = dt.NewRow()

                dr(IDColumnName) = ID
                dt.Rows.Add(dr)
            Next
        End If


        Return dt
    End Function

    Function ConvertListToDataTable(Of T)(list As List(Of T)) As DataTable
        Dim dt As New DataTable()

        Dim properties As PropertyInfo() = GetType(T).GetProperties()

        For Each [property] As PropertyInfo In properties
            dt.Columns.Add([property].Name, [property].PropertyType)
        Next

        For Each item As T In list
            Dim row As DataRow = dt.NewRow()

            For Each [property] As PropertyInfo In properties
                row([property].Name) = [property].GetValue(item, Nothing)
            Next

            dt.Rows.Add(row)
        Next

        Return dt
    End Function
#End Region

#Region "THIS IS USED FOR SORTING"
    Shared Function sortRecordsToTable(ByVal objList As IEnumerable, ByVal genericType As Type, Optional ByVal sortFilter As String = "", Optional ByVal classname As String = "", Optional ByVal sortDirection As String = "ASC") As DataTable
        Dim dt As New DataTable
        Dim sortColumn As String = String.Empty
        Dim sortColumnFound As Boolean = False

        'create the dt columns based on DataColumMappings of passed Class

        Dim properties As IEnumerable(Of PropertyInfo) = genericType.GetProperties().Where(Function(prop) prop.IsDefined(GetType(DataColumnMapping), False))

        Dim declaredClassProperties As IEnumerable(Of PropertyInfo) = genericType.GetProperties().Where(Function(prop) prop.IsDefined(GetType(DeclaredClass), False))


        ' create the dt columns of the base class

        For Each prop In properties
            dt.Columns.Add(CType(Attribute.GetCustomAttribute(prop, GetType(DataColumnMapping)), DataColumnMapping).columnField)

            If Not sortFilter = "" Then
                If prop.Name = sortFilter Then
                    sortColumn = CType(Attribute.GetCustomAttribute(prop, GetType(DataColumnMapping)), DataColumnMapping).columnField
                    sortColumnFound = True
                End If
            End If
        Next

        ' add dt columns of declared classes inside base class
        Dim dcPropertyInfo As IEnumerable(Of PropertyInfo)

        For Each dc In declaredClassProperties
            dcPropertyInfo = dc.PropertyType.GetProperties().Where(Function(prop) prop.IsDefined(GetType(DataColumnMapping), False))

            For Each dcProp In dcPropertyInfo
                dt.Columns.Add(CType(Attribute.GetCustomAttribute(dcProp, GetType(DataColumnMapping)), DataColumnMapping).columnField + "." + dc.Name)

                If Not sortColumnFound Then
                    If Not sortFilter = "" Then
                        If dc.Name = classname Then
                            If dcProp.Name = sortFilter Then
                                sortColumn = CType(Attribute.GetCustomAttribute(dcProp, GetType(DataColumnMapping)), DataColumnMapping).columnField + "." + dc.Name
                                sortColumnFound = True
                            End If
                        End If
                    End If
                End If
            Next
        Next

        ' add dt columns of ACCESS MATRIX CLASS inside base class
        Dim accessClassProperties As IEnumerable(Of PropertyInfo) = genericType.GetProperties().Where(Function(prop) prop.Name.Equals("Access"))

        Dim acPropertyInfo As IEnumerable(Of PropertyInfo)

        For Each ac In accessClassProperties
            acPropertyInfo = ac.PropertyType.GetProperties().Where(Function(prop) prop.IsDefined(GetType(DataColumnMapping), False))

            For Each acProp In acPropertyInfo
                dt.Columns.Add(CType(Attribute.GetCustomAttribute(acProp, GetType(DataColumnMapping)), DataColumnMapping).columnField + "." + ac.Name)
            Next
        Next


        For Each obj In objList
            Dim dr As DataRow = dt.NewRow

            For Each prop In properties
                Try
                    dr(CType(Attribute.GetCustomAttribute(prop, GetType(DataColumnMapping)), DataColumnMapping).columnField) = prop.GetValue(obj, Nothing)
                Catch ex As Exception

                End Try
            Next

            For Each dc In declaredClassProperties
                dcPropertyInfo = dc.PropertyType.GetProperties().Where(Function(prop) prop.IsDefined(GetType(DataColumnMapping), False))
                Try
                    Dim dummyClass As Object

                    dummyClass = Activator.CreateInstance(dc.PropertyType)


                    dummyClass = dc.GetValue(obj, Nothing)

                    For Each dcProp In dcPropertyInfo
                        dr(CType(Attribute.GetCustomAttribute(dcProp, GetType(DataColumnMapping)), DataColumnMapping).columnField + "." + dc.Name) = dcProp.GetValue(dummyClass, Nothing)
                    Next
                Catch ex As Exception

                End Try
            Next

            ' REBINDING ACCESS MATRIX
            For Each ac In accessClassProperties
                acPropertyInfo = ac.PropertyType.GetProperties().Where(Function(prop) prop.IsDefined(GetType(DataColumnMapping), False))
                Try
                    Dim dummyClass As Object

                    dummyClass = Activator.CreateInstance(ac.PropertyType)


                    dummyClass = ac.GetValue(obj, Nothing)

                    For Each acProp In acPropertyInfo
                        dr(CType(Attribute.GetCustomAttribute(acProp, GetType(DataColumnMapping)), DataColumnMapping).columnField + "." + ac.Name) = acProp.GetValue(dummyClass, Nothing)
                    Next
                Catch ex As Exception

                End Try
            Next

            dt.Rows.Add(dr)
        Next

        If sortColumn = String.Empty Then
            Return dt
        Else
            Dim dv As DataView = dt.DefaultView

            dv.Sort = sortColumn + " " + sortDirection

            Return dv.ToTable

        End If


    End Function

    Shared Function listRecordsAsEnumerable(ByVal dt As DataTable, ByVal genericType As Type) As IEnumerable
        Dim list As New List(Of Object)

        Dim columnNames As DataColumnCollection = dt.Columns

        For Each row As DataRow In dt.Rows
            list.Add(Activator.CreateInstance(genericType, row, columnNames))
        Next

        Return list
    End Function
#End Region

#Region "ENCRYPTION"
    Shared Function Encrypt(ByVal strToEncrypt As String) As String
        Dim strEncrypted As String = String.Empty
        Dim sysToken As String = System.Configuration.ConfigurationManager.GetSection("Commons")("sysToken").ToString()

        If Not String.IsNullOrWhiteSpace(strToEncrypt) Then
            Dim encryptor As New LBPCryptography.Encryptor
            Dim value As Byte() = ASCIIEncoding.ASCII.GetBytes(sysToken + strToEncrypt)
            Dim key As String = System.Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(sysToken))
            Dim encrytedstring As Byte() = encryptor.AES_Encrypt(value, key)

            strEncrypted = System.Convert.ToBase64String(encrytedstring)
        End If

        Return strEncrypted
    End Function

    Shared Function Decrypt(ByVal strToDecrypt As String) As String
        Dim strDecrypted As String = String.Empty
        Dim sysToken As String = System.Configuration.ConfigurationManager.GetSection("Commons")("sysToken").ToString()

        Dim decrytor As New LBPCryptography.Decryptor
        Dim value As Byte() = System.Convert.FromBase64String(strToDecrypt)
        Dim key As String = System.Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(sysToken))
        Dim decryptedstring As String = System.Text.Encoding.UTF8.GetString(decrytor.AES_Decrypt(value, key))

        strDecrypted = decryptedstring.Substring(Len(sysToken))

        Return strDecrypted

    End Function
#End Region

#Region "ENCRYPTION - API"
    Shared Function APIEncrypt(ByVal str As String, ByVal salt As String) As String
        Dim strEncrypted As String = String.Empty

        If Not String.IsNullOrWhiteSpace(str) Then
            Dim encryptor As New LBPCryptography.Encryptor
            Dim value As Byte() = ASCIIEncoding.ASCII.GetBytes(salt + str)
            Dim key As String = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(salt))
            Dim encrytedstring As Byte() = encryptor.AES_Encrypt(value, key)

            strEncrypted = Convert.ToBase64String(encrytedstring)
        End If

        Return strEncrypted
    End Function

    Shared Function APIDecrypt(ByVal str As String, ByVal salt As String) As String
        Dim strDecrypted As String = String.Empty
        Dim decrytor As New LBPCryptography.Decryptor
        Dim value As Byte() = Convert.FromBase64String(str)
        Dim key As String = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(salt))
        Dim decryptedstring As String = Encoding.UTF8.GetString(decrytor.AES_Decrypt(value, key))

        strDecrypted = decryptedstring.Substring(Len(salt))

        Return strDecrypted
    End Function
#End Region



#Region "VALIDATION RETURN"
    Friend Function ValidationResult() As Boolean
        isSuccess = True

        If Not String.IsNullOrWhiteSpace(objErrorMessage) Then
            isSuccess = False
        End If

        Return isSuccess
    End Function
#End Region

#Region "MISCELLANEOUS"
    Friend Function Insertbreak() As String
        If String.IsNullOrWhiteSpace(objErrorMessage) Then
            Return ""
        Else
            Return "<br />"
        End If
    End Function

    Public Function validateProperties(ByVal propertyNames As List(Of String)) As Boolean
        Dim success As Boolean = True

        For Each propName In propertyNames
            If Not validateProperty(propName) Then
                success = False
            End If
        Next

        Return success
    End Function
#End Region

End Class
