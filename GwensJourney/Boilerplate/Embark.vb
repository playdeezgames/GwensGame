Module Embark
    Sub Run(engine As IEngine, context As IContext)
        context.Initialize()
        CurrentArea.Run(engine, context)
    End Sub
End Module
