Friend Module KeepGoing
    Friend Sub Run(engine As IEngine, context As IContext)
        context.DistanceRemaining.Change(-context.Pace.Read())
        context.HungerCounter.Change(context.Pace.Read())
        NewArea.Run(context)
        Neutral.Run(engine, context)
    End Sub
End Module
