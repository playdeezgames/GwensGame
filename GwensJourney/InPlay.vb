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
        ResetSnax()
        ResetDistanceRemaining()
        ResetPace()
        RollAbilities()
    End Sub
End Module
