Module Dice
    Private _random As New Random()
    Function D6() As Integer
        Return _random.Next(1, 7)
    End Function
    Function D20() As Integer
        Return _random.Next(1, 21)
    End Function

End Module
