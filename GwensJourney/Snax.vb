Module Snax
    Private Const INITIAL_SNAX As Integer = 10
    Private _snax As Integer
    Function GetSnax() As Integer
        Return _snax
    End Function
    Sub ChangeSnax(delta As Integer)
        _snax += delta
    End Sub
    Sub ResetSnax()
        ChangeSnax(INITIAL_SNAX - GetSnax())
    End Sub
End Module
