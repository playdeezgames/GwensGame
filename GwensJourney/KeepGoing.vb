Friend Module KeepGoing
    Friend Sub Run()
        DistanceRemaining.Change(-Pace.Read())
        HungerCounter.Change(Pace.Read())
        If HasArrived() Then
            TheEnd.Run()
        Else
            CurrentArea.Run()
        End If
    End Sub
End Module
