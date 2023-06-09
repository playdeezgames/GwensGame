Friend Module KeepGoing
    Friend Sub Run(engine As IEngine, context As IContext)
        context.DistanceRemaining.Change(-context.Pace.Read())
        context.HungerCounter.Change(context.Pace.Read())
        context.Foraging.GenerateDifficulty()
        context.Foraging.GenerateAbundance()
        Neutral.Run(engine, context)
    End Sub
End Module
