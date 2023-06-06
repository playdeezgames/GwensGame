Friend Module MainLoop
    Friend Sub Run(engine As IEngine, context As IContext)
        While engine.ActionItems.HasAny()
            Do
                engine.Prompts.Show()
            Loop Until engine.ActionItems.Choose(context)
        End While
    End Sub
End Module
