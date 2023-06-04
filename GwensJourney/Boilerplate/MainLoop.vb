Friend Module MainLoop
    Friend Sub Run(engine As IEngine)
        While engine.ActionItems.HasAny()
            Do
                engine.Prompts.Show()
            Loop Until engine.ActionItems.Choose()
        End While
    End Sub
End Module
