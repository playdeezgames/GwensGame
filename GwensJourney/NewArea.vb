Friend Module NewArea
    Sub Run(context As IContext)
        context.Foraging.GenerateDifficulty()
        context.Foraging.GenerateAbundance()
    End Sub
End Module
