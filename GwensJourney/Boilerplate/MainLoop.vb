Friend Module MainLoop
    Friend Sub Run(engine As IEngine)
        While HasAny()
            Do
                engine.Prompts.Show()
            Loop Until Choose()
        End While
    End Sub
End Module
