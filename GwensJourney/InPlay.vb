Module InPlay
    Private _inPlay As Boolean
    Function IsInPlay() As Boolean
        Return _inPlay
    End Function
    Sub SetGameOver()
        _inPlay = False
    End Sub
    Sub StartGame()
        _inPlay = True
        ChangeSnax(10 - GetSnax())
        ChangeDistanceRemaining(2000 - GetDistanceRemaining())
        SetPace(3)
        RollAbilities()
    End Sub
End Module
