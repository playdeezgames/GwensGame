Module Embark
    Sub Run(engine As IEngine)
        LegacyContext.Initialize()
        CurrentArea.Run(engine)
    End Sub
End Module
