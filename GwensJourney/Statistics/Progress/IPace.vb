Public Interface IPace
    Function Read() As Integer
    Sub Write(pace As Integer)
    Function Name() As String
    Function Name(index As Integer) As String
    Sub Reset()
End Interface
