Friend Module KeepGoing
    Friend Sub Run()
        DistanceRemaining.Change(-Pace.Read())
        HungerCounter.Change(Pace.Read())
        If HasArrived() Then
            TheEnd.Run()
            Return
        End If
        If IsDead() Then
            Context.SetGameOver()
            GameOver.Run()
            Return
        End If
        CurrentArea.Run()
    End Sub
End Module
