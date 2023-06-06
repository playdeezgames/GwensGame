Friend Module KeepGoing
    Friend Sub Run(engine As IEngine, context As IContext)
        context.DistanceRemaining.Change(-context.Pace.Read())
        context.HungerCounter.Change(context.Pace.Read())
        If context.DistanceRemaining.HasArrived() Then
            TheEnd.Run(engine, context)
            Return
        End If
        If context.HungerState.IsDead() Then
            context.SetGameOver()
            GameOver.Run(engine)
            Return
        End If
        CurrentArea.Run(engine, context)
    End Sub
End Module
