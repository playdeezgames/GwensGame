Friend Module Dice
    Friend Function D4() As Integer
        Return RNG.FromRange(1, 4)
    End Function
    Friend Function D6() As Integer
        Return RNG.FromRange(1, 6)
    End Function
    Friend Function D20() As Integer
        Return RNG.FromRange(1, 20)
    End Function
End Module
