Friend Module Neutral
    Friend Sub Run(engine As IEngine, context As IContext)
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
