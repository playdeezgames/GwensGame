Module Embark
    Sub Run(engine As IEngine)
        Context.Initialize()
        CurrentArea.Run(engine)
    End Sub
End Module
