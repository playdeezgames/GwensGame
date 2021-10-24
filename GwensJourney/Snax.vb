Module Snax
    Private _snax As Integer
    Function GetSnax() As Integer
        Return _snax
    End Function
    Sub ChangeSnax(delta As Integer)
        _snax += delta
    End Sub

End Module
