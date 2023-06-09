Friend Module NewArea
    Sub Run(context As IContext)
        context.Foraging.GenerateDifficulty()
        context.Foraging.GenerateAbundance()
        context.Shortcut.Generate()
    End Sub
End Module
