Friend Module InPlay
    Friend Function IsInPlay() As Boolean
        Return Context.Flag(FlagNames.InPlay)
    End Function
    Friend Sub SetGameOver()
        Context.Flag(FlagNames.InPlay) = False
    End Sub
    Friend Sub Initialize()
        Context.Flag(FlagNames.InPlay) = True
        Reset()
        ResetDistanceRemaining()
        ResetPace()
        RollAbilities()
    End Sub
End Module
