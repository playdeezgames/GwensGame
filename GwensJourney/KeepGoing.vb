Friend Module KeepGoing
    Friend Sub Run(engine As IEngine)
        LegacyDistanceRemaining.Change(-LegacyPace.Read())
        LegacyHungerCounter.Change(LegacyPace.Read())
        If HasArrived() Then
            TheEnd.Run(engine)
            Return
        End If
        If IsDead() Then
            LegacyContext.SetGameOver()
            GameOver.Run(engine)
            Return
        End If
        CurrentArea.Run(engine)
    End Sub
End Module
