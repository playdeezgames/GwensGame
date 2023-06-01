Friend Module MainLoop
    Friend Sub Run()
        While HasAny()
            Do
                Show()
            Loop Until Choose()
        End While
    End Sub
End Module
