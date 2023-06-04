Friend Module KeepGoing
    Friend Sub Run(engine As IEngine)
        DistanceRemaining.Change(-Pace.Read())
        HungerCounter.Change(Pace.Read())
        If HasArrived() Then
            TheEnd.Run(engine)
            Return
        End If
        If IsDead() Then
            Context.SetGameOver()
            GameOver.Run(engine)
            Return
        End If
        CurrentArea.Run(engine)
    End Sub
End Module
