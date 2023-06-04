Friend Module MainLoop
    Friend Sub Run(engine As IEngine)
        While HasAny()
            Do
                Show(engine)
            Loop Until Choose()
        End While
    End Sub
End Module
