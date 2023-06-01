Friend Module Dice
    Private ReadOnly _random As New Random()
    Friend Function D6() As Integer
        Return _random.Next(1, 7)
    End Function
    Friend Function D20() As Integer
        Return _random.Next(1, 21)
    End Function
End Module
